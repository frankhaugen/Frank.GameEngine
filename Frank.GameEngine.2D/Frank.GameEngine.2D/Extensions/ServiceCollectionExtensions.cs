using MonoGame.Extended.Input.InputListeners;
using Frank.GameEngine._2D.Models.Configuration;

namespace Frank.GameEngine._2D.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddGame(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IGameWindow, GameWindow>();
            services.AddSingleton<IInputService, InputService>();
            services.AddSingleton<IDrawer, Drawer>();

            services.Configure<GameOptions>(configuration.GetSection(nameof(GameOptions)));
            services.Configure<PlayerOptions>(configuration.GetSection(nameof(PlayerOptions)));

            return services;
        }
    }
}