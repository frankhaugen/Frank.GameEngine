using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Frank.GameEngine.Core.Logging;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly DirectoryInfo _logDirectory;
    private readonly ConcurrentDictionary<string, FileWriter> _loggers = new();

    public FileLoggerProvider(DirectoryInfo logDirectory)
    {
        _logDirectory = logDirectory;
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (!_loggers.TryGetValue(categoryName, out var logger))
            logger = _loggers.GetOrAdd(categoryName, new FileWriter(_logDirectory, categoryName));
        return new FileLogger(logger);
    }

    public void Dispose()
    {
    }
}