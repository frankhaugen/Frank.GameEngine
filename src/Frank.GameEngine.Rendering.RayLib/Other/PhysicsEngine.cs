using System.Numerics;
using System.Threading.Channels;
using Frank.GameEngine.Primitives;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Frank.GameEngine.Rendering.RayLib;

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