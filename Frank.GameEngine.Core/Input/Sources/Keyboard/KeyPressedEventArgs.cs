using Microsoft.Xna.Framework.Input;

namespace Frank.GameEngine.Core.Input.Sources.Keyboard;

public struct KeyPressedEventArgs
{
    public Keys Key { get; }
    public bool IsRepeat { get; }

    public KeyPressedEventArgs(Keys key, bool isRepeat)
    {
        Key = key;
        IsRepeat = isRepeat;
    }
}