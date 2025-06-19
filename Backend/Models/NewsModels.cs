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

    // Modelos adicionales para analíticas y configuración
    public class AnalyticsData
    {
        public List<DailyMetric> DailyPosts { get; set; } = new();
        public List<CategoryStats> CategoryDistribution { get; set; } = new();
        public PerformanceMetrics Performance { get; set; } = new();
    }

    public class DailyMetric
    {
        public string Date { get; set; } = "";
        public int Posts { get; set; }
        public int Engagement { get; set; }
    }

    public class CategoryStats
    {
        public string Category { get; set; } = "";
        public int Count { get; set; }
        public string Color { get; set; } = "";
    }

    public class PerformanceMetrics
    {
        public int TotalReach { get; set; }
        public int TotalEngagement { get; set; }
        public double AvgEngagementRate { get; set; }
        public string BestPerformingCategory { get; set; } = "";
    }

    public class SystemStatus
    {
        public Dictionary<string, ServiceStatus> Services { get; set; } = new();
        public ScheduleInfo Schedule { get; set; } = new();
        public SystemMetrics Metrics { get; set; } = new();
    }

    public class ServiceStatus
    {
        public string Status { get; set; } = "";
        public DateTime LastCheck { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class ScheduleInfo
    {
        public DateTime NextExecution { get; set; }
        public DateTime LastExecution { get; set; }
        public bool IsActive { get; set; }
    }

    public class SystemMetrics
    {
        public string Uptime { get; set; } = "";
        public int TotalRequests { get; set; }
        public double SuccessRate { get; set; }
    }

    // Modelos para el sistema de usuarios y tareas
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class UserTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public bool Completed { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Login
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    // Respuestas de la API
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }
    }
}
