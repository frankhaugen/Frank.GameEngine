using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Extensions;

public static class Vector2Extensions
{
    
    public static bool IsColliding(this Vector2 vertex) => vertex.X < 0 || vertex.X > 800 || vertex.Y < 0 || vertex.Y > 600;
    
    public static Vector2 Rotate(this Vector2 vertex, float angle) => Vector2.Transform(vertex, Matrix.CreateRotationZ(angle));
    
    public static Vector3 ToVector3(this Vector2 vertex) => new(vertex.X, vertex.Y, 0);
    
    public static VertexPositionColor[] ToLineList(this Vector2[] vertices, Color color)
    {
        var lineList = new VertexPositionColor[vertices.Length * 2];
        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = vertices[i];
            lineList[i * 2] = new VertexPositionColor(vertex.ToVector3(), color);
            lineList[i * 2 + 1] = new VertexPositionColor(vertices[(i + 1) % vertices.Length].ToVector3(), color);
        }
        return lineList;
    }
}