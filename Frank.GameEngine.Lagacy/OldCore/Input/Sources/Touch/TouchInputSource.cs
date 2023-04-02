using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Frank.GameEngine.Lagacy.OldCore.Input.Sources.Touch;

public class TouchInputSource : InputSource
{
    private readonly float _deadzone;
    private TouchCollection _touchState;

    public TouchInputSource(float deadzone = 1.0f)
    {
        _deadzone = deadzone;
    }

    public event EventHandler<TouchMoveEventArgs> TouchMoveEvent;
    public event EventHandler<TouchGestureEventArgs> TouchGestureEvent;

    public override void Update(GameTime gameTime)
    {
        _touchState = TouchPanel.GetState();

        foreach (var touch in _touchState)
        {
            if (touch.State == TouchLocationState.Moved && _touchState.FindById(touch.Id, out var previousTouch))
            {
                var deltaX = touch.Position.X - previousTouch.Position.X;
                var deltaY = touch.Position.Y - previousTouch.Position.Y;

                if (Math.Abs(deltaX) > _deadzone || Math.Abs(deltaY) > _deadzone)
                {
                    var touchMoveEventArgs = new TouchMoveEventArgs(touch.Id, touch.Position, deltaX, deltaY);
                    TouchMoveEvent?.Invoke(this, touchMoveEventArgs);
                }
            }
        }

        while (TouchPanel.IsGestureAvailable)
        {
            var gesture = TouchPanel.ReadGesture();
            var touchGestureEventArgs = new TouchGestureEventArgs(gesture);
            TouchGestureEvent?.Invoke(this, touchGestureEventArgs);
        }
    }
}