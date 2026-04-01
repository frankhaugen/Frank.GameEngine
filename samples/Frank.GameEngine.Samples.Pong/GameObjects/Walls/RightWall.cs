using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Pong.GameObjects.Walls;

public class RightWall : GameObject
{
    public RightWall()
    {
        Transform.Position =
            new Vector3(GameConstants.ScreenWidth - GameConstants.WallWidth, GameConstants.WallOffset, 0);
        Shape.Polygon = PolygonFactory.CreateCube(GameConstants.WallWidth,
            GameConstants.ScreenHeight - GameConstants.WallOffset, 0);
        Shape.Color = Rgba32.Crimson;
    }
}