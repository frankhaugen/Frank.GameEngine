using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Core;

public class Renderer : IRenderer
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly SpriteBatch _spriteBatch;
    private readonly PrimitiveBatch _primitiveBatch;

    public Renderer(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;
        _spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
        _primitiveBatch = new PrimitiveBatch(graphics.GraphicsDevice);
    }

    public void Dispose() => _graphics.Dispose();

    public void Render(IGameObject gameObject)
    {

        // Create a matrix that scales the Y axis by -1 and translates the origin to the bottom left corner
        var transformMatrix = Matrix.CreateScale(1, -1, 1) * Matrix.CreateTranslation(0, _graphics.GraphicsDevice.Viewport.Height, 0);

        // Set the transform matrix as the active sprite batch transform
        _spriteBatch.Begin(transformMatrix: transformMatrix);

        _primitiveBatch.Begin(PrimitiveType.LineList);
        var polygon = gameObject.Polygon.Translate(gameObject.Position);

        for (var i = 0; i < polygon.Vertices.Length - 1; i++)
        {
            _primitiveBatch.AddVertex(polygon.Vertices[i], gameObject.Color);
            _primitiveBatch.AddVertex(polygon.Vertices[i + 1], gameObject.Color);
        }

        // Add a line from the last vertex to the first vertex to complete the shape
        _primitiveBatch.AddVertex(polygon.Vertices[gameObject.Polygon.Vertices.Length - 1], gameObject.Color);
        _primitiveBatch.AddVertex(polygon.Vertices[0], gameObject.Color);

        _primitiveBatch.End();
        _spriteBatch.End();
    }

    /// <summary>
    /// Gets a texture with the specified width, height, and color.
    /// </summary>
    /// <param name="width">The width of the texture in pixels. Default is 1.</param>
    /// <param name="height">The height of the texture in pixels. Default is 1.</param>
    /// <param name="color">The color of the texture. Default is White.</param>
    /// <returns>A texture with the specified width, height, and color.</returns>
    public Texture2D GetColor(int width = 1, int height = 1, Color color = default)
    {
        if (color == default)
        {
            color = Color.White;
        }

        var data = new Color[width * height];
        for (var i = 0; i < data.Length; i++)
        {
            data[i] = color;
        }

        var texture = new Texture2D(_graphics.GraphicsDevice, width, height);
        texture.SetData(data);
        return texture;
    }
}