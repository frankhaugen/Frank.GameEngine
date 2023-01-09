using Frank.GameEngine.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Frank.GameEngine.Core.Services;

public class InitializeService : IInitialize
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