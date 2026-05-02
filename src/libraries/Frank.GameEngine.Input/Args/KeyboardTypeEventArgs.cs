namespace Frank.GameEngine.Input.Args;

public class KeyboardTypeEventArgs : KeyboardEventArgs
{
    public KeyboardTypeEventArgs(KeyboardKey KeyboardKey, DateTimeOffset eventTime) : base(KeyboardKey, eventTime)
    {
    }
}