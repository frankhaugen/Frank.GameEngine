namespace Frank.GameEngine.Lagacy.OldCore.Input.Sources.Joystick;

/// <summary>
/// Provides data for the <see cref="JoystickInputSource.JoystickMove"/> event.
/// </summary>
public class JoystickMoveEventArgs : EventArgs
{
    /// <summary>
    /// Gets the identifier of the joystick that generated the event.
    /// </summary>
    public int JoystickId { get; }

    /// <summary>
    /// Gets the identifier of the axis that was moved.
    /// </summary>
    public int AxisId { get; }

    /// <summary>
    /// Gets the amount the axis was moved.
    /// </summary>
    public float Delta { get; }

    /// <summary>
    /// Gets the value of the axis after it was moved.
    /// </summary>
    public float Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JoystickMoveEventArgs"/> class.
    /// </summary>
    /// <param name="joystickId">The identifier of the joystick that generated the event.</param>
    /// <param name="axisId">The identifier of the axis that was moved.</param>
    /// <param name="delta">The amount the axis was moved.</param>
    /// <param name="value">The value of the axis after it was moved.</param>
    public JoystickMoveEventArgs(int joystickId, int axisId, float delta, float value)
    {
        JoystickId = joystickId;
        AxisId = axisId;
        Delta = delta;
        Value = value;
    }
}