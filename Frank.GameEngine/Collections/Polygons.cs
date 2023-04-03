using Frank.GameEngine.Types;
using System.Collections;

namespace Frank.GameEngine.Collections;

public class Polygons : IEnumerable<Polygon>
{
    private readonly List<Polygon> _polygons = new();

    public void Add(Polygon polygon)
    {
        _polygons.Add(polygon);
    }

    public IEnumerator<Polygon> GetEnumerator()
    {
        return _polygons.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}