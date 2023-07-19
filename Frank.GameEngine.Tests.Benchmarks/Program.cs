using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Frank.GameEngine.Tests.Benchmarks;

BenchmarkRunner.Run<FaceFactoryBenchmark>(new DebugBuildConfig());
