using Frank.GameEngine.Lagacy.A.OldCore.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.A.OldCore.Graphics.Management;

public interface IGraphicsManager : IInitializeService
{
    GraphicsDevice GraphicsDevice { get; }
    GraphicsDeviceManager GraphicsDeviceManager { get; }
    void Instanciate(Game game);
}