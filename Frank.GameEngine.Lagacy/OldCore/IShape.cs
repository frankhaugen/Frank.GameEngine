using Frank.GameEngine.Lagacy.OldCore.Shapes;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.OldCore;

public interface IShape
{
    Polygon3D Polygon { get; }
    Color Color { get; }
    float Alpha { get; }
    bool IsWireframe { get; }
}