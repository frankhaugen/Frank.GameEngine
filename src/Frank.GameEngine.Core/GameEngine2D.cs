using Frank.GameEngine.Audio.Ogg;
using Frank.GameEngine.Input;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;

namespace Frank.GameEngine.Core;

/// <summary>
///     2D-focused facade: same audio/input bootstrap as <see cref="GameEngine" />, but drives <see cref="IRenderer2D" />.
/// </summary>
public sealed class GameEngine2D : IDisposable
{
    private static readonly TimeSpan ShutdownWait = TimeSpan.FromSeconds(10);

    private readonly IInputSource _input;

    private IRenderer2D? _renderer2D;
    private Task? _audioBackgroundTask;
    private Task? _inputBackgroundTask;

    public GameEngine2D(IAudioPlayer audioPlayer, IInputSource inputSource)
    {
        AudioPlayer = audioPlayer;
        _input = inputSource;
    }

    public GameEngine2D(IAudioPlayer audioPlayer) : this(audioPlayer, new InputManager())
    {
    }

    public Scene2DManager Scene2DManager { get; } = new();

    public IInputSource Input => _input;

    public IAudioPlayer AudioPlayer { get; }

    public Scene2D? CurrentScene2D => Scene2DManager.CurrentScene2D;

    public bool IsInitialized { get; private set; }

    public void Initialize(IRenderer2D renderer2D)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (IsInitialized)
            throw new InvalidOperationException("GameEngine2D is already initialized. Call Shutdown() before Initialize() again.");

        if (CurrentScene2D is null)
            throw new InvalidOperationException("Cannot initialize: no current 2D scene. Select a scene before calling Initialize.");

        _renderer2D = renderer2D;

        _audioBackgroundTask = Task.Factory.StartNew(
            () => AudioPlayer.PlayLooping(0),
            CancellationToken.None,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);

        _inputBackgroundTask = Task.Factory.StartNew(
            () => _input.Start(),
            CancellationToken.None,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);

        IsInitialized = true;
    }

    public void Shutdown()
    {
        if (_disposed)
            return;

        if (!IsInitialized && _audioBackgroundTask is null && _inputBackgroundTask is null)
            return;

        AudioPlayer.Stop();
        _input.Stop();

        var tasks = new List<Task>(2);
        if (_audioBackgroundTask is { IsCompleted: false } a)
            tasks.Add(a);
        if (_inputBackgroundTask is { IsCompleted: false } i)
            tasks.Add(i);

        if (tasks.Count > 0)
        {
            try
            {
                Task.WaitAll(tasks.ToArray(), ShutdownWait);
            }
            catch (AggregateException)
            {
            }
        }

        _audioBackgroundTask = null;
        _inputBackgroundTask = null;
        IsInitialized = false;
        _renderer2D = null;
    }

    public void Draw()
    {
        if (CurrentScene2D is null)
            return;
        if (_renderer2D is null)
            throw new InvalidOperationException("Cannot draw before Initialize(IRenderer2D) has completed.");
        _renderer2D.Render(CurrentScene2D);
    }

    private bool _disposed;

    public void Dispose()
    {
        if (_disposed)
            return;
        Shutdown();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
