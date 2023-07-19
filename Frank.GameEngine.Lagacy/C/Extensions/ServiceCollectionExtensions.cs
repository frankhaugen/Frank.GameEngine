using Frank.GameEngine.Collections;
using Frank.GameEngine.Rendering;

namespace Frank.GameEngine.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGame(this IServiceCollection services)
    {
        // services.AddSingleton<IRenderer, Renderer>();
        services.AddSingleton<IRenderQueue, RenderQueue>();
        services.AddSingleton<GameObjects>();
        // services.AddSingleton<IOptions<WindowOptions>>(Options.Create(new WindowOptions() {GameWindowSettings = GameWindowSettings.Default, NativeWindowSettings = NativeWindowSettings.Default}));
        services.AddSingleton<GameBase>();
        
        return services;
    }
}