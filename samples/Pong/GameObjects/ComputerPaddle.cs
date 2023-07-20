using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class ComputerPaddle : GameObject
{
    public ComputerPaddle()
    {
        Transform.Position = new Vector3(19, 20, 0);
        Shape.Polygon = PolygonFactory.CreateCube(3, 5, 0);
        Shape.Color = Color.White;
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