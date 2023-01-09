using Frank.GameEngine.Core.Input.Sources.Keyboard;
using Frank.GameEngine.Core.Input.Sources.Mouse;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Input;

public class InputHandler : IInputHandler
{
    private readonly KeyboardInputSource _keyboardInputSource = new();
    private readonly MouseInputSource _mouseInputSource = new();

    public void Update(GameTime gameTime)
    {
        _keyboardInputSource.Update(gameTime);
        _mouseInputSource.Update(gameTime);
    }
    
    public void RegisterKeyboardEventHandler(EventHandler<KeyPressedEventArgs> keyboardEventHandler) => _keyboardInputSource.KeyPressed += keyboardEventHandler;

    public void RegisterMouseEventHandler(EventHandler<MouseClickEventArgs> mouseEventHandler) => _mouseInputSource.MouseClickEvent += mouseEventHandler;

    public void RegisterMouseEventHandler(EventHandler<MouseMoveEventArgs> mouseEventHandler) => _mouseInputSource.MouseMoveEvent += mouseEventHandler;

    public void RegisterMouseEventHandler(EventHandler<MouseScrollEventArgs> mouseEventHandler) => _mouseInputSource.MouseScrollEvent += mouseEventHandler;
}