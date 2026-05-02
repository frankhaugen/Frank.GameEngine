using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Html;

public class CanvasRenderer : IRenderer
{
    public void Render(Scene scene)
    {
        throw new NotImplementedException();
    }

    public void Render(Scene scene, Action<string> callback)
    {
        var canvasRenderer = new CanvasRendererBuilder()
            .WithShapes(scene.GetTransformedShapes())
            .Build();

        callback(canvasRenderer);
    }
}