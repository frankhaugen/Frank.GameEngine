using Frank.GameEngine.Extensions;
using Frank.GameEngine.Types;

namespace Frank.GameEngine.Primitives;

public class Triangle : Polygon
{
    public Triangle(Vertex A, Vertex B, Vertex C) : base(new HashSet<Vertex> { A, B, C })
    {
    }
}