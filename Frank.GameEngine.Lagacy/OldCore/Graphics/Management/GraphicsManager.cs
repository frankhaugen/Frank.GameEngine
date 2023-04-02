using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.OldCore.Graphics.Management;

public class GraphicsManager : IGraphicsManager
{
    private readonly IOptions<GameOptions> _options;

    private bool _instanciated = false;

    public GraphicsManager(IOptions<GameOptions> options)
    {
        _options = options;
    }

    public GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;
    public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

    public void Initialize()
    {
        if (!_instanciated) return;
        
        GraphicsDeviceManager.PreferredBackBufferWidth = _options.Value.Resolution.Width;
        GraphicsDeviceManager.PreferredBackBufferHeight = _options.Value.Resolution.Height;
        GraphicsDeviceManager.IsFullScreen = _options.Value.Fullscreen;
        GraphicsDeviceManager.SynchronizeWithVerticalRetrace = _options.Value.VSync;

        GraphicsDeviceManager.ApplyChanges();
    }

    public void Instanciate(Game game)
    {
        if (_instanciated) return;
        GraphicsDeviceManager = new GraphicsDeviceManager(game);
        _instanciated = true;
    }
}