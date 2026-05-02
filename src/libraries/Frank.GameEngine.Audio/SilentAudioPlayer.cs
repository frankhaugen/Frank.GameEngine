namespace Frank.GameEngine.Audio.Ogg;

/// <summary>
/// No-op player for hosts without platform-specific audio (for example non-Windows when using console beep).
/// </summary>
public sealed class SilentAudioPlayer : IAudioPlayer
{
    public void Play(int soundId)
    {
    }

    public void PlayLooping(int soundId)
    {
    }

    public void Stop()
    {
    }
}
