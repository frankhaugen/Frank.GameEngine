using System.Diagnostics;
using System.Threading.Channels;
using Microsoft.Extensions.Hosting;

namespace Frank.GameEngine.Rendering.RayLib;

public class TickProducer(Channel<Tick> tickChannel) : BackgroundService
{
    public TimeSpan Interval { get; private set; } = TimeSpan.FromSeconds(1 / 60d);

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