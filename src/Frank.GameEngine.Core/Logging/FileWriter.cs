namespace Frank.GameEngine.Core.Logging;

public class FileWriter
{
    private readonly FileInfo _logFile;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public FileWriter(DirectoryInfo logDirectory, string category)
    {
        var logFileName = $"{DateTime.UtcNow.Date:yyyy-MM-dd}_{category}.log";
        _logFile = new FileInfo(Path.Combine(logDirectory.FullName, logFileName));
    }

    public void Write(string message)
    {
        _semaphore.Wait();
        try
        {
            using var stream = _logFile.Open(FileMode.Append, FileAccess.Write, FileShare.Read);
            using var writer = new StreamWriter(stream);
            writer.WriteLine(message);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}