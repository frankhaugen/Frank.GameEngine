using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Svg;

public static class IntRectExtensions
{
    public static string ToSvgViewBox(this IntRect rectangle) =>
        $"{rectangle.X} {rectangle.Y} {rectangle.Width} {rectangle.Height}";
}
