namespace Frank.GameEngine.App;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly GameBase _game;

    public Worker(ILogger<Worker> logger, GameBase game)
    {
        _logger = logger;
        _game = game;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _game.Run();
    }
}
