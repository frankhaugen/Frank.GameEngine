using Frank.GameEngine.Audio.Midi;
using Frank.GameEngine.Audio.Ogg;

namespace Frank.GameEngine.Audio.Console;

public class ConsoleAudioPlayer : IAudioPlayer
{
    private readonly TuneLibrary _tunes;
    private bool _isLooping;

    public ConsoleAudioPlayer(TuneLibrary tunes)
    {
        _tunes = tunes;
    }

    public void Play(int soundId)
    {
        if (!_tunes.ContainsKey(soundId)) return;
        var tune = _tunes[soundId];
        foreach (var n in tune)
            if (n.NoteTone == Tone.REST)
                Thread.Sleep((int)n.NoteDuration);
            else
                System.Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
    }

    public void PlayLooping(int soundId)
    {
        _isLooping = true;
        while (_isLooping) Play(soundId);
    }

    public void Stop()
    {
        _isLooping = false;
    }
}