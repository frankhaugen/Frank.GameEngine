using Frank.GameEngine.Lagacy.A._2d.Models.Configuration;
using MonoGame.Extended.Input.InputListeners;

namespace Frank.GameEngine.Lagacy.A._2d.Extensions
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