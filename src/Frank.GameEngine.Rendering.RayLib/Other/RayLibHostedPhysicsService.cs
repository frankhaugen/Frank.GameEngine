using System.Numerics;
using System.Threading.Channels;
using Frank.GameEngine.Primitives;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Frank.GameEngine.Rendering.RayLib;

/// <summary>
/// Experimental hosted-service step that consumes <see cref="Tick"/> messages and pushes shapes into <see cref="RenderQueue"/>.
/// This is not the core simulation type in the Frank.GameEngine.Physics assembly; it is part of the Raylib plus Generic Host channel demo only.
/// </summary>
public class RayLibHostedPhysicsService(
    ILogger<RayLibHostedPhysicsService> logger,
    RenderQueue renderQueue,
    ChannelWriter<PhysicsEngineSignoff> writer,
    ChannelReader<Tick> reader)
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
            logger.LogInformation("RayLib hosted physics service cancelled");
        }
        catch (Exception e)
        {
            logger.LogError(e, "RayLib hosted physics service failed");
        }
    }

    public async Task UpdateAsync(Tick tick)
    {
        logger.LogDebug("Hosted physics step for frame {FrameNumber}", tick.FrameNumber);

        var deltaTime = (float)tick.DeltaTime.TotalSeconds;

        var gravity = new Vector3(0, -0.98f, 0) * deltaTime;
        _shape.Translate(gravity);

        renderQueue.Add(tick, _shape);

        await writer.WriteAsync(new PhysicsEngineSignoff(tick));
    }
}
