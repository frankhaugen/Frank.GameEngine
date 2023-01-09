using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Input.Sources.GamePad;

public class GamePadTriggerEventArgs : EventArgs
{
    public PlayerIndex GamePadId { get; }
    public GamePadTriggerType Type { get; }
    public float Value { get; }

    public GamePadTriggerEventArgs(PlayerIndex gamePadId, GamePadTriggerType type, float value)
    {
        GamePadId = gamePadId;
        Type = type;
        Value = value;
    }
}