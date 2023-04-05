using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Pyramid : Polygon
{
    public Pyramid(Vertex A, Vertex B, Vertex C, Vertex D) : base(new HashSet<Vertex> { A, B, C, D })
    {
    }
}