using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frank.GameEngine.Core.Input.Sources.Mouse;

public class MouseInputSource : InputSource
{
    private MouseState _mouseState;
    private MouseState _previousMouseState;
    private float _deadzone;

    public MouseInputSource(float deadzone = 1.0f)
    {
        _deadzone = deadzone;
    }

    public override void Update(GameTime gameTime)
    {
        _previousMouseState = _mouseState;
        _mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

        if (_mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
        {
            RaiseMouseClickEvent(MouseButton.Left);
        }
        if (_mouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released)
        {
            RaiseMouseClickEvent(MouseButton.Right);
        }
        if (_mouseState.MiddleButton == ButtonState.Pressed && _previousMouseState.MiddleButton == ButtonState.Released)
        {
            RaiseMouseClickEvent(MouseButton.Middle);
        }

        if (_mouseState.ScrollWheelValue != _previousMouseState.ScrollWheelValue)
        {
            MouseScrollEvent?.Invoke(this, new MouseScrollEventArgs(_mouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue));
        }

        var distance = Vector2.Distance(_mouseState.Position.ToVector2(), _previousMouseState.Position.ToVector2());
        if (distance > _deadzone)
        {
            var velocity = (_mouseState.Position.ToVector2() - _previousMouseState.Position.ToVector2()) / (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseMoveEvent?.Invoke(this, new MouseMoveEventArgs(_mouseState.Position - _previousMouseState.Position, _mouseState.Position, velocity));
        }
    }

    public event EventHandler<MouseClickEventArgs>? MouseClickEvent;
    public event EventHandler<MouseScrollEventArgs>? MouseScrollEvent;
    public event EventHandler<MouseMoveEventArgs>? MouseMoveEvent;

    private void RaiseMouseClickEvent(MouseButton button)
    {
        MouseClickEvent?.Invoke(this, new MouseClickEventArgs(button, _mouseState.Position));
    }
}