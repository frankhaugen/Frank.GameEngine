using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering;

public interface IRenderer
{
    void Render(Scene scene);

    void Render(Scene scene, Action<string> callback);
}