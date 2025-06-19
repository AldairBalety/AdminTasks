using Backend.Models;
using Backend.Services;

namespace Backend.HostedServices
{
    public class NewsProcessRelease : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NewsProcessRelease> _logger;
        private readonly IConfiguration _configuration;

        public NewsProcessRelease(IServiceProvider serviceProvider, ILogger<NewsProcessRelease> logger, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("🚀 NewsProcessRelease iniciado - Sistema inteligente de publicación");
            
            // Intervalo configurable (por defecto cada 4 horas)
            var intervalMinutes = _configuration.GetValue<int>("NewsBot:IntervalMinutes", 240);
            var interval = TimeSpan.FromMinutes(intervalMinutes);
            
            // Calcular próxima ejecución
            var nextRun = GetNextOptimalTime();
            _logger.LogInformation("⏰ Próxima ejecución: {NextRun} ({TimeUntil} desde ahora)", 
                nextRun.ToString("yyyy-MM-dd HH:mm:ss"), 
                (nextRun - DateTime.Now).ToString(@"hh\:mm\:ss"));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.Now;
                    if (now >= nextRun)
                    {
                        await ProcessNewsGeneration();
                        nextRun = GetNextOptimalTime();
                        _logger.LogInformation("⏰ Próxima ejecución programada: {NextRun}", nextRun.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    
                    // Esperar 10 minutos antes de verificar de nuevo
                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Error en NewsProcessRelease");
                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken); // Esperar más en caso de error
                }
            }
        }

        private async Task ProcessNewsGeneration()
        {
            using var scope = _serviceProvider.CreateScope();
            var newsService = scope.ServiceProvider.GetRequiredService<NewsService>();
            
            try
            {
                _logger.LogInformation("🎯 Iniciando generación automática de noticia");
                
                // Seleccionar categoría de forma inteligente
                var category = SelectOptimalCategory();
                var cta = SelectOptimalCTA();
                
                var request = new NewsGenerationRequest
                {
                    Category = category,
                    CTA = cta,
                    GenerateImage = true,
                    PublishToFacebook = _configuration.GetValue<bool>("NewsBot:AutoPublish", true)
                };
                
                _logger.LogInformation("📝 Generando noticia: Categoría={Category}, CTA={CTA}", category, cta);
                
                var newsItem = await newsService.GenerateNewsAsync(request);
                
                // Log del resultado
                var statusEmoji = newsItem.Status switch
                {
                    PublicationStatus.Published => "✅",
                    PublicationStatus.ReadyForManualPosting => "⚠️",
                    PublicationStatus.Failed => "❌",
                    _ => "📝"
                };
                
                _logger.LogInformation("{Emoji} Noticia procesada: {Title} | Status: {Status}", 
                    statusEmoji, newsItem.Title, newsItem.Status);
                
                if (!string.IsNullOrEmpty(newsItem.ErrorMessage))
                {
                    _logger.LogWarning("⚠️ Mensaje: {Message}", newsItem.ErrorMessage);
                }
                
                if (!string.IsNullOrEmpty(newsItem.FacebookPostId))
                {
                    _logger.LogInformation("📱 Post de Facebook: {PostId}", newsItem.FacebookPostId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error procesando generación automática de noticia");
            }
        }

        private NewsCategory SelectOptimalCategory()
        {
            // Algoritmo inteligente para seleccionar categoría basado en el día y hora
            var now = DateTime.Now;
            var dayOfWeek = (int)now.DayOfWeek;
            var hour = now.Hour;
            
            // Lunes-Viernes: Contenido más profesional en horas laborales
            if (dayOfWeek >= 1 && dayOfWeek <= 5 && hour >= 9 && hour <= 17)
            {
                var businessCategories = new[]
                {
                    NewsCategory.AI,
                    NewsCategory.Programming,
                    NewsCategory.DataScience,
                    NewsCategory.Cybersecurity,
                    NewsCategory.StartupNews
                };
                return businessCategories[Random.Shared.Next(businessCategories.Length)];
            }
            
            // Fines de semana o fuera de horario: Contenido más general
            var casualCategories = new[]
            {
                NewsCategory.Technology,
                NewsCategory.TechTrends,
                NewsCategory.WebDevelopment,
                NewsCategory.MobileDevelopment
            };
            
            return casualCategories[Random.Shared.Next(casualCategories.Length)];
        }

        private CallToAction SelectOptimalCTA()
        {
            // Rotar CTAs de forma inteligente
            var ctas = new[]
            {
                CallToAction.ReadMore,
                CallToAction.LearnMore,
                CallToAction.DiscoverMore,
                CallToAction.GetStarted,
                CallToAction.TryNow
            };
            
            return ctas[Random.Shared.Next(ctas.Length)];
        }

        private DateTime GetNextOptimalTime()
        {
            // Horarios óptimos para publicación en Facebook (mayor engagement)
            var optimalHours = new[] { 9, 12, 15, 18, 21 }; // 9 AM, 12 PM, 3 PM, 6 PM, 9 PM
            
            var now = DateTime.Now;
            var nextHour = optimalHours.FirstOrDefault(h => h > now.Hour);
            
            if (nextHour == 0)
            {
                // No hay más horas hoy, programar para mañana
                return new DateTime(now.Year, now.Month, now.Day, optimalHours[0], 0, 0).AddDays(1);
            }
            else
            {
                return new DateTime(now.Year, now.Month, now.Day, nextHour, 0, 0);
            }
        }
    }
}
