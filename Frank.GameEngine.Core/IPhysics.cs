namespace Frank.GameEngine.Core;

public interface IPhysics
{
    void Update(IGameObject gameObject, TimeSpan elapsed);
}