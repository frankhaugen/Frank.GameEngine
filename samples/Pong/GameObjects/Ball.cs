using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class Ball : GameObject
{
    public Ball()
    {
        Transform.Position = new Vector3(0, 0, 0);
        Shape.Polygon = PolygonFactory.CreateCircle(10, 16);
        Shape.Color = Color.White;
    }
}