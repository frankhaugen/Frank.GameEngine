using Microsoft.Xna.Framework;
using System.Globalization;

namespace Frank.GameEngine.Core.Extensions;

public static class ColorExtensions
{
    public static Color Average(this Color color, Color other)
    {
        return new Color(
            (color.R + other.R) / 2,
            (color.G + other.G) / 2,
            (color.B + other.B) / 2,
            (color.A + other.A) / 2);
    }

    public static Color Random(this Color color)
    {
        var random = new Random();
        return new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    }

    public static Color FromHex(this Color color, string hex)
    {
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length != 6)
            throw new ArgumentException("Hex value must be 6 characters long");

        var r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        var g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        var b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);

        return new Color(r, g, b);
    }
}
public static class EnumerableExtensions
{
    public static T Random<T>(this IEnumerable<T> source, int seed)
    {
        var random = new Random(seed);
        var list = source.ToList();
        return list[random.Next(list.Count)];
    }
    
    public static T Random<T>(this IEnumerable<T> source)
    {
        var random = new Random();
        var list = source.ToList();
        return list[random.Next(list.Count)];
    }
}

public static class Transform3DExtensions
{
    public static Matrix GetWorldMatrix(this ITransform transform)
    {
        return Matrix.CreateScale(transform.Scale) * Matrix.CreateFromQuaternion(transform.Rotation) * Matrix.CreateTranslation(transform.Position);
    }
}