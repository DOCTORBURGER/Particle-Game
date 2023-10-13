using Microsoft.Xna.Framework;
using System;

namespace ParticleGame.Particle_Sytems
{
    public class RandomHelper
    {
        static Random random = new Random();

        public static int Next() => random.Next();

        public static int Next(int maxValue) => random.Next(maxValue);

        public static int Next(int minValue, int maxValue) => random.Next(minValue, maxValue);

        public static float NextFloat()
        {
            return (float)random.NextDouble();
        }

        public static float NextFloat(float minValue, float maxValue)
        {
            return minValue + (float)random.NextDouble() * (maxValue - minValue);
        }

        public static Vector2 NextDirection()
        {
            float angle = NextFloat(0, MathHelper.TwoPi);
            return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        }

        public static Vector2 RandomDirection(float minAngle, float maxAngle)
        {
            float angle = NextFloat(minAngle, maxAngle);
            return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        }

        public static Vector2 RandomPosition(Rectangle bounds)
        {
            return new Vector2(
                NextFloat(bounds.X, bounds.X + bounds.Width),
                NextFloat(bounds.Y, bounds.Y + bounds.Height)
                );
        }
    }
}
