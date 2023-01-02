using FluentAssertions;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Tests;

public static class Vector2Extensions
{
    public static void ShouldBeApproximately(this Vector2 actual, Vector2 expected, float delta)
    {
        actual.X.Should().BeApproximately(expected.X, delta);
        actual.Y.Should().BeApproximately(expected.Y, delta);
    }
}