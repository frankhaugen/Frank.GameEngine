using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace Frank.GameEngine.Input.Args;

public readonly struct MouseChangeEventArgs
{
    public MouseChangeEventArgs(DateTimeOffset eventTime, Vector2 position, float wheelDelta, bool leftButton, bool rightButton, bool middleButton)
    {
        EventTime = eventTime;
        Position = position;
        WheelDelta = wheelDelta;
        LeftButton = leftButton;
        RightButton = rightButton;
        MiddleButton = middleButton;
    }
    
    public DateTimeOffset EventTime { get; }
    
    public Vector2 Position { get; }
    
    public float WheelDelta { get; }
    
    public bool LeftButton { get; }
    
    public bool RightButton { get; }
    
    public bool MiddleButton { get; }
}