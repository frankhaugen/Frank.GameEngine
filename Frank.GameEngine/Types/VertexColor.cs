using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Types;

public struct VertexColor
{
    public Vertex Vertex { get; private set; }
    public Color Color { get; private set; }
    
    public VertexColor(Vertex vertex, Color color)
    {
        Vertex = vertex;
        Color = color;
    }
    
    public VertexColor(float x, float y, float z, Color color)
    {
        Vertex = new(x, y, z);
        Color = color;
    }
    
    public VertexColor(float x, float y, Color color)
    {
        Vertex = new(x, y);
        Color = color;
    }
    
    public VertexColor(float x, Color color)
    {
        Vertex = new(x);
        Color = color;
    }
    
    public VertexColor(Vector3 vector, Color color)
    {
        Vertex = new(vector);
        Color = color;
    }
    
    public VertexColor(Vector2 vector, Color color)
    {
        Vertex = new(vector);
        Color = color;
    }
    
    public VertexColor(Vector2 vector, float z, Color color)
    {
        Vertex = new(vector, z);
        Color = color;
    }
    
    public VertexColor(Vector3 vector, float z, Color color)
    {
        Vertex = new(vector, z);
        Color = color;
    }
    
    public VertexColor(Vector4 vector, Color color)
    {
        Vertex = new(vector);
        Color = color;
    }
    
    public VertexColor(Vector4 vector, float z, Color color)
    {
        Vertex = new(vector, z);
        Color = color;
    }
    
    public VertexColor(Vertex vertex, float r, float g, float b, float a)
    {
        Vertex = vertex;
        Color = new(r, g, b, a);
    }
    
    public VertexColor(float x, float y, float z, float r, float g, float b, float a)
    {
        Vertex = new(x, y, z);
        Color = new(r, g, b, a);
    }
    
    public VertexColor(float x, float y, float r, float g, float b, float a)
    {
        Vertex = new(x, y);
        Color = new(r, g, b, a);
    }
    
    public VertexColor(float x, float r, float g, float b, float a)
    {
        Vertex = new(x);
        Color = new(r, g, b, a);
    }
}