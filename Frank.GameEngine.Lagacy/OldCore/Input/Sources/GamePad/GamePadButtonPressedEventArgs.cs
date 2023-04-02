using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frank.GameEngine.Lagacy.OldCore.Input.Sources.GamePad;

public class GamePadButtonPressedEventArgs
{
    public PlayerIndex GamePadId { get; }
    public Buttons Button { get; }
    public GamePadState CurrentState { get; }

    public GamePadButtonPressedEventArgs(PlayerIndex gamePadId, Buttons button, GamePadState currentState)
    {
        GamePadId = gamePadId;
        Button = button;
        CurrentState = currentState;
    }
}