using MonoGame.Extended.Input.InputListeners;

namespace Frank.GameEngine._2D;

public class InputService : IInputService
{
    public InputService()
    {
        GuiKeyboardListener = new KeyboardListener();
        GuiMouseListener = new MouseListener();
        GuiGamePadListener = new GamePadListener();
        GuiTouchListener = new TouchListener();
    }

    public KeyboardListener GuiKeyboardListener { get; }
    public MouseListener GuiMouseListener { get; }
    public GamePadListener GuiGamePadListener { get; }
    public TouchListener GuiTouchListener { get; }
}