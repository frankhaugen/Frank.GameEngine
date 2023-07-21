using Frank.GameEngine.Core;
using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class Ball : GameObject
{
    public Ball()
    {
        Transform.Position = new Vector3(100, 50, 0);
        Shape.Polygon = PolygonFactory.CreateCircle(2, 6);
        Shape.Color = Color.White;
        Rigidbody.Velocity = Random.Shared.NextDirection(10);
    }
}