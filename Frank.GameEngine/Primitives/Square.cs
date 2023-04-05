using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Primitives;

public class Square : Polygon
{
    public Square(Vector3 center, int size) : base(new HashSet<Vertex>
    {
        center + new Vector3(-size, -size, 0),
        center + new Vector3(size, -size, 0),
        center + new Vector3(size, size, 0),
        center + new Vector3(-size, size, 0)
    })
    {
    }
}