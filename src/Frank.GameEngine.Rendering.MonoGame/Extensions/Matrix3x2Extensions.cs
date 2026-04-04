using System.Numerics;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Rendering.MonoGame.Extensions;

public static class Matrix3x2Extensions
{
    public static Matrix ToXnaMatrix(this Matrix3x2 m) =>
        new(m.M11, m.M12, 0f, 0f, m.M21, m.M22, 0f, 0f, 0f, 0f, 1f, 0f, m.M31, m.M32, 0f, 1f);
}
