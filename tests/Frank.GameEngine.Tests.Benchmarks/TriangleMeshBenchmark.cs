using BenchmarkDotNet.Attributes;
using System.Numerics;
using Frank.GameEngine.Assets;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Tests.Benchmarks;

[MemoryDiagnoser]
public class TriangleMeshBenchmark
{
    private readonly TriangleMesh _teapot = ModelsAssets.GetTeapotMesh();

    [Benchmark]
    public TriangleMesh TranslateTeapot()
    {
        return _teapot.Translate(new Vector3(1f, 2f, 3f));
    }

    [Benchmark]
    public int CountFaces()
    {
        var n = 0;
        foreach (var _ in _teapot.GetFaces())
            n++;
        return n;
    }
}
