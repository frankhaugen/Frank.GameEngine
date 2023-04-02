using Frank.GameEngine.Lagacy.OldCore.Input.Sources.Keyboard;
using Frank.GameEngine.Lagacy.OldCore.Input.Sources.Mouse;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.OldCore.Input;

public interface IInputHandler
{
    void Update(GameTime gameTime);
    void RegisterKeyboardEventHandler(EventHandler<KeyPressedEventArgs> keyboardEventHandler);
    void RegisterMouseEventHandler(EventHandler<MouseClickEventArgs> mouseEventHandler);
    void RegisterMouseEventHandler(EventHandler<MouseMoveEventArgs> mouseEventHandler);
    void RegisterMouseEventHandler(EventHandler<MouseScrollEventArgs> mouseEventHandler);
}