using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.Calculators;

public struct DirectionsCalculator
{
	public static Vector2 CalculateDirection(Vector2 from, Vector2 to)
	{
		var direction = to - from;
		direction.Normalize();
		return direction;
	}

	public static (float heading, float speed) Vector2ToHeadingAndSpeed(Vector2 vector)
	{
		var headingRadians = MathF.Atan2(vector.Y, vector.X);
		var heading = MathHelper.ToDegrees(headingRadians);
		var speed = vector.Length();
		return (heading, speed);
	}

	public static Vector2 HeadingAndSpeedToVector2(float heading, float speed)
	{
		var headingRadians = MathHelper.ToRadians(heading);
		var x = speed * MathF.Cos(headingRadians);
		var y = speed * MathF.Sin(headingRadians);
		return new Vector2(x, y);
	}
}