using Frank.GameEngine.Core.GameObjects;

namespace Frank.GameEngine.Core.Physics;

public interface IPhysicsEngine
{
	void Update(IGameObject gameObject, TimeSpan elapsed);
}