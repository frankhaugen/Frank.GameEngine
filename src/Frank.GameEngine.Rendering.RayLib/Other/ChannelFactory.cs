using System.Threading.Channels;
using Microsoft.Extensions.Caching.Memory;

namespace Frank.GameEngine.Rendering.RayLib;

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