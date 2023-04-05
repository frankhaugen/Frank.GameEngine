using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine;

public static class SphereFactory
{
    public static VertexPositionColor[] CreateSphere(float radius, Color color, int segments = 16)
    {
        var vertices = new List<VertexPositionColor>();
        var indices = new List<short>();
        var latitudeBands = segments;
        var longitudeBands = segments;
        for (var lat = 0; lat <= latitudeBands; lat++)
        {
            var theta = lat * MathF.PI / latitudeBands;
            var sinTheta = MathF.Sin(theta);
            var cosTheta = MathF.Cos(theta);

            for (var lon = 0; lon <= longitudeBands; lon++)
            {
                var phi = lon * 2 * MathF.PI / longitudeBands;
                var sinPhi = MathF.Sin(phi);
                var cosPhi = MathF.Cos(phi);

                var x = cosPhi * sinTheta;
                var y = cosTheta;
                var z = sinPhi * sinTheta;

                var vertex = new VertexPositionColor(new Vector3(x, y, z) * radius, color);
                vertices.Add(vertex);

                if (lat < latitudeBands && lon < longitudeBands)
                {
                    var first = (lat * (longitudeBands + 1)) + lon;
                    var second = first + longitudeBands + 1;

                    indices.Add((short)first);
                    indices.Add((short)second);
                    indices.Add((short)(first + 1));

                    indices.Add((short)second);
                    indices.Add((short)(second + 1));
                    indices.Add((short)(first + 1));
                }
            }
        }

        return vertices.ToArray();
    }
}