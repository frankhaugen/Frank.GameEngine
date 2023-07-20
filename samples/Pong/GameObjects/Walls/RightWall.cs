using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class RightWall : GameObject
{
    public RightWall()
    {
        Transform.Position = new Vector3(234, 3, 0);
        Shape.Polygon = PolygonFactory.CreateCube(2, 94, 0);
        Shape.Color = Color.White;
    }
}