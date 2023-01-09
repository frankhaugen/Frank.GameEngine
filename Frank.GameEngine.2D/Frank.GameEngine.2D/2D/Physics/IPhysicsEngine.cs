using Frank.GameEngine.Core._2D.GameObjects;

namespace Frank.GameEngine.Core._2D.Physics;

public interface IPhysicsEngine
{
	void Update(IGameObject gameObject, TimeSpan elapsed);
}