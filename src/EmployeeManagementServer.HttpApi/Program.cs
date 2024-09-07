using EmployeeManagementServer.Application;
using EmployeeManagementServer.HttpApi.Extensions.Logging;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Context.HttpRequest;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

/* Configure logging using the LogService. The settings can be adjusted in appsettings.json. */
LogService.ConfigureLogging(builder.Configuration, isExceptionDetailsEnabled: false, logTargets: LogTargets.Console); /*
     * Console logging is always enabled by default on the system.
     * To enable additional log targets, use the LogTargets enum.
     * Example: (LogTargets.Console | LogTargets.Seq) or (default: LogTargets.Console) or (LogTargets.Seq).
     *
     * The 'isExceptionDetailsEnabled' parameter controls whether detailed exception information is included in the logs.
     * Set this to true if you want to enrich log entries with detailed exception information.
     */
builder.Host.UseSerilog(Log.Logger);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

IWebHostEnvironment env = builder.Environment;
builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

if (env.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationServices();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(configureOptions =>
{
    configureOptions.SuppressModelStateInvalidFilter =
        true; /* Configure API behavior options to suppress automatic model state validation. */
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext =
        Enricher.HttpRequestEnricher; /* Enriches logs with additional (custom) HTTP request context. */
});
app.UseHttpLogging();

app.MapControllers();

app.Run();
await Log.CloseAndFlushAsync();