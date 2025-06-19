using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models;
using OpenAI;
using Backend.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración local si existe
if (File.Exists("appsettings.Local.json"))
{
    builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
}

// Configuración existente
ConfigureServices.Configure(builder);

// Configuración adicional para el bot de Facebook
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicios del bot de noticias
builder.Services.AddHttpClient();

// Configurar OpenAI Client
builder.Services.AddSingleton<OpenAIClient>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var apiKey = configuration["OpenAI:ApiKey"];
    
    if (string.IsNullOrEmpty(apiKey) || apiKey == "TU_OPENAI_API_KEY_AQUI")
    {
        throw new InvalidOperationException("OpenAI API Key no configurada. Agrega 'OpenAI:ApiKey' en appsettings.json");
    }
    
    return new OpenAIClient(apiKey);
});

builder.Services.AddScoped<NewsService>();

var app = builder.Build();

// Configuración existente
ConfigureMiddleware.Configure(app);
ConfigureEndpoints.Configure(app);

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint principal - Generar noticia
app.MapPost("/api/news/generate", async ([FromBody] NewsGenerationRequest request, NewsService newsService) =>
{
    try
    {
        var result = await newsService.GenerateNewsAsync(request);
        return Results.Ok(new ApiResponse<NewsItem>
        {
            Success = true,
            Message = "Noticia generada exitosamente",
            Data = result
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new ApiResponse<object>
        {
            Success = false,
            Message = ex.Message
        });
    }
});

app.Run();
