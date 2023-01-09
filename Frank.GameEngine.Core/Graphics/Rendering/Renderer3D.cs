using Frank.GameEngine.Core.Extensions;
using Frank.GameEngine.Core.Graphics.Management;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Core.Graphics.Rendering;

public class Renderer3D : IRenderer
{
    private readonly IGraphicsManager _graphicsManager;
    private readonly ICamera3D _camera;
    private readonly IOptions<GameOptions> _options;

    public Renderer3D(IGraphicsManager graphicsManager, ICamera3D camera, IOptions<GameOptions> options)
    {
        _graphicsManager = graphicsManager;
        _camera = camera;
        _options = options;
    }

    public void Render(IGameObject gameObject)
    {
        _graphicsManager.GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black.Random());
        _graphicsManager.GraphicsDeviceManager.BeginDraw();
        
        var vertexBuffer = gameObject.GetVertexBuffer(_graphicsManager.GraphicsDevice);
        var indexBuffer = gameObject.GetIndexBuffer(_graphicsManager.GraphicsDevice);

        _graphicsManager.GraphicsDevice.SetVertexBuffer(vertexBuffer);
        _graphicsManager.GraphicsDevice.Indices = indexBuffer;
        
        _camera.SetTarget(gameObject);
        
        var basicEffect = new BasicEffect(_graphicsManager.GraphicsDevice)
        {
            World = gameObject.Transform.GetWorldMatrix(),
            View = _camera.GetView(),
            Projection = _camera.GetProjection(_options.Value.Resolution.Width, _options.Value.Resolution.Height, 1, 10000),
            VertexColorEnabled = true
        };
        
        foreach (var pass in basicEffect.CurrentTechnique.Passes)
        {
            pass.Apply();
            _graphicsManager.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexBuffer.VertexCount);
        }
        
        _graphicsManager.GraphicsDeviceManager.EndDraw();
    }
}