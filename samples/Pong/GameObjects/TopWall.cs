using Frank.GameEngine.Primitives;
using System.Drawing;
using System.Numerics;

namespace Pong.GameObjects;

public class TopWall : GameObject
{
    public TopWall()
    {
        Transform.Position = new Vector3(0, 90, 0);
        Shape.Polygon = PolygonFactory.CreateCube(240, 10, 0);
        Shape.Color = Color.White;
    }
}