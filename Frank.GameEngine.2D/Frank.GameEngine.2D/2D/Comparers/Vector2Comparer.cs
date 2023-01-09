using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.Comparers;

public class Vector2Comparer : IEqualityComparer<Vector2>
{
    private readonly float _precision;

    public Vector2Comparer(float precision)
    {
        _precision = precision;
    }

    public bool Equals(Vector2 x, Vector2 y)
    {
        return MathF.Abs(x.X - y.X) < _precision && MathF.Abs(x.Y - y.Y) < _precision;
    }

    public int GetHashCode(Vector2 obj)
    {
        return obj.GetHashCode();
    }
}
