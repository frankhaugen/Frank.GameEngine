using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Input.Sources.Mouse;

public struct MouseClickEventArgs
{
    public MouseButton Button { get; }
    public Point Position { get; }

    public MouseClickEventArgs(MouseButton button, Point position)
    {
        Button = button;
        Position = position;
    }
}