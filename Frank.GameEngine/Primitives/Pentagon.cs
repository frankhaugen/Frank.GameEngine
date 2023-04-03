using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Pentagon : Polygon
{
    public Pentagon(Vertex A, Vertex B, Vertex C, Vertex D, Vertex E) : base(new HashSet<Vertex> { A, B, C, D, E })
    {
    }
}