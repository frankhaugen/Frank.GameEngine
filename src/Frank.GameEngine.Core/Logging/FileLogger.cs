using Microsoft.Extensions.Logging;

namespace Frank.GameEngine.Core.Logging;

public class FileLogger : ILogger
{
    private readonly FileWriter _writer;

    public FileLogger(string category) : this(
        new FileWriter(new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "logs")), category))
    {
    }

    public FileLogger(FileWriter writer)
    {
        _writer = writer;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        _writer.Write(formatter(state, exception));
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return new FileLoggerScope();
    }
}