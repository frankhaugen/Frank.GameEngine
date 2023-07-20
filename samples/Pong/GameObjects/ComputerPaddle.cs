using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class ComputerPaddle : GameObject
{
    public ComputerPaddle()
    {
        Transform.Position = new Vector3(100, 50, 0);
        Shape.Polygon = PolygonFactory.CreateCube(10, 20, 0);
        Shape.Color = Color.White;
    }
}