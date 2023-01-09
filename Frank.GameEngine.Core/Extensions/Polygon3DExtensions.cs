using Frank.GameEngine.Core.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Core.Extensions;

public static class Polygon2DExtensions
{
    
    public static Polygon2D Translate(this Polygon2D polygon, Vector2 position)
    {
        var translatedVertices = polygon.Vertices.Select(vertex => vertex + position).ToArray();
        return new Polygon2D(translatedVertices);
    }
}


public static class Polygon3DExtensions
{
    public static VertexBuffer ToVertexBuffer(this Polygon3D polygon, GraphicsDevice graphicsDevice, Color color)
    {
        var vertices = polygon.Vertices.Select(v => new VertexPositionColor(v, color)).ToArray();
        var vertexBuffer = new VertexBuffer(graphicsDevice, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
        vertexBuffer.SetData(vertices);
        return vertexBuffer;
    }

    public static int[] ToIndicies(this Polygon3D polygon) => Enumerable.Range(0, polygon.Vertices.Length).ToArray();
    public static IndexBuffer ToIndexBuffer(this Polygon3D polygon, GraphicsDevice graphicsDevice)
    {
        var indices = Enumerable.Range(0, polygon.Vertices.Length).Select(i => (short)i).ToArray();
        var indexBuffer = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
        indexBuffer.SetData(indices);
        return indexBuffer;
    }
    
    public static VertexPositionColor[] ToVertexPositionColor(this Polygon3D polygon, Vector3 position, Color color) =>
        polygon.Vertices.Select(vertex => new VertexPositionColor(vertex + position, color)).ToArray();

    public static VertexPositionColor ToVertexPositionColor(this Vector3 vertex, Color color) => new VertexPositionColor(vertex, color);
    
    public static Polygon2D ToPolygon2D(this Polygon3D polygon) => new Polygon2D(polygon.Vertices.Select(v => new Vector2(v.X, v.Y)).ToArray());
}