namespace DLS.StarformNET
{
    using System;

    public static class Utilities
    {
        public static Random Random = new Random();

        public static void InitWithSeed(int seed)
        {
            Random = new Random(seed);
        }

        public static double Pow2(double a)
        {
            return a * a;
        }

        public static double Pow3(double a)
        {
            return a * a * a;
        }

        public static double Pow4(double a)
        {
            return a * a * a * a;
        }

        public static double Pow1_4(double a)
        {
            return Math.Sqrt(Math.Sqrt(a));
        }

        public static double Pow1_3(double a)
        {
            return Math.Pow(a, (1.0 / 3.0));
        }

        public static double RandomNumber(double inner, double outer)
        {
            var range = outer - inner;
            return Random.NextDouble() * range + inner;
        }

        public static double About(double value, double variation)
        {
            return (value + (value * RandomNumber(-variation, variation)));
        }

        public static double RandomEccentricity()
        {
            double e;

            e = 1.0 - Math.Pow(RandomNumber(0.0, 1.0), GlobalConstants.ECCENTRICITY_COEFF);

            if (e > .99)    // Note that this coresponds to a random
            {
                e = .99;    // number less than 10E-26
                            // It happens with GNU C for -S254 -W27
            }
            return (e);
        }
    }
}
