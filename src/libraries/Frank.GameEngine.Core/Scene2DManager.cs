using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Core;

public sealed class Scene2DManager
{
    public List<Scene2D> MenuScenes { get; } = new();

    public List<Scene2D> GameScenes { get; } = new();

    public Scene2D? CurrentScene2D { get; private set; }

    public void SelectScene(Guid id)
    {
        CurrentScene2D = MenuScenes.FirstOrDefault(x => x.Id == id)
                         ?? GameScenes.FirstOrDefault(x => x.Id == id);
    }
}
