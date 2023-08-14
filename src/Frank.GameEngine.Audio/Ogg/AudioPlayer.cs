using NAudio.Vorbis;
using NAudio.Wave;

namespace Frank.GameEngine.Audio.Ogg;

public class AudioPlayer : IAudioPlayer
{
    private readonly ClipLibrary _clipLibrary;

    public AudioPlayer(ClipLibrary clipLibrary)
    {
        _clipLibrary = clipLibrary;
    }

    public void Play(int clipId)
    {
        using var waveOut = new WaveOutEvent();
        using var reader = new VorbisWaveReader(new MemoryStream(_clipLibrary[clipId].Bytes));

        waveOut.Init(reader);
        waveOut.PlaybackStopped += (s, e) =>
        {
            waveOut.Dispose();
            reader.Dispose();
        };
        waveOut.Play();
    }

    public void PlayLooping(int soundId)
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }
}