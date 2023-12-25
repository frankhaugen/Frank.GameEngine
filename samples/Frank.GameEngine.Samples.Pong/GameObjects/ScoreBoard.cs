namespace Pong.GameObjects;

public class ScoreBoard
{
    public int PlayerScore { get; private set; }

    public int ComputerScore { get; private set; }

    public void PlayerScored()
    {
        PlayerScore++;
    }

    public void ComputerScored()
    {
        ComputerScore++;
    }

    public override string ToString()
    {
        return $"Player: {PlayerScore:000}\nComputer: {ComputerScore:000}";
    }
}