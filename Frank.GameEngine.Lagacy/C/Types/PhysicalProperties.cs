using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Types;

public class PhysicalProperties
{
    public Vector3 Velocity { get; set; } = Vector3.Zero;
    public float Mass { get; set; } = 0f;
}