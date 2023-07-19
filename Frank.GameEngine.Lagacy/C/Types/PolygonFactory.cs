using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Types;

public class PolygonFactory
{
    public static IPolygon CreateTriangle(Vector2 A, Vector2 B, Vector2 C) => new Triangle(A, B, C);
    
    public static IPolygon CreateSquare(Vector2 position, float width, float height)
    {
        var halfWidth = width / 2;
        var halfHeight = height / 2;
        var A = new Vector2(position.X - halfWidth, position.Y - halfHeight);
        var B = new Vector2(position.X + halfWidth, position.Y - halfHeight);
        var C = new Vector2(position.X + halfWidth, position.Y + halfHeight);
        var D = new Vector2(position.X - halfWidth, position.Y + halfHeight);
        return new Square(A, B, C, D);
    }
}