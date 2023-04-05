using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Mesh : Polygon
{
    public Mesh(IEnumerable<Vertex> vertices) : base(vertices)
    {
    }
}