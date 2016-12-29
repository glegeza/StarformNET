namespace DLS.StarformNet
{
    public static class UnitConversions
    {
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
            return presmmHg * GlobalConstants.MMHG_TO_MILLIBARS;
        }

        /// <summary>
        /// Converts cm to km
        /// </summary>
        /// <param name="cm">Units cm</param>
        /// <returns>Units km</returns>
        public static double CMToKM(double cm)
        {
            return cm / GlobalConstants.CM_PER_KM;
        }

        /// <summary>
        /// Converts from solar masses to earth masses
        /// </summary>
        /// <param name="sm">Mass in Solar masses</param>
        /// <returns>Mass in Earth masses</returns>
        public static double SolarMassesToEarthMasses(double sm)
        {
            return sm * GlobalConstants.SUN_MASS_IN_EARTH_MASSES;
        }

        /// <summary>
        /// Converts pressure from millibars to atm
        /// </summary>
        /// <param name="mb">Pressure in millibars</param>
        /// <returns>Pressure in atm</returns>
        public static double MillibarsToAtm(double mb)
        {
            return mb / GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS;
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
            var presPerPart1Atm = GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS / 1000000.0;
            return ppm * presPerPart1Atm * atm;
        }
    }
}
