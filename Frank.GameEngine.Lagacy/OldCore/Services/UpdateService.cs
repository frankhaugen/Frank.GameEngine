using Frank.GameEngine.Lagacy.OldCore.Input;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.OldCore.Services;

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