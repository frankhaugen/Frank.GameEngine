using Frank.GameEngine.Types;
using System.Collections.Concurrent;

namespace Frank.GameEngine.Rendering;

public class RenderQueue : ConcurrentQueue<IPolygon>, IRenderQueue
{
}