namespace Frank.GameEngine.Audio;

public interface IAudioEngine
{
    public void Play(Guid soundId);
    
    public void PlayLooping(Guid soundId);
    
    public void Stop();
}

public interface IAudioLibrary
{
    public ReadOnlyMemory<float> this[Guid id] { get; }
    
    public Guid Add(ReadOnlyMemory<float> audio);
}

public interface IAudioSampleProvider
{
    public Guid Id { get; }
    public ReadOnlyMemory<float> Get { get; }
}

public interface IAudioPlayer2
{
    public void Play(Guid soundId);
    
    public void PlayLooping(Guid soundId);
    
    public void Stop(Int128 soundId);
}

internal class PredictableGuidGenerator
{
    private readonly Random _random;
    private readonly byte[] _bytes = new byte[16];

    public PredictableGuidGenerator(int seed = 0) => _random = new Random(seed);

    public Guid GenerateGuid()
    {
        _random.NextBytes(_bytes);
        return new Guid(_bytes);
    }
}