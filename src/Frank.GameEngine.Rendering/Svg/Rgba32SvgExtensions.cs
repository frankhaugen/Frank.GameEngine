using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Svg;

internal static class Rgba32SvgExtensions
{
    /// <summary>CSS hex color (#RRGGBB).</summary>
    public static string ToCssHex(this Rgba32 color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";
}
