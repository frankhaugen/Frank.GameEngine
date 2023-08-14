using System.Numerics;

namespace Frank.GameEngine.Core;

public static class RandomExtensions
{
    public static T NextEnum<T>(this Random random) where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        var index = random.Next(0, values.Length);
        return (T)values.GetValue(index);
    }

    /// <summary>
    ///     Gets a random direction vector and multiplies it by the force.
    /// </summary>
    /// <param name="random"></param>
    /// <param name="force"></param>
    /// <returns></returns>
    public static Vector3 NextDirection(this Random random, float force)
    {
        return random.NextDirection() * force;
    }

    /// <summary>
    ///     Gets a random direction vector.
    /// </summary>
    /// <param name="random"></param>
    /// <returns></returns>
    public static Vector3 NextDirection(this Random random)
    {
        var x = random.Next(-1, 1);
        var y = random.Next(-1, 1);
        var z = random.Next(-1, 1);
        var direction = new Vector3(x, y, z);
        direction = Vector3.Normalize(direction); // Normalize direction vector
        return direction;
    }
}