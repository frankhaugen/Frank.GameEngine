namespace Frank.GameEngine.Audio.Midi;

public class MidiTrack
{
    public List<MidiNote> Notes { get; set; } = new List<MidiNote>();
    public MidiInstrument Instrument { get; set; }
    public int Channel { get; set; }
}