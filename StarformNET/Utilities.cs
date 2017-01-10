namespace DLS.StarformNET
{
    using System;

    public static class Utilities
    {
        public static Random Random = new Random();

        public static bool AlmostEqual(double v1, double v2, double diff=0.00001)
        {
            return Math.Abs(v1 - v2) <= Math.Abs(v1 * .00001);
        }

        public static void InitRandomSeed(int seed)
        {
            Random = new Random(seed);
        }

        public static double GetSemiMinorAxis(double a, double e)
        {
            return a * Math.Sqrt(1 - Math.Pow(e, 2));
        }

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

        public static double RandomNumber()
        {
            return Random.NextDouble();
        }

        public static int RandomInt(int lowerBound, int upperBound)
        {
            return Random.Next(lowerBound, upperBound);
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
            return 1.0 - Math.Pow(Random.NextDouble(), GlobalConstants.ECCENTRICITY_COEFF);
        }
    }
}
