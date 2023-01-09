using Microsoft.Xna.Framework;

namespace Frank.GameEngine._2D;

public interface IGameWindow : IDisposable
{
    Vector2 Center { get; }
    Game Game { get; }

    // include all the Properties/Methods that you'd want to use on your Game class below.
    Microsoft.Xna.Framework.GameWindow Window { get; }

    event EventHandler<EventArgs> Exiting;

    void Run();
    void Exit();


    //void Initialize();
    //void LoadContent();
    //void UnloadContent();
    //void Update(GameTime gameTime);
    //void Draw(GameTime gameTime);
}