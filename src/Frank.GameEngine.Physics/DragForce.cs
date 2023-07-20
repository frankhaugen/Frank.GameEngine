using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Physics;

public class DragForce : IForce
{
    public Vector3? Calculate(GameObject gameObject, TimeSpan deltaTime)
    {
        var drag = new Vector3(0, 0, 0);
        return drag;
    }
}