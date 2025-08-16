namespace BaseSource.EndPoint.WebApi.Providers.Logger.SystemEventProvider;

public interface ISystemEventLogger
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? ex = null);
}
public class SystemEventLogger : ISystemEventLogger, IDisposable
{
    private readonly string _logFilePath;
    private readonly StreamWriter _logWriter;
    private readonly object _lock = new();

    public SystemEventLogger(string logDirectory = "Logs")
    {
        Directory.CreateDirectory(logDirectory);
        _logFilePath = Path.Combine(logDirectory, $"SystemLog_{DateTime.Now:yyyyMMdd_HHmmss}.log");
        _logWriter = new StreamWriter(_logFilePath, append: true) { AutoFlush = true };

        LogInformation($"Logger initialized at {DateTime.Now}");
    }

    public void LogInformation(string message)
    {
        Log("INFO", message);
    }

    public void LogWarning(string message)
    {
        Log("WARN", message);
    }

    public void LogError(string message, Exception? ex = null)
    {
        var fullMessage = ex == null ? message : $"{message} - Exception: {ex}";
        Log("ERROR", fullMessage);
    }

    private void Log(string level, string message)
    {
        lock (_lock)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";
            _logWriter.WriteLine(logEntry);
            Console.WriteLine(logEntry); // Also output to console
        }
    }

    public void Dispose()
    {
        _logWriter?.Dispose();
        GC.SuppressFinalize(this);
    }
}


// Usage:
//using var logger = new SystemEventLogger();
//logger.LogInformation("System initialized");