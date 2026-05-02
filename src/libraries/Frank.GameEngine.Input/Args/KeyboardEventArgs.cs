namespace Frank.GameEngine.Input.Args;

public abstract class KeyboardEventArgs
{
    protected KeyboardEventArgs(KeyboardKey KeyboardKey, DateTimeOffset eventTime)
    {
        this.KeyboardKey = KeyboardKey;
        EventTime = eventTime;
    }

    public KeyboardKey KeyboardKey { get; }

    public DateTimeOffset EventTime { get; }

    public override string ToString()
    {
        return $"{KeyboardKey} pressed at {EventTime}";
    }
}