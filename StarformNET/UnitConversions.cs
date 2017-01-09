namespace DLS.StarformNET
{
    /// <summary>
    /// Simple helper methods for converting between different unit types.
    /// </summary>
    public static class UnitConversions
    {
        public static double MB_IN_MMHG = 1.3332239;
        public static double CM_PER_KM = 1.0E5;
        public static double EARTH_SURF_PRES_IN_MILLIBARS = 1013.25;
        public static double SUN_MASS_IN_EARTH_MASSES = 332775.64;

        /// <summary>
        /// Converts temperature from Kelvin to Fahrenheit degrees.
        /// </summary>
        /// <param name="tempK">Temperature in Kelvin</param>
        /// <returns>Temperature in Fahrenheit degrees</returns>
        public static double KelvinToFahrenheit(double tempK)
        {
            return tempK * (9.0 / 5.0) - 459.67;
        }

        /// <summary>
        /// Converts pressure from mmHg to millibars
        /// </summary>
        /// <param name="presmmHg">Pressure in mmHg</param>
        /// <returns><Pressure in millibars/returns>
        public static double MMHGToMillibars(double presmmHg)
        {
            return presmmHg * MB_IN_MMHG;
        }

        /// <summary>
        /// Converts cm to km
        /// </summary>
        /// <param name="cm">Units cm</param>
        /// <returns>Units km</returns>
        public static double CMToKM(double cm)
        {
            return cm / CM_PER_KM;
        }

        /// <summary>
        /// Converts from solar masses to earth masses
        /// </summary>
        /// <param name="sm">Mass in Solar masses</param>
        /// <returns>Mass in Earth masses</returns>
        public static double SolarMassesToEarthMasses(double sm)
        {
            return sm * SUN_MASS_IN_EARTH_MASSES;
        }

        /// <summary>
        /// Converts pressure from millibars to atm
        /// </summary>
        /// <param name="mb">Pressure in millibars</param>
        /// <returns>Pressure in atm</returns>
        public static double MillibarsToAtm(double mb)
        {
            return mb / EARTH_SURF_PRES_IN_MILLIBARS;
        }

        /// <summary>
        /// Calculates the partial pressure of an atmospheric gas in millibars
        /// from the concentration given in parts-per-million.
        /// </summary>
        /// <param name="ppm">Concentration in parts-per-million.</param>
        /// <param name="atm">Total pressure of the gas in atm (default=1)</param>
        /// <returns>Partial pressure in millibars</returns>
        public static double PPMToMillibars(double ppm, double atm=1.0)
        {
            var pct = ppm / 1000000.0;
            var presPerPart1Atm = EARTH_SURF_PRES_IN_MILLIBARS / 1000000.0;
            return pct * EARTH_SURF_PRES_IN_MILLIBARS * atm;
        }

        /// <summary>
        /// Converts partial pressure in millibars to PPM at a specific
        /// atmospheric pressure.
        /// </summary>
        /// <param name="mb">Gas partial pressure in millibars</param>
        /// <param name="atm">Atmospheric pressure in atm</param>
        /// <returns>Pressure in PPM</returns>
        public static double MillibarsToPPM(double mb, double atm=1.0)
        {
            return (mb / (EARTH_SURF_PRES_IN_MILLIBARS * atm)) * 1000000;
        }
    }
}
