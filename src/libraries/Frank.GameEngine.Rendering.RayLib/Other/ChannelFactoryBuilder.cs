using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.GameEngine.Rendering.RayLib;

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