// See https://aka.ms/new-console-template for more information

using Frank.GameEngine;

Console.WriteLine("Hello, World!");

using (var game = new MyGame())
{
	game.Run();
}