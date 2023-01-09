using Frank.GameEngine.Core.Shapes;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public interface IShape
{
    Polygon3D Polygon { get; }
    Color Color { get; }
    float Alpha { get; }
    bool IsWireframe { get; }
}