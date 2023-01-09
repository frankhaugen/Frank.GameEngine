using Frank.GameEngine.Core._2D.Calculators;
using Frank.GameEngine.Core._2D.Physics;

namespace Frank.GameEngine.Tests
{
	public class AerodynamicsCalculatorTests
	{
		// Tests for AerodynamicsCalculator that passes and have valid data for the test. And that serve as guards for changes in the code.



		[Fact]
		public void CalculateDragCoefficientOfFluid_WhenCalledWithFluidAndVelocityAndCharacteristicLength_ReturnsDragCoefficient()
		{
			// Arrange
			var fluid = new Fluid(FluidName.Water);
			var velocity = 10f;
			var characteristicLength = 1f;
			var expected = 0.5f;

			// Act
			var actual = AerodynamicsCalculator.CalculateDragCoefficientOfFluid(fluid, velocity, characteristicLength);
			// Assert
			Assert.Equal(expected, actual);
		}
	}
}