using Frank.GameEngine.Audio.Ogg;
using Frank.GameEngine.Input;
using Frank.GameEngine.Physics;
using Frank.GameEngine.Primitives;
using Frank.GameEngine.Rendering;
using Microsoft.Extensions.Logging;

namespace Frank.GameEngine.Core;

/// <summary>
///     Facade that wires physics, scenes, input, audio, and an <see cref="IRenderer" />.
///     Call <see cref="Initialize" /> once a scene is selected, then <see cref="Shutdown" /> (or <see cref="Dispose" />)
///     before dropping the instance so background input/audio work is stopped cleanly.
/// </summary>
public sealed class GameEngine : IDisposable
{
    private static readonly TimeSpan ShutdownWait = TimeSpan.FromSeconds(10);

    private IRenderer? _renderer;
    private Task? _audioBackgroundTask;
    private Task? _inputBackgroundTask;

    public GameEngine(PhysicsEngine physicsEngine, IAudioPlayer audioPlayer)
    {
        PhysicsEngine = physicsEngine;
        AudioPlayer = audioPlayer;
    }

    public ILoggerProvider? LoggerProvider { get; set; }

    public bool IsInitialized { get; private set; }

    public SceneManager SceneManager { get; } = new();

    public InputManager InputManager { get; } = new();

    public PhysicsEngine PhysicsEngine { get; }

    public IAudioPlayer AudioPlayer { get; }

    public Scene? CurrentScene => SceneManager.CurrentScene;

    /// <summary>
    ///     Starts background audio looping and global input capture. Requires a current scene and a renderer.
    /// </summary>
    /// <exception cref="InvalidOperationException">No current scene, or already initialized.</exception>
    public void Initialize(IRenderer renderer)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (IsInitialized)
            throw new InvalidOperationException("GameEngine is already initialized. Call Shutdown() before Initialize() again.");

        if (CurrentScene is null)
            throw new InvalidOperationException("Cannot initialize: no current scene. Select a scene before calling Initialize.");

        _renderer = renderer;

        _audioBackgroundTask = Task.Factory.StartNew(
            () => AudioPlayer.PlayLooping(0),
            CancellationToken.None,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);

        _inputBackgroundTask = Task.Factory.StartNew(
            () => InputManager.Start(),
            CancellationToken.None,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);

        IsInitialized = true;
    }

    /// <summary>
    ///     Stops background audio and input and waits for background work to finish (bounded wait).
    ///     Safe to call multiple times or when not initialized.
    /// </summary>
    public void Shutdown()
    {
        if (_disposed)
            return;

        if (!IsInitialized && _audioBackgroundTask is null && _inputBackgroundTask is null)
            return;

        AudioPlayer.Stop();
        InputManager.Stop();

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
                // Background failures after Stop are non-fatal for shutdown.
            }
        }

        _audioBackgroundTask = null;
        _inputBackgroundTask = null;
        IsInitialized = false;
        _renderer = null;
    }

    public void Update(UpdateArgs args)
    {
        if (CurrentScene is null)
            return;

        PhysicsEngine.Update(CurrentScene, args.ElapsedTime);
    }

    public void Draw()
    {
        if (CurrentScene is null)
            return;
        if (_renderer is null)
            throw new InvalidOperationException("Cannot draw before Initialize(IRenderer) has completed.");
        _renderer.Render(CurrentScene);
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
