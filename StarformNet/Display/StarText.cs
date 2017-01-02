namespace DLS.StarformNET.Display
{
    using System;
    using System.Text;
    using Data;

    public static class StarText
    {
        /// <summary>
        /// Returns a simple, multi-line string containing basic
        /// information on a star using units relative to the Sun.
        /// </summary>
        public static string GetFullStarTextRelative(Star star, bool showUnits=true)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Star");
            sb.AppendLine("=========================");
            sb.AppendFormat("Luminosity: {0}", GetLuminosityRel(star, showUnits));
            sb.AppendLine();
            sb.AppendFormat("Mass: {0}", GetMassRel(star, showUnits));
            sb.AppendLine();
            sb.AppendFormat("Age: {0}", GetAgeStringRel(star, showUnits));
            return sb.ToString();
        }

        /// <summary>
        /// Returns a string containing the age of a star relative to the Sun.
        /// </summary>
        public static string GetAgeStringRel(Star star, bool showUnits=true)
        {
            var age = String.Format("{0:0.00}", star.Age / GlobalConstants.SUN_AGE_IN_YEARS);
            var units = showUnits ? " Solar Ages" : "";
            return age + units;
        }

        /// <summary>
        /// Returns a string containing the age of a star in years, displayed
        /// using scientific notation.
        public static string GetAgeStringYearsSciN(Star star, bool showUnits=true)
        {
            var age = String.Format("{0:E2}", star.Age);
            var units = showUnits ? " Years" : "";
            return age + units;
        }

        /// <summary>
        /// Returns a string containing the luminosity of a star in Solar
        /// Luminosity units.
        /// </summary>
        public static string GetLuminosityRel(Star star, bool showUnits=true)
        {
            var lum = String.Format("{0:0.00}", star.Luminosity);
            var units = showUnits ? " Solar Luminosity" : "";
            return lum + units;
        }

        /// <summary>
        /// Returns a string containing the luminosity of a star in Solar
        /// Mass units.
        /// </summary>
        public static string GetMassRel(Star star, bool showUnits=true)
        {
            var mass = String.Format("{0:0.00}", star.Mass);
            var units = showUnits ? " Solar Masses" : "";
            return mass + units;
        }
    }
}
