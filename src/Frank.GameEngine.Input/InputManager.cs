using Frank.GameEngine.Input.Args;
using Frank.GameEngine.Input.Converters;
using SharpHook;
using SharpHook.Native;
using System.ComponentModel;

namespace Frank.GameEngine.Input;

/// <summary>
/// Wrapper for the input hook.
/// </summary>
public class InputManager
{
    private readonly IGlobalHook _globalHook = new TaskPoolGlobalHook();

    // Mouse events
    public void OnMouseMove(Action<MouseChangeEventArgs> callback) => _globalHook.MouseMoved += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));

    public void OnMouseClick(Action<MouseChangeEventArgs> callback) => _globalHook.MouseClicked += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));

    public void OnMouseScroll(Action<MouseChangeEventArgs> callback) => _globalHook.MouseWheel += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));

    public void OnMouseDrag(Action<MouseChangeEventArgs> callback) => _globalHook.MouseDragged += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));

    public void OnMousePressed(Action<MouseChangeEventArgs> callback) => _globalHook.MousePressed += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));

    public void OnMouseReleased(Action<MouseChangeEventArgs> callback) => _globalHook.MouseReleased += (sender, args) => callback(MouseDataConverter.ConvertTo(args.Data, args.EventTime));

    // Keyboard events
    public void OnKeyboardKeyPress(Action<KeyboardPressEventArgs> callback) => _globalHook.KeyPressed += (sender, args) => callback(new KeyboardPressEventArgs(KeyboardKeyConverter.ConvertTo(args.Data.KeyCode), args.EventTime));

    public void OnKeyboardKeyRelease(Action<KeyboardReleaseEventArgs> callback) => _globalHook.KeyReleased += (sender, args) => callback(new KeyboardReleaseEventArgs(KeyboardKeyConverter.ConvertTo(args.Data.KeyCode), args.EventTime));

    public void OnKeyboardKeyTyped(Action<KeyboardTypeEventArgs> callback) => _globalHook.KeyTyped += (sender, args) => callback(new KeyboardTypeEventArgs(KeyboardKeyConverter.ConvertTo(args.Data.KeyCode), args.EventTime));

    /// <summary>
    /// Notifies the hook to start listening for events. Not intended to be called directly.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Start() => _globalHook.Run();

    /// <summary>
    /// Notifies the hook to stop listening for events. Not intended to be called directly.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Stop() => _globalHook.Dispose();
}