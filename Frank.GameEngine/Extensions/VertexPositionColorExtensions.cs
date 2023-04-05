using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Extensions;

public static class VertexPositionColorExtensions
{
    public static Vertex ToVertex(this VertexPositionColor vertexPositionColor) => new Vertex(vertexPositionColor.Position.X, vertexPositionColor.Position.Y, vertexPositionColor.Position.Z);
    
    public static VertexPositionColor ToVertexPositionColor(this Vertex vertex) => new VertexPositionColor(vertex.ToVector3(), Color.White);
    
    public static VertexPositionColor[] ToVertexPositionColors(this IEnumerable<Vertex> vertices) => vertices.Select(vertex => vertex.ToVertexPositionColor()).ToArray();
    
    public static Vertex[] ToVertices(this IEnumerable<VertexPositionColor> vertexPositionColors) => vertexPositionColors.Select(vertexPositionColor => vertexPositionColor.ToVertex()).ToArray();
    
    public static Vertex GetCenterpoint(this IEnumerable<Vertex> vertices) => new Vertex(vertices.Average(vertex => vertex.X), vertices.Average(vertex => vertex.Y), vertices.Average(vertex => vertex.Z));
    public static short[] GetIndices(this VertexPositionColor[] vertices)
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
}