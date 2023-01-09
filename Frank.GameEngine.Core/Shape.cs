using Frank.GameEngine.Core.Shapes;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public class Shape : IShape
{
    public Polygon3D Polygon { get; }
    public Color Color { get; }
    public float Alpha { get; }
    public bool IsWireframe { get; }

    public Shape(Polygon3D polygon, Color color, float alpha, bool isWireframe)
    {
        Polygon = polygon;
        Color = color;
        Alpha = alpha;
        IsWireframe = isWireframe;
    }
}