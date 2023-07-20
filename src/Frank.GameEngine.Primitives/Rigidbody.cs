using System.Numerics;

namespace Frank.GameEngine.Primitives;

public class Rigidbody
{
    public Vector3 Velocity { get; set; } = Vector3.Zero;
    
    public float Mass { get; set; } = 1f;
    
    public float Drag { get; set; } = 0.1f;
}