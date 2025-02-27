using Backend.Configuration;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices.Configure(builder);

var app = builder.Build();
ConfigureMiddleware.Configure(app);
ConfigureEndpoints.Configure(app);

var port = Environment.GetEnvironmentVariable("PORT") ?? "4000";

app.Run($"http://0.0.0.0:{port}");
