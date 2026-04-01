using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Assets.Testing.Scenes;

public class TeapotTestScene : Scene
{
    public TeapotTestScene() : base("Teapot Test Scene", new Camera())
    {
        var polygon = ModelsAssets.GetTeapot();
        var shape = new Shape { Polygon = polygon, Color = Rgba32.Chartreuse };
        var transform = TransformFactory.CreateTransform();
        var gameObject = GameObjectFactory.CreateGameObject(transform, shape, "Test Object");
        GameObjects.Add(gameObject);
    }
}