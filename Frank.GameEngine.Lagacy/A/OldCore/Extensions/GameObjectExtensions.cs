using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.A.OldCore.Extensions;

public static class GameObjectExtensions
{
    public static IndexBuffer GetIndexBuffer(this IGameObject gameObject, GraphicsDevice graphicsDevice)
    {
        var indices = gameObject.Shape.Polygon.ToIndicies();
        var indexBuffer = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, indices.Length, BufferUsage.None);
        indexBuffer.SetData(indices);
        return indexBuffer;
    }
    
    public static VertexBuffer GetVertexBuffer(this IGameObject gameObject, GraphicsDevice graphicsDevice)
    {
        var vertices = gameObject.Shape.Polygon.Vertices.Select(v => new VertexPositionColor(v, gameObject.Shape.Color)).ToArray();
        var vertexBuffer = new VertexBuffer(graphicsDevice, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.None);
        vertexBuffer.SetData(vertices);
        return vertexBuffer;
    }
    
    public static VertexPositionColor[] GetVertexPositionColor(this IGameObject gameObject)
    {
        var vertices = gameObject.Shape.Polygon.Vertices;
        var position = gameObject.Transform.Position;
        var color = gameObject.Shape.Color;

        return vertices.Select(vertex => new VertexPositionColor(vertex + position, color)).ToArray();
    }
}