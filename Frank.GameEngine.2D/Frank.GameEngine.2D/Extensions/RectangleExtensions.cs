using Microsoft.Xna.Framework;
using MonoGame.Extended.Shapes;

namespace Frank.GameEngine._2D.Extensions;

internal static class RectangleExtensions
{
    internal static IReadOnlyList<Vector2> GetVector2s(this Rectangle source)
    {
        var vectors = new List<Vector2>();
        vectors.Add(new Vector2(source.Left, source.Top));
        vectors.Add(new Vector2(source.Right, source.Top));
        vectors.Add(new Vector2(source.Right, source.Bottom));
        vectors.Add(new Vector2(source.Left, source.Bottom));
        return vectors;
    }

    internal static Polygon GetPolygon(this Rectangle source) => new(source.GetVector2s());
}