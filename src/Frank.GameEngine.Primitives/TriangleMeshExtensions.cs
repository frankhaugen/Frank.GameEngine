using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class TriangleMeshExtensions
{
    public static TriangleMesh GetCopy(this TriangleMesh mesh)
    {
        var v = mesh.Vertices.ToArray();
        var ix = mesh.Indices.ToArray();
        return new TriangleMesh(v, ix);
    }

    public static TriangleMesh Translate(this TriangleMesh mesh, Vector3 delta)
    {
        var v = mesh.Vertices.ToArray();
        for (var i = 0; i < v.Length; i++)
            v[i] += delta;

        return new TriangleMesh(v, mesh.Indices.ToArray());
    }

    public static TriangleMesh Rotate(this TriangleMesh mesh, Quaternion rotation)
    {
        var v = mesh.Vertices.ToArray();
        for (var i = 0; i < v.Length; i++)
            v[i] = Vector3.Transform(v[i], rotation);

        return new TriangleMesh(v, mesh.Indices.ToArray());
    }

    public static TriangleMesh Scale(this TriangleMesh mesh, float uniformScale)
    {
        var v = mesh.Vertices.ToArray();
        for (var i = 0; i < v.Length; i++)
            v[i] *= uniformScale;

        return new TriangleMesh(v, mesh.Indices.ToArray());
    }

    public static (Vector3 Min, Vector3 Max) GetAxisAlignedBoundingBox(this TriangleMesh mesh)
    {
        if (mesh.VertexCount == 0)
            return (Vector3.Zero, Vector3.Zero);

        var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        foreach (var vertex in mesh.Vertices)
        {
            min = Vector3.Min(min, vertex);
            max = Vector3.Max(max, vertex);
        }

        return (min, max);
    }
}
