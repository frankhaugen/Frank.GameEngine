using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Rendering.MonoGame.Extensions;

public static class ColorExtensions
{
    public static Color ToXnaColor(this Rgba32 color) => new(color.R, color.G, color.B, color.A);
}
