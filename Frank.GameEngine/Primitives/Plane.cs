using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Primitives;

public class Plane : Polygon
{
    public Plane(Vector3 origin, int size) : base(new HashSet<Vertex>
    {
        origin,
        origin + new Vector3(size, 0, 0),
        origin + new Vector3(size, size, 0),
        origin + new Vector3(0, size, 0)
    })
    {
    }
}