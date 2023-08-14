using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Physics;

/// <summary>
///     Drag is a force that is applied to a game object that is moving through a fluid, like air or water.
/// </summary>
public class DragForce : IForce
{
    private readonly float _dragCoefficient;

    public DragForce(float dragCoefficient)
    {
        _dragCoefficient = dragCoefficient;
    }

    public Vector3? Calculate(GameObject gameObject, TimeSpan deltaTime)
    {
        if (gameObject.Rigidbody.Velocity.Length() == 0) return new Vector3();
        var dragDirection = Vector3.Normalize(gameObject.Rigidbody.Velocity);
        var dragMagnitude = _dragCoefficient * gameObject.Rigidbody.Velocity.LengthSquared();
        var drag = dragDirection * dragMagnitude * (float)deltaTime.TotalSeconds * -1;

        return drag;
    }
}