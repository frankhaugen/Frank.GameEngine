using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Shapes;

public class Cube : IShape
{
    public Polygon3D Polygon { get; }
    public Color Color { get; }
    public float Alpha { get; }
    public bool IsWireframe { get; }
    public bool IsVisible { get; }

    public Cube(float size, Color color)
    {
        Color = color;
        // Generate vertices for cube
        var vertices = new[]
        {
            new Vector3(-size / 2,  size / 2, -size / 2),
            new Vector3( size / 2,  size / 2, -size / 2),
            new Vector3(-size / 2, -size / 2, -size / 2),
            new Vector3(-size / 2, -size / 2,  size / 2),
            new Vector3(-size / 2,  size / 2,  size / 2),
            new Vector3( size / 2, -size / 2, -size / 2),
            new Vector3( size / 2,  size / 2,  size / 2),
            new Vector3( size / 2, -size / 2,  size / 2)
        };

        // Create polygon from vertices
        Polygon = new Polygon3D(vertices);
    }
}