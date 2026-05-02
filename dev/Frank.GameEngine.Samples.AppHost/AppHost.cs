var builder = DistributedApplication.CreateBuilder(args);

// Register each sample as an executable resource. Start one windowed game at a time from the dashboard.
builder.AddProject<Projects.Frank_GameEngine_Samples_Battleship>("battleship");
builder.AddProject<Projects.Frank_GameEngine_Samples_BouncingBall>("bouncing-ball");
builder.AddProject<Projects.Frank_GameEngine_Samples_Fps>("fps");
builder.AddProject<Projects.Frank_GameEngine_Samples_Hello2D>("hello-2d");
builder.AddProject<Projects.Frank_GameEngine_Samples_Pong>("pong");

builder.Build().Run();
