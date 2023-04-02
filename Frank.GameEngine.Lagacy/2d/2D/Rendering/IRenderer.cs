using Frank.GameEngine.Lagacy._2d._2D.GameObjects;

namespace Frank.GameEngine.Lagacy._2d._2D.Rendering;

public interface IRenderer
{
	void Dispose();
	void Render(IGameObject gameObject);
}