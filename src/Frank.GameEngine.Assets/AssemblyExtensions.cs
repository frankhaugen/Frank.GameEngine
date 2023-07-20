using System.Reflection;

namespace Frank.GameEngine.Assets;

internal static class AssemblyExtensions
{
    public static string? GetManifestResourceName(this Assembly assembly, string name)
    {
        var names = assembly.GetManifestResourceNames();
        var result = names.FirstOrDefault(x => x.EndsWith(name, StringComparison.InvariantCultureIgnoreCase));
        return result;
    }

    public static Memory<byte> GetResource(this Assembly assembly, string resourceName)
    {
        using var existingResource = assembly.GetManifestResourceStream(resourceName);
        var memoryStream = new MemoryStream();
        existingResource!.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        return bytes;
    }
}