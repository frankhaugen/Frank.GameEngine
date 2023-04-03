

using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Extensions;

public static class VertexExtensions
{
    public static Vertex ToVertex(this Vector3 vector3) => new Vertex(vector3.X, vector3.Y, vector3.Z);
    
    public static Vector3 ToVector3(this Vertex vertex) => new Vector3(vertex.X, vertex.Y, vertex.Z);
    
    public static float[] ToFloats(this Vertex vertex) => new[] { vertex.X, vertex.Y, vertex.Z };
    
    public static short[] ToShorts(this Vertex vertex) => new short[] { short.CreateChecked(vertex.X), short.CreateChecked(vertex.Y), short.CreateChecked(vertex.Z)};
    
    public static IEnumerable<Vertex> ToVertices(this IEnumerable<Vector3> vector3s) => vector3s.Select(vector3 => vector3.ToVertex()).ToArray();
    
    public static IEnumerable<Vector3> ToVector3s(this IEnumerable<Vertex> vertices) => vertices.Select(vertex => vertex.ToVector3()).ToArray();
    
    public static IEnumerable<float> ToFloats(this IEnumerable<Vertex> vertices) => vertices.SelectMany(vertex => new[] { vertex.X, vertex.Y, vertex.Z });
    
    public static IEnumerable<short> ToShorts(this IEnumerable<Vertex> vertices) => vertices.SelectMany(vertex => new short[] { short.CreateChecked(vertex.X), short.CreateChecked(vertex.Y), short.CreateChecked(vertex.Z)});
        
}