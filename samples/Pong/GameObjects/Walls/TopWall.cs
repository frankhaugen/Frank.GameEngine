using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class TopWall : GameObject
{
    public TopWall()
    {
        Transform.Position = new Vector3(3, 94, 0);
        Shape.Polygon = PolygonFactory.CreateCube(234, 2, 0);
        Shape.Color = Color.White;
    }
}