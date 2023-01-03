using Frank.GameEngine.Core.Calculators;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Tests;

// Create the tests with cases for the following:
// - CalculateDirection
// - Vector2ToHeadingAndSpeed
// - HeadingAndSpeedToVector2
public class DirectionsCalculatorTests
{
	[Fact]
	public void CalculateDirection()
	{
		var direction = DirectionsCalculator.CalculateDirection(new Vector2(1, 1), new Vector2(3, 3));
		Assert.Equal(new Vector2(0.70710677f, 0.70710677f), direction);
	}

	[Fact]
	public void Vector2ToHeadingAndSpeed()
	{
		var (heading, speed) = DirectionsCalculator.Vector2ToHeadingAndSpeed(new Vector2(0.7071068f, 0.7071068f));
		Assert.Equal(45, heading);
		Assert.Equal(1, speed);
	}

	[Fact]
	public void HeadingAndSpeedToVector2()
	{
		var vector = DirectionsCalculator.HeadingAndSpeedToVector2(45, 1);
		Assert.Equal(new Vector2(0.70710677f, 0.70710677f), vector);
	}
}