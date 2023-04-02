
using Frank.GameEngine.Lagacy._2d._2D.GameObjects;

namespace Frank.GameEngine.Lagacy._2d._2D.Physics;

public interface IPhysicsEngine
{
	void Update(IGameObject gameObject, TimeSpan elapsed);
}