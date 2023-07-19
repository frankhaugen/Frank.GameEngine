using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;

//using MonoGame.Shapes;

namespace Frank.GameEngine.Lagacy.A._2d.Extensions;

internal static class Vector2Extensions
{
    internal static Point ToPoint(this Vector2 source) => new Point(Convert.ToInt32(source.X), Convert.ToInt32(source.Y));
    internal static Vector2 Translate(this Vector2 source, Vector2 destination) => source.Translate(destination.X, destination.Y);
    internal static Polygon ToPolygon(this IEnumerable<Vector2> source) => new(source);
    internal static Vector3 ToVector3(this Vector2 source) => new Vector3(source, 0);
    internal static Vector2 Copy(this Vector2 source) => new(source.X, source.Y);
    internal static Vector2 Add(this Vector2 source, Vector2 value) => Vector2.Add(source, value);
    internal static Vector2 Add(this Vector2 source, float xValue, float yValue) => Vector2.Add(source, new Vector2(xValue, yValue));
    internal static IReadOnlyList<Vector3> ToVector3s(this IEnumerable<Vector2> source) => new List<Vector3>(source.Select(x => x.ToVector3()));
}