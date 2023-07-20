using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class BottomWall : GameObject
{
    public BottomWall()
    {
        Transform.Position = new Vector3(0, -90, 0);
        Shape.Polygon = PolygonFactory.CreateCube(240, 10, 0);
        Shape.Color = Color.White;
    }
}