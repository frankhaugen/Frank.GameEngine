using Frank.GameEngine.Lagacy._2d._2D.Experimental;
using Frank.GameEngine.Lagacy._2d._2D.Extensions;
using Frank.GameEngine.Lagacy._2d._2D.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy._2d._2D.Rendering;

public class Renderer : IRenderer
{
	private readonly GraphicsDeviceManager _graphics;
	private readonly SpriteBatch _spriteBatch;
	private readonly PrimitiveBatch _primitiveBatch;
	private readonly Camera _camera;

	public Renderer(GraphicsDeviceManager graphics, Camera camera)
	{
		_graphics = graphics;
		_camera = camera;
		_spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
		_primitiveBatch = new PrimitiveBatch(graphics.GraphicsDevice);
	}

	public void Dispose() => _graphics.Dispose();

	public void Render(IGameObject gameObject)
	{
		if (gameObject.Texture != null)
		{
			// Create a matrix that scales the Y axis by -1 and translates the origin to the bottom left corner
			var transformMatrix = Matrix.CreateScale(1, -1, 1) *
			                      Matrix.CreateTranslation(0, _graphics.GraphicsDevice.Viewport.Height, 0);

			// Set the transform matrix as the active sprite batch transform
			//_spriteBatch.Begin(transformMatrix: transformMatrix);
			//_spriteBatch.Begin();
			_spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());
			_spriteBatch.Draw(gameObject.Texture, gameObject.Position, Color.White);
			_spriteBatch.End();
		}
		else
		{
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
		}
	}

	public Texture2D GetTexture(Enum seed, int width, int height, int variation = 25)
	{
		// Create a new texture with the specified dimensions
		Texture2D texture = new Texture2D(_graphics.GraphicsDevice, width, height);

		// Convert the Enum value to an integer seed
		int seedValue = (int)(object)seed;

		// Use the seed value to create a random number generator
		Random rng = new Random(seedValue + variation);

		// Get the centerpoint color for the range based on the Enum value
		Color centerColor = GetCenterColor(seed);

		// Create a color array to hold the pixel data for the texture
		Color[] colors = new Color[width * height];

		// Generate random colors within a certain range of the centerpoint color
		for (int i = 0; i < colors.Length; i++)
		{
			colors[i] = new Color(
				(byte)MathHelper.Clamp(centerColor.R + rng.Next(-variation, variation + 1), 0, 255),
				(byte)MathHelper.Clamp(centerColor.G + rng.Next(-variation, variation + 1), 0, 255),
				(byte)MathHelper.Clamp(centerColor.B + rng.Next(-variation, variation + 1), 0, 255)
			);
		}

		// Set the pixel data for the texture
		texture.SetData(colors);

		// Return the generated texture
		return texture;
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

	// Method for getting the centerpoint color for the range based on the Enum value
	private static Color GetCenterColor(Enum seed)
	{
		// Map the Enum value to a specific color using a switch statement
		switch (seed)
		{
			case RockType.Granite:
				return Color.Gray;
			case RockType.Limestone:
				return Color.White;
			case RockType.Sandstone:
				return Color.Tan;
			case RockType.Slate:
				return Color.SlateGray;
			case MineralType.Coal:
				return Color.Black;
			case MineralType.Iron:
				return Color.Gray;
			case MineralType.Gold:
				return Color.Gold;
			case MineralType.Diamond:
				return Color.DimGray;
			default:
				return Color.Transparent;
		}
	}
}