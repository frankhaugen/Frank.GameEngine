using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frank.GameEngine.Lagacy.OldCore.Input.Sources.GamePad;

public class GamePadMoveEventArgs
{
    public PlayerIndex GamePadId { get; }
    public GamePadState CurrentState { get; }

    public GamePadMoveEventArgs(PlayerIndex gamePadId, GamePadState currentState)
    {
        GamePadId = gamePadId;
        CurrentState = currentState;
    }
}