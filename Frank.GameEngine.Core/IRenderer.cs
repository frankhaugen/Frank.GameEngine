namespace Frank.GameEngine.Core;

public interface IRenderer
{
    void Dispose();
    void Render(IGameObject gameObject);
}