using Microsoft.Extensions.DependencyInjection;

namespace Frank.GameEngine.Core;

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
        _serviceCollection.AddSingleton<IPhysics, Physics>();
        return this;
    }

    public GameWorldConstructor AddGameWorld()
    {
        _serviceCollection.AddSingleton<IGameWorld, GameWorld>();
        return this;
    }


}