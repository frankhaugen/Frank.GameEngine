using System.ComponentModel;
using Frank.GameEngine.Input.Args;
using Frank.GameEngine.Input.Converters;
using SharpHook;

namespace Frank.GameEngine.Input;

/// <summary>
///     SharpHook-backed global input. Prefer taking <see cref="IInputSource" /> in application code.
/// </summary>
public class InputManager : IInputSource
{
    /// <summary>
    ///     Created on first subscription or <see cref="Start" /> so the engine can be constructed without loading
    ///     native libuiohook (needed for headless CI and for code paths that never register input or call <see cref="Start" />).
    /// </summary>
    private IGlobalHook? _globalHook;

    private IGlobalHook Hook => _globalHook ??= new TaskPoolGlobalHook();

    // Mouse events
    public void OnMouseMove(Action<MouseChangeEventArgs> callback)
    {
        Hook.MouseMoved += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));
    }

    public void OnMouseClick(Action<MouseChangeEventArgs> callback)
    {
        Hook.MouseClicked += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));
    }

    public void OnMouseScroll(Action<MouseChangeEventArgs> callback)
    {
        Hook.MouseWheel += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));
    }

    public void OnMouseDrag(Action<MouseChangeEventArgs> callback)
    {
        Hook.MouseDragged += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));
    }

    public void OnMousePressed(Action<MouseChangeEventArgs> callback)
    {
        Hook.MousePressed += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));
    }

    public void OnMouseReleased(Action<MouseChangeEventArgs> callback)
    {
        Hook.MouseReleased +=
            (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));
    }

    // Keyboard events
    public void OnKeyboardKeyPress(Action<KeyboardPressEventArgs> callback)
    {
        Hook.KeyPressed += (sender, args) =>
            callback(new KeyboardPressEventArgs(KeyboardKeyConverter.ConvertTo(args.Data.KeyCode), args.EventTime));
    }

    public void OnKeyboardKeyRelease(Action<KeyboardReleaseEventArgs> callback)
    {
        Hook.KeyReleased += (sender, args) =>
            callback(new KeyboardReleaseEventArgs(KeyboardKeyConverter.ConvertTo(args.Data.KeyCode), args.EventTime));
    }

    public void OnKeyboardKeyTyped(Action<KeyboardTypeEventArgs> callback)
    {
        Hook.KeyTyped += (sender, args) =>
            callback(new KeyboardTypeEventArgs(KeyboardKeyConverter.ConvertTo(args.Data.KeyCode), args.EventTime));
    }

    /// <summary>
    ///     Notifies the hook to start listening for events. Not intended to be called directly.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Start()
    {
        Hook.Run();
    }

    /// <summary>
    ///     Notifies the hook to stop listening for events. Not intended to be called directly.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Stop()
    {
        _globalHook?.Dispose();
        _globalHook = null;
    }
}
