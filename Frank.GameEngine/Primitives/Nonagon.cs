using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Nonagon : Polygon
{
    public Nonagon(Vertex A, Vertex B, Vertex C, Vertex D, Vertex E, Vertex F, Vertex G, Vertex H, Vertex I) : base(new HashSet<Vertex> { A, B, C, D, E, F, G, H, I })
    {
    }
}