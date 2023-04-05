﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy._2d._2D.Rendering;

public class PrimitiveBatch : IDisposable
{
	const int DefaultBufferSize = 500;

	VertexPositionColor[] vertices = new VertexPositionColor[DefaultBufferSize];

	int positionInBuffer = 0;

	BasicEffect basicEffect;

	GraphicsDevice device;

	PrimitiveType primitiveType;

	int numVertsPerPrimitive;

	bool hasBegun = false;

	bool isDisposed = false;

	public PrimitiveBatch(GraphicsDevice graphicsDevice)
	{
		if (graphicsDevice == null)
		{
			throw new ArgumentNullException("graphicsDevice");
		}

		device = graphicsDevice;

		basicEffect = new BasicEffect(graphicsDevice);
		basicEffect.VertexColorEnabled = true;

		basicEffect.Projection = Matrix.CreateOrthographicOffCenter
		(0, graphicsDevice.Viewport.Width,
			graphicsDevice.Viewport.Height, 0,
			0, 1);


		var transformMatrix = Matrix.CreateScale(1, -1, 1) * Matrix.CreateTranslation(0, graphicsDevice.Viewport.Height, 0);

		basicEffect.World = transformMatrix;
		basicEffect.View = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward,
			Vector3.Up);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing && !isDisposed)
		{
			if (basicEffect != null)
				basicEffect.Dispose();

			isDisposed = true;
		}
	}

	public void Begin(PrimitiveType primitiveType)
	{
		if (hasBegun)
		{
			throw new InvalidOperationException
				("End must be called before Begin can be called again.");
		}

		if (primitiveType == PrimitiveType.LineStrip ||
		    primitiveType == PrimitiveType.TriangleStrip)
		{
			throw new NotSupportedException
				("The specified primitiveType is not supported by PrimitiveBatch.");
		}

		this.primitiveType = primitiveType;

		numVertsPerPrimitive = NumVertsPerPrimitive(primitiveType);

		basicEffect.CurrentTechnique.Passes[0].Apply();

		hasBegun = true;
	}

	public void AddVertex(Vector2 vertex, Color color)
	{
		if (!hasBegun)
		{
			throw new InvalidOperationException
				("Begin must be called before AddVertex can be called.");
		}

		var newPrimitive = positionInBuffer % numVertsPerPrimitive == 0;

		if (newPrimitive &&
		    positionInBuffer + numVertsPerPrimitive >= vertices.Length)
		{
			Flush();
		}

		vertices[positionInBuffer].Position = new Vector3(vertex, 0);
		vertices[positionInBuffer].Color = color;

		positionInBuffer++;
	}

	public void End()
	{
		if (!hasBegun)
		{
			throw new InvalidOperationException
				("Begin must be called before End can be called.");
		}

		Flush();

		hasBegun = false;
	}

	private void Flush()
	{
		if (!hasBegun)
		{
			throw new InvalidOperationException
				("Begin must be called before Flush can be called.");
		}

		if (positionInBuffer == 0)
		{
			return;
		}

		var primitiveCount = positionInBuffer / numVertsPerPrimitive;

		device.DrawUserPrimitives(primitiveType, vertices, 0,
			primitiveCount);

		positionInBuffer = 0;
	}

	static private int NumVertsPerPrimitive(PrimitiveType primitive)
	{
		int numVertsPerPrimitive;
		switch (primitive)
		{
			case PrimitiveType.LineList:
				numVertsPerPrimitive = 2;
				break;
			case PrimitiveType.TriangleList:
				numVertsPerPrimitive = 3;
				break;
			default:
				throw new InvalidOperationException("primitive is not valid");
		}

		return numVertsPerPrimitive;
	}
}