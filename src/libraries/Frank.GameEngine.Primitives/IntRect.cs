namespace Frank.GameEngine.Primitives;

/// <summary>
///     Axis-aligned integer rectangle (replaces <c>System.Drawing.Rectangle</c> for scene bounds and SVG view boxes).
/// </summary>
public readonly struct IntRect : IEquatable<IntRect>
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }

    public IntRect(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public bool Equals(IntRect other) =>
        X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;

    public override bool Equals(object? obj) => obj is IntRect other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    public static bool operator ==(IntRect left, IntRect right) => left.Equals(right);

    public static bool operator !=(IntRect left, IntRect right) => !left.Equals(right);
}
