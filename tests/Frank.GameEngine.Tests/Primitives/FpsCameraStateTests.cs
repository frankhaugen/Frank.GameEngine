using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Primitives;

public class FpsCameraStateTests
{
    [Test]
    public void ApplyTo_SetsTargetAlongForward()
    {
        var cam = new Camera();
        var fps = new FpsCameraState
        {
            Position = Vector3.Zero,
            Yaw = 0f,
            Pitch = 0f
        };
        fps.ApplyTo(cam, 60f, 16f / 9f);
        cam.Target.Should().NotBe(cam.Position);
        (cam.Target - cam.Position).Length().Should().BeGreaterThan(0.5f);
    }

    [Test]
    public void AddLookDelta_ClampsPitch()
    {
        var fps = new FpsCameraState();
        fps.AddLookDelta(0f, 100f);
        fps.Pitch.Should().BeLessOrEqualTo(MathF.PI * 0.5f);
    }
}
