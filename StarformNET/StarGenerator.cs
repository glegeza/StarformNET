namespace DLS.StarformNET
{
    using Data;
    using System;

    public static class StarGenerator
    {
        public static double MinSunAge = 1.0E9;
        public static double MaxSunAge = 6.0E9;

        public static Star GetDefaultStar()
        {
            var sun = new Star();

            if (sun.Mass < 0.2 || sun.Mass > 1.5)
            {
                sun.Mass = Utilities.RandomNumber(0.7, 1.4);
            }

            if (sun.Luminosity == 0)
            {
                sun.Luminosity = Environment.Luminosity(sun.Mass);
            }

            sun.EcosphereRadiusAU = Math.Sqrt(sun.Luminosity);
            sun.Life = 1.0E10 * (sun.Mass / sun.Luminosity);

            sun.AgeYears = Utilities.RandomNumber(
                MinSunAge,
                sun.Life < MaxSunAge ? sun.Life : MaxSunAge);

            return sun;
        }
    }
}
