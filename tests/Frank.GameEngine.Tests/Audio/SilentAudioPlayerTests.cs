using FluentAssertions;
using Frank.GameEngine.Audio.Ogg;

namespace Frank.GameEngine.Tests.Audio;

public class SilentAudioPlayerTests
{
    [Test]
    public void Play_PlayLooping_Stop_DoNotThrow()
    {
        var player = new SilentAudioPlayer();

        player.Invoking(p => p.Play(0)).Should().NotThrow();
        player.Invoking(p => p.PlayLooping(99)).Should().NotThrow();
        player.Invoking(p => p.Stop()).Should().NotThrow();
    }
}
