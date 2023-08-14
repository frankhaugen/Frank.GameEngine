using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     A camera that defines the view of the scene.
/// </summary>
public record Camera
{
    /// <summary>
    ///     The position of the camera. Defaults to <see cref="Vector3.UnitZ" /> * 100.
    /// </summary>
    public Vector3 Position { get; set; } = Vector3.UnitZ * 100;

    /// <summary>
    ///     The target of the camera. Defaults to <see cref="Vector3.Zero" />.
    /// </summary>
    public Vector3 Target { get; set; } = Vector3.Zero;

    /// <summary>
    ///     The aspect ratio of the camera. Defaults to 1.6666666666f.
    /// </summary>
    public float AspectRatio { get; set; } = 1.6666666666f;

    /// <summary>
    ///     The up vector of the camera. Defaults to <see cref="Vector3.UnitY" />.
    /// </summary>
    public Vector3 Up { get; } = Vector3.UnitY;

    /// <summary>
    ///     The near plane distance of the camera. Defaults to 1f.
    /// </summary>
    public float NearPlaneDistance { get; set; } = 1f;

    /// <summary>
    ///     The far plane distance of the camera. Defaults to 1_000f.
    /// </summary>
    public float FarPlaneDistance { get; set; } = 1_000f;

    /// <summary>
    ///     The field of view of the camera. Defaults to <see cref="Constants.MathConstants.PiOver4" />.
    /// </summary>
    public float FieldOfView { get; set; } = Constants.MathConstants.PiOver4;

    /// <summary>
    ///     The view matrix of the camera.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"Position: {Position}\nTarget: {Target}\nAspectRatio: {AspectRatio:N}";
    }
}