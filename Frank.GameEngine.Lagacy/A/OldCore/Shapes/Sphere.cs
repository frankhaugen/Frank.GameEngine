using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A.OldCore.Shapes;

public class Sphere : IShape
{
    public Polygon3D Polygon { get; }
    public Color Color { get; }
    public float Alpha { get; }
    public bool IsWireframe { get; }

    public Sphere(float size, int segments)
    {
        // Generate vertices for sphere
        var vertices = new Vector3[segments * segments];
        for (int i = 0; i < segments; i++)
        {
            for (int j = 0; j < segments; j++)
            {
                var theta = (float)Math.PI * i / (segments - 1);
                var phi = (float)Math.PI * 2 * j / segments;
                vertices[i * segments + j] = new Vector3(
                    (float)(size * Math.Sin(theta) * Math.Cos(phi)),
                    (float)(size * Math.Cos(theta)),
                    (float)(size * Math.Sin(theta) * Math.Sin(phi))
                );
            }
        }

        // Create polygon from vertices
        Polygon = new Polygon3D(vertices);
        Color = Color.White;
        Alpha = 1f;
        IsWireframe = false;
    }
}