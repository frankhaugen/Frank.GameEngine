using FluentAssertions;
using Frank.GameEngine.Core;

namespace Frank.GameEngine.Tests.Core;

public class UpdateArgsTests
{
    [Fact]
    public void Equality_ByValue()
    {
        var a = new UpdateArgs(TimeSpan.FromTicks(100), TimeSpan.FromTicks(200));
        var b = new UpdateArgs(TimeSpan.FromTicks(100), TimeSpan.FromTicks(200));

        (a == b).Should().BeTrue();
        a.Should().Be(b);
    }

    [Fact]
    public void Deconstruct_ReturnsElapsedAndTotal()
    {
        var args = new UpdateArgs(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));

        var (elapsed, total) = args;

        elapsed.Should().Be(TimeSpan.FromSeconds(1));
        total.Should().Be(TimeSpan.FromSeconds(10));
    }
}
