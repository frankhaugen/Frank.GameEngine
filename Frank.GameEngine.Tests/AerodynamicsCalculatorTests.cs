using Frank.GameEngine.Core;

namespace Frank.GameEngine.Tests
{
    public class AerodynamicsCalculatorTests
    {
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
