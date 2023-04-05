using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Primitives;

public class Cylinder : Polygon
{
    private static readonly HashSet<Vertex> _vertices = new();

    public Cylinder(Vector3 origin, int radius, int height, int segments) : base(_vertices)
    {
        for (var j = 0; j < height; j++)
        {
            var y = origin.Y + j;
            for (var i = 0; i < segments; i++)
            {
                var theta = MathHelper.TwoPi * i / segments;
                var x = origin.X + (float)(radius * Math.Cos(theta));
                var z = origin.Z + (float)(radius * Math.Sin(theta));
                _vertices.Add(new Vertex(x, y, z));
            }
        }
    }
}