using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Octagon : Polygon
{
    public Octagon(Vertex A, Vertex B, Vertex C, Vertex D, Vertex E, Vertex F, Vertex G, Vertex H) : base(new HashSet<Vertex> { A, B, C, D, E, F, G, H })
    {
    }
}