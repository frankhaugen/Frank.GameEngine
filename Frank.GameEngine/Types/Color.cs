using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Types;

public struct ColorXXX
{
    public float R { get; private set; }
    public float G { get; private set; }
    public float B { get; private set; }
    public float A { get; private set; }
    
    public ColorXXX(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    
    public ColorXXX(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
        A = 1;
    }
    
    public ColorXXX(float r, float g)
    {
        R = r;
        G = g;
        B = 0;
        A = 1;
    }
    
    public ColorXXX(float r)
    {
        R = r;
        G = 0;
        B = 0;
        A = 1;
    }
    
    public ColorXXX(Vector3 vector, float a)
    {
        R = vector.X;
        G = vector.Y;
        B = vector.Z;
        A = a;
    }
    
    public ColorXXX(Vector2 vector, float b, float a)
    {
        R = vector.X;
        G = vector.Y;
        B = b;
        A = a;
    }
    
    public ColorXXX(Vector3 vector)
    {
        R = vector.X;
        G = vector.Y;
        B = vector.Z;
        A = 1;
    }
    
    public ColorXXX(Vector2 vector, float b)
    {
        R = vector.X;
        G = vector.Y;
        B = b;
        A = 1;
    }
    
    public ColorXXX(Vector4 vector)
    {
        R = vector.X;
        G = vector.Y;
        B = vector.Z;
        A = vector.W;
    }
    
    public ColorXXX(Color color, float a)
    {
        R = color.R;
        G = color.G;
        B = color.B;
        A = a;
    }
    
    public ColorXXX(Color color)
    {
        R = color.R;
        G = color.G;
        B = color.B;
        A = color.A;
    }
    
    public ColorXXX(float[] array)
    {
        R = array[0];
        G = array[1];
        B = array[2];
        A = array[3];
    }

    public ColorXXX(float[] array, int offset)
    {
        R = array[offset];
        G = array[offset + 1];
        B = array[offset + 2];
        A = array[offset + 3];
    }
    
    public static implicit operator ColorXXX((float r, float g, float b, float a) tuple) => new(tuple.r, tuple.g, tuple.b, tuple.a);
    
    public static implicit operator ColorXXX((float r, float g, float b) tuple) => new(tuple.r, tuple.g, tuple.b);
    
    public static implicit operator ColorXXX((float r, float g) tuple) => new(tuple.r, tuple.g);
    
    public static implicit operator ColorXXX(float r) => new(r);
    
    public static implicit operator ColorXXX(Vector3 vector) => new(vector);
    
    public static implicit operator ColorXXX(Vector4 vector) => new(vector);

    public static implicit operator ColorXXX(float[] array) => new(array);
    
    public static implicit operator ColorXXX((Color color, float a) tuple) => new(tuple.color, tuple.a);
}