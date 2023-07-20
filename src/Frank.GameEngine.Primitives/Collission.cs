using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
/// Represents a collision between two objects.
/// </summary>
/// <param name="Location"> The location of the collision.</param>
/// <param name="Normal">The normal of the collision. A normal is a vector that is perpendicular to a surface.</param>
/// <param name="A">The first object involved in the collision.</param>
/// <param name="B">The second object involved in the collision.</param>
/// <param name="Force">The force of the collision.</param>
/// <param name="ForceVectorNormalised">The normalised force vector of the collision.</param>
public readonly record struct Collision(Vector3 Location, Vector3 Normal, GameObject A, GameObject B, float Force, Vector3 ForceVectorNormalised);