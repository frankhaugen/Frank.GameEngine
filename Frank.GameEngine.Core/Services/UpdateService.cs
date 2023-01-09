using Frank.GameEngine.Core.Input;
using Frank.GameEngine.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Services;

public class UpdateService : IUpdateService
{
    private readonly ILogger<UpdateService> _logger;
    private readonly IInputHandler _inputHandler;
    public UpdateService(ILogger<UpdateService> logger, IInputHandler inputHandler)
    {
        _logger = logger;
        _inputHandler = inputHandler;
    }

    public void Update(GameTime gameTime)
    {
        _logger.LogDebug("Updating...");
        _inputHandler.Update(gameTime);
    }
}