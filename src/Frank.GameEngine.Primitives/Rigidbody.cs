using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
/// Represents a rigidbody. A rigidbody is an object that is affected by physics.
/// </summary>
public class Rigidbody
{
    /// <summary>
    /// The velocity of the object.
    /// </summary>
    public Vector3 Velocity { get; set; } = Vector3.Zero;
    
    /// <summary>
    /// If true, the object will bounce off of other objects when it collides with them.
    /// </summary>
    public bool IsBouncy { get; set; }
    
    /// <summary>
    /// The mass of the object.
    /// </summary>
    public float Mass { get; set; } = 1f;
    
    /// <summary>
    /// The amount of drag to apply to the object.
    /// </summary>
    public float Drag { get; set; } = 0.1f;
    
    /// <summary>
    /// If true, the object will be affected by gravity.
    /// </summary>
    public bool UseGravity { get; set; } = true;

    /// <summary>
    /// If true, the object will be affected by collisions.
    /// </summary>
    public bool IsColliding { get; set; }
}