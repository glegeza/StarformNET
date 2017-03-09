namespace DLS.StarformNET
{
    // TODO should maybe consider doing some sanity checking for negative distances and mass?
    // maybe? I dunno. Nah.

    /// <summary>
    /// Simple helper methods for converting between different unit types.
    /// </summary>
    public static class UnitConversions
    {
        public static double MB_IN_MMHG = 1.3332239;
        public static double CM_PER_KM = 1.0E5;
        public static double EARTH_SURF_PRES_IN_MILLIBARS = 1013.25;
        public static double SUN_MASS_IN_EARTH_MASSES = 332775.64;
        public static double SOLAR_MASS_IN_GRAMS = 1.989E33;
        public static double SOLAR_MASS_IN_KILOGRAMS = 1.989E30;
        public static double EARTH_MASS_IN_GRAMS = 5.977E27;
        public static double EARTH_DENSITY_IN_GCC = 5.52;
        public static double EARTH_RADIUS_IN_CM = 6.3714E8;
        public static double EARTH_RADIUS_IN_KM = 6371.393;

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
        /// Converts from units of Earth radius (R⊕) to centimeters.
        /// </summary>
        /// <param name="r">Distance in Earth radius units.</param>
        /// <returns>Distance in centimeters</returns>
        public static double EarthRadiusToCentimeters(double r)
        {
            return r * EARTH_RADIUS_IN_CM;
        }

        /// <summary>
        /// Converts from units of Earth radius (R⊕) to kilometers.
        /// </summary>
        /// <param name="r">Distance in Earth radius units.</param>
        /// <returns>Distance in kilometers</returns>
        public static double EarthRadiusToKilometers(double r)
        {
            return r * EARTH_RADIUS_IN_KM;
        }

        /// <summary>
        /// Converts from centimeters to Earth radius units (R⊕).
        /// </summary>
        /// <param name="cm">Distance in centimeters</param>
        /// <returns>Distance in Earth radius (1.0 = Radius of Earth)</returns>
        public static double CentimetersToEarthRadius(double cm)
        {
            return cm / EARTH_RADIUS_IN_CM;
        }

        /// <summary>
        /// Converts from kilometers to Earth radius units (R⊕).
        /// </summary>
        /// <param name="km"></param>
        /// <returns>Distance in Earth radius (1.0 = radius of Earth)</returns>
        public static double KilometersToEarthRadius(double km)
        {
            return km / EARTH_RADIUS_IN_KM;
        }

        /// <summary>
        /// Converts from solar masses to units of Earth mass (M⊕).
        /// </summary>
        /// <param name="sm">Mass in solar masses</param>
        /// <returns>Mass in Earth masses (1.0 = mass of Earth)</returns>
        public static double SolarMassesToEarthMasses(double sm)
        {
            return sm * SUN_MASS_IN_EARTH_MASSES;
        }

        /// <summary>
        /// Converts from units of solar mass (M☉) to kilograms.
        /// </summary>
        /// <param name="sm">Mass in solar masses</param>
        /// <returns>Mass in kilograms</returns>
        public static double SolarMassesToKilograms(double sm)
        {
            return sm * SOLAR_MASS_IN_KILOGRAMS;
        }

        /// <summary>
        /// Converts from units of solar mass (M☉) to grams.
        /// </summary>
        /// <param name="sm">Mass in solar masses</param>
        /// <returns>Mass in grams</returns>
        public static double SolarMassesToGrams(double sm)
        {
            return sm * SOLAR_MASS_IN_GRAMS;
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
