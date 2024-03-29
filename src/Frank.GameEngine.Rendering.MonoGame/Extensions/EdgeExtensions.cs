using Frank.GameEngine.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = System.Drawing.Color;

namespace Frank.GameEngine.Rendering.MonoGame.Extensions;

public static class EdgeExtensions
{
    public static VertexPositionColor[] ToVertexPositionColors(this IEnumerable<Edge> edges, Color color)
    {
        var edgesArray = edges.ToArray();
        var lineList = new VertexPositionColor[edgesArray.Length * 2];
        for (var i = 0; i < edgesArray.Length; i++)
        {
            var edge = edgesArray[i];
            lineList[i * 2] = new VertexPositionColor(new Vector3(edge.A.X, edge.A.Y, edge.A.Z), color.ToColor());
            lineList[i * 2 + 1] = new VertexPositionColor(new Vector3(edge.B.X, edge.B.Y, edge.B.Z), color.ToColor());
        }

        return lineList;
    }
}