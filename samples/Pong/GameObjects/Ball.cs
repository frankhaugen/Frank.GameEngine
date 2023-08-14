using System.Drawing;
using System.Numerics;
using Frank.GameEngine.Core;
using Frank.GameEngine.Primitives;

namespace Pong.GameObjects;

public class Ball : GameObject
{
    public Ball()
    {
        Transform.Position = new Vector3(GameConstants.ScreenWidth / 2, GameConstants.ScreenHeight / 2, 0);
        Shape.Polygon = PolygonFactory.CreateCircle(5, 32);
        Shape.Color = Color.White;
        Rigidbody.Velocity = Random.Shared.NextDirection(100);
    }
}