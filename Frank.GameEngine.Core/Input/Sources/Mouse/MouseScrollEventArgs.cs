namespace Frank.GameEngine.Core.Input.Sources.Mouse;

public struct MouseScrollEventArgs
{
    public int ScrollAmount { get; }

    public MouseScrollEventArgs(int scrollAmount)
    {
        ScrollAmount = scrollAmount;
    }
}