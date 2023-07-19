using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Extensions;

public static class Vector3Extensions
{
    
    public static bool IsColliding(this Vector3 vertex) => vertex.X < 0 || vertex.X > 800 || vertex.Y < 0 || vertex.Y > 600;
    
    public static Vector3 Rotate(this Vector3 vertex, float angle) => Vector3.Transform(vertex, Matrix.CreateRotationZ(angle));
    
    public static Vector2 ToVector2(this Vector3 vertex) => new(vertex.X, vertex.Y);
    
    public static VertexPositionColor[] ToLineList(this Vector3[] vertices, Color color)
    {
        var lineList = new VertexPositionColor[vertices.Length * 2];
        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = vertices[i];
            lineList[i * 2] = new VertexPositionColor(vertex, color);
            lineList[i * 2 + 1] = new VertexPositionColor(vertices[(i + 1) % vertices.Length], color);
        }
        return lineList;
    }
}