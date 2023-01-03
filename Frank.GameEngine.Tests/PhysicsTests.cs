using FluentAssertions;
using Frank.GameEngine.Core;
using Frank.GameEngine.Core.Physics;
using Frank.GameEngine.Core.Shapes;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Tests;

public class PhysicsTests
{
	[Fact]
	public void CalculateAcceleration_Should_Return_Correct_Acceleration()
	{
		// Arrange
		var environment = new EnvironmentalFactors
		{
			Gravity = 9.81f,
			Medium = new Fluid
			{
				Density = 1.225f,
				Viscosity = 1.81e-5f
			}
		};
		var physics = new PhysicsEngine(environment);
		var gameObject = new GameObject
		{
			Mass = 1f,
			Polygon = new Polygon(new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)),
			Velocity = new Vector2(1, 0)
		};

		// Act
		//var acceleration = physics.CalculateAcceleration(gameObject);

		// Assert
		//acceleration.X.Should().BeApproximately(-0.0000936f, 0.0000001f);
		//acceleration.Y.Should().BeApproximately(9.81f, 0.001f);
	}

	[Fact]
	public void Update_OnlyGravityActivated_ExpectedBehavior()
	{
		// Arrange
		var physics = new PhysicsEngine(new EnvironmentalFactors
		{
			Gravity = -9.81f,
			Medium = new Fluid(FluidName.Hydrogen),
			Wind = Vector2.Zero
		});

		var gameObject = new GameObject
		{
			Mass = 10f,
			Velocity = Vector2.Zero,
			Direction = Vector2.UnitX,
			Position = new Vector2(0, 0),
			Polygon = PolygonFactory.GetSquare(25),
			CollissionEnabled = true,
			PhysicsEnebled = true
		};

		// Act
		physics.Update(gameObject, TimeSpan.FromSeconds(1));

		// Assert
		Assert.Equal(Vector2.UnitX, gameObject.Direction);
		Assert.Equal(new Vector2(0, -9.81f), gameObject.Velocity);
		Assert.Equal(new Vector2(0, -9.81f), gameObject.Position);
	}

	[Fact]
	public void Update_Should_Update_GameObject_Velocity_And_Position()
	{
		// Arrange
		var environment = new EnvironmentalFactors
		{
			Gravity = 9.81f,
			Medium = new Fluid
			{
				Name = FluidName.Air,
				Density = 1.225f,
				Viscosity = 1.81e-5f
			}
		};
		var physics = new PhysicsEngine(environment);
		var gameObject = new GameObject
		{
			Mass = 1f,
			Polygon = new Polygon(new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)),
			Velocity = new Vector2(1, 0),
			Position = new Vector2(0, 0)
		};
		var elapsed = TimeSpan.FromSeconds(1);

		// Act
		physics.Update(gameObject, elapsed);

		// Assert
		gameObject.Velocity.X.Should().BeApproximately(0.819f, 0.001f);
		gameObject.Velocity.Y.Should().BeApproximately(9.81f, 0.001f);
		gameObject.Position.X.Should().BeApproximately(0.819f, 0.001f);
		gameObject.Position.Y.Should().BeApproximately(9.81f, 0.001f);
	}


	[Theory]
	[InlineData(0, 0, 0, 0, 1, 1f, FluidName.Air, 0, 9.81f, 0, 9.81f)]
	[InlineData(5, 0, 0, 0, 1, 1f, FluidName.Air, 5, 9.81f, 0, 9.81f)]
	[InlineData(0, 0, 5, 0, 9, 1f, FluidName.Air, 45, 9.81f, 45, 9.81f)]
	[InlineData(0, 0, 0, 5, 1, 1f, FluidName.Air, 0, 14.71f, 0, 14.71f)]
	[InlineData(0, 0, 5, 5, 1, 1f, FluidName.Air, 5, 14.71f, 5, 14.71f)]
	[InlineData(0, 0, 0, 0, 5, 1f, FluidName.Air, 0, 4.9f, 0, 4.9f)]
	[InlineData(0, 0, 0, 0, 2, 1f, FluidName.Air, 0, 19.6f, 0, 19.6f)]
	[InlineData(0, 0, 0, 0, 1, 1f, FluidName.Water, 0, 9.81f, 0, 9.81f)]
	[InlineData(0, 0, 0, 0, 5, 1f, FluidName.Water, 0, 4.9f, 0, 4.9f)]
	[InlineData(0, 0, 0, 0, 2, 1f, FluidName.Water, 0, 19.6f, 0, 19.6f)]
	[InlineData(0, 0, 0, 0, 8, 2f, FluidName.Air, 0, 78.48f, 0, 78.48f)]
	[InlineData(0, 0, 0, 0, 7, 0.5f, FluidName.Air, 0, 12.155f, 0, 12.155f)]
	[InlineData(0, 0, -5, -5, 10, 1f, FluidName.Air, -50, -147.1f, -50, -147.1f)]
	public void Update_Should_Update_Position_And_Velocity(float positionX,
		float positionY,
		float velocityX,
		float velocityY,
		int elapsedSeconds,
		float radius,
		FluidName medium,
		float expectedPositionX,
		float expectedPositionY,
		float expectedVelocityX,
		float expectedVelocityY)
	{
		// Arrange
		var elapsed = TimeSpan.FromSeconds(elapsedSeconds);
		var expectedVelocity = new Vector2(expectedVelocityX, expectedVelocityY);
		var expectedPosition = new Vector2(expectedPositionX, expectedPositionY);
		var gameObject = new GameObject
		{
			Position = new Vector2(positionX, positionY),
			Velocity = new Vector2(velocityX, velocityY),
			Mass = 1f,
			Polygon = PolygonFactory.GetCircle(32, radius)
		};
		var physics = new PhysicsEngine(new EnvironmentalFactors { Gravity = 9.81f, Medium = new Fluid(medium) });

		// Act
		physics.Update(gameObject, elapsed);

		// Assert
		gameObject.Position.ShouldBeApproximately(expectedPosition, 0.00001f);
		gameObject.Velocity.ShouldBeApproximately(expectedVelocity, 0.00001f);
	}
}