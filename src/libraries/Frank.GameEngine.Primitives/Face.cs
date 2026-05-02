using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Represents a face between three points.
/// </summary>
/// <param name="A"></param>
/// <param name="B"></param>
/// <param name="C"></param>
public readonly record struct Face(Vector3 A, Vector3 B, Vector3 C);