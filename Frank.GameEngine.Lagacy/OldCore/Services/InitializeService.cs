namespace Frank.GameEngine.Lagacy.OldCore.Services;

public class InitializeService : IInitializeService
{
    private readonly ILogger<InitializeService> _logger;

    public InitializeService(ILogger<InitializeService> logger)
    {
        _logger = logger;
    }

    public void Initialize()
    {
        _logger.LogDebug("Initializing...");
    }
}