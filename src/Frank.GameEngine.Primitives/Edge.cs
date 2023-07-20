using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
/// Represents a line between two points.
/// </summary>
/// <param name="A"></param>
/// <param name="B"></param>
public readonly record struct Edge(Vector3 A, Vector3 B);