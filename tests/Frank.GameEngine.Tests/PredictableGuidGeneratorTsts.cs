using Frank.GameEngine.Audio;
using Xunit.Abstractions;

namespace Frank.GameEngine.Tests;

public class PredictableGuidGeneratorTsts
{
    private readonly ITestOutputHelper _outputHelper;

    public PredictableGuidGeneratorTsts(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public void GenerateGuidsFromDefaults()
    {
        var generator = new PredictableGuidGenerator();
        var guid = generator.GenerateGuid();
        _outputHelper.WriteLine(guid.ToString());
        Assert.Equal("6f460c1a-755d-d8e4-ad67-65d5f519dbc8", guid.ToString());
    }
    
    [Fact]
    public void GenerateGuidsFromSeed()
    {
        var generator = new PredictableGuidGenerator();
        var guid = generator.GenerateGuid();
        _outputHelper.WriteLine(guid.ToString());
        Assert.Equal("6f460c1a-755d-d8e4-ad67-65d5f519dbc8", guid.ToString());
        
        _outputHelper.WriteLine(generator.GenerateGuid().ToString());
        _outputHelper.WriteLine(generator.GenerateGuid().ToString());
        _outputHelper.WriteLine(generator.GenerateGuid().ToString());
        _outputHelper.WriteLine(generator.GenerateGuid().ToString());
    }
}