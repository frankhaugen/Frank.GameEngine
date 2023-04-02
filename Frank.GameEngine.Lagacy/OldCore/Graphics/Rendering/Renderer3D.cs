using Frank.GameEngine.Lagacy.OldCore.Extensions;
using Frank.GameEngine.Lagacy.OldCore.Graphics.Management;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.OldCore.Graphics.Rendering;

public class Renderer3D : IRenderer
{
    private readonly IGraphicsManager _graphicsManager;
    private readonly ICamera3D _camera;
    private readonly IOptions<GameOptions> _options;
    
    private SpriteBatch _spriteBatch;

    public Renderer3D(IGraphicsManager graphicsManager, ICamera3D camera, IOptions<GameOptions> options)
    {
        _graphicsManager = graphicsManager;
        _camera = camera;
        _options = options;
    }

    public void Render(IGameObject gameObject)
    {
        _graphicsManager.GraphicsDeviceManager.BeginDraw();
        
        // Sets a random background color for each frame
        _graphicsManager.GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black.Random()); 

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
            Vector3[] vertercies = new Vector3[vertexBuffer.VertexCount];
            vertexBuffer.GetData(vertercies);
            var data = vertercies.Select(x => x.ToVertexPositionColor(gameObject.Shape.Color)).ToArray();
            _graphicsManager.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, data, 0, vertexBuffer.VertexCount / 3);
            // _graphicsManager.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, indexBuffer.IndexCount / 3);
        }
        
        _graphicsManager.GraphicsDeviceManager.EndDraw();
    }
    
    public void RenderX(IGameObject gameObject)
    {
        
        if (_spriteBatch == null)
        {
            _spriteBatch = new SpriteBatch(_graphicsManager.GraphicsDevice);
        }
        // if (_primitiveBatch == null)
        // {
        //     _primitiveBatch = new PrimitiveBatch(_graphicsManager.GraphicsDevice);
        // }
        //
        _graphicsManager.GraphicsDevice.Clear(Color.Black); 
        
        _spriteBatch.Begin();
        _spriteBatch.DrawCircle(Vector2.Zero, 350f, 64, Color.Purple, 7f);
        _spriteBatch.DrawPolygon(gameObject.Transform.Position.ToVector2(), gameObject.Shape.Polygon.ToPolygon2D(), gameObject.Shape.Color);
        _spriteBatch.End();

        // _primitiveBatch.Begin(PrimitiveType.LineList);
        // var polygon = gameObject.Shape.Polygon.ToPolygon2D().Translate(gameObject.Transform.Position.ToVector2());
        //
        // for (var i = 0; i < polygon.Vertices.Length - 1; i++)
        // {
        //     _primitiveBatch.AddVertex(polygon.Vertices[i], gameObject.Shape.Color);
        //     _primitiveBatch.AddVertex(polygon.Vertices[i + 1], gameObject.Shape.Color);
        // }
        //
        // // Add a line from the last vertex to the first vertex to complete the shape
        // _primitiveBatch.AddVertex(polygon.Vertices[^1], gameObject.Shape.Color);
        // _primitiveBatch.AddVertex(polygon.Vertices[0], gameObject.Shape.Color);
        //
        // _primitiveBatch.End();
    }
}