namespace Frank.GameEngine.Primitives;

/// <summary>
///     Integer 2D point for grids and board-style indexing (replaces <c>System.Drawing.Point</c> on hot paths).
/// </summary>
public readonly struct IntPoint : IEquatable<IntPoint>
{
    public int X { get; init; }
    public int Y { get; init; }

    public IntPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(IntPoint other) => X == other.X && Y == other.Y;

    public override bool Equals(object? obj) => obj is IntPoint other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public static bool operator ==(IntPoint left, IntPoint right) => left.Equals(right);

    public static bool operator !=(IntPoint left, IntPoint right) => !left.Equals(right);
}
