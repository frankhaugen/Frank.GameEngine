using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Hendecagon : Polygon
{
    public Hendecagon(Vertex A, Vertex B, Vertex C, Vertex D, Vertex E, Vertex F, Vertex G, Vertex H, Vertex I, Vertex J, Vertex K) : base(new HashSet<Vertex> { A, B, C, D, E, F, G, H, I, J, K })
    {
    }
}