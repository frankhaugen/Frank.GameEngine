using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.ThreeJsRenderer;

public class ThreeJsRenderer : IRenderer
{
    public void Render(Scene scene)
    {
        throw new NotImplementedException();
    }

    public void Render(Scene scene, Action<string> callback)
    {
        var threeJsRenderer = new ThreeJsRendererBuilder()
            .WithImports()
            .WithCreateGeometryFunction()
            .WithCreateRendererFunction()
            .WithCreateMeshFunction()
            .WithCreateCameraFunction(scene.Camera)
            .WithCreateMeshesFunction(scene.GetTransformedShapes())
            .WithCreateSceneFunction()
            .WithEntryCall()
            .Build();

        callback(threeJsRenderer);
    }
}