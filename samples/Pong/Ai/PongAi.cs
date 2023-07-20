using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Pong.Ai;

public class PongAi
{
    private readonly GameObject _paddle;
    private readonly GameObject _target;

    public PongAi(GameObject paddle, GameObject target)
    {
        _paddle = paddle;
        _target = target;
    }

    public float Speed { get; set; } = 1f;

    public void Update()
    {
        var targetPosition = _target.Transform.Position;
        var paddlePosition = _paddle.Transform.Position;
        MovePaddle(targetPosition, paddlePosition);
    }

    private void MovePaddle(Vector3 targetPosition, Vector3 paddlePosition)
    {
        const int tolerance = 5;
        var heightDifference = Vector3.Distance(targetPosition, paddlePosition);
        if (targetPosition.Y > paddlePosition.Y && heightDifference > tolerance)
            _paddle.MoveUp(Speed);
        else if (targetPosition.Y < paddlePosition.Y && heightDifference > tolerance)
            _paddle.MoveDown(Speed);
    }
}