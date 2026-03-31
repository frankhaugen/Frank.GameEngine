using FluentAssertions;
using Frank.GameEngine.Audio;
using TUnit.Core;

namespace Frank.GameEngine.Tests.Generators;

public class PredictableGuidGeneratorTests
{
    [Test]
    public void GenerateGuid_IsDeterministic_ForDefaultSeed()
    {
        var first = new PredictableGuidGenerator().GenerateGuid();
        var second = new PredictableGuidGenerator().GenerateGuid();
        first.Should().Be(second);
        TestContext.Current!.Output.WriteLine(first.ToString());
    }

    [Test]
    public void GenerateGuid_SubsequentValues_Differ()
    {
        var generator = new PredictableGuidGenerator();
        var a = generator.GenerateGuid();
        var b = generator.GenerateGuid();
        a.Should().NotBe(b);
        TestContext.Current!.Output.WriteLine($"{a} then {b}");
    }
}
