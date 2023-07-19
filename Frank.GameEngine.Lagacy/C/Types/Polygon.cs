using Microsoft.Xna.Framework;
using System.Collections;

namespace Frank.GameEngine.Types;

public abstract class Polygon : IPolygon
{
    private readonly List<Vector2> _points = new();
    
    public Polygon(IEnumerable<Vector2> points)
    {
        _points.AddRange(points);
    }

    public Vector2 this[int index]
    {
        get => _points[index];
        set => _points[index] = value;
    }

    public int Count => _points.Count;

    public void Add(Vector2 point) => _points.Add(point);

    public void AddRange(IEnumerable<Vector2> points) => _points.AddRange(points);

    public void Clear() => _points.Clear();

    public bool Contains(Vector2 point) => _points.Contains(point);

    public bool Remove(Vector2 point) => _points.Remove(point);

    public IEnumerator<Vector2> GetEnumerator() => _points.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}