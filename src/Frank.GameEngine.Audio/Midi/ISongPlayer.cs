namespace Frank.GameEngine.Audio.Midi;

public interface ISongPlayer
{
    Task PlaySong(MidiSong song);
}