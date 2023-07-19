using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Extensions;

public static class PolygonExtensions
{
    public static VertexPositionColor[] ToVertexPositionColors(this IPolygon polygon, Color color) => polygon.Select(vertex => new VertexPositionColor(vertex.ToVector3(), color)).ToArray();
    
    public static VertexPositionColor[] ToLineList(this IPolygon polygon, Color color) => polygon.ToVertexPositionColors(color);
}