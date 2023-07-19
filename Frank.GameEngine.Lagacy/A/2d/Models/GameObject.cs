using Frank.GameEngine.Lagacy.A._2d.Extensions;
using Frank.Libraries.Calculators.FluentCalculation;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Shapes;

namespace Frank.GameEngine.Lagacy.A._2d.Models;

public interface ITransform
{
    Vector2 Position { get; set; }
    Quaternion Direction { get; set; }
}

public interface IRigidbody
{
    Vector2 Velocity { get; set; }
    public float Mass { get; set; }
}

public class Transform : ITransform
{
    public Vector2 Position { get; set; }
    public Quaternion Direction { get; set; }
}

public class GameObject : ITransform, IRigidbody
{
    public GameObject(Polygon polygon)
    {
        Polygon = polygon;
    }

    public Vector2 Position { get; set; }
    public Quaternion Direction { get; set; }

    public Vector2 Velocity { get; set; }
    public float Mass { get; set; }

    public Polygon Polygon { get; }

    public Color Color { get; set; }

    public Drawable GetDrawable()
    {
        return new Drawable(Position, Polygon, Color);
    }
}

public static class PolygonFactory
{
    public static Polygon GetSquare(float width, float height) => new Rectangle(Vector2.Zero.X.ToInt(), Vector2.Zero.Y.ToInt(), width.ToInt(), height.ToInt()).GetPolygon();
    public static Polygon GetCircle(float radius, int sides) => CreateCircle(radius, sides).ToPolygon();


    private static Vector2[] CreateCircle(double radius, int sides)
    {
        var circle = new Vector2[sides];
        var num1 = 2.0 * Math.PI / (double)sides;
        var num2 = 0.0;
        for (var index = 0; index < sides; ++index)
        {
            circle[index] = new Vector2((float)(radius * Math.Cos(num2)), (float)(radius * Math.Sin(num2)));
            num2 += num1;
        }

        return circle;
    }

    private static Vector2[] CreateEllipse(float rx, float ry, int sides)
    {
        var ellipse = new Vector2[sides];
        var num1 = 0.0;
        var num2 = 2.0 * Math.PI / (double)sides;
        var index = 0;
        while (index < sides)
        {
            var x = rx * (float)Math.Cos(num1);
            var y = ry * (float)Math.Sin(num1);
            ellipse[index] = new Vector2(x, y);
            ++index;
            num1 += num2;
        }

        return ellipse;
    }
}

public record Drawable(Vector2 center, Polygon polygon, Color color);