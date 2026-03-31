using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Physics.Forces;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Physics;

public class GravityForceTests
{
    [Fact]
    public void Calculate_ReturnsPositiveY_AlignedWithEarthGravityConstant_ForWholeSecondDelta()
    {
        var gravity = new GravityForce();
        var body = new GameObject();

        var impulse = gravity.Calculate(body, TimeSpan.FromSeconds(1));

        impulse.Should().NotBeNull();
        impulse!.Value.X.Should().Be(0f);
        impulse.Value.Z.Should().Be(0f);
        impulse.Value.Y.Should().BeApproximately(Constants.TerrestrialConstants.EarthGravity, 0.0001f);
    }

    [Fact]
    public void Calculate_UsesTimeSpanSecondsComponent_NotTotalSeconds()
    {
        var gravity = new GravityForce();
        var body = new GameObject();

        var impulse = gravity.Calculate(body, TimeSpan.FromSeconds(2.5));

        impulse!.Value.Y.Should().BeApproximately(Constants.TerrestrialConstants.EarthGravity * 2f, 0.0001f,
            "implementation uses deltaTime.Seconds (0-59 component), so fractional seconds beyond the seconds field are truncated in the usual way");
    }
}
