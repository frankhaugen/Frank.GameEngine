using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
/// Represents a collision between two objects.
/// </summary>
/// <param name="Location"> The location of the collision.</param>
/// <param name="A">The first object involved in the collision.</param>
/// <param name="B">The second object involved in the collision.</param>
/// <param name="Force">The force and direction of the collision.</param>
/// <param name="Normal">The normal of the collision. A normal is a vector that is perpendicular to the surface of the object.</param>
public readonly record struct Collision(Vector3 Location, GameObject A, GameObject B, Vector3 Force, Vector3 Normal);