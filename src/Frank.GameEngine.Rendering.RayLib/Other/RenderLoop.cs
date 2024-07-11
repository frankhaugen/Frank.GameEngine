using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Frank.GameEngine.Rendering.RayLib;

public class RenderLoop(ILogger<RenderLoop> logger, IHostApplicationLifetime applicationLifetime, ChannelReader<PhysicsEngineSignoff> reader, IRenderer renderer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await foreach (var signoff in reader.ReadAllAsync(stoppingToken))
            {
                // await renderer.Render();
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