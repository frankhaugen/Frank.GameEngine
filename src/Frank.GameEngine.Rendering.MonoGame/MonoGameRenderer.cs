using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.MonoGame.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Rendering.MonoGame;

public class MonoGameRenderer : IRenderer
{
    private readonly IGraphicsDeviceContext _graphicsDeviceContext;

    public MonoGameRenderer(IGraphicsDeviceContext graphicsDeviceContext)
    {
        _graphicsDeviceContext = graphicsDeviceContext;
    }

    public void Render(Scene scene)
    {
        var shapes = scene.GetTransformedShapes();

        using var effect = CreateBasicEffect(scene.Camera);
        foreach (var pass in effect.CurrentTechnique.Passes)
        {
            pass.Apply();
            foreach (var shape in shapes) _graphicsDeviceContext.GraphicsDevice.Draw(shape);
        }
    }

    public void Render(Scene scene, Action<string> callback)
    {
        Render(scene);
    }

    private BasicEffect CreateBasicEffect(Camera camera)
    {
        return new BasicEffect(_graphicsDeviceContext.GraphicsDevice)
        {
            VertexColorEnabled = true,
            World = Matrix.Identity,
            View = camera.GetViewMatrix(),
            Projection = camera.GetProjectionMatrix()
        };
    }
}