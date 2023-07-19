namespace Frank.GameEngine.Lagacy.A.OldCore.Input.Sources.Joystick;

public class JoystickButtonPressedEventArgs
{
    public int JoystickId { get; }
    public int ButtonIndex { get; }

    public JoystickButtonPressedEventArgs(int joystickId, int buttonIndex)
    {
        JoystickId = joystickId;
        ButtonIndex = buttonIndex;
    }
}