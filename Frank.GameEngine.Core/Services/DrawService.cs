using Frank.GameEngine.Core.Graphics.Rendering;
using Frank.GameEngine.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Services;

public class DrawService : IDrawService
{
    private readonly ILogger<DrawService> _logger;
    private readonly IRenderer _renderer;
    private readonly GameObjects _gameObjects;

    public DrawService(ILogger<DrawService> logger, IRenderer renderer, GameObjects gameObjects)
    {
        _logger = logger;
        _renderer = renderer;
        _gameObjects = gameObjects;
    }
    
    public void Draw(GameTime gameTime)
    {
        _logger.LogDebug("Drawing...");
        foreach (var gameObject in _gameObjects)
        {
            _renderer.Render(gameObject);
        }
    }
}