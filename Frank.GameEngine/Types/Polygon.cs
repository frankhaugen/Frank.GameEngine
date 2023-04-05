using System.Diagnostics;

namespace Frank.GameEngine.Types;

public abstract class Polygon : IPolygon
{
    public IEnumerable<Vertex> Vertices { get; }

    protected Polygon(IEnumerable<Vertex> vertices)
    {
        Vertices = vertices;
    }
    
    public override string ToString() => (Vertices.ToString() ?? GetType().FullName) ?? throw new InvalidOperationException();
}