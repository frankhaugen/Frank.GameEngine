using Frank.GameEngine.Lagacy.A._2d.Extensions;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A._2d.Maths
{
    internal static class MathConstants
    {
        public const float Rad2Deg = 180f / MathF.PI;
        public const float Deg2Rad = MathF.PI / 180f;
    }

    internal static class PhysicsConstants
    {
        public const float G = -9.81f;
    }

    internal static class PhysicsHelper
    {
        public static Vector2 GetGravityVector(float mass) => new(0f, mass * PhysicsConstants.G);

        public static Vector2 GetNextPosition(Vector2 position, Vector2 velocity, float ΔTime)
        {
            var nextPosition = position.Copy();
            nextPosition.X += velocity.X * ΔTime;
            nextPosition.Y += velocity.Y * ΔTime;
            return nextPosition;
        }

        public static Vector2 GetNextPosition(Vector2 source, float mass, float ΔTime) => GetNextPosition(source, AddSimulationForces(source, mass, ΔTime), ΔTime);

        public static Vector2 AddSimulationForces(Vector2 source, float mass, float ΔTime)
        {
            var velocity = source.Copy();
            var gravity = GetGravityVector(mass);
            var acceleration = new Vector2(gravity.X / mass, gravity.Y / mass);
            velocity.X += acceleration.X * ΔTime;
            velocity.Y += acceleration.Y * ΔTime;

            return velocity;
        }
    }

    internal static class MathHelper
    {
        public static Vector2 CalcDirectionFromAngle(float angle) => new(MathF.Cos(ConvertDegreesToRadians(angle)), MathF.Sin(ConvertDegreesToRadians(angle)));

        public static float ConvertRadiansToDegrees(float radians) => radians * MathConstants.Rad2Deg;

        public static float ConvertDegreesToRadians(float degrees) => degrees * MathConstants.Deg2Rad;
    }
}