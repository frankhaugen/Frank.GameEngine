using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Heptagon : Polygon
{
    public Heptagon(Vertex A, Vertex B, Vertex C, Vertex D, Vertex E, Vertex F, Vertex G) : base(new HashSet<Vertex> { A, B, C, D, E, F, G })
    {
    }
}