using Frank.GameEngine.Types;

namespace Frank.GameEngine.Rendering;

public interface IRenderQueue
{
    void Clear();
    void CopyTo(IPolygon[] array, int index);
    void Enqueue(IPolygon item);
    IEnumerator<IPolygon> GetEnumerator();
    IPolygon[] ToArray();
    bool TryDequeue(out IPolygon result);
    bool TryPeek(out IPolygon result);
    int Count { get; }
    bool IsEmpty { get; }
}