using Frank.GameEngine.Core._2D.GameObjects;

namespace Frank.GameEngine.Core._2D.Rendering;

public interface IRenderer
{
	void Dispose();
	void Render(IGameObject gameObject);
}