using NAudio.Midi;

namespace Frank.GameEngine.Audio.Midi;

public class SongPlayer : ISongPlayer
{
    private readonly MidiOut _midiOut;

    public SongPlayer(MidiOut midiOut)
    {
        _midiOut = midiOut;
    }

    public async Task PlaySong(MidiSong song)
    {
        await Parallel.ForEachAsync(song.Tracks,
            async (track, cancellationToken) => { await PlayTrack(_midiOut, track); });
    }

    private async Task PlayTrack(MidiOut midiOut, MidiTrack track)
    {
        _midiOut.Send(MidiMessage.ChangePatch((int)track.Instrument, track.Channel).RawData);
        foreach (var note in track.Notes) await PlayNote(midiOut, (int)note.Note, (int)note.Duration, track.Channel);
    }

    private async Task PlayNote(MidiOut midiOut, int note, int duration, int channel)
    {
        midiOut.Send(MidiMessage.StartNote(note, 100, channel).RawData);
        await Task.Delay(duration);
        midiOut.Send(MidiMessage.StopNote(note, 100, channel).RawData);
    }
}