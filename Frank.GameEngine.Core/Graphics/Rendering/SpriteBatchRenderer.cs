using Frank.GameEngine.Core.Extensions;
using Frank.GameEngine.Core.Graphics.Management;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Core.Graphics.Rendering;

public class SpriteBatchRenderer : IRenderer
{
    private readonly IGraphicsManager _graphicsManager;
    private readonly ICamera3D _camera;
    private SpriteBatch _spriteBatch;

    public SpriteBatchRenderer(IGraphicsManager graphicsManager, ICamera3D camera)
    {
        _graphicsManager = graphicsManager;
        _camera = camera;
        // _spriteBatch = graphicsManager.GraphicsDevice != null ? new SpriteBatch(graphicsManager.GraphicsDevice) : null;
    }

    public void Render(IGameObject gameObject)
    {
        if (_spriteBatch == null)
        {
            _spriteBatch = new SpriteBatch(_graphicsManager.GraphicsDevice);
        }
        
        _graphicsManager.GraphicsDevice.Clear(Color.Black);
        _graphicsManager.GraphicsDeviceManager.BeginDraw();

        _spriteBatch.Begin();
        // _spriteBatch.Begin(transformMatrix: _camera.GetView());
        _spriteBatch.Draw(GenerateTexture2D(gameObject), gameObject.Transform.Position.ToVector2(), gameObject.Shape.Color);
        _spriteBatch.End();

        _graphicsManager.GraphicsDeviceManager.EndDraw();
    }

    private Texture2D GenerateTexture2D(IGameObject gameObject)
    {
        var renderTarget = new RenderTarget2D(_graphicsManager.GraphicsDevice, 128, 128, false, SurfaceFormat.Color, DepthFormat.Depth24);
        return _graphicsManager.GraphicsDevice.GenerateTexture2D(renderTarget);
    }
    private Texture3D GenerateTexture3D(IGameObject gameObject)
    {
        // Create a render target to draw the 3D model to
        var renderTarget = new RenderTarget2D(_graphicsManager.GraphicsDevice, 128, 128, false, SurfaceFormat.Color, DepthFormat.Depth24);

        // Set the render target as the current render target
        _graphicsManager.GraphicsDevice.SetRenderTarget(renderTarget);
        _graphicsManager.GraphicsDevice.Clear(Color.Transparent);

        // Create a vertex and index buffer for the 3D model
        var vertexBuffer = gameObject.GetVertexBuffer(_graphicsManager.GraphicsDevice);
        var indexBuffer = gameObject.GetIndexBuffer(_graphicsManager.GraphicsDevice);

        // Set the vertex and index buffer
        _graphicsManager.GraphicsDevice.SetVertexBuffer(vertexBuffer);
        _graphicsManager.GraphicsDevice.Indices = indexBuffer;

        // Create a basic effect to draw the 3D model
        var basicEffect = new BasicEffect(_graphicsManager.GraphicsDevice)
        {
            World = gameObject.Transform.GetWorldMatrix(),
            View = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up),
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1f, 1f, 500f),
            VertexColorEnabled = true
        };

        // Draw the 3D model to the render target
        foreach (var pass in basicEffect.CurrentTechnique.Passes)
        {
            pass.Apply();
            _graphicsManager.GraphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList,
                0,
                0,
                vertexBuffer.VertexCount,
                0,
                indexBuffer.IndexCount / 3
            );
        }

// Reset the current render target to the screen
        _graphicsManager.GraphicsDevice.SetRenderTarget(null);

// Create a texture3D from the render target data
        var texture3D = new Texture3D(_graphicsManager.GraphicsDevice, renderTarget.Width, renderTarget.Height, renderTarget.DepthStencilFormat.ToString().Length, false, SurfaceFormat.Color);
        Color[] colorData = new Color[renderTarget.Width * renderTarget.Height];
        renderTarget.GetData(colorData);
        texture3D.SetData(colorData);

// Dispose of the render target
        renderTarget.Dispose();

// Return the texture3D
        return texture3D;
    }
}