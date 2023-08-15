using System.Drawing;
using System.Numerics;
using Frank.GameEngine.Core;
using Frank.GameEngine.Primitives;

namespace BouncingBall;

public class MainScene : Scene
{
    public MainScene(string name, Camera camera) : base(name, camera)
    {
    }
}

public class Floor : GameObject
{
    public Floor()
    {
        Shape.Polygon = PolygonFactory.CreateCube(GameConstants.ScreenWidth,10, 0);
        Shape.Color = Color.Crimson;
    }
}

public class Ball : GameObject
{
    public Ball()
    {
        Transform.Position = new Vector3(GameConstants.ScreenWidth / 2, GameConstants.ScreenHeight / 2, 0);
        Shape.Polygon = PolygonFactory.CreateCircle(5, 12);
        Shape.Color = Color.Chartreuse;
    }
}