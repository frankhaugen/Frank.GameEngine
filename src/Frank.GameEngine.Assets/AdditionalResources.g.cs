using System;
using System.IO;

namespace Frank.GameEngine.Assets
{
    public static class AdditionalResources2
    {
        public static class Models
        {
            public static byte[] teapot =>
                File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Models/teapot.obj"));
        }

        public static class Audio
        {
            public static class Midi
            {
                public static class Songs
                {
                    public static byte[] ImperialMarch =>
                        File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Audio/Midi/Songs/ImperialMarch.json"));

                    public static byte[] RescueRangers =>
                        File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Audio/Midi/Songs/RescueRangers.json"));
                }
            }
        }
    }
}