namespace Frank.GameEngine.Input.Args;

public class KeyboardReleaseEventArgs : KeyboardEventArgs
{
    public KeyboardReleaseEventArgs(KeyboardKey KeyboardKey, DateTimeOffset eventTime) : base(KeyboardKey, eventTime)
    {
    }
}