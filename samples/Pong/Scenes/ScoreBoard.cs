namespace Pong.Scenes;

public class ScoreBoard
{
    public int PlayerScore { get; private set; }
    
    public int ComputerScore { get; private set; }
    
    public void PlayerScored() => PlayerScore++;

    public void ComputerScored() => ComputerScore++;
}