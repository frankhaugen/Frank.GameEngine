using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Extensions;

public static class PolygonExtensions
{
    public static Vertex[] ToVertices(this IPolygon polygon) => polygon.Vertices.ToArray();
    
    public static string GetString(this IPolygon polygon) => polygon.Vertices.GetString();
    
    public static VertexPositionColor[] ToLineList(this IPolygon polygon, Color color)
    {
        var vertexArray = polygon.Vertices.ToArray();
        var lineList = new VertexPositionColor[vertexArray.Length * 2];
        for (var i = 0; i < vertexArray.Length; i++)
        {
            lineList[i * 2] = new VertexPositionColor(vertexArray[i].ToVector3(), color);
            lineList[i * 2 + 1] = new VertexPositionColor(vertexArray[(i + 1) % vertexArray.Length].ToVector3(), color);
        }
        return lineList;
    }
    
    public static Vector3[] ToVector3s(this IPolygon polygon) => polygon.Vertices.Select(vertex => vertex.ToVector3()).ToArray();
    
    public static float[] ToFloats(this IPolygon polygon) => polygon.Vertices.SelectMany(vertex => new[] { vertex.X, vertex.Y, vertex.Z }).ToArray();
    
    public static short[] ToShorts(this IPolygon polygon) => polygon.Vertices.SelectMany(vertex => new short[] { short.CreateChecked(vertex.X), short.CreateChecked(vertex.Y), short.CreateChecked(vertex.Z)}).ToArray();
    
    public static short[] GetIndices(this IPolygon polygon) => polygon.ToVertices().GetIndices();
    
    public static Vertex[] ToVertices(this IEnumerable<float> floats)
    {
        var vertices = new List<Vertex>();
        for (var i = 0; i < floats.Count(); i += 3)
        {
            vertices.Add(new Vertex(floats.ElementAt(i), floats.ElementAt(i + 1), floats.ElementAt(i + 2)));
        }
        return vertices.ToArray();
    }
    
    public static VertexPositionColor[] ToVertexPositionColors(this IPolygon polygon, Color color) => polygon.Vertices.Select(vertex => new VertexPositionColor(vertex.ToVector3(), color)).ToArray();
}