using System.Buffers;
using System.Numerics;
using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework.Graphics;
using XnaVector3 = Microsoft.Xna.Framework.Vector3;

namespace Frank.GameEngine.Rendering.MonoGame.Extensions;

public static partial class GraphicsDeviceExtensions
{
    public static void Draw(this GraphicsDevice graphicsDevice, GameObject gameObject)
    {
        var shape = gameObject.Shape;
        graphicsDevice.Draw(shape);
    }

    public static void Draw(this GraphicsDevice graphicsDevice, Shape shape)
    {
        if (shape.TriangleMesh != null)
        {
            graphicsDevice.DrawTriangleList(shape.TriangleMesh, shape.Color);
            return;
        }

        graphicsDevice.DrawTriangleList(shape.Polygon, shape.Color);
    }

    public static void DrawLineList(this GraphicsDevice graphicsDevice, Polygon polygon, Rgba32 color)
    {
        var lineList = polygon.ToVertexPositionColors(color);
        graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lineList, 0,
            lineList.Length / PrimitiveType.LineList.GetVertexCount());
    }

    public static void DrawTriangleList(this GraphicsDevice graphicsDevice, Polygon polygon, Rgba32 color)
    {
        var faces = polygon.FacesSpan;
        if (faces.Length == 0)
            return;

        var vertexCount = faces.Length * 3;
        var pool = ArrayPool<VertexPositionColor>.Shared;
        var buffer = pool.Rent(vertexCount);
        try
        {
            var xnaColor = color.ToXnaColor();
            for (var i = 0; i < faces.Length; i++)
            {
                var f = faces[i];
                var o = i * 3;
                buffer[o] = new VertexPositionColor(ToXna(f.A), xnaColor);
                buffer[o + 1] = new VertexPositionColor(ToXna(f.B), xnaColor);
                buffer[o + 2] = new VertexPositionColor(ToXna(f.C), xnaColor);
            }

            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, buffer, 0, faces.Length);
        }
        finally
        {
            pool.Return(buffer);
        }
    }

    public static void DrawTriangleList(this GraphicsDevice graphicsDevice, TriangleMesh mesh, Rgba32 color)
    {
        var triCount = mesh.TriangleCount;
        if (triCount == 0)
            return;

        var vertexCount = triCount * 3;
        var pool = ArrayPool<VertexPositionColor>.Shared;
        var buffer = pool.Rent(vertexCount);
        try
        {
            var xnaColor = color.ToXnaColor();
            var verts = mesh.Vertices;
            var indices = mesh.Indices;
            for (var t = 0; t < triCount; t++)
            {
                var baseIdx = t * 3;
                var i0 = indices[baseIdx];
                var i1 = indices[baseIdx + 1];
                var i2 = indices[baseIdx + 2];
                var o = baseIdx;
                buffer[o] = new VertexPositionColor(ToXna(verts[i0]), xnaColor);
                buffer[o + 1] = new VertexPositionColor(ToXna(verts[i1]), xnaColor);
                buffer[o + 2] = new VertexPositionColor(ToXna(verts[i2]), xnaColor);
            }

            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, buffer, 0, triCount);
        }
        finally
        {
            pool.Return(buffer);
        }
    }

    private static XnaVector3 ToXna(Vector3 v) => new(v.X, v.Y, v.Z);
}
