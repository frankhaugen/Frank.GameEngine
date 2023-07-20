using Frank.GameEngine.Primitives;
using Pong.GameObjects;

namespace Pong.Scenes;

public class PongBoard : Scene
{
    public PongBoard(Camera camera) : base("Game Board", camera)
    {
        
    }

    public PlayerPaddle Player { get; }
    
    public ComputerPaddle Computer { get; }
    
    public Ball Ball { get; }
    
    public ScoreBoard ScoreBoard { get; }
}