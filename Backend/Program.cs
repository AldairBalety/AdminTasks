using Backend.Configuration;

var builder = WebApplication.CreateBuilder(args);

// if (builder.Environment.IsDevelopment())
//     builder.Configuration.AddJsonFile("Backend/appsettings.Development.json", optional: true, reloadOnChange: true);
// else
//     builder.Configuration.AddJsonFile("Backend/appsettings.json", optional: false, reloadOnChange: true);

ConfigureServices.Configure(builder);

var app = builder.Build();
ConfigureMiddleware.Configure(app);
ConfigureEndpoints.Configure(app);

var port = Environment.GetEnvironmentVariable("PORT") ?? "4000";

app.Run($"http://0.0.0.0:{port}");
