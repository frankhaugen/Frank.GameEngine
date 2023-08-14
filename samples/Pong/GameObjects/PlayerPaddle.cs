using System.Drawing;
using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Pong.GameObjects;

public class PlayerPaddle : GameObject
{
    public PlayerPaddle()
    {
        Transform.Position = new Vector3(GameConstants.WallOffset * 2, GameConstants.ScreenHeight / 2, 0);
        Shape.Polygon = PolygonFactory.CreateCube(GameConstants.PaddleWidth, GameConstants.PaddleHeight, 0);
        Shape.Color = Color.Chartreuse;
    }

    public void MoveUp(float speed)
    {
        Rigidbody.Velocity = new Vector3(0, speed, 0);
    }

    public void MoveDown(float speed)
    {
        Rigidbody.Velocity = new Vector3(0, -speed, 0);
    }

    public void MoveLeft(float speed)
    {
        Rigidbody.Velocity = new Vector3(-speed, 0, 0);
    }

    public void MoveRight(float speed)
    {
        Rigidbody.Velocity = new Vector3(speed, 0, 0);
    }
}