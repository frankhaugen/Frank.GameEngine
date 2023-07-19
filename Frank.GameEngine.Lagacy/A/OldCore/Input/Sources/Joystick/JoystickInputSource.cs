using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frank.GameEngine.Lagacy.A.OldCore.Input.Sources.Joystick;

public class JoystickInputSource : InputSource
{
    private readonly float _deadzone;
    private readonly Dictionary<int, (JoystickState, JoystickState)> _joystickStates;
    public JoystickInputSource(float deadzone = 0.2f, int maxJoysticks = 4)
    {
        _deadzone = deadzone;
        _joystickStates = new Dictionary<int, (JoystickState, JoystickState)>();

        for (int i = 0; i < maxJoysticks; i++)
        {
            _joystickStates[i] = (new JoystickState(), new JoystickState());
        }
    }

    public event EventHandler<JoystickButtonPressedEventArgs> JoystickButtonPressed;
    public event EventHandler<JoystickMoveEventArgs> JoystickMove;

    public override void Update(GameTime gameTime)
    {
        for (var i = 0; i < _joystickStates.Count; i++)
        {
            var joystickState = _joystickStates[i];
            joystickState.Item1 = joystickState.Item2;
            joystickState.Item2 = Microsoft.Xna.Framework.Input.Joystick.GetState(i);

            UpdateJoystickButtons(i, joystickState.Item1, joystickState.Item2);
            UpdateJoystickMove(i, joystickState.Item1, joystickState.Item2);

        }
    }

    private void UpdateJoystickButtons(int joystickId, JoystickState previousState, JoystickState currentState)
    {
        for (int i = 0; i < currentState.Buttons.Count(); i++)
        {
            if (currentState.Buttons[i] == ButtonState.Pressed && previousState.Buttons[i] == ButtonState.Released)
            {
                JoystickButtonPressed?.Invoke(this, new JoystickButtonPressedEventArgs(joystickId, i));
            }
        }
    }

    private void UpdateJoystickMove(int joystickId, JoystickState previousState, JoystickState currentState)
    {
        for (int i = 0; i < currentState.Axes.Count(); i++)
        {
            var axisDelta = currentState.Axes[i] - previousState.Axes[i];

            if (Math.Abs(axisDelta) > _deadzone)
            {
                JoystickMove?.Invoke(this, new JoystickMoveEventArgs(joystickId, i, axisDelta, currentState.Axes[i]));
            }
        }
    }


}