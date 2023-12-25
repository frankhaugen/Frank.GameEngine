using Frank.GameEngine.Audio.Midi;

namespace Frank.GameEngine.Audio.Extensions;

public static class NoteExtensions
{
    private const double ReferenceFrequency = 440.0; // Frequency of A4
    private const int ReferencePosition = (int)Note.A4; // Position of A4 in the enum

    public static double GetFrequency(this Note note)
    {
        int halfStepsFromA4 = (int)note - ReferencePosition;
        return ReferenceFrequency * Math.Pow(2, halfStepsFromA4 / 12.0);
    }
}