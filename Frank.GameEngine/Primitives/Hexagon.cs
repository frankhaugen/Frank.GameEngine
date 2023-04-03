using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Hexagon : Polygon
{
    public Hexagon(Vertex A, Vertex B, Vertex C, Vertex D, Vertex E, Vertex F) : base(new HashSet<Vertex> { A, B, C, D, E, F })
    {
    }
}