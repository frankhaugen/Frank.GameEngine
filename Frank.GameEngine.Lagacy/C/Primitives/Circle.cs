using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Primitives;

public class Circle : Polygon
{
    public Circle(IEnumerable<Vector2> points) : base(points)
    {
    }

    public Circle(params Vector2[] points) : base(points)
    {
    }
}