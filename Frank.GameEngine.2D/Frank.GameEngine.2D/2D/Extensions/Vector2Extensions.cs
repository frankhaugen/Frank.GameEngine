using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.Extensions;

public static class Vector2Extensions
{
	public static void EnsureNoNaNs(this Vector2 forceDueToDrag, bool throwIfNaN = false)
	{
		if (float.IsNaN(forceDueToDrag.X))
		{
			if (throwIfNaN)
			{
				throw new ArgumentException("forceDueToDrag.X is NaN");
			}

			forceDueToDrag.X = 0f;
		}

		if (float.IsNaN(forceDueToDrag.Y))
		{
			if (throwIfNaN)
			{
				throw new ArgumentException("forceDueToDrag.Y is NaN");
			}

			forceDueToDrag.Y = 0f;
		}
	}

	public static float GetMagnitude(this Vector2 vector)
	{
		// Use the Pythagorean theorem to calculate the magnitude of the vector
		return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
	}

	public static Vector2 GetNormal(this Vector2 vector)
	{
		// Use math to get the normal of the vector
		return new Vector2(-vector.Y, vector.X);
	}

	public static Vector2 GetTangent(this Vector2 vector)
	{
		// Use math to get the tangent of the vector
		return new Vector2(vector.Y, -vector.X);
	}
}