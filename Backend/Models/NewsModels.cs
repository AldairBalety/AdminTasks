using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    // Enums para el sistema de noticias
    public enum NewsCategory
    {
        Technology,
        AI,
        Programming,
        WebDevelopment,
        MobileDevelopment,
        DataScience,
        Cybersecurity,
        StartupNews,
        TechTrends
    }

    public enum CallToAction
    {
        ReadMore,
        LearnMore,
        DiscoverMore,
        GetStarted,
        Subscribe,
        Download,
        TryNow,
        BookDemo,
        ContactUs
    }

    public enum PublicationStatus
    {
        Ready,
        Published,
        Failed,
        ReadyForManualPosting
    }

    // Converter personalizado para enums
    public class JsonStringEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();
            if (Enum.TryParse<T>(stringValue, true, out var result))
            {
                return result;
            }
            return default(T);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    // Modelos principales
    public class NewsGenerationRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter<NewsCategory>))]
        public NewsCategory Category { get; set; } = NewsCategory.Technology;
        
        public string? CustomTopic { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter<CallToAction>))]
        public CallToAction CTA { get; set; } = CallToAction.ReadMore;
        
        public bool GenerateImage { get; set; } = true;
        public bool PublishToFacebook { get; set; } = false;
    }

    public class NewsItem
    {
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string ImagePrompt { get; set; } = "";
        
        [JsonConverter(typeof(JsonStringEnumConverter<NewsCategory>))]
        public NewsCategory Category { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter<CallToAction>))]
        public CallToAction CTA { get; set; }
        
        public string CTAText { get; set; } = "";
        public List<string> HashTags { get; set; } = new();
        
        [JsonConverter(typeof(JsonStringEnumConverter<PublicationStatus>))]
        public PublicationStatus Status { get; set; } = PublicationStatus.Ready;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? FacebookPostId { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class FacebookConfig
    {
        public string PageAccessToken { get; set; } = "";
        public string PageId { get; set; } = "";
        public string AppId { get; set; } = "";
        public string AppSecret { get; set; } = "";
    }

    public class FacebookTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = "";
        
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "";
        
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }

    public class FacebookPageResponse
    {
        [JsonPropertyName("data")]
        public List<FacebookPage> Data { get; set; } = new();
    }

    public class FacebookPage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = "";
        
        [JsonPropertyName("perms")]
        public List<string> Perms { get; set; } = new();
    }

    public class FacebookPostResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";
        
        [JsonPropertyName("error")]
        public FacebookError? Error { get; set; }
    }

    public class FacebookError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
        
        [JsonPropertyName("code")]
        public int Code { get; set; }
        
        [JsonPropertyName("error_subcode")]
        public int ErrorSubcode { get; set; }
    }

    public class NewsStats
    {
        public int TotalGenerated { get; set; }
        public int Published { get; set; }
        public int Failed { get; set; }
        public int PendingManual { get; set; }
        public DateTime LastGenerated { get; set; }
        public List<NewsItem> RecentNews { get; set; } = new();
    }

    // Respuestas de API
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }
        public string? Error { get; set; }
    }
}
