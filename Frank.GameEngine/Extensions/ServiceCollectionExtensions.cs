﻿using Frank.GameEngine.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.GameEngine.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGame(this IServiceCollection services)
    {
        // services.AddSingleton<IRenderer, Renderer>();
        services.AddSingleton<IRenderQueue, RenderQueue>();
        // services.AddSingleton<IOptions<WindowOptions>>(Options.Create(new WindowOptions() {GameWindowSettings = GameWindowSettings.Default, NativeWindowSettings = NativeWindowSettings.Default}));
        services.AddSingleton<GameBase>();
        
        return services;
    }
}