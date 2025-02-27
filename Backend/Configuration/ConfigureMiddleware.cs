namespace Backend.Configuration;
public static class ConfigureMiddleware
{
    public static void Configure(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors(policy =>
            policy.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
        );
    }
}