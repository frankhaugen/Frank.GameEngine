using Frank.GameEngine.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Frank.GameEngine;

public class GameHost : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public GameHost(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var game = _serviceProvider.GetRequiredService<GameService>();
        game.Exiting += (sender, args) => StopAsync(stoppingToken);
        game.Initialize();
        game.Run();
    }
}