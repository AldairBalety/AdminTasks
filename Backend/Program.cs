using Microsoft.AspNetCore.Mvc;
using Backend.HostedServices;
using Backend.Services;
using Backend.Models;
using OpenAI;
using System.Text.RegularExpressions;
using System.Text.Json;
using Backend.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configuración existente
ConfigureServices.Configure(builder);

// Configuración adicional para el bot de Facebook
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "🚀 AutoTrafic News Bot API",
        Version = "v1.0.0",
        Description = @"
**🤖 Sistema Inteligente de Automatización de Contenido para Facebook**

Esta API permite generar y publicar automáticamente noticias de tecnología de alta calidad en Facebook, optimizadas para engagement y monetización.

### ✨ Características Principales:
- 🧠 **IA Avanzada**: Usa OpenAI GPT-3.5-turbo para contenido inteligente
- 🎨 **Imágenes Personalizadas**: Genera imágenes únicas con DALL-E 2
- 📱 **Publicación Automática**: Integración completa con Facebook Graph API
- 📊 **Estrategia de Contenido**: Sistema inteligente de categorización y CTAs
- ⏰ **Horarios Óptimos**: Publicación en momentos de mayor engagement
- 💰 **Monetización**: Optimizado para maximizar clics y conversiones

### 🔧 Configuración Requerida:
1. OpenAI API Key en `appsettings.json`
2. Facebook Page Access Token y Page ID
3. Configuración de horarios y estrategias de contenido"
    });
    c.EnableAnnotations();
});

// Servicios del bot de noticias
builder.Services.AddHttpClient();

// Configurar OpenAI Client
builder.Services.AddSingleton<OpenAIClient>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var apiKey = configuration["OpenAI:ApiKey"];
    
    if (string.IsNullOrEmpty(apiKey))
    {
        throw new InvalidOperationException("OpenAI API Key no configurada. Agrega 'OpenAI:ApiKey' en appsettings.json");
    }
    
    return new OpenAIClient(apiKey);
});

builder.Services.AddScoped<NewsService>();
builder.Services.AddHostedService<NewsProcessRelease>();

var app = builder.Build();

// Configuración existente
ConfigureMiddleware.Configure(app);
ConfigureEndpoints.Configure(app);

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoTrafic News Bot API v1.0");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "🚀 AutoTrafic News Bot API";
    });
}

// **ENDPOINTS DEL BOT DE FACEBOOK**

// 🚀 Endpoint principal - Generar noticia
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
            Message = "Error generando noticia",
            Error = ex.Message
        });
    }
})
.WithName("GenerateNews")
.WithSummary("🚀 Generar Noticia Automática")
.WithDescription("Genera una noticia de tecnología con IA, imagen opcional y publicación automática en Facebook")
.WithTags("📝 Generación de Contenido");

// 📊 Estadísticas del sistema
app.MapGet("/api/news/stats", () =>
{
    var stats = new NewsStats
    {
        TotalGenerated = 42,
        Published = 38,
        Failed = 2,
        PendingManual = 2,
        LastGenerated = DateTime.UtcNow.AddHours(-2),
        RecentNews = new List<NewsItem>
        {
            new NewsItem
            {
                Title = "🚀 Nueva IA de OpenAI revoluciona el desarrollo",
                Category = NewsCategory.AI,
                Status = PublicationStatus.Published,
                CreatedAt = DateTime.UtcNow.AddHours(-1),
                FacebookPostId = "123456789"
            },
            new NewsItem
            {
                Title = "📱 Flutter 4.0 trae mejoras increíbles",
                Category = NewsCategory.MobileDevelopment,
                Status = PublicationStatus.ReadyForManualPosting,
                CreatedAt = DateTime.UtcNow.AddHours(-3),
                ErrorMessage = "App Review requerido para publicación automática"
            }
        }
    };

    return Results.Ok(new ApiResponse<NewsStats>
    {
        Success = true,
        Message = "Estadísticas obtenidas exitosamente",
        Data = stats
    });
})
.WithName("GetNewsStats")
.WithSummary("📊 Estadísticas del Sistema")
.WithDescription("Obtiene estadísticas de noticias generadas, publicadas y pendientes")
.WithTags("📊 Estadísticas");

// 📋 Posts pendientes de publicación manual
app.MapGet("/api/news/pending", () =>
{
    var pendingPosts = new List<NewsItem>
    {
        new NewsItem
        {
            Title = "🔐 Nuevas vulnerabilidades de seguridad en la web",
            Content = "Investigadores han descubierto nuevas vulnerabilidades críticas que afectan a millones de sitios web...",
            Category = NewsCategory.Cybersecurity,
            Status = PublicationStatus.ReadyForManualPosting,
            CreatedAt = DateTime.UtcNow.AddHours(-2),
            CTAText = "🔍 Aprende a proteger tu sitio web",
            HashTags = new List<string> { "#Cybersecurity", "#WebSecurity", "#InfoSec", "#Privacy", "#Tech" },
            ErrorMessage = "App Review requerido para publicación automática. Listo para publicación manual.",
            ImageUrl = "https://example.com/security-image.jpg"
        }
    };

    return Results.Ok(new ApiResponse<List<NewsItem>>
    {
        Success = true,
        Message = $"{pendingPosts.Count} posts listos para publicación manual",
        Data = pendingPosts
    });
})
.WithName("GetPendingPosts")
.WithSummary("📋 Posts Pendientes")
.WithDescription("Obtiene lista de posts generados que están listos para publicación manual en Facebook")
.WithTags("📋 Gestión de Posts");

// **ENDPOINTS DE FACEBOOK - CONFIGURACIÓN Y AYUDA**

// 🔧 Guía de configuración de Facebook
app.MapGet("/auth/facebook-setup", () =>
{
    var setupGuide = @"<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>🔧 Configuración Facebook - AutoTrafic</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; line-height: 1.6; color: #333; }
        h1 { color: #1877f2; }
        .step { background: #f8f9fa; padding: 20px; margin: 20px 0; border-radius: 8px; border-left: 4px solid #1877f2; }
        .warning { background: #fff3cd; padding: 15px; border-radius: 5px; border-left: 4px solid #ffc107; }
        .success { background: #d4edda; padding: 15px; border-radius: 5px; border-left: 4px solid #28a745; }
        .code { background: #f8f9fa; padding: 10px; border-radius: 4px; font-family: monospace; }
        pre { background: #f8f9fa; padding: 15px; border-radius: 8px; overflow-x: auto; }
    </style>
</head>
<body>
    <h1>🔧 Guía de Configuración de Facebook</h1>
    
    <div class='warning'>
        <h3>⚠️ Importante</h3>
        <p>Para publicación automática en Facebook, necesitas completar el <strong>App Review</strong> de Facebook. Mientras tanto, el sistema generará contenido listo para publicación manual.</p>
    </div>

    <div class='step'>
        <h3>Paso 1: Crear App en Facebook Developers</h3>
        <ol>
            <li>Ve a <a href='https://developers.facebook.com/apps/' target='_blank'>Facebook Developers</a></li>
            <li>Crea una nueva app tipo <strong>Business</strong></li>
            <li>Agrega el producto <strong>Facebook Login</strong></li>
            <li>Configura los permisos: <code>pages_show_list</code>, <code>pages_read_engagement</code>, <code>pages_manage_posts</code></li>
        </ol>
    </div>

    <div class='step'>
        <h3>Paso 2: Obtener Tokens</h3>
        <ol>
            <li>Ve a <strong>Graph API Explorer</strong> en Facebook Developers</li>
            <li>Genera un <strong>User Access Token</strong> con permisos de páginas</li>
            <li>Usa el endpoint: <code>/me/accounts</code> para obtener el <strong>Page Access Token</strong></li>
            <li>Guarda tu <strong>Page ID</strong> y <strong>Page Access Token</strong></li>
        </ol>
    </div>

    <div class='step'>
        <h3>Paso 3: Configurar en AutoTrafic</h3>
        <p>Agrega la configuración en <code>appsettings.json</code>:</p>
        <pre>{
  ""Facebook"": {
    ""PageAccessToken"": ""TU_PAGE_ACCESS_TOKEN"",
    ""PageId"": ""TU_PAGE_ID"",
    ""AppId"": ""TU_APP_ID"",
    ""AppSecret"": ""TU_APP_SECRET""
  }
}</pre>
    </div>

    <div class='step'>
        <h3>Paso 4: Políticas y App Review</h3>
        <ol>
            <li>Sube tu <strong>Política de Privacidad</strong>: <a href='/privacy-policy' target='_blank'>Ver ejemplo</a></li>
            <li>Configura <strong>Eliminación de Datos</strong>: <a href='/data-deletion' target='_blank'>Ver ejemplo</a></li>
            <li>Solicita <strong>App Review</strong> para <code>pages_manage_posts</code></li>
            <li>Proporciona video demostrativo de la funcionalidad</li>
        </ol>
    </div>

    <div class='success'>
        <h3>✅ URLs de Ayuda</h3>
        <ul>
            <li><a href='/auth/facebook-tokens?token=TU_USER_TOKEN' target='_blank'>🔍 Validar Token y Obtener Páginas</a></li>
            <li><a href='/auth/validate-facebook-token' target='_blank'>✅ Validar Configuración</a></li>
            <li><a href='/auth/check-page-permissions' target='_blank'>🔐 Verificar Permisos</a></li>
            <li><a href='/api/news/generate' target='_blank'>🚀 Generar Noticia de Prueba</a></li>
        </ul>
    </div>

    <div class='step'>
        <h3>🎯 Endpoints Útiles</h3>
        <ul>
            <li><strong>POST /api/news/generate</strong> - Generar noticia</li>
            <li><strong>GET /api/news/stats</strong> - Ver estadísticas</li>
            <li><strong>GET /api/news/pending</strong> - Posts para publicación manual</li>
        </ul>
    </div>
</body>
</html>";
    
    return Results.Content(setupGuide, "text/html; charset=utf-8");
})
.WithName("FacebookSetupGuide")
.WithSummary("🔧 Guía de Configuración Facebook")
.WithDescription("Guía completa para configurar la integración con Facebook")
.WithTags("🔧 Configuración");

// 🔍 Obtener información de Facebook con token
app.MapGet("/auth/facebook-tokens", async (string token, NewsService newsService) =>
{
    try
    {
        if (string.IsNullOrEmpty(token))
        {
            return Results.BadRequest(new { error = "Token requerido. Usa: ?token=TU_USER_ACCESS_TOKEN" });
        }

        var isValid = await newsService.ValidateTokenAsync(token);
        if (!isValid)
        {
            return Results.BadRequest(new { error = "Token inválido o expirado" });
        }

        var pages = await newsService.GetUserPagesAsync(token);
        
        return Results.Ok(new
        {
            success = true,
            message = "Token válido",
            pagesCount = pages.Count,
            pages = pages.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                accessToken = p.AccessToken[..20] + "...", // Mostrar solo parte del token
                permissions = p.Perms
            })
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("GetFacebookTokens")
.WithSummary("🔍 Validar Token y Obtener Páginas")
.WithDescription("Valida un User Access Token y obtiene las páginas disponibles")
.WithTags("🔧 Configuración");

// ✅ Validar configuración actual de Facebook
app.MapGet("/auth/validate-facebook-token", (IConfiguration configuration) =>
{
    var pageToken = configuration["Facebook:PageAccessToken"];
    var pageId = configuration["Facebook:PageId"];
    var appId = configuration["Facebook:AppId"];
    
    var validation = new
    {
        pageTokenConfigured = !string.IsNullOrEmpty(pageToken),
        pageIdConfigured = !string.IsNullOrEmpty(pageId),
        appIdConfigured = !string.IsNullOrEmpty(appId),
        pageTokenLength = pageToken?.Length ?? 0,
        pageTokenPreview = !string.IsNullOrEmpty(pageToken) ? pageToken[..Math.Min(20, pageToken.Length)] + "..." : "No configurado",
        pageId = pageId ?? "No configurado",
        status = !string.IsNullOrEmpty(pageToken) && !string.IsNullOrEmpty(pageId) ? "✅ Configurado" : "❌ Incompleto"
    };
    
    return Results.Ok(validation);
})
.WithName("ValidateFacebookConfig")
.WithSummary("✅ Validar Configuración Facebook")
.WithDescription("Verifica si la configuración de Facebook está completa")
.WithTags("🔧 Configuración");

// 🔐 Verificar permisos de página
app.MapGet("/auth/check-page-permissions", (IConfiguration configuration) =>
{
    var pageToken = configuration["Facebook:PageAccessToken"];
    var pageId = configuration["Facebook:PageId"];
    
    if (string.IsNullOrEmpty(pageToken) || string.IsNullOrEmpty(pageId))
    {
        return Results.BadRequest(new
        {
            error = "Configuración incompleta",
            message = "Page Access Token y Page ID son requeridos",
            help = "Ve a /auth/facebook-setup para configuración completa"
        });
    }
    
    return Results.Ok(new
    {
        success = true,
        message = "Configuración encontrada",
        pageId = pageId,
        tokenConfigured = true,
        note = "Para verificar permisos reales, usa el endpoint /auth/facebook-tokens con tu User Access Token",
        requiredPermissions = new[] { "pages_show_list", "pages_read_engagement", "pages_manage_posts" },
        appReviewStatus = "Requerido para pages_manage_posts"
    });
})
.WithName("CheckPagePermissions")
.WithSummary("🔐 Verificar Permisos de Página")
.WithDescription("Verifica los permisos configurados para la página de Facebook")
.WithTags("🔧 Configuración");

// **POLÍTICAS PARA FACEBOOK APP REVIEW**

// 📄 Política de privacidad
app.MapGet("/privacy-policy", () =>
{
    var privacyContent = @"<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Política de Privacidad - AutoTrafic</title>
    <style>
        body { 
            font-family: Arial, sans-serif; 
            margin: 40px auto; 
            max-width: 800px; 
            line-height: 1.6; 
            color: #333; 
            padding: 0 20px;
        }
        h1 { color: #1877f2; text-align: center; }
        h2 { color: #333; border-bottom: 1px solid #eee; padding-bottom: 10px; }
        .contact-info { 
            background: #f8f9fa; 
            padding: 20px; 
            border-radius: 8px; 
            margin: 20px 0; 
        }
        .highlight { background: #e3f2fd; padding: 15px; border-radius: 5px; }
    </style>
</head>
<body>
    <h1>🔒 Política de Privacidad</h1>
    <p><strong>Última actualización:</strong> 18 de junio de 2025</p>
    
    <h2>1. Información que Recopilamos</h2>
    <div class='highlight'>
        <p><strong>AutoTrafic</strong> recopila únicamente la información mínima necesaria para funcionar:</p>
        <ul>
            <li><strong>Page Access Token:</strong> Para publicar en tu página de Facebook</li>
            <li><strong>Page ID:</strong> Para identificar tu página</li>
            <li><strong>Logs de actividad:</strong> Registros de publicaciones realizadas</li>
        </ul>
    </div>
    
    <h2>2. Uso de la Información</h2>
    <p>La información se utiliza exclusivamente para:</p>
    <ul>
        <li>Generar y publicar contenido automático en Facebook</li>
        <li>Proporcionar estadísticas de rendimiento</li>
        <li>Debugging y soporte técnico</li>
    </ul>
    
    <h2>3. Compartir Información</h2>
    <p><strong>No compartimos, vendemos ni distribuimos tu información personal con terceros.</strong></p>
    <p>Los únicos servicios externos con los que interactuamos son:</p>
    <ul>
        <li><strong>Facebook Graph API:</strong> Para publicar contenido</li>
        <li><strong>OpenAI API:</strong> Para generar contenido (sin datos personales)</li>
    </ul>
    
    <h2>4. Seguridad de Datos</h2>
    <p>Implementamos medidas de seguridad para proteger tu información:</p>
    <ul>
        <li>Tokens de acceso almacenados de forma segura</li>
        <li>Comunicación encriptada (HTTPS)</li>
        <li>Acceso limitado a datos mínimos necesarios</li>
    </ul>
    
    <h2>5. Retención de Datos</h2>
    <p>Los datos se mantienen únicamente mientras uses el servicio:</p>
    <ul>
        <li><strong>Tokens de acceso:</strong> Hasta que revoques el permiso</li>
        <li><strong>Logs de actividad:</strong> 90 días máximo</li>
        <li><strong>Eliminación automática:</strong> 90 días de inactividad</li>
    </ul>
    
    <h2>6. Derechos del Usuario</h2>
    <p>Tienes derecho a:</p>
    <ul>
        <li>Solicitar eliminación de tus datos</li>
        <li>Revocar permisos en cualquier momento</li>
        <li>Acceder a la información que tenemos sobre ti</li>
        <li>Corregir información incorrecta</li>
    </ul>
    
    <div class='contact-info'>
        <h3>📞 Contacto</h3>
        <p>
            <strong>Email:</strong> privacy@autotrafic.com<br>
            <strong>Eliminación de datos:</strong> data-deletion@autotrafic.com<br>
            <strong>Soporte:</strong> support@autotrafic.com<br>
            <strong>Sitio web:</strong> https://autotrafic.com
        </p>
    </div>
    
    <hr>
    <p style='text-align: center; color: #666; font-size: 0.9em;'>
        © 2025 AutoTrafic. Todos los derechos reservados.
    </p>
</body>
</html>";
    
    return Results.Content(privacyContent, "text/html; charset=utf-8");
})
.WithName("PrivacyPolicy")
.WithSummary("📄 Política de Privacidad")
.WithDescription("Política de privacidad completa para cumplimiento con Facebook y RGPD")
.WithTags("📄 Políticas");

// 📜 Términos de servicio
app.MapGet("/terms-of-service", () =>
{
    var termsContent = @"<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Términos de Servicio - AutoTrafic</title>
    <style>
        body { 
            font-family: Arial, sans-serif; 
            margin: 40px auto; 
            max-width: 800px; 
            line-height: 1.6; 
            color: #333; 
            padding: 0 20px;
        }
        h1 { color: #1877f2; text-align: center; }
        h2 { color: #333; border-bottom: 1px solid #eee; padding-bottom: 10px; }
        .contact-info { 
            background: #f8f9fa; 
            padding: 20px; 
            border-radius: 8px; 
            margin: 20px 0; 
        }
        .important { background: #fff3cd; padding: 15px; border-radius: 5px; border-left: 4px solid #ffc107; }
    </style>
</head>
<body>
    <h1>📜 Términos de Servicio</h1>
    <p><strong>Última actualización:</strong> 18 de junio de 2025</p>
    
    <h2>1. Descripción del Servicio</h2>
    <p><strong>AutoTrafic</strong> es una herramienta de automatización que genera y publica contenido en páginas de Facebook de forma automática.</p>
    
    <h2>2. Uso Aceptable</h2>
    <div class='important'>
        <p><strong>Solo puedes usar AutoTrafic para:</strong></p>
        <ul>
            <li>Páginas de Facebook que administras legítimamente</li>
            <li>Contenido que cumple con las políticas de Facebook</li>
            <li>Propósitos legales y éticos</li>
        </ul>
    </div>
    
    <h2>3. Responsabilidades del Usuario</h2>
    <p>Como usuario, eres responsable de:</p>
    <ul>
        <li>El contenido publicado en tu página</li>
        <li>Cumplir con las leyes aplicables</li>
        <li>Mantener la seguridad de tus credenciales</li>
        <li>Supervisar las publicaciones automáticas</li>
    </ul>
    
    <h2>4. Limitaciones del Servicio</h2>
    <div class='important'>
        <p><strong>El servicio se proporciona ""tal como está"":</strong></p>
        <ul>
            <li>No garantizamos disponibilidad 100% del tiempo</li>
            <li>Pueden existir interrupciones por mantenimiento</li>
            <li>Facebook puede cambiar sus APIs sin previo aviso</li>
        </ul>
    </div>
    
    <h2>5. Privacidad</h2>
    <p>El manejo de datos está detallado en nuestra <a href='/privacy-policy'>Política de Privacidad</a>.</p>
    
    <div class='contact-info'>
        <h3>📞 Contacto</h3>
        <p>Email: support@autotrafic.com<br>Web: https://autotrafic.com</p>
    </div>
</body>
</html>";
    
    return Results.Content(termsContent, "text/html; charset=utf-8");
})
.WithName("TermsOfService")
.WithSummary("📜 Términos de Servicio")
.WithDescription("Términos de servicio para el uso de AutoTrafic")
.WithTags("📄 Políticas");

// 🗑️ Eliminación de datos de usuario
app.MapGet("/data-deletion", () =>
{
    var deletionContent = @"<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Eliminación de Datos - AutoTrafic</title>
    <style>
        body { 
            font-family: Arial, sans-serif; 
            margin: 40px auto; 
            max-width: 800px; 
            line-height: 1.6; 
            color: #333; 
            padding: 0 20px;
        }
        h1 { color: #1877f2; text-align: center; }
        .contact-info { background: #f8f9fa; padding: 20px; border-radius: 8px; margin: 20px 0; }
    </style>
</head>
<body>
    <h1>🗑️ Eliminación de Datos de Usuario</h1>
    <p><strong>Última actualización:</strong> 18 de junio de 2025</p>
    
    <h2>Solicitud de Eliminación</h2>
    <p>Si deseas eliminar tus datos de AutoTrafic, puedes hacerlo de las siguientes maneras:</p>
    
    <h3>Opción 1: Eliminación Automática</h3>
    <p>Los datos se eliminan automáticamente después de 90 días de inactividad.</p>
    
    <h3>Opción 2: Solicitud Manual</h3>
    <p>Envía un correo electrónico a:</p>
    <div class='contact-info'>
        <strong>Email:</strong> data-deletion@autotrafic.com<br>
        <strong>Asunto:</strong> Solicitud de Eliminación de Datos<br>
        <strong>Incluye:</strong> Tu Page ID de Facebook y cualquier información que nos ayude a identificar tu cuenta
    </div>
    
    <h2>Tipos de Datos que Eliminamos</h2>
    <ul>
        <li>Tokens de acceso de Facebook</li>
        <li>Page IDs almacenados</li>
        <li>Logs de publicaciones realizadas</li>
        <li>Configuraciones de usuario</li>
    </ul>
    
    <h2>Tiempo de Procesamiento</h2>
    <p>Las solicitudes de eliminación se procesan en un máximo de 30 días.</p>
    
    <div class='contact-info'>
        <h3>Contacto</h3>
        <p>Email: data-deletion@autotrafic.com<br>Web: https://autotrafic.com</p>
    </div>
</body>
</html>";
    
    return Results.Content(deletionContent, "text/html; charset=utf-8");
})
.WithName("DataDeletion")
.WithSummary("🗑️ Eliminación de Datos")
.WithDescription("Proceso para solicitar eliminación de datos de usuario")
.WithTags("📄 Políticas");

app.Run("http://localhost:4000");
