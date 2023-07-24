namespace Frank.GameEngine.Audio.Midi;

public struct MidiNote
{
    public Note Note { get; set; }
    public Duration Duration { get; set; }

    public MidiNote(Note note, Duration duration)
    {
        Note = note;
        Duration = duration;
    }
}