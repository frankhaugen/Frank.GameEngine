namespace Frank.GameEngine.Extensions;

public static class DirectoryInfoExtensions
{
    public static DirectoryInfo CreateSubdirectory(this DirectoryInfo directoryInfo, string path)
    {
        var fullPath = Path.Combine(directoryInfo.FullName, path);
        return Directory.CreateDirectory(fullPath);
    }

    public static FileInfo CreateFile(this DirectoryInfo directoryInfo, string path)
    {
        var fullPath = Path.Combine(directoryInfo.FullName, path);
        return new FileInfo(fullPath);
    }

    public static FileInfo GetFile(this DirectoryInfo directoryInfo, string path)
    {
        var fullPath = Path.Combine(directoryInfo.FullName, path);
        return new FileInfo(fullPath);
    }
}