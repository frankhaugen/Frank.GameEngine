using Frank.GameEngine.Input.Args;

namespace Frank.GameEngine.Input;

/// <summary>
///     No-op input for hosts that poll hardware themselves (e.g. Raylib <c>Input.IsKeyDown</c>) so game facades do not
///     start a global hook thread.
/// </summary>
public sealed class NullInputSource : IInputSource
{
    public void OnMouseMove(Action<MouseChangeEventArgs> callback)
    {
    }

    public void OnMouseClick(Action<MouseChangeEventArgs> callback)
    {
    }

    public void OnMouseScroll(Action<MouseChangeEventArgs> callback)
    {
    }

    public void OnMouseDrag(Action<MouseChangeEventArgs> callback)
    {
    }

    public void OnMousePressed(Action<MouseChangeEventArgs> callback)
    {
    }

    public void OnMouseReleased(Action<MouseChangeEventArgs> callback)
    {
    }

    public void OnKeyboardKeyPress(Action<KeyboardPressEventArgs> callback)
    {
    }

    public void OnKeyboardKeyRelease(Action<KeyboardReleaseEventArgs> callback)
    {
    }

    public void OnKeyboardKeyTyped(Action<KeyboardTypeEventArgs> callback)
    {
    }

    public void Start()
    {
    }

    public void Stop()
    {
    }
}
