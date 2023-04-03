using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Tetragon : Polygon
{
    public Tetragon(Vertex A, Vertex B, Vertex C, Vertex D) : base(new HashSet<Vertex> { A, B, C, D })
    {
    }
}