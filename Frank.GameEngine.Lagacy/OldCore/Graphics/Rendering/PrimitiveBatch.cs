using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.OldCore.Graphics.Rendering;

public class PrimitiveBatch : IDisposable
{
	private const int DefaultBufferSize = 500;
	private readonly VertexPositionColor[] _vertices = new VertexPositionColor[DefaultBufferSize];
	private readonly BasicEffect _basicEffect;
	private readonly GraphicsDevice _device;

	private PrimitiveType _primitiveType;
	private int _numVertsPerPrimitive;
	private bool hasBegun = false;
	private bool isDisposed = false;
	private int positionInBuffer = 0;

	public PrimitiveBatch(GraphicsDevice graphicsDevice)
	{
		_device = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
		_basicEffect = new BasicEffect(graphicsDevice);
		_basicEffect.VertexColorEnabled = true;
		_basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, 1);
		var transformMatrix = Matrix.CreateScale(1, -1, 1) * Matrix.CreateTranslation(0, graphicsDevice.Viewport.Height, 0);
		_basicEffect.World = transformMatrix;
		_basicEffect.View = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (!disposing || isDisposed) return;
		_basicEffect.Dispose();
		isDisposed = true;
	}

	public void Begin(PrimitiveType primitiveType)
	{
		if (hasBegun) throw new InvalidOperationException("End must be called before Begin can be called again.");
		if (primitiveType is PrimitiveType.LineStrip or PrimitiveType.TriangleStrip) throw new NotSupportedException("The specified primitiveType is not supported by PrimitiveBatch.");
		_primitiveType = primitiveType;
		_numVertsPerPrimitive = NumVertsPerPrimitive(primitiveType);
		_basicEffect.CurrentTechnique.Passes[0].Apply();
		hasBegun = true;
	}

	public void AddVertex(Vector2 vertex, Color color)
	{
		if (!hasBegun) throw new InvalidOperationException("Begin must be called before AddVertex can be called.");
		var newPrimitive = positionInBuffer % _numVertsPerPrimitive == 0;
		if (newPrimitive && positionInBuffer + _numVertsPerPrimitive >= _vertices.Length) Flush();

		_vertices[positionInBuffer].Position = new Vector3(vertex, 0);
		_vertices[positionInBuffer].Color = color;
		positionInBuffer++;
	}

	public void End()
	{
		if (!hasBegun) throw new InvalidOperationException("Begin must be called before End can be called.");
		Flush();
		hasBegun = false;
	}

	private void Flush()
	{
		if (!hasBegun) throw new InvalidOperationException("Begin must be called before Flush can be called.");

		if (positionInBuffer == 0) return;

		var primitiveCount = positionInBuffer / _numVertsPerPrimitive;

		_device.DrawUserPrimitives(_primitiveType, _vertices, 0, primitiveCount);

		positionInBuffer = 0;
	}

	static private int NumVertsPerPrimitive(PrimitiveType primitive)
	{
		var numVertsPerPrimitive = primitive switch
		{
			PrimitiveType.LineList => 2,
			PrimitiveType.TriangleList => 3,
			_ => throw new InvalidOperationException("primitive is not valid")
		};

		return numVertsPerPrimitive;
	}
}