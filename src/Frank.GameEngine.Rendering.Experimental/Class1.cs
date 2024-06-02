using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Channels;
using Frank.GameEngine.Primitives;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Raylib_cs;

namespace Frank.GameEngine.Rendering.Experimental;

public class TickProducer(Channel<Tick> tickChannel) : BackgroundService
{
    public TimeSpan Interval { get; private set; } = TimeSpan.FromSeconds(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var frameNumber = 0uL;
        var stopwatch = Stopwatch.StartNew();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            tickChannel.Writer.TryWrite(new Tick(frameNumber, stopwatch.Elapsed, Interval));
            await Task.Delay(Interval, stoppingToken);
            frameNumber++;
        }
    }
}

public class RenderLoop(ILogger<RenderLoop> logger, IHostApplicationLifetime applicationLifetime, IWindow window, ChannelReader<PhysicsEngineSignoff> reader) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await foreach (var signoff in reader.ReadAllAsync(stoppingToken))
            {
                await window.RenderFrameAsync(signoff.Tick);
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Render loop cancelled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Render loop failed");
            applicationLifetime.StopApplication();
        }
    }
}

public class Window : IWindow
{
    private readonly ILogger<Window> _logger;
    private readonly RenderQueue _renderQueue;

    public Window(ILogger<Window> logger, RenderQueue renderQueue)
    {
        _logger = logger;
        _renderQueue = renderQueue;

        Raylib.InitWindow(800, 600, "Frank's Game Engine");
        Raylib.ShowCursor();
        Raylib.DrawFPS(0, 0);
        Raylib.SetExitKey(KeyboardKey.Escape);
    }

    public async Task RenderFrameAsync(Tick tick)
    {
        _logger.LogDebug("Rendering frame {FrameNumber}", tick.FrameNumber);
        
        // Raylib.ClearBackground(new Color( 255, 255, 255, 255));
        Raylib.ClearBackground(new Color(
            (byte)(tick.FrameNumber % 255),
            (byte)(tick.FrameNumber % 255),
            (byte)(tick.FrameNumber % 255),
            (byte)(tick.FrameNumber % 255)
            ));
        
        Raylib.BeginDrawing();
        
        var shapes = _renderQueue.DestructiveGet(tick);
        foreach (var shape in shapes)
        {
            var color = shape.Color;
            var edges = shape.Polygon.Edges.ToArray();

            foreach (var edge in edges)
                Raylib.DrawLine3D(edge.A, edge.B, new Color(color.R, color.G, color.B, color.A));
        }

        Raylib.EndDrawing();
        
        await Task.CompletedTask;
    }
}

public class RenderQueue
{
    private readonly ConcurrentDictionary<ulong, List<Shape>> _shapes = new();
    
    public void Add(Tick tick, Shape shape)
    {
        var list = _shapes.GetOrAdd(tick.FrameNumber, _ => new List<Shape>());
        list.Add(shape);
    }
    
    public IEnumerable<Shape> DestructiveGet(Tick tick)
        => !_shapes.Remove(tick.FrameNumber, out var value) ? Enumerable.Empty<Shape>() : value;
}

public interface IWindow
{
    Task RenderFrameAsync(Tick tick);
}

public class PhysicsEngine(ILogger<PhysicsEngine> logger, RenderQueue renderQueue, ChannelWriter<PhysicsEngineSignoff> writer, ChannelReader<Tick> reader)
    : BackgroundService
{
    private readonly Shape _shape = new()
    {
        Polygon = new Polygon(new[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 10, 0),
            new Vector3(10, 10, 0),
            new Vector3(10, 0, 0),
        }),
        Color = System.Drawing.Color.Crimson
    };


    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await foreach (var tick in reader.ReadAllAsync(stoppingToken))
            {
                await UpdateAsync(tick);
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Physics engine cancelled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Physics engine failed");
        }
    }
    
    public async Task UpdateAsync(Tick tick)
    {
        logger.LogDebug("Updating physics for frame {FrameNumber}", tick.FrameNumber);
        
        var deltaTime = (float) tick.DeltaTime.TotalSeconds;
        
        // Add gravity
        var gravity = new Vector3(0, -0.98f, 0) * deltaTime;
        _shape.Translate(gravity);
        
        renderQueue.Add(tick, _shape);
        
        // Sign off
        await writer.WriteAsync(new PhysicsEngineSignoff(tick));
    }
}

public record PhysicsEngineSignoff(Tick Tick);

public record Tick(ulong FrameNumber, TimeSpan TotalTime, TimeSpan DeltaTime); 

public class ChannelFactory(IMemoryCache cache)
{
    public Channel<T> CreateChannel<T>() where T : class => cache.GetOrCreate<Channel<T>>(typeof(T).Name,x =>
    {
        x.SlidingExpiration = TimeSpan.FromDays(5);
        return Channel.CreateUnbounded<T>(new UnboundedChannelOptions()
        {
            SingleReader = false,
            SingleWriter = true
        });
    }) ?? throw new InvalidOperationException($"Channel<{nameof(T)}> not found");
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameEngine(this IServiceCollection services)
    {
        services.AddTickProducer();
        services.AddPhysicsEngine();
        services.AddRenderLoop();
        services.AddChannelFactory(builder => builder
            .AddChannel<Tick>()
            .AddChannel<PhysicsEngineSignoff>());
        return services;
    }
 
    public static IServiceCollection AddTickProducer(this IServiceCollection services)
    {
        services.AddHostedService<TickProducer>();
        return services;
    }
    
    public static IServiceCollection AddPhysicsEngine(this IServiceCollection services)
    {
        services.AddHostedService<PhysicsEngine>();
        return services;
    }
    
    public static IServiceCollection AddRenderLoop(this IServiceCollection services)
    {
        services.AddSingleton<IWindow, Window>();
        services.AddSingleton<RenderQueue>();
        services.AddHostedService<RenderLoop>();
        return services;
    }
    
    public static IServiceCollection AddChannelFactory(this IServiceCollection services, Action<ChannelFactoryBuilder> configure)
    {
        services.AddMemoryCache();
        services.AddSingleton<ChannelFactory>();
        ChannelFactoryBuilder builder = new(services);
        configure(builder);
        return services;
    }
}

public class ChannelFactoryBuilder(IServiceCollection services)
{
    public ChannelFactoryBuilder AddChannel<T>() where T : class
    {
        services.AddSingleton<Channel<T>>(provider => provider.GetRequiredService<ChannelFactory>().CreateChannel<T>());
        services.AddSingleton<ChannelReader<T>>(provider => provider.GetRequiredService<Channel<T>>().Reader);
        services.AddSingleton<ChannelWriter<T>>(provider => provider.GetRequiredService<Channel<T>>().Writer);
        return this;
    }
}