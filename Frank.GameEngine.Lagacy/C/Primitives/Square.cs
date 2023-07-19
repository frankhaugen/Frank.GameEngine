using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Primitives;

public class Square : Polygon
{
    public Square(IEnumerable<Vector2> points) : base(points)
    {
    }

    public Square(params Vector2[] points) : base(points)
    {
    }
}