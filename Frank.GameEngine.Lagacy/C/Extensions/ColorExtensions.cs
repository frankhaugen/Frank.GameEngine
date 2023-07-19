
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Extensions;

public static class ColorExtensions
{
    public static Color ToColor(this Vector4 vector4) => new Color(vector4.X, vector4.Y, vector4.Z, vector4.W);
    
    public static Vector4 ToVector4(this Color color) => new Vector4(color.R, color.G, color.B, color.A);
    
    // public static float[] ToFloats(this Color color) => new[] { color.R, color.G, color.B, color.A };
    
    public static short[] ToShorts(this Color color) => new short[] { short.CreateChecked(color.R), short.CreateChecked(color.G), short.CreateChecked(color.B), short.CreateChecked(color.A)};
    
    public static IEnumerable<Color> ToColors(this IEnumerable<Vector4> vector4s) => vector4s.Select(vector4 => vector4.ToColor()).ToArray();
    
    public static IEnumerable<Vector4> ToVector4s(this IEnumerable<Color> colors) => colors.Select(color => color.ToVector4()).ToArray();
    
    // public static IEnumerable<float> ToFloats(this IEnumerable<Color> colors) => colors.SelectMany(color => new[] { color.R, color.G, color.B, color.A });
    
    public static IEnumerable<short> ToShorts(this IEnumerable<Color> colors) => colors.SelectMany(color => new short[] { short.CreateChecked(color.R), short.CreateChecked(color.G), short.CreateChecked(color.B), short.CreateChecked(color.A)});
        
}