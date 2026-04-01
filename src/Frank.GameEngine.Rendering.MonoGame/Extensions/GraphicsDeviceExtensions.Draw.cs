using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework.Graphics;

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
        var polygon = shape.Polygon;
        var color = shape.Color;
        graphicsDevice.DrawTriangleList(polygon, color);
    }

    public static void DrawLineList(this GraphicsDevice graphicsDevice, Polygon polygon, Rgba32 color)
    {
        var lineList = polygon.ToVertexPositionColors(color);
        graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lineList, 0,
            lineList.Length / PrimitiveType.LineList.GetVertexCount());
    }

    public static void DrawTriangleList(this GraphicsDevice graphicsDevice, Polygon polygon, Rgba32 color)
    {
        var faceList = FaceFactory.Create(polygon);
        var xna = color.ToXnaColor();
        var triangleList = faceList.Select(x => new VertexPositionColor(x.GetNormal(), xna)).ToArray();
        graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList, 0,
            triangleList.Length / PrimitiveType.TriangleList.GetVertexCount());
    }
}
