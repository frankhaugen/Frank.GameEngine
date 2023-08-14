using System.Drawing;
using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Rendering.MonoGame.Extensions;

public static class ShapeExtensions
{
    public static VertexPositionColor[] ToVertexPositionColors(this Shape shape, Color color)
    {
        var polygon = shape.Polygon;
        var edges = polygon.Edges;
        var lines = edges.ToVertexPositionColors(color);
        return lines;
    }
}