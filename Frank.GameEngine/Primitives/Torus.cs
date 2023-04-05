using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Primitives;

public class Torus : Polygon
{
    private static readonly HashSet<Vertex> _vertices = new();

    public Torus(Vector3 origin, int radius, int thickness, int segments) : base(_vertices)
    {
        for (var i = 0; i < segments; i++)
        {
            var theta = MathHelper.TwoPi * i / segments;
            for (var j = 0; j < segments; j++)
            {
                var phi = MathHelper.Pi * j / segments;
                var x = origin.X + (float)(radius * Math.Sin(phi) * Math.Cos(theta));
                var y = origin.Y + (float)(radius * Math.Sin(phi) * Math.Sin(theta));
                var z = origin.Z + (float)(radius * Math.Cos(phi));
                _vertices.Add(new Vertex(x, y, z));
            }
        }
    }
}