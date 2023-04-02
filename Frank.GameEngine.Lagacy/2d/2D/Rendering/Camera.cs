using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy._2d._2D.Rendering;

public class Camera
{
	private const float ZoomMin = 0.1f;
	private const float ZoomMax = 10f;

	public Vector2 Position { get; set; }
	public float Rotation { get; set; }
	public float Zoom { get; set; } = 1f;

	public Matrix GetViewMatrix()
	{
		return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
		       Matrix.CreateRotationZ(Rotation) *
		       Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
		       Matrix.CreateTranslation(new Vector3(0, 0, 0));
	}

	public void Move(Vector2 amount)
	{
		Position += amount;
	}

	public void Rotate(float amount)
	{
		Rotation += amount;
	}

	public void ZoomIn(float amount)
	{
		Zoom = MathHelper.Clamp(Zoom - amount, ZoomMin, ZoomMax);
	}

	public void ZoomOut(float amount)
	{
		Zoom = MathHelper.Clamp(Zoom + amount, ZoomMin, ZoomMax);
	}
}