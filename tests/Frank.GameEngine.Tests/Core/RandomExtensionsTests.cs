using System.Numerics;
using FluentAssertions;
using Frank.GameEngine.Core;

namespace Frank.GameEngine.Tests.Core;

public class RandomExtensionsTests
{
    private enum TinyEnum
    {
        Alpha,
        Beta
    }

    [Fact]
    public void NextEnum_WithSeededRandom_IsDeterministic()
    {
        var random = new Random(2026);

        var values = Enumerable.Range(0, 50).Select(_ => random.NextEnum<TinyEnum>()).ToList();

        values.Should().OnlyContain(v => v == TinyEnum.Alpha || v == TinyEnum.Beta);
        values.Distinct().Should().HaveCount(2, "both enum values should appear over many draws");
    }

    [Fact]
    public void NextDirection_ReturnsUnitLength_WhenNonZeroComponents()
    {
        var random = new Random(42);
        var directions = Enumerable.Range(0, 200).Select(_ => random.NextDirection()).ToList();

        foreach (var d in directions)
        {
            if (!float.IsNaN(d.Length()) && d.Length() > 0.0001f)
                d.Length().Should().BeApproximately(1f, 0.02f);
        }
    }

    [Fact]
    public void NextDirection_WithForce_ScalesMagnitude()
    {
        var random = new Random(7);
        var v = random.NextDirection(5f);

        if (!float.IsNaN(v.Length()) && v.Length() > 0.0001f)
            v.Length().Should().BeApproximately(5f, 0.1f);
    }
}
