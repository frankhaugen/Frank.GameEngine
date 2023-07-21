using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Physics;

/// <summary>
/// Drag is a force that is applied to a game object that is moving through a fluid, like air or water.
/// </summary>
public class DragForce : IForce
{
    public Vector3? Calculate(GameObject gameObject, TimeSpan deltaTime)
    {
        var drag = new Vector3(0, 0, 0);
        return drag;
    }
}