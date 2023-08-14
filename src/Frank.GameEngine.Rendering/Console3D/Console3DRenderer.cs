using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Console3D;

public class Console3DRenderer : IRenderer
{
    public void Render(Scene scene)
    {
    }

    public void Render(Scene scene, Action<string> callback)
    {
        var console3DRenderer = new Console3DRendererBuilder()
            .WithShapes(scene.GetTransformedShapes())
            .Build();

        callback(console3DRenderer);
    }
}