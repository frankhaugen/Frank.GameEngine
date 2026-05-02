using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Indexed triangle list for arbitrary meshes (OBJ with <c>f</c> lines, FBX/glTF via Assimp). Use instead of
///     <see cref="Polygon" /> when geometry is not a single closed vertex loop fan-triangulated from vertex 0.
/// </summary>
public sealed class TriangleMesh
{
    private readonly Vector3[] _vertices;
    private readonly int[] _indices;

    /// <summary>
    ///     Creates a mesh from vertex positions and a triangle index list (3 indices per triangle, counter-clockwise when
    ///     viewed from the front face).
    /// </summary>
    public TriangleMesh(IReadOnlyList<Vector3> vertices, IReadOnlyList<int> indices)
    {
        ArgumentNullException.ThrowIfNull(vertices);
        ArgumentNullException.ThrowIfNull(indices);
        if (indices.Count % 3 != 0)
            throw new ArgumentException("Index count must be a multiple of 3 for triangles.", nameof(indices));

        _vertices = vertices.Count == 0 ? Array.Empty<Vector3>() : new Vector3[vertices.Count];
        for (var i = 0; i < vertices.Count; i++)
            _vertices[i] = vertices[i];

        _indices = indices.Count == 0 ? Array.Empty<int>() : new int[indices.Count];
        for (var i = 0; i < indices.Count; i++)
            _indices[i] = indices[i];

        if (_vertices.Length == 0 && _indices.Length != 0)
            throw new ArgumentException("Cannot have indices with zero vertices.", nameof(indices));

        for (var i = 0; i < _indices.Length; i++)
        {
            var idx = _indices[i];
            if (idx < 0 || idx >= _vertices.Length)
                throw new ArgumentOutOfRangeException(nameof(indices), $"Index {idx} is out of range for {_vertices.Length} vertices.");
        }
    }

    public ReadOnlySpan<Vector3> Vertices => _vertices;

    public ReadOnlySpan<int> Indices => _indices;

    public int VertexCount => _vertices.Length;

    public int TriangleCount => _indices.Length / 3;

    /// <summary>All triangles as <see cref="Face" /> values in world/index order.</summary>
    public IEnumerable<Face> GetFaces()
    {
        for (var i = 0; i < _indices.Length; i += 3)
        {
            var a = _vertices[_indices[i]];
            var b = _vertices[_indices[i + 1]];
            var c = _vertices[_indices[i + 2]];
            yield return new Face(a, b, c);
        }
    }
}
