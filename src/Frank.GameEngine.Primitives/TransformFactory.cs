using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Factory for creating transforms.
/// </summary>
public static class TransformFactory
{
    /// <summary>
    ///     Creates a transform with the default values.
    /// </summary>
    /// <returns></returns>
    public static Transform CreateTransform()
    {
        var transform = new Transform
        {
            Position = Vector3.Zero,
            Rotation = Quaternion.Identity,
            Scale = 1f
        };
        return transform;
    }

    /// <summary>
    ///     Creates a transform with the specified position and scale.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Transform CreateTransform(Vector3 position, float scale = 1f)
    {
        var transform = new Transform
        {
            Position = position,
            Rotation = Quaternion.Identity,
            Scale = scale
        };
        return transform;
    }

    /// <summary>
    ///     Creates a transform with the specified position and rotation.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Transform CreateTransform(Vector3 position, Quaternion rotation, float scale = 1f)
    {
        var transform = new Transform
        {
            Position = position,
            Rotation = rotation,
            Scale = scale
        };
        return transform;
    }
}