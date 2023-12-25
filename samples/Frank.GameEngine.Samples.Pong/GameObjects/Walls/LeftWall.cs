using System.Drawing;
using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Pong.GameObjects;

public class LeftWall : GameObject
{
    public LeftWall()
    {
        Transform.Position = new Vector3(0, GameConstants.WallOffset, 0);
        Shape.Polygon = PolygonFactory.CreateCube(GameConstants.WallWidth,
            GameConstants.ScreenHeight - GameConstants.WallOffset, 0);
        Shape.Color = Color.Chartreuse;
    }
}