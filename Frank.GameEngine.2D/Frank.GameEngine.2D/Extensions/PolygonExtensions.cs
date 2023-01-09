using Microsoft.Xna.Framework;
using MonoGame.Extended.Shapes;

namespace Frank.GameEngine._2D.Extensions;

public static class PolygonExtensions
{
    public static IReadOnlyList<Vector2> GetVector2s(this Polygon source) => source.Vertices;
}