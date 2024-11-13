using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    public static IConfiguration Configuration { get; private set; }

    static void Main(string[] args)
    {
        ConfigureAppSettings();
        var logPath = GetLogPathWithFallback();
        EnsureDirectoryExists(logPath);

        var serviceProvider = ConfigureServices(logPath);
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("Application working.");
    }

    private static void ConfigureAppSettings()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
    }

    private static string GetLogPathWithFallback() => Configuration["ApplicationSettings:LogPath"] ?? "Logs";

    private static void EnsureDirectoryExists(string path)
    {
        if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private static ServiceProvider ConfigureServices(string logPath) =>
        new ServiceCollection()
            .AddLogging(configure => configure.AddFileLogger(logPath))
            .BuildServiceProvider();
}

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, string logPath)
    {
        builder.Services.AddSingleton<ILoggerProvider>(_ => new FileLoggerProvider(logPath));
        return builder;
    }
}

public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _logPath;

    public FileLoggerProvider(string logPath)
    {
        _logPath = logPath;
    }

    public ILogger CreateLogger(string categoryName) => new FileLogger(_logPath);

    public void Dispose() { }
}

public class FileLogger : ILogger
{
    private readonly object _lock = new object();
    private readonly string _logDirectory;

    public FileLogger(string logDirectory)
    {
        _logDirectory = logDirectory;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var logFilePath = Path.Combine(_logDirectory, $"{DateTime.Now:yyyyMMdd}.log");
        var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {formatter(state, exception)}";

        if (exception != null)
        {
            logEntry += Environment.NewLine + exception;
        }

        lock (_lock)
        {
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
    }
}
