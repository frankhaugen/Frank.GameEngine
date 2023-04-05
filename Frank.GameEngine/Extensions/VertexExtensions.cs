

using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Extensions;

public static class VertexExtensions
{
    public static Vertex ToVertex(this Vector3 vector3) => new Vertex(vector3.X, vector3.Y, vector3.Z);
    
    public static Vector3 ToVector3(this Vertex vertex) => new Vector3(vertex.X, vertex.Y, vertex.Z);
    
    public static float[] ToFloats(this Vertex vertex) => new[] { vertex.X, vertex.Y, vertex.Z };
    
    public static short[] ToShorts(this Vertex vertex) => new short[] { short.CreateChecked(vertex.X), short.CreateChecked(vertex.Y), short.CreateChecked(vertex.Z)};
    
    public static string GetString(this IEnumerable<Vertex> vertices) => string.Join(", ", vertices.Select(vertex => vertex.ToString()));
    
    public static IEnumerable<Vertex> ToVertices(this IEnumerable<Vector3> vector3s) => vector3s.Select(vector3 => vector3.ToVertex()).ToArray();
    
    public static IEnumerable<Vector3> ToVector3s(this IEnumerable<Vertex> vertices) => vertices.Select(vertex => vertex.ToVector3()).ToArray();
    
    public static IEnumerable<float> ToFloats(this IEnumerable<Vertex> vertices) => vertices.SelectMany(vertex => new[] { vertex.X, vertex.Y, vertex.Z });
    
    public static IEnumerable<short> ToShorts(this IEnumerable<Vertex> vertices) => vertices.SelectMany(vertex => new short[] { short.CreateChecked(vertex.X), short.CreateChecked(vertex.Y), short.CreateChecked(vertex.Z)});
    
    public static short[] GetIndices(this Vertex[] vertices)
    {
        var indices = new List<short>();
        for (var i = 0; i < vertices.Length - 2; i++)
        {
            if (i % 2 == 0)
            {
                indices.Add((short)i);
                indices.Add((short)(i + 1));
                indices.Add((short)(i + 2));
            }
            else
            {
                indices.Add((short)i);
                indices.Add((short)(i + 2));
                indices.Add((short)(i + 1));
            }
        }
        return indices.ToArray();
    }
    
    public static short[] GetIndices(this IEnumerable<Vertex> vertices) => vertices.ToArray().GetIndices();
    
    public static int GetVertexCount(this IEnumerable<Vertex> vertices) => vertices.Count();
    
    public static Vertex[] ToVertices(this IEnumerable<float> floats)
    {
        var vertices = new List<Vertex>();
        for (var i = 0; i < floats.Count(); i += 3)
        {
            vertices.Add(new Vertex(floats.ElementAt(i), floats.ElementAt(i + 1), floats.ElementAt(i + 2)));
        }
        return vertices.ToArray();
    }
    
    public static Vertex[] ToVertices(this IEnumerable<short> shorts)
    {
        var vertices = new List<Vertex>();
        for (var i = 0; i < shorts.Count(); i += 3)
        {
            vertices.Add(new Vertex(shorts.ElementAt(i), shorts.ElementAt(i + 1), shorts.ElementAt(i + 2)));
        }
        return vertices.ToArray();
    }
    
    public static VertexColor[] ToVertexColors(this IEnumerable<Vertex> vertices, Color color) => vertices.Select(vertex => new VertexColor(vertex, color)).ToArray();
    public static VertexPositionColor[] ToVertexPositionColors(this IEnumerable<Vertex> vertices, Color color) => vertices.Select(vertex => new VertexPositionColor(vertex.ToVector3(), color)).ToArray();
    public static VertexPositionColor[] ToLineList(this IEnumerable<Vertex> vertices, Color color)
    {
        var vertexArray = vertices.ToArray();
        var lineList = new VertexPositionColor[vertexArray.Length * 2];
        for (var i = 0; i < vertexArray.Length; i++)
        {
            lineList[i * 2] = new VertexPositionColor(vertexArray[i].ToVector3(), color);
            lineList[i * 2 + 1] = new VertexPositionColor(vertexArray[(i + 1) % vertexArray.Length].ToVector3(), color);
        }
        return lineList;
    }
}