using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public static class Vector2Extensions
{

    public static void EnsureNoNaNs(this Vector2 forceDueToDrag, bool throwIfNaN = false)
    {
        if (float.IsNaN(forceDueToDrag.X))
        {
            if (throwIfNaN)
            {
                throw new ArgumentException("forceDueToDrag.X is NaN");
            }
            forceDueToDrag.X = 0;
        }

        if (float.IsNaN(forceDueToDrag.Y))
        {
            if (throwIfNaN)
            {
                throw new ArgumentException("forceDueToDrag.Y is NaN");
            }
            forceDueToDrag.Y = 0;
        }
    }

    public static float GetMagnitude(this Vector2 vector)
    {
        // Use the Pythagorean theorem to calculate the magnitude of the vector
        return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
    }
}