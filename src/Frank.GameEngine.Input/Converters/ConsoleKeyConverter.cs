using SharpHook.Native;

namespace Frank.GameEngine.Input.Converters;

public static class ConsoleKeyConverter
{
    public static ConsoleKey ToConsoleKey(this KeyCode keyCode)
    {
        return keyCode switch
        {
            KeyCode.VcEscape => ConsoleKey.Escape,
            KeyCode.VcF1 => ConsoleKey.F1,
            KeyCode.VcF2 => ConsoleKey.F2,
            KeyCode.VcF3 => ConsoleKey.F3,
            KeyCode.VcF4 => ConsoleKey.F4,
            KeyCode.VcF5 => ConsoleKey.F5,
            KeyCode.VcF6 => ConsoleKey.F6,
            KeyCode.VcF7 => ConsoleKey.F7,
            KeyCode.VcF8 => ConsoleKey.F8,
            KeyCode.VcF9 => ConsoleKey.F9,
            KeyCode.VcF10 => ConsoleKey.F10,
            KeyCode.VcF11 => ConsoleKey.F11,
            KeyCode.VcF12 => ConsoleKey.F12,
            KeyCode.VcF13 => ConsoleKey.F13,
            KeyCode.VcF14 => ConsoleKey.F14,
            KeyCode.VcF15 => ConsoleKey.F15,
            KeyCode.VcF16 => ConsoleKey.F16,
            KeyCode.VcF17 => ConsoleKey.F17,
            KeyCode.VcF18 => ConsoleKey.F18,
            KeyCode.VcF19 => ConsoleKey.F19,
            KeyCode.VcF20 => ConsoleKey.F20,
            KeyCode.VcF21 => ConsoleKey.F21,
            KeyCode.VcF22 => ConsoleKey.F22,
            KeyCode.VcF23 => ConsoleKey.F23,
            KeyCode.VcF24 => ConsoleKey.F24,
            KeyCode.VcUp => ConsoleKey.UpArrow,
            KeyCode.VcDown => ConsoleKey.DownArrow,
            KeyCode.VcLeft => ConsoleKey.LeftArrow,
            KeyCode.VcRight => ConsoleKey.RightArrow,
            KeyCode.VcEnter => ConsoleKey.Enter,
            KeyCode.VcSpace => ConsoleKey.Spacebar,
            KeyCode.VcBackspace => ConsoleKey.Backspace,
            KeyCode.VcTab => ConsoleKey.Tab,
            KeyCode.VcInsert => ConsoleKey.Insert,
            KeyCode.VcDelete => ConsoleKey.Delete,
            KeyCode.VcPageUp => ConsoleKey.PageUp,
            KeyCode.VcPageDown => ConsoleKey.PageDown,
            KeyCode.VcHome => ConsoleKey.Home,
            KeyCode.VcEnd => ConsoleKey.End,
            KeyCode.VcPrintScreen => ConsoleKey.PrintScreen,
            KeyCode.VcPause => ConsoleKey.Pause,
            KeyCode.VcNumPad0 => ConsoleKey.NumPad0,
            KeyCode.VcNumPad1 => ConsoleKey.NumPad1,
            KeyCode.VcNumPad2 => ConsoleKey.NumPad2,
            KeyCode.VcNumPad3 => ConsoleKey.NumPad3,
            KeyCode.VcNumPad4 => ConsoleKey.NumPad4,
            KeyCode.VcNumPad5 => ConsoleKey.NumPad5,
            KeyCode.VcNumPad6 => ConsoleKey.NumPad6,
            KeyCode.VcNumPad7 => ConsoleKey.NumPad7,
            KeyCode.VcNumPad8 => ConsoleKey.NumPad8,
            KeyCode.VcNumPad9 => ConsoleKey.NumPad9,
            KeyCode.VcNumPadDivide => ConsoleKey.Divide,
            KeyCode.VcNumPadMultiply => ConsoleKey.Multiply,
            KeyCode.VcNumPadSubtract => ConsoleKey.Subtract,
            KeyCode.VcNumPadAdd => ConsoleKey.Add,
            KeyCode.VcNumPadEnter => ConsoleKey.Enter,
            KeyCode.VcSemicolon => ConsoleKey.Oem1,
            KeyCode.VcComma => ConsoleKey.OemComma,
            KeyCode.VcPeriod => ConsoleKey.OemPeriod,
            KeyCode.VcSlash => ConsoleKey.Oem2,
            KeyCode.VcA => ConsoleKey.A,
            KeyCode.VcB => ConsoleKey.B,
            KeyCode.VcC => ConsoleKey.C,
            KeyCode.VcD => ConsoleKey.D,
            KeyCode.VcE => ConsoleKey.E,
            KeyCode.VcF => ConsoleKey.F,
            KeyCode.VcG => ConsoleKey.G,
            KeyCode.VcH => ConsoleKey.H,
            KeyCode.VcI => ConsoleKey.I,
            KeyCode.VcJ => ConsoleKey.J,
            KeyCode.VcK => ConsoleKey.K,
            KeyCode.VcL => ConsoleKey.L,
            KeyCode.VcM => ConsoleKey.M,
            KeyCode.VcN => ConsoleKey.N,
            KeyCode.VcO => ConsoleKey.O,
            KeyCode.VcP => ConsoleKey.P,
            KeyCode.VcQ => ConsoleKey.Q,
            KeyCode.VcR => ConsoleKey.R,
            KeyCode.VcS => ConsoleKey.S,
            KeyCode.VcT => ConsoleKey.T,
            KeyCode.VcU => ConsoleKey.U,
            KeyCode.VcV => ConsoleKey.V,
            KeyCode.VcW => ConsoleKey.W,
            KeyCode.VcX => ConsoleKey.X,
            KeyCode.VcY => ConsoleKey.Y,
            KeyCode.VcZ => ConsoleKey.Z,
            KeyCode.Vc1 => ConsoleKey.D1,
            KeyCode.Vc2 => ConsoleKey.D2,
            KeyCode.Vc3 => ConsoleKey.D3,
            KeyCode.Vc4 => ConsoleKey.D4,
            KeyCode.Vc5 => ConsoleKey.D5,
            KeyCode.Vc6 => ConsoleKey.D6,
            KeyCode.Vc7 => ConsoleKey.D7,
            KeyCode.Vc8 => ConsoleKey.D8,
            KeyCode.Vc9 => ConsoleKey.D9,
            KeyCode.Vc0 => ConsoleKey.D0,
            KeyCode.VcMinus => ConsoleKey.OemMinus,
            _ => throw new ArgumentOutOfRangeException(nameof(keyCode), keyCode, null)
        };
    }
}