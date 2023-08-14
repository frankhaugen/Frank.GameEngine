using System.Drawing;
using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Pong.GameObjects;

public class TopWall : GameObject
{
    public TopWall()
    {
        Transform.Position = new Vector3(GameConstants.WallOffset, 0, 0);
        Shape.Polygon = PolygonFactory.CreateCube(GameConstants.ScreenWidth - GameConstants.WallOffset,
            GameConstants.WallWidth, 0);
        Shape.Color = Color.White;
    }
}