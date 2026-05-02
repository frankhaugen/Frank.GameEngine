namespace Frank.GameEngine.Primitives;

/// <summary>
///     8-bit RGBA color for meshes and scenes without referencing <c>System.Drawing</c>.
/// </summary>
public readonly struct Rgba32 : IEquatable<Rgba32>
{
    public byte R { get; init; }
    public byte G { get; init; }
    public byte B { get; init; }
    public byte A { get; init; }

    public Rgba32(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public static Rgba32 FromArgb(byte a, byte r, byte g, byte b) => new(r, g, b, a);

    public static Rgba32 White => new(255, 255, 255);
    public static Rgba32 Black => new(0, 0, 0);
    public static Rgba32 Red => new(255, 0, 0);
    public static Rgba32 Chartreuse => new(127, 255, 0);
    public static Rgba32 Crimson => new(220, 20, 60);

    public bool Equals(Rgba32 other) => R == other.R && G == other.G && B == other.B && A == other.A;

    public override bool Equals(object? obj) => obj is Rgba32 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(R, G, B, A);

    public static bool operator ==(Rgba32 left, Rgba32 right) => left.Equals(right);

    public static bool operator !=(Rgba32 left, Rgba32 right) => !left.Equals(right);

    public override string ToString() => $"RGBA({R},{G},{B},{A})";
}
