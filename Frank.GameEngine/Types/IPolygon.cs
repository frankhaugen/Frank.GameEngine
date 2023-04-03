namespace Frank.GameEngine.Types;

public interface IPolygon
{
    IEnumerable<Vertex> Vertices { get; }
}