using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Primitives;

public class PolygonFactoryTests
{
    [Fact]
    public void CreateCube_SingleArg_HasEightVertices()
    {
        var poly = PolygonFactory.CreateCube(2f);

        poly.Length.Should().Be(8);
    }

    [Fact]
    public void CreateRectangle_HasFourVertices()
    {
        var poly = PolygonFactory.CreateRectangle(4f, 2f, Vector3.Zero);

        poly.Length.Should().Be(4);
    }

    [Fact]
    public void CreateLine_HasTwoVertices()
    {
        var poly = PolygonFactory.CreateLine(Vector3.Zero, Vector3.UnitX);

        poly.Length.Should().Be(2);
    }

    [Fact]
    public void CreateCircle_HasExpectedVertexCount()
    {
        const int sides = 12;
        var poly = PolygonFactory.CreateCircle(sides, 1f, Vector3.Zero);

        poly.Length.Should().Be(sides);
    }
}
