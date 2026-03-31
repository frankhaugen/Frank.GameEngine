using FluentAssertions;
using Frank.GameEngine.Input;
using Frank.GameEngine.Input.Converters;
using SharpHook.Data;

namespace Frank.GameEngine.Tests.Input;

public class KeyboardKeyConverterTests
{
    [Theory]
    [InlineData(KeyCode.VcA, KeyboardKey.A)]
    [InlineData(KeyCode.VcZ, KeyboardKey.Z)]
    [InlineData(KeyCode.VcEscape, KeyboardKey.Escape)]
    [InlineData(KeyCode.VcSpace, KeyboardKey.Space)]
    [InlineData(KeyCode.VcEnter, KeyboardKey.Enter)]
    [InlineData(KeyCode.VcLeft, KeyboardKey.Left)]
    public void ConvertTo_MapsCommonKeys(KeyCode native, KeyboardKey expected)
    {
        KeyboardKeyConverter.ConvertTo(native).Should().Be(expected);
    }

    [Fact]
    public void ConvertTo_Unknown_ReturnsNone()
    {
        var unknown = unchecked((KeyCode)int.MaxValue);
        KeyboardKeyConverter.ConvertTo(unknown).Should().Be(KeyboardKey.None);
    }
}
