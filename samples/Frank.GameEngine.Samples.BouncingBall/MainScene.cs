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
        Transform.Position = new Vector3(0, -100, 0);
        Shape.Polygon = PolygonFactory.CreateCube(100 -10,10, 0);
        Shape.Color = Color.Crimson;
        Rigidbody.UseGravity = false;
        Rigidbody.IsColliding = false;
        Rigidbody.Velocity = new Vector3(0, 0, 0);
    }
}

public class Ball : GameObject
{
    public Ball()
    {
        Transform.Position = new Vector3(150, -25, 0);
        Shape.Polygon = PolygonFactory.CreateCircle(5, 12);
        Shape.Color = Color.Chartreuse;
        Rigidbody.UseGravity = true;
        Rigidbody.IsColliding = true;
    }
}