using Frank.GameEngine.Input.Converters;

namespace Frank.GameEngine.Input.Args;

public class KeyboardPressEventArgs : KeyboardEventArgs
{
    public KeyboardPressEventArgs(KeyboardKey KeyboardKey, DateTimeOffset eventTime) : base(KeyboardKey, eventTime)
    {
    }
}