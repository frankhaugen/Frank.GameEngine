using Frank.GameEngine.Types;
using System.Collections;

namespace Frank.GameEngine.Collections;

public class Polygons : IEnumerable<IPolygon>
{
    private readonly List<IPolygon> _polygons = new();

    public void Add(IPolygon polygon)
    {
        _polygons.Add(polygon);
    }

    public IEnumerator<IPolygon> GetEnumerator()
    {
        return _polygons.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}