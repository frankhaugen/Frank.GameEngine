namespace Frank.GameEngine.Audio.Console;

public struct Note
{
    public Note(Tone frequency, Duration time)
    {
        NoteTone = frequency;
        NoteDuration  = time;
    }

    public Tone NoteTone { get; }
    public Duration NoteDuration { get; }
}