using Frank.GameEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Core.Graphics.Management;

public interface IGraphicsManager : IInitialize
{
    GraphicsDevice GraphicsDevice { get; }
    GraphicsDeviceManager GraphicsDeviceManager { get; }
    void Instanciate(Game game);
}