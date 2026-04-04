namespace Frank.GameEngine.Primitives;

public static class Scene2DExtensions
{
    public static IEnumerable<GameObject2D> GetActiveSorted(this Scene2D scene) =>
        scene.GameObjects.Where(g => g.IsActive).OrderBy(g => g.ZOrder);
}
