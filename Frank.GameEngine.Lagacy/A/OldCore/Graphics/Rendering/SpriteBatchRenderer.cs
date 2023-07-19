using Frank.GameEngine.Lagacy.A.OldCore.Extensions;
using Frank.GameEngine.Lagacy.A.OldCore.Graphics.Management;
using Frank.GameEngine.Lagacy.A.OldCore.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Frank.GameEngine.Lagacy.A.OldCore.Graphics.Rendering;

public class SpriteBatchRenderer : IRenderer
{
    private readonly IGraphicsManager _graphicsManager;
    private readonly ICamera3D _camera;
    private PrimitiveBatch _primitiveBatch;
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
        if (_primitiveBatch == null)
        {
            _primitiveBatch = new PrimitiveBatch(_graphicsManager.GraphicsDevice);
        }
        
        _graphicsManager.GraphicsDevice.Clear(Color.Black); 
        
        // _spriteBatch.Begin();
        // _spriteBatch.DrawCircle(Vector2.Zero, 600f, 32, Color.Purple, 10f);
        // _spriteBatch.DrawPolygon(gameObject.Transform.Position.ToVector2(), gameObject.Shape.Polygon.ToPolygon2D(), gameObject.Shape.Color);
        // _spriteBatch.End();

        _primitiveBatch.Begin(PrimitiveType.LineList);
        var polygon = gameObject.Shape.Polygon.ToPolygon2D().Translate(gameObject.Transform.Position.ToVector2());
        
        for (var i = 0; i < polygon.Vertices.Length - 1; i++)
        {
            _primitiveBatch.AddVertex(polygon.Vertices[i], gameObject.Shape.Color);
            _primitiveBatch.AddVertex(polygon.Vertices[i + 1], gameObject.Shape.Color);
        }
        
        // Add a line from the last vertex to the first vertex to complete the shape
        _primitiveBatch.AddVertex(polygon.Vertices[^1], gameObject.Shape.Color);
        _primitiveBatch.AddVertex(polygon.Vertices[0], gameObject.Shape.Color);
        
        _primitiveBatch.End();
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

public static class ShapeExtensions
  {
    private static Texture2D _whitePixelTexture;

    private static Texture2D GetTexture(SpriteBatch spriteBatch)
    {
      if (ShapeExtensions._whitePixelTexture == null)
      {
        ShapeExtensions._whitePixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        ShapeExtensions._whitePixelTexture.SetData<Color>(new Color[1]
        {
          Color.White
        });
        spriteBatch.Disposing += (EventHandler<EventArgs>) ((sender, args) =>
        {
          ShapeExtensions._whitePixelTexture?.Dispose();
          ShapeExtensions._whitePixelTexture = (Texture2D) null;
        });
      }
      return ShapeExtensions._whitePixelTexture;
    }

    public static void DrawPolygon(
      this SpriteBatch spriteBatch,
      Vector2 position,
      Polygon2D polygon,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      spriteBatch.DrawPolygon(position, (IReadOnlyList<Vector2>) polygon.Vertices, color, thickness, layerDepth);
    }

    public static void DrawPolygon(
      this SpriteBatch spriteBatch,
      Vector2 offset,
      IReadOnlyList<Vector2> points,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      if (points.Count == 0)
        return;
      if (points.Count == 1)
      {
        spriteBatch.DrawPoint(points[0], color, (float) (int) thickness);
      }
      else
      {
        Texture2D texture = ShapeExtensions.GetTexture(spriteBatch);
        for (int index = 0; index < points.Count - 1; ++index)
          ShapeExtensions.DrawPolygonEdge(spriteBatch, texture, points[index] + offset, points[index + 1] + offset, color, thickness, layerDepth);
        ShapeExtensions.DrawPolygonEdge(spriteBatch, texture, points[points.Count - 1] + offset, points[0] + offset, color, thickness, layerDepth);
      }
    }

    private static void DrawPolygonEdge(
      SpriteBatch spriteBatch,
      Texture2D texture,
      Vector2 point1,
      Vector2 point2,
      Color color,
      float thickness,
      float layerDepth)
    {
      float x = Vector2.Distance(point1, point2);
      float num = (float) Math.Atan2((double) point2.Y - (double) point1.Y, (double) point2.X - (double) point1.X);
      Vector2 vector2 = new Vector2(x, thickness);
      spriteBatch.Draw(texture, point1, new Rectangle?(), color, num, Vector2.Zero, vector2, SpriteEffects.None, layerDepth);
    }

    public static void FillRectangle(
      this SpriteBatch spriteBatch,
      Rectangle rectangle,
      Color color,
      float layerDepth = 0.0f)
    {
      spriteBatch.FillRectangle(rectangle.Location.ToVector2(), rectangle.Size, color, layerDepth);
    }

    public static void FillRectangle(
      this SpriteBatch spriteBatch,
      Vector2 location,
      Point size,
      Color color,
      float layerDepth = 0.0f)
    {
      spriteBatch.Draw(GetTexture(spriteBatch), location, color);
    }

    public static void FillRectangle(
      this SpriteBatch spriteBatch,
      float x,
      float y,
      int width,
      int height,
      Color color,
      float layerDepth = 0.0f)
    {
      spriteBatch.FillRectangle(new Vector2(x, y), new Point(width, height) , color, layerDepth);
    }

    public static void DrawRectangle(
      this SpriteBatch spriteBatch,
      Rectangle rectangle,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      Texture2D texture = ShapeExtensions.GetTexture(spriteBatch);
      Vector2 vector2_1 = new Vector2(rectangle.X, rectangle.Y);
      Vector2 vector2_2 = new Vector2(rectangle.Right - thickness, rectangle.Y);
      Vector2 vector2_3 = new Vector2(rectangle.X, rectangle.Bottom - thickness);
      Vector2 vector2_4 = new Vector2(rectangle.Width, thickness);
      Vector2 vector2_5 = new Vector2(thickness, rectangle.Height);
      spriteBatch.Draw(texture, vector2_1, new Rectangle?(), color, 0.0f, Vector2.Zero, vector2_4, SpriteEffects.None, layerDepth);
      spriteBatch.Draw(texture, vector2_1, new Rectangle?(), color, 0.0f, Vector2.Zero, vector2_5, SpriteEffects.None, layerDepth);
      spriteBatch.Draw(texture, vector2_2, new Rectangle?(), color, 0.0f, Vector2.Zero, vector2_5, SpriteEffects.None, layerDepth);
      spriteBatch.Draw(texture, vector2_3, new Rectangle?(), color, 0.0f, Vector2.Zero, vector2_4, SpriteEffects.None, layerDepth);
    }

    public static void DrawRectangleX(
      this SpriteBatch spriteBatch,
      Vector2 location,
      Vector2 size,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      spriteBatch.DrawRectangle(new Rectangle(location.ToPoint(), size.ToPoint()), color, thickness, layerDepth);
    }

    public static void DrawRectangle(
      this SpriteBatch spriteBatch,
      float x,
      float y,
      float width,
      float height,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      spriteBatch.DrawRectangle(new Rectangle((int)x, (int)y, (int)width, (int)height), color, thickness, layerDepth);
    }

    public static void DrawLine(
      this SpriteBatch spriteBatch,
      float x1,
      float y1,
      float x2,
      float y2,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      spriteBatch.DrawLine(new Vector2(x1, y1), new Vector2(x2, y2), color, thickness, layerDepth);
    }

    public static void DrawLine(
      this SpriteBatch spriteBatch,
      Vector2 point1,
      Vector2 point2,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      float length = Vector2.Distance(point1, point2);
      float angle = (float) Math.Atan2((double) point2.Y - (double) point1.Y, (double) point2.X - (double) point1.X);
      spriteBatch.DrawLine(point1, length, angle, color, thickness, layerDepth);
    }

    public static void DrawLine(
      this SpriteBatch spriteBatch,
      Vector2 point,
      float length,
      float angle,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      Vector2 vector2_1 = new Vector2(0.0f, 0.5f);
      Vector2 vector2_2 = new Vector2(length, thickness);
      spriteBatch.Draw(ShapeExtensions.GetTexture(spriteBatch), point, new Rectangle?(), color, angle, vector2_1, vector2_2, SpriteEffects.None, layerDepth);
    }

    public static void DrawPoint(
      this SpriteBatch spriteBatch,
      float x,
      float y,
      Color color,
      float size = 1f,
      float layerDepth = 0.0f)
    {
      spriteBatch.DrawPoint(new Vector2(x, y), color, size, layerDepth);
    }

    public static void DrawPoint(
      this SpriteBatch spriteBatch,
      Vector2 position,
      Color color,
      float size = 1f,
      float layerDepth = 0.0f)
    {
      Vector2 vector2_1 = Vector2.One * size;
      Vector2 vector2_2 = new Vector2(0.5f) - new Vector2(size * 0.5f);
      spriteBatch.Draw(ShapeExtensions.GetTexture(spriteBatch), position + vector2_2, new Rectangle?(), color, 0.0f, Vector2.Zero, vector2_1, SpriteEffects.None, layerDepth);
    }


    public static void DrawCircle(
      this SpriteBatch spriteBatch,
      Vector2 center,
      float radius,
      int sides,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      spriteBatch.DrawPolygon(center, (IReadOnlyList<Vector2>) ShapeExtensions.CreateCircle((double) radius, sides), color, thickness, layerDepth);
    }

    public static void DrawCircle(
      this SpriteBatch spriteBatch,
      float x,
      float y,
      float radius,
      int sides,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      spriteBatch.DrawPolygon(new Vector2(x, y), (IReadOnlyList<Vector2>) ShapeExtensions.CreateCircle((double) radius, sides), color, thickness, layerDepth);
    }

    public static void DrawEllipse(
      this SpriteBatch spriteBatch,
      Vector2 center,
      Vector2 radius,
      int sides,
      Color color,
      float thickness = 1f,
      float layerDepth = 0.0f)
    {
      spriteBatch.DrawPolygon(center, (IReadOnlyList<Vector2>) ShapeExtensions.CreateEllipse(radius.X, radius.Y, sides), color, thickness, layerDepth);
    }

    private static Vector2[] CreateCircle(double radius, int sides)
    {
      Vector2[] circle = new Vector2[sides];
      double num1 = 2.0 * Math.PI / (double) sides;
      double num2 = 0.0;
      for (int index = 0; index < sides; ++index)
      {
        circle[index] = new Vector2((float) (radius * Math.Cos(num2)), (float) (radius * Math.Sin(num2)));
        num2 += num1;
      }
      return circle;
    }

    private static Vector2[] CreateEllipse(float rx, float ry, int sides)
    {
      Vector2[] ellipse = new Vector2[sides];
      double num1 = 0.0;
      double num2 = 2.0 * Math.PI / (double) sides;
      int index = 0;
      while (index < sides)
      {
        float x = rx * (float) Math.Cos(num1);
        float y = ry * (float) Math.Sin(num1);
        ellipse[index] = new Vector2(x, y);
        ++index;
        num1 += num2;
      }
      return ellipse;
    }
  }