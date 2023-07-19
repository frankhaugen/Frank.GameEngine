using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Types;

public interface IPolygon : IEnumerable<Vector2>
{
    Vector2 this[int index] { get; set; }
    int Count { get; }
    void Add(Vector2 point);
    void AddRange(IEnumerable<Vector2> points);
    void Clear();
    bool Contains(Vector2 point);
    bool Remove(Vector2 point);
}