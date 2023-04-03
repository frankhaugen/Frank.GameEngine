namespace Frank.GameEngine.Types;

public abstract class Polygon : IPolygon
{
    public IEnumerable<Vertex> Vertices { get; }

    protected Polygon(IEnumerable<Vertex> vertices)
    {
        Vertices = vertices;
    }
}