using Frank.GameEngine.Primitives;
using Pong.GameObjects;

namespace Pong.Scenes;

public class PongBoard : Scene
{
    public PongBoard(Camera camera) : base("Game Board", camera)
    {
    }

    public required PlayerPaddle Player { get; set; }

    public required ComputerPaddle Computer { get; set; }

    public required Ball Ball { get; set; }

    public ScoreBoard ScoreBoard { get; set; } = new();
}