using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Assets;

internal class ObjPolygon
{
    public List<Vector3> Vertices { get; } = new();
    public List<Vector2> Uvs { get; } = new();
    public List<Vector3> Normals { get; } = new();
    public List<Face> Faces { get; } = new();
}