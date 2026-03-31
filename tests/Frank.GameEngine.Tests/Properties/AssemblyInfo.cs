using TUnit.Core;

// Avoid rare cross-test interference (Roslyn generator smoke tests, shared statics) on highly parallel CI agents.
[assembly: NotInParallel]
