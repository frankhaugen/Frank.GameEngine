using Frank.GameEngine.Lagacy.A._2d._2D.Physics;
using Frank.GameEngine.Lagacy.A._2d._2D.Rendering;
using Frank.GameEngine.Lagacy.A._2d._2D.World;

namespace Frank.GameEngine.Lagacy.A._2d._2D.Experimental;

public class GameWorldConstructor
{
	private readonly IServiceCollection _serviceCollection;

	public GameWorldConstructor()
	{
		_serviceCollection = new ServiceCollection();
	}

	public GameWorldConstructor AddRenderer()
	{
		_serviceCollection.AddSingleton<IRenderer, Renderer>();
		return this;
	}

	public GameWorldConstructor AddPhysics()
	{
		_serviceCollection.AddSingleton<IPhysicsEngine, PhysicsEngine>();
		return this;
	}

	public GameWorldConstructor AddGameWorld()
	{
		_serviceCollection.AddSingleton<IGameWorld, GameWorld>();
		return this;
	}
}