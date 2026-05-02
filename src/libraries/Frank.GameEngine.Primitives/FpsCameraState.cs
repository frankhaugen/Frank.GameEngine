using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     First-person style orientation: yaw around world up (Y), pitch around the strafe axis. Apply to a
///     <see cref="Camera" /> each frame after reading mouse deltas and before movement.
/// </summary>
public sealed class FpsCameraState
{
    private const float PitchLimit = MathF.PI * 0.499f;

    /// <summary>Eye position in world space.</summary>
    public Vector3 Position { get; set; }

    /// <summary>Yaw in radians (rotation about +Y, right-handed).</summary>
    public float Yaw { get; set; }

    /// <summary>Pitch in radians (look up/down).</summary>
    public float Pitch { get; set; }

    public Vector3 GetForward()
    {
        var cosP = MathF.Cos(Pitch);
        return new Vector3(MathF.Sin(Yaw) * cosP, MathF.Sin(Pitch), MathF.Cos(Yaw) * cosP);
    }

    public Vector3 GetRight()
    {
        return Vector3.Normalize(Vector3.Cross(GetForward(), Vector3.UnitY));
    }

    public void AddLookDelta(float deltaYaw, float deltaPitch)
    {
        Yaw += deltaYaw;
        Pitch += deltaPitch;
        Pitch = Math.Clamp(Pitch, -PitchLimit, PitchLimit);
    }

    /// <summary>
    ///     Writes position, target, and field of view into <paramref name="camera" /> (degrees for FOV).
    /// </summary>
    public void ApplyTo(Camera camera, float verticalFovDegrees, float aspectRatio)
    {
        var forward = GetForward();
        camera.Position = Position;
        camera.Target = Position + forward;
        camera.Up = Vector3.UnitY;
        camera.FieldOfView = verticalFovDegrees;
        camera.AspectRatio = aspectRatio;
    }
}
