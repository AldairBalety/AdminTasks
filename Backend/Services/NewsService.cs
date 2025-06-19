using Backend.Models;
using OpenAI;
using OpenAI.Chat;
using System.Text.Json;

namespace Backend.Services
{
    public class NewsService
    {
        private readonly OpenAIClient _openAIClient;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NewsService> _logger;

        public NewsService(
            OpenAIClient openAIClient, 
            HttpClient httpClient, 
            IConfiguration configuration,
            ILogger<NewsService> logger)
        {
            _openAIClient = openAIClient;
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<NewsItem> GenerateNewsAsync(NewsGenerationRequest request)
        {
            try
            {
                _logger.LogInformation("Iniciando generación de noticia para categoría: {Category}", request.Category);

                // 1. Generar contenido de la noticia
                var newsContent = await GenerateNewsContentAsync(request);
                
                // 2. Generar imagen si está habilitado
                string imageUrl = "";
                string imagePrompt = "";
                
                if (request.GenerateImage)
                {
                    var imageResult = await GenerateNewsImageAsync(newsContent.Title, request.Category);
                    imageUrl = imageResult.Url;
                    imagePrompt = imageResult.Prompt;
                }

                // 3. Crear el objeto NewsItem
                var newsItem = new NewsItem
                {
                    Title = newsContent.Title,
                    Content = newsContent.Content,
                    ImageUrl = imageUrl,
                    ImagePrompt = imagePrompt,
                    Category = request.Category,
                    CTA = request.CTA,
                    CTAText = GetCTAText(request.CTA),
                    HashTags = newsContent.HashTags,
                    Status = PublicationStatus.Ready,
                    CreatedAt = DateTime.UtcNow
                };

                // 4. Publicar en Facebook si está habilitado
                if (request.PublishToFacebook)
                {
                    var facebookResult = await PublishToFacebookAsync(newsItem);
                    newsItem.FacebookPostId = facebookResult.PostId;
                    newsItem.Status = facebookResult.Success ? PublicationStatus.Published : PublicationStatus.Failed;
                    newsItem.ErrorMessage = facebookResult.ErrorMessage;
                }

                _logger.LogInformation("Noticia generada exitosamente con ID: {Title}", newsItem.Title);
                return newsItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar noticia");
                throw;
            }
        }

        private async Task<(string Title, string Content, List<string> HashTags)> GenerateNewsContentAsync(NewsGenerationRequest request)
        {
            var categoryDescriptions = new Dictionary<NewsCategory, string>
            {
                { NewsCategory.Technology, "tecnología general, gadgets, innovaciones tecnológicas" },
                { NewsCategory.AI, "inteligencia artificial, machine learning, automatización" },
                { NewsCategory.Programming, "desarrollo de software, lenguajes de programación, frameworks" },
                { NewsCategory.WebDevelopment, "desarrollo web, frontend, backend, APIs" },
                { NewsCategory.MobileDevelopment, "desarrollo móvil, iOS, Android, aplicaciones" },
                { NewsCategory.DataScience, "ciencia de datos, análisis, big data, estadísticas" },
                { NewsCategory.Cybersecurity, "ciberseguridad, hacking ético, protección de datos" },
                { NewsCategory.StartupNews, "startups, emprendimiento, inversión, tecnología empresarial" },
                { NewsCategory.TechTrends, "tendencias tecnológicas, futuro de la tecnología, innovación" }
            };

            var categoryDesc = categoryDescriptions[request.Category];
            var customTopicPrompt = !string.IsNullOrEmpty(request.CustomTopic) 
                ? $"Tema específico: {request.CustomTopic}" 
                : "";

            var prompt = $@"
Eres un experto redactor de contenido para redes sociales especializado en tecnología.

Genera una noticia profesional y atractiva sobre {categoryDesc}.
{customTopicPrompt}

La noticia debe ser:
- Informativa y precisa
- Optimizada para engagement en Facebook
- Entre 150-300 palabras
- Con un tono profesional pero accesible
- Que genere curiosidad sin ser clickbait

Estructura requerida:
1. Un título llamativo y profesional (máximo 60 caracteres)
2. Contenido de 150-300 palabras
3. Lista de 3-5 hashtags relevantes

Devuelve la respuesta en formato JSON:
{{
  ""title"": ""Título de la noticia"",
  ""content"": ""Contenido completo de la noticia"",
  ""hashtags"": [""#hashtag1"", ""#hashtag2"", ""#hashtag3""]
}}

IMPORTANTE: 
- Solo devuelve el JSON válido, sin texto adicional
- Asegúrate de que el contenido sea factual y relevante
- Los hashtags deben ser populares y relevantes para la categoría
";

            var chatMessages = new List<ChatMessage>
            {
                new SystemChatMessage("Eres un experto redactor de contenido para redes sociales especializado en tecnología."),
                new UserChatMessage(prompt)
            };

            var response = await _openAIClient.GetChatClient("gpt-3.5-turbo").CompleteChatAsync(chatMessages);

            var jsonContent = response.Value.Content[0].Text;

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var newsData = JsonSerializer.Deserialize<dynamic>(jsonContent, options);
                
                var title = newsData.GetProperty("title").GetString() ?? "Título no disponible";
                var content = newsData.GetProperty("content").GetString() ?? "Contenido no disponible";
                
                var hashtags = new List<string>();
                if (newsData.TryGetProperty("hashtags", out JsonElement hashtagsProperty))
                {
                    foreach (var hashtag in hashtagsProperty.EnumerateArray())
                    {
                        hashtags.Add(hashtag.GetString() ?? "");
                    }
                }

                return (title, content, hashtags);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error parsing GPT response: {Response}", jsonContent);
                
                // Fallback: extraer información manualmente
                var title = ExtractBetween(jsonContent, "\"title\":", ",") ?? "Noticia de Tecnología";
                var content = ExtractBetween(jsonContent, "\"content\":", "\"hashtags\"") ?? "Contenido no disponible.";
                var hashtags = new List<string> { "#Technology", "#Innovation", "#TechNews" };

                return (title.Trim('"').Trim(), content.Trim('"').Trim(), hashtags);
            }
        }

        private async Task<(string Url, string Prompt)> GenerateNewsImageAsync(string title, NewsCategory category)
        {
            try
            {
                var imagePrompts = new Dictionary<NewsCategory, string>
                {
                    { NewsCategory.Technology, "modern technology, sleek devices, futuristic design" },
                    { NewsCategory.AI, "artificial intelligence, neural networks, robot brain, digital mind" },
                    { NewsCategory.Programming, "coding, developer workspace, multiple monitors, code on screen" },
                    { NewsCategory.WebDevelopment, "web development, responsive design, browser interfaces" },
                    { NewsCategory.MobileDevelopment, "mobile apps, smartphones, app interfaces, mobile UI" },
                    { NewsCategory.DataScience, "data visualization, graphs, charts, analytics dashboard" },
                    { NewsCategory.Cybersecurity, "cybersecurity, digital lock, network security, firewall" },
                    { NewsCategory.StartupNews, "startup office, team collaboration, innovation, business growth" },
                    { NewsCategory.TechTrends, "future technology, innovation, digital transformation" }
                };

                var basePrompt = imagePrompts.GetValueOrDefault(category, "modern technology");
                var fullPrompt = $"Professional, modern, high-quality image representing: {basePrompt}. Clean, minimalist style, suitable for social media. No text or logos. Digital art style.";

                var imageClient = _openAIClient.GetImageClient("dall-e-2");
                var imageResult = await imageClient.GenerateImageAsync(fullPrompt);

                if (imageResult?.Value?.ImageUri != null)
                {
                    return (imageResult.Value.ImageUri.ToString(), fullPrompt);
                }

                _logger.LogWarning("No se pudo generar imagen para la noticia");
                return ("", fullPrompt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar imagen");
                return ("", "");
            }
        }

        private async Task<(bool Success, string PostId, string ErrorMessage)> PublishToFacebookAsync(NewsItem newsItem)
        {
            try
            {
                var facebookConfig = _configuration.GetSection("Facebook").Get<FacebookConfig>();
                
                if (string.IsNullOrEmpty(facebookConfig?.PageAccessToken) || string.IsNullOrEmpty(facebookConfig.PageId))
                {
                    return (false, "", "Configuración de Facebook no encontrada");
                }

                var message = $"{newsItem.Title}\n\n{newsItem.Content}\n\n{string.Join(" ", newsItem.HashTags)}";
                
                var postData = new
                {
                    message = message,
                    link = newsItem.ImageUrl,
                    access_token = facebookConfig.PageAccessToken
                };

                var json = JsonSerializer.Serialize(postData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"https://graph.facebook.com/v18.0/{facebookConfig.PageId}/feed", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<FacebookPostResponse>(responseContent);
                    
                    _logger.LogInformation("Publicación exitosa en Facebook: {PostId}", result.Id);
                    return (true, result.Id, "");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error al publicar en Facebook: {Error}", errorContent);
                    return (false, "", errorContent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al publicar en Facebook");
                return (false, "", ex.Message);
            }
        }

        private string GetCTAText(CallToAction cta)
        {
            return cta switch
            {
                CallToAction.ReadMore => "Leer Más",
                CallToAction.LearnMore => "Aprende Más",
                CallToAction.DiscoverMore => "Descubrir Más",
                CallToAction.GetStarted => "Comenzar",
                CallToAction.Subscribe => "Suscribirse",
                CallToAction.Download => "Descargar",
                CallToAction.TryNow => "Probar Ahora",
                CallToAction.BookDemo => "Reservar Demo",
                CallToAction.ContactUs => "Contactanos",
                _ => "Leer Más"
            };
        }

        private string? ExtractBetween(string text, string start, string end)
        {
            var startIndex = text.IndexOf(start);
            if (startIndex == -1) return null;

            startIndex += start.Length;
            var endIndex = text.IndexOf(end, startIndex);
            if (endIndex == -1) return null;

            return text.Substring(startIndex, endIndex - startIndex);
        }
    }
}
