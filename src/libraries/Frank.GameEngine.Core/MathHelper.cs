namespace Frank.GameEngine.Core;

public class MathHelper
{
    private const double DegToRad = Math.PI / 180.0;

    public static float ToRadians(float degrees)
    {
        return (float)(degrees * DegToRad);
    }

    public static float ToDegrees(float radians)
    {
        return (float)(radians / DegToRad);
    }

    public static float Clamp(float value, float min, float max)
    {
        return MathF.Min(MathF.Max(value, min), max);
    }

    public static float Lerp(float value1, float value2, float amount)
    {
        return value1 + (value2 - value1) * amount;
    }
}