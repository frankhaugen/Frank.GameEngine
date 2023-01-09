using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Frank.GameEngine.Core.Input.Sources.Touch;

/// <summary>
/// Event arguments for touch gesture events.
/// </summary>
public struct TouchGestureEventArgs
{
    /// <summary>
    /// The type of gesture that was performed.
    /// </summary>
    public GestureType GestureType { get; }

    /// <summary>
    /// The position of the gesture in screen coordinates.
    /// </summary>
    public Vector2 Position { get; }

    /// <summary>
    /// The distance that the gesture moved, if applicable.
    /// </summary>
    public Vector2 Delta { get; }

    public TouchGestureEventArgs(GestureSample gesture)
    {
        GestureType = gesture.GestureType;
        Position = gesture.Position;
        Delta = gesture.Delta;
    }
}