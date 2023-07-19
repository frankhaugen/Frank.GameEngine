using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A.OldCore.Input.Sources.Touch;

/// <summary>
/// Event arguments for touch move events.
/// </summary>
public struct TouchMoveEventArgs
{
    /// <summary>
    /// The unique identifier for the touch.
    /// </summary>
    public int TouchId { get; }

    /// <summary>
    /// The current position of the touch.
    /// </summary>
    public Vector2 Position { get; }

    /// <summary>
    /// The change in position of the touch since the last update.
    /// </summary>
    public Vector2 Delta { get; }

    /// <summary>
    /// Creates a new instance of <see cref="TouchMoveEventArgs"/>.
    /// </summary>
    /// <param name="touchId">The unique identifier for the touch.</param>
    /// <param name="position">The current position of the touch.</param>
    /// <param name="deltaX">The change in the x-coordinate of the touch since the last update.</param>
    /// <param name="deltaY">The change in the y-coordinate of the touch since the last update.</param>
    public TouchMoveEventArgs(int touchId, Vector2 position, float deltaX, float deltaY)
    {
        TouchId = touchId;
        Position = position;
        Delta = new Vector2(deltaX, deltaY);
    }
}