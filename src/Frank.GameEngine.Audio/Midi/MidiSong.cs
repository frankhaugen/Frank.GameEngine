namespace Frank.GameEngine.Audio.Midi;

public class MidiSong
{
    public List<MidiTrack> Tracks { get; set; }
    public string Name { get; set; }
    public string Composer { get; set; }

    public MidiSong(string name, string composer)
    {
        Name = name;
        Composer = composer;
        Tracks = new List<MidiTrack>();
    }

    public MidiSong()
    {
    }
}