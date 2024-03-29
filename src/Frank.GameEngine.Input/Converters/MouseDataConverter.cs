using System.Numerics;
using Frank.GameEngine.Input.Args;
using SharpHook.Native;

namespace Frank.GameEngine.Input.Converters;

public static class MouseDataConverter
{
    public static MouseChangeEventArgs ConvertTo(MouseEventData mouseData, DateTimeOffset eventTime)
    {
        return new MouseChangeEventArgs(
            eventTime,
            new Vector2(mouseData.X, mouseData.Y),
            0,
            mouseData.Button == MouseButton.Button1,
            mouseData.Button == MouseButton.Button2,
            mouseData.Button == MouseButton.Button3
        );
    }

    public static MouseChangeEventArgs ConvertTo(MouseWheelEventData mouseData, DateTimeOffset eventTime)
    {
        return new MouseChangeEventArgs(eventTime, new Vector2(mouseData.X, mouseData.Y), mouseData.Rotation, false,
            false, false);
    }
}