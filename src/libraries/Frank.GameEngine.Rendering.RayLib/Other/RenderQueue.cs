using System.Collections.Concurrent;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.RayLib;

public class RenderQueue
{
    private readonly ConcurrentDictionary<ulong, List<Shape>> _shapes = new();
    
    public void Add(Tick tick, Shape shape)
    {
        var list = _shapes.GetOrAdd(tick.FrameNumber, _ => new List<Shape>());
        list.Add(shape);
    }
    
    public IEnumerable<Shape> DestructiveGet(Tick tick)
        => !_shapes.Remove(tick.FrameNumber, out var value) ? Enumerable.Empty<Shape>() : value;
}