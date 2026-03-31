namespace Frank.GameEngine.Audio.Midi;

public class MidiSong
{
    public MidiSong(string name, string composer)
    {
        Name = name;
        Composer = composer;
        Tracks = new List<MidiTrack>();
    }

    public MidiSong()
    {
        Tracks = new List<MidiTrack>();
        Name = string.Empty;
        Composer = string.Empty;
    }

    public List<MidiTrack> Tracks { get; set; } = new();

    public string Name { get; set; } = string.Empty;

    public string Composer { get; set; } = string.Empty;
}