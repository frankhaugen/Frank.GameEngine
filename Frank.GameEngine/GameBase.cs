using Frank.GameEngine.Extensions;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;
using Frank.GameEngine.Types;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine;

public class GameBase : Game
{
    // private readonly IOptions<WindowOptions> _options;
    private readonly IRenderer _renderer;
    private readonly IRenderQueue _renderQueue;
    private SpriteBatch _spriteBatch;

    public GameBase(IOptions<WindowOptions> options, IRenderQueue renderQueue)
    {
        // _options = options;
        _renderer = new Renderer(renderQueue, new GraphicsDeviceContext(new GraphicsDeviceManager(this)));
        _renderQueue = renderQueue;
    }
    
    protected override void Initialize()
    {
        _renderer.Initialize();
        EnqueuePolygon();
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        IsMouseVisible = true;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }
    
    protected override void Update(GameTime gameTime)
    {
        if (_renderQueue.IsEmpty)
            EnqueuePolygon();

        base.Update(gameTime);
    }
    
    private void EnqueuePolygon()
    {
        // var polygon = PolygonFactory.CreateTriangle(new Vertex(0, 0, 0), new Vertex(1, 0, 0), new Vertex(0, 1, 0));
        // var polygon = PolygonFactory.CreatePyramid(new Vertex(0, 0, 0), new Vertex(-1, 0, 0), new Vertex(0, -1, 0), new Vertex(0, 0, -1));
        var polygon = PolygonFactory.CreateSquare(new Vertex(0, 0, 0), 1);
        // var polygon = PolygonFactory.CreateCube(new Vertex(0, 0, 0), 1);
        // var polygon = PolygonFactory.CreateSphere(new Vertex(0, 0, 0), 1, 10);
        _renderQueue.Enqueue(polygon);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        using var effect = new BasicEffect(GraphicsDevice);
        effect.VertexColorEnabled = true;
        effect.World = Matrix.Identity;
        effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);
        effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 100);
        
        
        while (_renderQueue.TryDequeue(out var polygon))
        {
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPolygon(polygon, Color.LawnGreen, PrimitiveType.LineList);
            }
        }

        base.Draw(gameTime);
    }

    
}

public static class Texture2DGenerator
{
    public static Texture2D GenerateTexture2D(this GraphicsDevice graphicsDevice, Color color) => graphicsDevice.GenerateTexture2D(graphicsDevice.GenerateRenderTarget2D());
    
    public static Texture2D GenerateTexture2D(this GraphicsDevice graphicsDevice, RenderTarget2D renderTarget)
    {
        // Get the color data from the render target
        var colorData = new Color[renderTarget.Width * renderTarget.Height];
        renderTarget.GetData(colorData);

        // Create a new Texture2D object using the color data
        var texture = new Texture2D(graphicsDevice, renderTarget.Width, renderTarget.Height);
        texture.SetData(colorData);

        return texture;
    }
    
    public static RenderTarget2D GenerateRenderTarget2D(this GraphicsDevice graphicsDevice) => new(graphicsDevice, 128, 128, false, SurfaceFormat.Color, DepthFormat.Depth24);
}