using Frank.GameEngine.Rendering.Console;

namespace Frank.GameEngine.Tests.Application;

public class ConsoleRendererWorker : BackgroundService
{
    private readonly ILogger<ConsoleRendererWorker> _logger;

    public ConsoleRendererWorker(ILogger<ConsoleRendererWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var renderer = new ConsoleRenderer(256, 4);
        var engine = new Core.GameEngine();
        
        
    }
}