using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Primitives;

public class Triangle : Polygon
{
    public Triangle(IEnumerable<Vector2> points) : base(points)
    {
    }

    public Triangle(params Vector2[] points) : base(points)
    {
    }
}