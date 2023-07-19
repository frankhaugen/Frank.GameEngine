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
    public void OnMouseMove(Action<MouseEventData> callback) => _globalHook.MouseMoved += (sender, args) => callback(args.Data);

    public void OnMouseClick(Action<MouseEventData> callback) => _globalHook.MouseClicked += (sender, args) => callback(args.Data);

    public void OnMouseScroll(Action<MouseWheelEventData> callback) => _globalHook.MouseWheel += (sender, args) => callback(args.Data);

    public void OnMouseDrag(Action<MouseEventData> callback) => _globalHook.MouseDragged += (sender, args) => callback(args.Data);

    public void OnMousePressed(Action<MouseEventData> callback) => _globalHook.MousePressed += (sender, args) => callback(args.Data);

    public void OnMouseReleased(Action<MouseEventData> callback) => _globalHook.MouseReleased += (sender, args) => callback(args.Data);

    // Keyboard events
    public void OnKeyboardKeyPress(Action<KeyboardEventData> callback) => _globalHook.KeyPressed += (sender, args) => callback(args.Data);

    public void OnKeyboardKeyRelease(Action<KeyboardEventData> callback) => _globalHook.KeyReleased += (sender, args) => callback(args.Data);

    public void OnKeyboardKeyTyped(Action<KeyboardEventData> callback) => _globalHook.KeyTyped += (sender, args) => callback(args.Data);

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