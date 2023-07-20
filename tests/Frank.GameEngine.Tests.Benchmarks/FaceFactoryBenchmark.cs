using BenchmarkDotNet.Attributes;
using Frank.GameEngine.Assets;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Benchmarks;

[MemoryDiagnoser]
[ThreadingDiagnoser]
[DisassemblyDiagnoser]
public class FaceFactoryBenchmark
{
    private readonly Polygon _polygon = ModelsAssets.GetTeapot();

    [Benchmark]
    public void Parallel()
    {
        var result = FaceFactory.Create(_polygon);
    }

    [Benchmark]
    public void Normal()
    {
        var result = FaceFactory.Create(_polygon, false);
    }
}