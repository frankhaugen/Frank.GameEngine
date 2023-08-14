using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Rendering.MonoGame.Extensions;

public static class ColorExtensions
{
    public static Color ToColor(this System.Drawing.Color color)
    {
        return new Color(color.R, color.G, color.B, color.A);
    }

    public static System.Drawing.Color ToColor(this Color color)
    {
        return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}