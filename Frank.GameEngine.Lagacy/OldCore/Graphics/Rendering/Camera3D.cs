using Frank.GameEngine.Lagacy.OldCore.Input;
using Frank.GameEngine.Lagacy.OldCore.Input.Sources.Keyboard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frank.GameEngine.Lagacy.OldCore.Graphics.Rendering;

public class Camera3D : ICamera3D
{
    private readonly IInputHandler _inputHandler;

    public Camera3D(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.RegisterKeyboardEventHandler(KeyboardEventHandler);
    }

    private void KeyboardEventHandler(object? sender, KeyPressedEventArgs e)
    {
        switch (e.Key)
        {
            case Keys.W:
                Position += Vector3.Forward * 10f;
                break;
            case Keys.S:
                Position += Vector3.Backward * 10f;
                break;
            case Keys.A:
                Position += Vector3.Left * 10f;
                break;
            case Keys.D:
                Position += Vector3.Right * 10f;
                break;
            case Keys.Space:
                Position += Vector3.Up * 10f;
                break;
            case Keys.LeftControl:
                Position += Vector3.Down * 10f;
                break;
        }
    }

    public Vector3 Position { get; set; } = Vector3.Forward * -250f;
    public Vector3 Target { get; set; } = Vector3.Zero;
    public Vector3 Up { get; set; } = Vector3.Up;

    public Matrix ViewMatrix => Matrix.CreateLookAt(Position, Target, Up);
    public Matrix ProjectionMatrix { get; set; } = Matrix.Identity;
    
}