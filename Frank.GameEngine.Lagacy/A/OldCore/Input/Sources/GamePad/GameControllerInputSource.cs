using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frank.GameEngine.Lagacy.A.OldCore.Input.Sources.GamePad;

public class GameControllerInputSource : InputSource
{
    private readonly float _deadzone;
    private readonly Dictionary<PlayerIndex, (GamePadState, GamePadState)> _gamePadStates;

    public GameControllerInputSource(float deadzone = 0.2f, int maxGamePads = 4)
    {
        _deadzone = deadzone;
        _gamePadStates = new Dictionary<PlayerIndex, (GamePadState, GamePadState)>();

        for (int i = 0; i < maxGamePads; i++)
        {
            _gamePadStates[(PlayerIndex)i] = (new GamePadState(), new GamePadState());
        }
    }

    public event EventHandler<GamePadButtonPressedEventArgs> GamePadButtonPressed;
    public event EventHandler<GamePadMoveEventArgs> GamePadMove;
    public event EventHandler<GamePadTriggerEventArgs> GamePadTrigger;

    public override void Update(GameTime gameTime)
    {
        foreach (var gamePadState in _gamePadStates)
        {
            var (previousState, currentState) = gamePadState.Value;
            previousState = currentState;
            currentState = Microsoft.Xna.Framework.Input.GamePad.GetState(gamePadState.Key);

            UpdateGamePadButtons(gamePadState.Key, previousState, currentState);
            UpdateGamePadMove(gamePadState.Key, previousState, currentState);
            UpdateGamePadTriggers(gamePadState.Key, previousState, currentState);
        }
    }

    private void UpdateGamePadButtons(PlayerIndex gamePadId, GamePadState previousState, GamePadState currentState)
    {
        foreach (var button in Enum.GetValues<Buttons>())
        {
            if (currentState.IsButtonDown(button) && !previousState.IsButtonDown(button))
            {
                GamePadButtonPressed?.Invoke(this, new GamePadButtonPressedEventArgs(gamePadId, button, currentState));
            }
        }
    }

    private void UpdateGamePadMove(PlayerIndex gamePadId, GamePadState previousState, GamePadState currentState)
    {
        var leftStickDelta = previousState.ThumbSticks.Left - currentState.ThumbSticks.Left;
        var rightStickDelta = previousState.ThumbSticks.Right - currentState.ThumbSticks.Right;
        if (leftStickDelta.Length() > _deadzone)
        {
            GamePadMove?.Invoke(this, new GamePadMoveEventArgs(gamePadId, currentState));
        }
        if (rightStickDelta.Length() > _deadzone)
        {
            GamePadMove?.Invoke(this, new GamePadMoveEventArgs(gamePadId, currentState));
        }
    }

    private void UpdateGamePadTriggers(PlayerIndex gamePadId, GamePadState previousState, GamePadState currentState)
    {
        if (Math.Abs(currentState.Triggers.Left - previousState.Triggers.Left) > _deadzone)
        {
            GamePadTrigger?.Invoke(this, new GamePadTriggerEventArgs(gamePadId, GamePadTriggerType.LeftTrigger, currentState.Triggers.Left));
        }

        if (Math.Abs(currentState.Triggers.Right - previousState.Triggers.Right) > _deadzone)
        {
            GamePadTrigger?.Invoke(this, new GamePadTriggerEventArgs(gamePadId, GamePadTriggerType.RightTrigger, currentState.Triggers.Right));
        }
    }

}