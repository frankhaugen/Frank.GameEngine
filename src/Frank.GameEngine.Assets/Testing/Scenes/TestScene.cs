using Frank.GameEngine.Primitives;
using System.Drawing;

namespace Frank.GameEngine.Assets.Testing.Scenes;

public class BasicTestScene : Scene
{
    public BasicTestScene() : base("Test Scene", new Camera())
    {
        var shape = ShapeFactory.CreateCube(Color.Chartreuse, 5f);
        var transform = TransformFactory.CreateTransform();
        var gameObject = GameObjectFactory.CreateGameObject(transform, shape, "Test Object");
        GameObjects.Add(gameObject);
    }
}

public class TeapotTestScene : Scene
{
    public TeapotTestScene() : base("Teapot Test Scene", new Camera())
    {
        var polygon = ModelsAssets.GetTeapot();
        var shape = new Shape() { Polygon = polygon, Color = Color.Chartreuse };
        var transform = TransformFactory.CreateTransform();
        var gameObject = GameObjectFactory.CreateGameObject(transform, shape, "Test Object");
        GameObjects.Add(gameObject);
    }
}

