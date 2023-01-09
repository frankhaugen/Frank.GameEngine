// See https://aka.ms/new-console-template for more information

using Frank.GameEngine;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddGame(builder.Configuration);
var app = builder.Build();
app.Run();

