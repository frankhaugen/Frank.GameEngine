using System.Drawing;
using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Pong.GameObjects;

public class BottomWall : GameObject
{
    public BottomWall()
    {
        Transform.Position =
            new Vector3(GameConstants.WallOffset, GameConstants.ScreenHeight - GameConstants.WallWidth, 0);
        Shape.Polygon = PolygonFactory.CreateCube(GameConstants.ScreenWidth - GameConstants.WallOffset,
            GameConstants.WallWidth, 0);
        Shape.Color = Color.White;
    }
}