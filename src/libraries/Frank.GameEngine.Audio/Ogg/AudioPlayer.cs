using NAudio.Vorbis;
using NAudio.Wave;

namespace Frank.GameEngine.Audio.Ogg;

/// <summary>
///     NAudio-based Ogg Vorbis player. <see cref="Play" /> is one-shot; <see cref="PlayLooping" /> runs until
///     <see cref="Stop" />. All members are thread-safe.
/// </summary>
public sealed class AudioPlayer : IAudioPlayer
{
    private readonly ClipLibrary _clipLibrary;
    private readonly object _gate = new();

    private WaveOutEvent? _loopOut;
    private VorbisWaveReader? _loopReader;
    private bool _looping;

    public AudioPlayer(ClipLibrary clipLibrary)
    {
        _clipLibrary = clipLibrary;
    }

    public void Play(int clipId)
    {
        var clip = _clipLibrary[clipId];
        lock (_gate)
        {
            StopLoopUnlocked();
            var waveOut = new WaveOutEvent();
            var reader = new VorbisWaveReader(new MemoryStream(clip.Bytes));

            waveOut.Init(reader);
            waveOut.PlaybackStopped += (_, _) =>
            {
                waveOut.Dispose();
                reader.Dispose();
            };
            waveOut.Play();
        }
    }

    public void PlayLooping(int soundId)
    {
        var clip = _clipLibrary[soundId];
        lock (_gate)
        {
            StopLoopUnlocked();
            _loopReader = new VorbisWaveReader(new MemoryStream(clip.Bytes));
            _loopOut = new WaveOutEvent();
            _loopOut.Init(_loopReader);
            _looping = true;
            _loopOut.PlaybackStopped += OnLoopPlaybackStopped;
            _loopOut.Play();
        }
    }

    public void Stop()
    {
        lock (_gate)
            StopLoopUnlocked();
    }

    private void OnLoopPlaybackStopped(object? sender, StoppedEventArgs e)
    {
        lock (_gate)
        {
            if (!_looping || _loopOut is null || _loopReader is null)
                return;

            try
            {
                _loopReader.Position = 0;
                _loopOut.Play();
            }
            catch (ObjectDisposedException)
            {
                StopLoopUnlocked();
            }
        }
    }

    private void StopLoopUnlocked()
    {
        _looping = false;
        if (_loopOut is not null)
        {
            _loopOut.PlaybackStopped -= OnLoopPlaybackStopped;
            try
            {
                _loopOut.Stop();
            }
            catch (ObjectDisposedException)
            {
                // ignore
            }

            _loopOut.Dispose();
            _loopOut = null;
        }

        _loopReader?.Dispose();
        _loopReader = null;
    }
}
