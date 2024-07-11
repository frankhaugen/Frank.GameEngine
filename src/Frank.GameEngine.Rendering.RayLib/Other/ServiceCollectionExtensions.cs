using Microsoft.Extensions.DependencyInjection;

namespace Frank.GameEngine.Rendering.RayLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameEngine(this IServiceCollection services)
    {
        services.AddTickProducer();
        services.AddPhysicsEngine();
        services.AddRenderLoop();
        services.AddChannelFactory(builder => builder
            .AddChannel<Tick>()
            .AddChannel<PhysicsEngineSignoff>());
        return services;
    }
 
    public static IServiceCollection AddTickProducer(this IServiceCollection services)
    {
        services.AddHostedService<TickProducer>();
        return services;
    }
    
    public static IServiceCollection AddPhysicsEngine(this IServiceCollection services)
    {
        services.AddHostedService<PhysicsEngine>();
        return services;
    }
    
    public static IServiceCollection AddRenderLoop(this IServiceCollection services)
    {
        // services.AddSingleton<IWindow, Window>();
        services.AddSingleton<RenderQueue>();
        services.AddHostedService<RenderLoop>();
        return services;
    }
    
    public static IServiceCollection AddChannelFactory(this IServiceCollection services, Action<ChannelFactoryBuilder> configure)
    {
        services.AddMemoryCache();
        services.AddSingleton<ChannelFactory>();
        ChannelFactoryBuilder builder = new(services);
        configure(builder);
        return services;
    }
}