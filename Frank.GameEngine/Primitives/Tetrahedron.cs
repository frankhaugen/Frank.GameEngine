using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Primitives;

public class Tetrahedron : Polygon
{
    public Tetrahedron(Vector3 origin, int size) : base(new HashSet<Vertex>
    {
        origin,
        origin + new Vector3(size, 0, 0),
        origin + new Vector3(size, size, 0),
        origin + new Vector3(0, size, 0),
        origin + new Vector3(0, 0, size),
        origin + new Vector3(size, 0, size),
        origin + new Vector3(size, size, size),
        origin + new Vector3(0, size, size)
    })
    {
    }
}