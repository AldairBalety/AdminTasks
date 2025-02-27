namespace Backend.Configuration;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

public static class ConfigureServices
{
    public static void Configure(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
        var secretKey = builder.Configuration["Authentication:secretKey"];
        var issuer = builder.Configuration["Authentication:issuer"];
        var audience = builder.Configuration["Authentication:audience"];

        builder.Services.AddSingleton(new MySqlDbContext(connectionString));
        builder.Services.AddScoped(provider => new JwtTokenService(secretKey, issuer, audience));
        builder.Services.AddCors();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                Reference = new OpenApiReference { Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme },
            };
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend API", Version = "v1" });
        });
    }
}