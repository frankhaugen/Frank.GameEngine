using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.OldCore.Input.Sources.Mouse;

public struct MouseMoveEventArgs
{
    public Point Direction { get; }
    public Point Position { get; }
    public Vector2 Velocity { get; }

    public MouseMoveEventArgs(Point direction, Point position, Vector2 velocity)
    {
        Direction = direction;
        Position = position;
        Velocity = velocity;
    }
}