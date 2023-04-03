using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Frank.GameEngine.Types;

[DebuggerDisplay("X = {X}, Y = {Y}, Z = {Z}")]
public struct Vertex
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }
    
    public Vertex(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
    public Vertex(float x, float y)
    {
        X = x;
        Y = y;
        Z = 0;
    }
    
    public Vertex(float x)
    {
        X = x;
        Y = 0;
        Z = 0;
    }
    
    public Vertex(Vector3 vector)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
    }
    
    public Vertex(Vector2 vector)
    {
        X = vector.X;
        Y = vector.Y;
        Z = 0;
    }
    
    public Vertex(Vector2 vector, float z)
    {
        X = vector.X;
        Y = vector.Y;
        Z = z;
    }
    
    public Vertex(Vector3 vector, float z)
    {
        X = vector.X;
        Y = vector.Y;
        Z = z;
    }
    
    public Vertex(Vector4 vector)
    {
        X = vector.X;
        Y = vector.Y;
        Z = vector.Z;
    }
    
    public Vertex(Vector4 vector, float z)
    {
        X = vector.X;
        Y = vector.Y;
        Z = z;
    }
    
    public Vertex(Vertex vertex)
    {
        X = vertex.X;
        Y = vertex.Y;
        Z = vertex.Z;
    }
    
    public static Vertex operator +(Vertex a, Vertex b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static Vertex operator -(Vertex a, Vertex b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static Vertex operator *(Vertex a, Vertex b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

    public static Vertex operator /(Vertex a, Vertex b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

    public static Vertex operator +(Vertex a, float b) => new(a.X + b, a.Y + b, a.Z + b);
    
    public static Vertex operator -(Vertex a, float b) => new(a.X - b, a.Y - b, a.Z - b);
    
    public static Vertex operator *(Vertex a, float b) => new(a.X * b, a.Y * b, a.Z * b);
    
    public static Vertex operator /(Vertex a, float b) => new(a.X / b, a.Y / b, a.Z / b);
    
    public static Vertex operator +(float a, Vertex b) => new(a + b.X, a + b.Y, a + b.Z);
    
    public static Vertex operator -(float a, Vertex b) => new(a - b.X, a - b.Y, a - b.Z);
    
    public static Vertex operator *(float a, Vertex b) => new(a * b.X, a * b.Y, a * b.Z);
    
    public static Vertex operator /(float a, Vertex b) => new(a / b.X, a / b.Y, a / b.Z);
    
    public static bool operator ==(Vertex a, Vertex b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    
    public static bool operator !=(Vertex a, Vertex b) => a.X != b.X || a.Y != b.Y || a.Z != b.Z;
    
    public static implicit operator float[](Vertex vertex) => new float[] {vertex.X, vertex.Y, vertex.Z};
    
    public static implicit operator Vector3(Vertex vertex) => new(vertex.X, vertex.Y, vertex.Z);
    
    public static implicit operator Vector2(Vertex vertex) => new(vertex.X, vertex.Y);
    
    public static implicit operator Vector4(Vertex vertex) => new(vertex.X, vertex.Y, vertex.Z, 1);
    
    public static implicit operator Vertex(Vector3 vector) => new(vector.X, vector.Y, vector.Z);
    
    public static implicit operator Vertex(Vector2 vector) => new(vector.X, vector.Y);
    
    public static implicit operator Vertex(Vector4 vector) => new(vector.X, vector.Y, vector.Z);
    
    public static implicit operator Vertex(float value) => new(value);
    
    public static implicit operator Vertex((float x, float y, float z) tuple) => new(tuple.x, tuple.y, tuple.z);
    
    public static implicit operator Vertex((float x, float y) tuple) => new(tuple.x, tuple.y);
    
    public static implicit operator Vertex((float x, float y, float z, float w) tuple) => new(tuple.x, tuple.y, tuple.z);
    
    public static implicit operator Vertex((float x, float y, float z, float w, float u, float v) tuple) => new(tuple.x, tuple.y, tuple.z);
    
    public static implicit operator Vertex((float x, float y, float z, float w, float u, float v, float s, float t) tuple) => new(tuple.x, tuple.y, tuple.z);

    public override bool Equals(object? obj) => obj is Vertex vertex && this == vertex;
    
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    
    public override string ToString() => $"({X}, {Y}, {Z})";
};