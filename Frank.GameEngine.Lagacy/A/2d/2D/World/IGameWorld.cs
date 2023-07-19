using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A._2d._2D.World;

public interface IGameWorld
{
	void AddGameObject(IGameObject gameObject);
	void Update(GameTime gameTime);
	void Render(GameTime gameTime);
	void Dispose();
}