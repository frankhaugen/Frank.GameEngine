using FluentAssertions;
using Frank.GameEngine.Input;
using Frank.GameEngine.Input.Converters;
using SharpHook.Data;

namespace Frank.GameEngine.Tests.Input;

public class KeyboardKeyConverterTests
{
    [Test]
    [Arguments(KeyCode.VcA, KeyboardKey.A)]
    [Arguments(KeyCode.VcZ, KeyboardKey.Z)]
    [Arguments(KeyCode.VcEscape, KeyboardKey.Escape)]
    [Arguments(KeyCode.VcSpace, KeyboardKey.Space)]
    [Arguments(KeyCode.VcEnter, KeyboardKey.Enter)]
    [Arguments(KeyCode.VcLeft, KeyboardKey.Left)]
    public void ConvertTo_MapsCommonKeys(KeyCode native, KeyboardKey expected)
    {
        KeyboardKeyConverter.ConvertTo(native).Should().Be(expected);
    }

    [Test]
    public void ConvertTo_Unknown_ReturnsNone()
    {
        var unknown = unchecked((KeyCode)int.MaxValue);
        KeyboardKeyConverter.ConvertTo(unknown).Should().Be(KeyboardKey.None);
    }
}
