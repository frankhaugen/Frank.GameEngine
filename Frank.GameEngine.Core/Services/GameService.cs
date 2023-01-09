using Frank.GameEngine.Core.Graphics.Management;
using Frank.GameEngine.Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Services;

public class GameService : Game
{
    private readonly IGraphicsManager _graphicsManager;
    private readonly IUpdateService _updateService;
    private readonly IDrawService _drawService;
    private readonly IOptions<GameOptions> _options;
    private bool _ready;
    
    public GameService(IGraphicsManager graphicsManager, IUpdateService updateService, IDrawService drawService, IOptions<GameOptions> options)
    {
        _graphicsManager = graphicsManager;
        _updateService = updateService;
        _drawService = drawService;
        _options = options;
    }

    public new void Initialize()
    {
        _graphicsManager.Instanciate(this);
        IsMouseVisible = _options.Value.ShowMouse;
        
        _graphicsManager.Initialize();
        _ready = true;

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (!_ready) return;
        _updateService.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (!_ready) return;
        _drawService.Draw(gameTime);
        base.Draw(gameTime);
    }
}