using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Assets;

public static class ObjParser
{
    /// <summary>
    ///     Parses OBJ into an indexed triangle mesh (uses <c>f</c> lines when present).
    /// </summary>
    public static TriangleMesh ParseTriangleMesh(Memory<byte> bytes) =>
        ObjHelper.ParseTriangleMesh(bytes.Span);

    /// <summary>
    ///     Legacy path: builds a <see cref="Polygon" /> from vertex order only (no <c>f</c> semantics). Prefer
    ///     <see cref="ParseTriangleMesh" /> for real models.
    /// </summary>
    public static Polygon GetPolygon(Memory<byte> bytes)
    {
        var mesh = ObjHelper.ParseTriangleMesh(bytes.Span);
        var list = new List<Vector3>();
        foreach (var v in mesh.Vertices)
            list.Add(v);
        return new Polygon(list);
    }
}
