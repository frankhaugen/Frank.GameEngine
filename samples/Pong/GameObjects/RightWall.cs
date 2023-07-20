using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class RightWall : GameObject
{
    public RightWall()
    {
        Transform.Position = new Vector3(120, 0, 0);
        Shape.Polygon = PolygonFactory.CreateCube(10, 100, 0);
        Shape.Color = Color.White;
    }
}