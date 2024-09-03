using EmployeeManagementServer.HttpApi.Extensions.Logging;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Context.HttpRequest;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

/* Configure logging using the LogService, settings can be adjusted in appsettings.json */
LogService.ConfigureLogging(builder.Configuration, LogTargets.Console); /*
                                                                         * Console logging is default enabled on system, mean: it is suggested that enable the console option.
                                                                         * To enable additional log targets, use the LogTargets enum.
                                                                         * Example: (LogTargets.Console | LogTargets.Seq) or (default: LogTargets.Console) or (LogTargets.Seq) or ...
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

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.Run();
await Log.CloseAndFlushAsync();