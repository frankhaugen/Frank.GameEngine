using System.Drawing;
using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Pong.GameObjects;

public class ComputerPaddle : GameObject
{
    public ComputerPaddle()
    {
        Transform.Position = new Vector3(GameConstants.ScreenWidth - GameConstants.WallOffset * 2,
            GameConstants.ScreenHeight / 2, 0);
        Shape.Polygon = PolygonFactory.CreateCube(GameConstants.PaddleWidth, GameConstants.PaddleHeight, 0);
        Shape.Color = Color.Crimson;
    }

    public void MoveUp(float speed)
    {
        Rigidbody.Velocity = new Vector3(0, speed, 0);
    }

    public void MoveDown(float speed)
    {
        Rigidbody.Velocity = new Vector3(0, -speed, 0);
    }
}