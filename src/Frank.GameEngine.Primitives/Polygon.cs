using System.Collections;
using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
/// Represents a polygon. A polygon is a shape of vertices.
/// </summary>
public class Polygon : IEnumerable<Vector3>
{
    private readonly Vector3[] _vertices;
    private readonly Edge[] _edges;

    internal Polygon()
    {
        _vertices = Array.Empty<Vector3>();
        _edges = Array.Empty<Edge>();
    }

    /// <summary>
    /// Creates a polygon from a list of vertices. A polygon is a shape of vertices.
    /// </summary>
    /// <param name="vertices"></param>
    public Polygon(IEnumerable<Vector3> vertices)
    {
        _vertices = vertices.ToArray();
        _edges = new Edge[_vertices.Length + 1];
        for (var i = 0; i < _vertices.Length; i++)
        {
            var a = _vertices[i];
            var b = _vertices[(i + 1) % _vertices.Length];
            _edges[i] = new Edge(a, b);
        }
        
        var finaleEdge = new Edge(_vertices[^1], _vertices[0]);
        _edges[^1] = finaleEdge;
    }

    /// <summary>
    /// Gets the number of vertices in the polygon. A polygon is a shape of vertices.
    /// </summary>
    public int Length => _vertices.Length;

    /// <summary>
    /// Gets the enumerator for the vertices in the polygon. A polygon is a shape of vertices.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<Vector3> GetEnumerator() => ((IEnumerable<Vector3>)_vertices).GetEnumerator();

    /// <summary>
    /// Gets the enumerator for the vertices in the polygon. A polygon is a shape of vertices.
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Gets the vertex at the specified index. A polygon is a shape of vertices.
    /// </summary>
    /// <param name="i"></param>
    public Vector3 this[int i]
    {
        get => _vertices[i];
        set => _vertices[i] = value;
    }

    /// <summary>
    /// Gets the edges of the polygon. An edge is a line between two points <see cref="Edge"/>.
    /// </summary>
    public IEnumerable<Edge> Edges => _edges;
    
    /// <summary>
    /// Gets the position of the polygon. The position is the average of all the vertices.
    /// </summary>
    public Vector3 Position => _vertices.Aggregate((a, b) => a + b) / _vertices.Length;

    // public float Width => _vertices.Max(x => x.X) - _vertices.Min(x => x.X);
    //
    // public float Height => _vertices.Max(x => x.Y) - _vertices.Min(x => x.Y);
    //
    // public float Depth => _vertices.Max(x => x.Z) - _vertices.Min(x => x.Z);

    public override string ToString() => $"Vertices: {string.Join(", ", _vertices)}";
}