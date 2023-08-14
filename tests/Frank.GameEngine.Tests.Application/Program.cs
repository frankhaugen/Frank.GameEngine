using Frank.GameEngine.Tests.Application;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(x => x.ClearProviders())
    .ConfigureServices(services => { services.AddHostedService<ConsoleRendererWorker>(); })
    .Build();

host.Run();