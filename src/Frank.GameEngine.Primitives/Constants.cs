﻿using System.Numerics;

namespace Frank.GameEngine.Primitives;

/// <summary>
///     Represents a collision between two objects.
/// </summary>
public static class Constants
{
    /// <summary>
    ///     The TerrestrialConstants class provides constants that are specific to Earth and commonly used in physics and
    ///     mathematics.
    /// </summary>
    public static class TerrestrialConstants
    {
        /// <summary>
        ///     Average radius of Earth in kilometers.
        ///     Example usage: double circumference = 2 * UniversalConstants.Pi * TerrestrialConstants.EarthRadius.
        /// </summary>
        public const decimal EarthRadius = 6371M;

        /// <summary>
        ///     Earth's surface gravity in m/s^2.
        ///     Example usage: double weight = mass * TerrestrialConstants.EarthGravity.
        /// </summary>
        public const float EarthGravity = 9.80665f;

        /// <summary>
        ///     Earth's orbital speed in km/s.
        ///     Example usage: decimal yearInSeconds = 2 * UniversalConstants.Pi * TerrestrialConstants.EarthRadius /
        ///     TerrestrialConstants.EarthOrbitalSpeed * 60 * 60 * 24.
        /// </summary>
        public const decimal EarthOrbitalSpeed = 29.78M;
    }

    /// <summary>
    ///     The UniversalConstants class provides constants that are universal and commonly used in physics and mathematics.
    /// </summary>
    public static class UniversalConstants
    {
        /// <summary>
        ///     Speed of light in meters per second.
        ///     Example usage: double distance = UniversalConstants.SpeedOfLight * timeInSeconds.
        /// </summary>
        public const decimal SpeedOfLight = 299792458M;

        /// <summary>
        ///     Pi, the ratio of the circumference of a circle to its diameter.
        ///     Example usage: double circumference = 2 * UniversalConstants.Pi * radius.
        /// </summary>
        public const decimal Pi = 3.14159265358979323846M;

        /// <summary>
        ///     Gravitational Constant in m^3 kg^-1 s^-2.
        ///     Example usage: double force = UniversalConstants.GravitationalConstant * (mass1 * mass2 / Math.Pow(distance, 2)).
        /// </summary>
        public const decimal GravitationalConstant = 6.67430e-11M;

        /// <summary>
        ///     Planck's Constant in Joule seconds.
        ///     Example usage: double energy = UniversalConstants.PlancksConstant * frequency.
        /// </summary>
        public const decimal PlancksConstant = 6.62607015e-34M;
    }

    /// <summary>
    ///     The MathConstants class provides constants that are commonly used in mathematics.
    /// </summary>
    public static class MathConstants
    {
        /// <summary>
        ///     The value of pi divided by two.
        /// </summary>
        public const float PiOver2 = (float)(Math.PI / 2.0);

        /// <summary>
        ///     Pi divided by four.
        /// </summary>
        public const float PiOver4 = (float)(Math.PI / 4.0);

        /// <summary>
        ///     The value of pi times two.
        /// </summary>
        public const float TwoPi = (float)(Math.PI * 2.0);

        /// <summary>
        ///     Epsilon is the smallest value that a float can have different from zero.
        /// </summary>
        public const float Epsilon = 1.192092896e-07f;

        /// <summary>
        ///     The value of pi divided by 180.
        /// </summary>
        public const float Deg2Rad = (float)(Math.PI / 180.0);

        /// <summary>
        ///     The value of 180 divided by pi.
        /// </summary>
        public const float Rad2Deg = (float)(180.0 / Math.PI);

        /// <summary>
        ///     Infinity is a constant value that represents positive infinity.
        /// </summary>
        public const float Infinity = float.PositiveInfinity;

        /// <summary>
        ///     NegativeInfinity is a constant value that represents negative infinity.
        /// </summary>
        public const float NegativeInfinity = float.NegativeInfinity;
    }

    public static class PhysicsConstants
    {
        /// <summary>
        ///     The energy released per gram of gunpowder in joules.
        /// </summary>
        public const float GunpowderEnergyPerGramInJoules = 3000;
    }

    /// <summary>
    ///     The AspectRatioConstants class provides constants that are specific to aspect ratios and commonly used in physics
    ///     and mathematics.
    /// </summary>
    public static class AspectRatioConstants
    {
        public const float Landscape = 1.333f;
        public const float Portrait = 0.75f;
        public const float Letterbox = 1.6f;
        public const float Square = 1f;
        public const float Imax = 1.9f;
        public const float Console = 1.777f;
        public const float Ultrawide = 2.37f;
        public const float Standard = 1.333f;
        public const float Widescreen = 1.6f;
        public const float SuperUltrawide = 3.37f;
    }

    /// <summary>
    ///     The VectorConstants class provides constants that are specific to vectors and commonly used in physics and
    ///     mathematics.
    /// </summary>
    public static class VectorConstants
    {
        public static Vector3 Up = new(0, 0, 1);
        public static Vector3 Down = new(0, -1, 0);
        public static Vector3 Left = new(-1, 0, 0);
        public static Vector3 Right = new(1, 0, 0);

        /// <summary>
        ///     The Vector3Constants class provides constants that are specific to Vector3 and commonly used in physics and
        ///     mathematics.
        /// </summary>
        public static class PhysicsConstants
        {
            public static readonly Vector3 EarthGravity = Down * TerrestrialConstants.EarthGravity;
        }
    }
}