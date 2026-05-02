namespace Frank.GameEngine.Primitives;

public static class Scene2DExtensions
{
    /// <summary>Allocation-free: clears <paramref name="buffer" />, adds active objects, sorts by <see cref="GameObject2D.ZOrder" />.</summary>
    public static void CollectActiveSorted(this Scene2D scene, List<GameObject2D> buffer)
    {
        buffer.Clear();
        var list = scene.GameObjects;
        for (var i = 0; i < list.Count; i++)
        {
            var g = list[i];
            if (g.IsActive)
                buffer.Add(g);
        }

        buffer.Sort(static (a, b) => a.ZOrder.CompareTo(b.ZOrder));
    }

    /// <summary>Convenience LINQ view (allocates). Prefer <see cref="CollectActiveSorted" /> in render loops.</summary>
    public static IEnumerable<GameObject2D> GetActiveSorted(this Scene2D scene) =>
        scene.GameObjects.Where(g => g.IsActive).OrderBy(g => g.ZOrder);
}
