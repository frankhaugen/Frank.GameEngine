using Frank.GameEngine.Rendering.Experimental;
using Frank.GameEngine.Tests.Application;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(x => x.AddConsole())
    // .ConfigureServices(services => { services.AddHostedService<ConsoleRendererWorker>(); })
    .ConfigureServices(services => { services.AddGameEngine(); })
    .Build();
 
host.Run();