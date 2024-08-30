using EmployeeManagementServer.Domain.Shared.Constants.Database;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Constants;
using EmployeeManagementServer.HttpApi.Extensions.Logging.Utils;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace EmployeeManagementServer.HttpApi.Extensions.Logging;

/// <summary>
/// Provides logging configuration services using Serilog, based on the application's configuration settings.
/// </summary>
public static class LogService
{
    /// <summary>
    /// Configures Serilog based on the provided logging options in the appsettings.json configuration.
    /// This method sets up different logging sinks (e.g., Console, Database, Seq) based on the settings.
    /// </summary>
    /// <param name="configuration">The configuration object containing logging settings.</param>
    public static void ConfigureLogging(IConfiguration configuration)
    {
        /* Retrieve the log level from the configuration */
        var logLevelString = configuration[LoggingDefaults.DefaultLogLevel];
        if (!Enum.TryParse<LoggingLevels>(logLevelString, true, out var logLevel))
            logLevel = LoggingLevels.Information; /* Default to Information if not specified */

        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Is(LogLevelConverter.ConvertEnumToSerilogEventLevel(logLevel))
            .Enrich.FromLogContext()
            .WriteTo.Console(); /* LoggingDefaults.IsConsoleLoggingEnabled is true, mean: it is a system's unchangeable value. */

        /* Check if database logging is enabled */
        if (LoggingDefaults.IsDatabaseLoggingEnabled)
        {
            var columnOptions = new Dictionary<string, ColumnWriterBase>
            {
                {"message", new RenderedMessageColumnWriter()},
                {"message_template", new MessageTemplateColumnWriter()},
                {"level", new LevelColumnWriter()},
                {"time_stamp", new TimestampColumnWriter()},
                {"exception", new ExceptionColumnWriter()},
                {"log_event", new LogEventSerializedColumnWriter()}
            };

            loggerConfig.WriteTo.PostgreSQL(
                connectionString: configuration.GetConnectionString("Logging"),
                tableName: EmployeeManagementServerDatabaseConsts.DbTablePrefix + configuration["Logging:Settings:Database:TableName"],
                needAutoCreateTable: true,
                columnOptions: columnOptions);
        }
        
        /* Check if Seq logging is enabled */
        if (LoggingDefaults.IsSeqLoggingEnabled)
            loggerConfig.WriteTo.Seq(serverUrl: configuration.GetValue<string>("Logging:Settings:Seq:ServerUrl")!);
        
        Log.Logger = loggerConfig.CreateLogger();
    }
}