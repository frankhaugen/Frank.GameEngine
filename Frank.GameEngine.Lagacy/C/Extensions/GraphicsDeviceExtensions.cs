using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Extensions;

public static class GraphicsDeviceExtensions
{
    public static void DrawLine(this GraphicsDevice graphicsDevice, Vector2 v1, Vector2 v2, Color color)
    {
        var vertices = new[] { v1, v2 };
        var lineList = vertices.ToLineList(color);
        graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lineList, 0, lineList.Length / 2);
    }
    
    public static void DrawPolygon(this GraphicsDevice graphicsDevice, IPolygon polygon, Color color, PrimitiveType primitiveType)
    {
        if (primitiveType == PrimitiveType.LineList)
        {
            var lineList = polygon.ToLineList(color);
            graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, lineList, 0, lineList.Length / primitiveType.GetVertexCount());
        }
        else
        {
            var vertexArray = polygon.ToVertexPositionColors(color);
            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexArray, 0, vertexArray.Length / primitiveType.GetVertexCount());
        }
    }
}