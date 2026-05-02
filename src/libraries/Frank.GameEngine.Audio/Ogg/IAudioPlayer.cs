namespace Frank.GameEngine.Audio.Ogg;

public interface IAudioPlayer
{
    public void Play(int soundId);

    public void PlayLooping(int soundId);

    public void Stop();
}