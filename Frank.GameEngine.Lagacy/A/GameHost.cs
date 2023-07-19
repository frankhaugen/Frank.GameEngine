using Frank.GameEngine.Lagacy.A.OldCore.Services;

namespace Frank.GameEngine.Lagacy.A;

public class GameHost : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<GameHost> _logger;

    public GameHost(IServiceProvider serviceProvider, ILogger<GameHost> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var game = _serviceProvider.GetRequiredService<GameService>();
        game.Exiting += (sender, args) => Exit(sender, args, stoppingToken);
        game.Initialize();
        game.Run();
    }
    
    private void Exit(object? sender, EventArgs e, CancellationToken stoppingToken)
    {
        var game = (GameService)sender;
        game.Exit();
        game.Dispose();
        StopAsync(stoppingToken).GetAwaiter().GetResult();
        _logger.LogInformation("Game stopped at: {time}", DateTime.UtcNow);
        Environment.Exit(0);
    }
}