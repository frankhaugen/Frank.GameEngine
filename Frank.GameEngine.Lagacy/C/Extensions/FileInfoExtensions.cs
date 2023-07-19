namespace Frank.GameEngine.Extensions;

public static class FileInfoExtensions
{
    public static async Task<string> ReadAllTextAsync(this FileInfo fileInfo)
    {
        await using var stream = fileInfo.OpenRead();
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
    public static async Task<IEnumerable<string>> ReadAllLinesAsync(this FileInfo fileInfo)
    {
        await using var stream = fileInfo.OpenRead();
        using var reader = new StreamReader(stream);
        var lines = new List<string>();
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            lines.Add(line);
        }
        return lines;
    }

    public static async Task WriteAllTextAsync(this FileInfo fileInfo, string text)
    {
        await using var stream = fileInfo.OpenWrite();
        await using var writer = new StreamWriter(stream);
        await writer.WriteAsync(text);
    }
    
    public static async Task WriteAllLinesAsync(this FileInfo fileInfo, IEnumerable<string> lines)
    {
        var text = string.Join(Environment.NewLine, lines);
        await fileInfo.WriteAllTextAsync(text);
    }
    
    public static async Task AppendAllLinesAsync(this FileInfo fileInfo, IEnumerable<string> lines)
    {
        var text = string.Join(Environment.NewLine, lines);
        await fileInfo.AppendAllTextAsync(text);
    }
    
    
    
    public static async Task AppendAllTextAsync(this FileInfo fileInfo, string text)
    {
        await using var stream = fileInfo.Open(FileMode.Append, FileAccess.Write);
        await using var writer = new StreamWriter(stream);
        await writer.WriteAsync(text);
    }
    
    public static async Task<byte[]> ReadAllBytesAsync(this FileInfo fileInfo)
    {
        await using var stream = fileInfo.OpenRead();
        using var reader = new BinaryReader(stream);
        return reader.ReadBytes((int) fileInfo.Length);
    }
    
    public static async Task WriteAllBytesAsync(this FileInfo fileInfo, byte[] bytes)
    {
        await using var stream = fileInfo.OpenWrite();
        await using var writer = new BinaryWriter(stream);
        writer.Write(bytes);
    }
    
}