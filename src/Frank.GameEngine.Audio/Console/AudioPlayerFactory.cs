using Frank.GameEngine.Audio.Midi;
using Frank.GameEngine.Audio.Ogg;

namespace Frank.GameEngine.Audio.Console;

public static class AudioPlayerFactory
{
    public static IAudioPlayer CreateConsoleAudioPlayer(params Tune[] tunes)
    {
        var library = new TuneLibrary();

        for (var index = 0; index < tunes.Length; index++)
        {
            var tune = tunes[index];
            library.Add(index, tune);
        }

        return new ConsoleAudioPlayer(library);
    }
}