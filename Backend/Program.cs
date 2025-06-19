using Backend.Configuration;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices.Configure(builder);

var app = builder.Build();
ConfigureMiddleware.Configure(app);
ConfigureEndpoints.Configure(app);

app.Run("http://localhost:4000");
