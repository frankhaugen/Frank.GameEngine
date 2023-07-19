using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frank.GameEngine.Lagacy.A.OldCore.Input.Sources.Keyboard;

public class KeyboardInputSource : InputSource
{
    private KeyboardState _keyboardState;
    private KeyboardState _keyboardPreviousState;

    public override void Update(GameTime gameTime)
    {
        _keyboardPreviousState = _keyboardState;
        _keyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

        foreach (var key in _keyboardState.GetPressedKeys())
        {
            bool isRepeat = _keyboardPreviousState.IsKeyDown(key);
            var keyPressedEventArgs = new KeyPressedEventArgs(key, isRepeat);
            KeyPressed?.Invoke(this, keyPressedEventArgs);
        }
    }

    public event EventHandler<KeyPressedEventArgs>? KeyPressed;
}