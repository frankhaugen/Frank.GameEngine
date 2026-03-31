using FluentAssertions;
using Frank.GameEngine.Audio;
using TUnit.Core;

namespace Frank.GameEngine.Tests.Generators;

public class PredictableGuidGeneratorTests
{
    [Test]
    public void GenerateGuidsFromDefaults()
    {
        var generator = new PredictableGuidGenerator();
        var guid = generator.GenerateGuid();
        TestContext.Current!.Output.WriteLine(guid.ToString());
        guid.ToString().Should().Be("6f460c1a-755d-d8e4-ad67-65d5f519dbc8");
    }

    [Test]
    public void GenerateGuidsFromSeed()
    {
        var generator = new PredictableGuidGenerator();
        var guid = generator.GenerateGuid();
        TestContext.Current!.Output.WriteLine(guid.ToString());
        guid.ToString().Should().Be("6f460c1a-755d-d8e4-ad67-65d5f519dbc8");

        TestContext.Current!.Output.WriteLine(generator.GenerateGuid().ToString());
        TestContext.Current.Output.WriteLine(generator.GenerateGuid().ToString());
        TestContext.Current.Output.WriteLine(generator.GenerateGuid().ToString());
        TestContext.Current.Output.WriteLine(generator.GenerateGuid().ToString());
    }
}
