using Frank.GameEngine.Primitives;
using Pong.GameObjects;

namespace Pong.Scenes;

public class PongBoard : Scene
{
    public PongBoard(Camera camera) : base("Game Board", camera)
    {
    }

    public PlayerPaddle Player { get; set; }

    public ComputerPaddle Computer { get; set; }

    public Ball Ball { get; set; }

    public ScoreBoard ScoreBoard { get; set; } = new();
}