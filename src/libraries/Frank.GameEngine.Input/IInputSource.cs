using Frank.GameEngine.Input.Args;

namespace Frank.GameEngine.Input;

/// <summary>
///     Platform input capture (keyboard and mouse). <see cref="InputManager" /> is the SharpHook-backed default.
///     Inject a custom implementation (or a test double) when constructing <c>Frank.GameEngine.Core.GameEngine</c>.
/// </summary>
public interface IInputSource
{
    void OnMouseMove(Action<MouseChangeEventArgs> callback);
    void OnMouseClick(Action<MouseChangeEventArgs> callback);
    void OnMouseScroll(Action<MouseChangeEventArgs> callback);
    void OnMouseDrag(Action<MouseChangeEventArgs> callback);
    void OnMousePressed(Action<MouseChangeEventArgs> callback);
    void OnMouseReleased(Action<MouseChangeEventArgs> callback);
    void OnKeyboardKeyPress(Action<KeyboardPressEventArgs> callback);
    void OnKeyboardKeyRelease(Action<KeyboardReleaseEventArgs> callback);
    void OnKeyboardKeyTyped(Action<KeyboardTypeEventArgs> callback);

    /// <summary>Starts listening (typically blocks until <see cref="Stop" />).</summary>
    void Start();

    /// <summary>Stops listening and releases native resources.</summary>
    void Stop();
}
