using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Extensions;

public static class Transform3DExtensions
{
    public static Matrix GetWorldMatrix(this ITransform transform)
    {
        return Matrix.CreateScale(transform.Scale) * Matrix.CreateFromQuaternion(transform.Rotation) * Matrix.CreateTranslation(transform.Position);
    }
}