using Frank.GameEngine.Rendering;
using Frank.GameEngine.Types;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine;

public class GameBase : Game
{
    private readonly IOptions<WindowOptions> _options;
    private readonly IRenderer _renderer;
    private readonly IRenderQueue _renderQueue;

    private readonly List<IPolygon> _polygons;

    public GameBase(IOptions<WindowOptions> options, IRenderer renderer, IRenderQueue renderQueue)
    {
        _options = options;
        _renderer = renderer;
        _renderQueue = renderQueue;
        _polygons = new List<IPolygon>();
    }
    
    protected override void Initialize()
    {
        _renderer.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }
    
    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        _renderer.Render();
        base.Draw(gameTime);
    }
    
}

