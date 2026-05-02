using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering.MonoGame.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Rendering.MonoGame;

public class MonoGameRenderer : IRenderer
{
    private readonly IGraphicsDeviceContext _graphicsDeviceContext;
    private BasicEffect? _effect;

    public MonoGameRenderer(IGraphicsDeviceContext graphicsDeviceContext)
    {
        _graphicsDeviceContext = graphicsDeviceContext;
    }

    public void Render(Scene scene)
    {
        var gd = _graphicsDeviceContext.GraphicsDevice;
        _effect ??= new BasicEffect(gd)
        {
            VertexColorEnabled = true,
            World = Matrix.Identity
        };

        var camera = scene.Camera;
        _effect.View = camera.GetViewMatrix();
        _effect.Projection = camera.GetProjectionMatrix();

        foreach (var pass in _effect.CurrentTechnique.Passes)
        {
            pass.Apply();
            foreach (var go in scene.GameObjects)
            {
                if (!go.IsActive)
                    continue;
                gd.Draw(go.GetTransformedShape());
            }
        }
    }

    public void Render(Scene scene, Action<string> callback)
    {
        Render(scene);
    }
}