
namespace DLS.StarformNET.Display
{
    using System;
    using System.Linq;
    using System.Text;
    using Data;
    using System.Collections.Generic;

    public static class PlanetText
    {
        public static string GetSystemText(List<Planet> planets)
        {
            var sb = new StringBuilder();
            var sun = planets[0].Star;
            sb.AppendLine(StarText.GetFullStarTextRelative(sun, true));
            sb.AppendLine();

            foreach (var p in planets)
            {
                sb.AppendLine(GetPlanetText(p));
                sb.AppendLine();
                sb.AppendLine();
            }
            return sb.ToString();
        }
        public static string GetPlanetText(Planet planet)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", GetPlanetNumber(planet), GetPlanetTypeText(planet));
            sb.AppendLine();
            sb.AppendLine("-------------------------");
            sb.AppendLine();
            sb.AppendFormat("Orbital Distance: {0}\n", GetOrbitalDistanceAU(planet));
            sb.AppendLine();
            sb.AppendFormat("Equatorial Radius: {0}\n", GetRadiusER(planet));
            sb.AppendLine();
            sb.AppendFormat("Surface Gravity: {0}\n", GetSurfaceGravityG(planet));
            sb.AppendLine();
            sb.AppendFormat("Escape Velocity: {0}\n", GetEscapeVelocity(planet));
            sb.AppendLine();
            sb.AppendFormat("Mass: {0}\n", GetMassStringEM(planet));
            sb.AppendLine();
            sb.AppendFormat("Density: {0}\n", GetDensity(planet));
            sb.AppendLine();
            sb.AppendFormat("Length of Year: {0}\n", GetOrbitalPeriodDay(planet));
            sb.AppendLine();
            sb.AppendFormat("Length of Day: {0}\n", GetLengthofDayHours(planet));
            sb.AppendLine();
            sb.AppendFormat("Average Day Temperature: {0}\n", GetDayTemp(planet));
            sb.AppendLine();
            sb.AppendFormat("Average Night Temperature: {0}\n", GetNightTemp(planet));
            sb.AppendLine();
            sb.AppendFormat("Boiling Point: {0}\n", GetBoilingPoint(planet));
            sb.AppendLine();
            sb.AppendFormat("Greenhouse Rise: {0}\n", GetGreenhouseRise(planet));
            sb.AppendLine();
            sb.AppendFormat("Water Cover: {0}\n", GetHydrosphere(planet));
            sb.AppendLine();
            sb.AppendFormat("Ice Cover: {0}\n", GetIceCover(planet));
            sb.AppendLine();
            sb.AppendFormat("Cloud Cover: {0}\n", GetCloudCover(planet));
            sb.AppendLine();
            sb.AppendFormat("Surface Pressure: {0}\n", GetSurfacePressureStringAtm(planet));
            sb.AppendLine();
            sb.AppendFormat("Atmospheric Composition (Percentage): {0}\n", GetAtmoString(planet));
            sb.AppendLine();
            sb.AppendFormat("Atmospheric Composition (Partial Pressure): {0}\n", GetAtmoStringPP(planet));

            return sb.ToString();
        }

        public static string GetDensity(Planet planet)
        {
            return String.Format("{0:0.00} g/cm3", planet.DensityGCC);
        }

        public static string GetBoilingPoint(Planet planet)
        {
            if (planet.Type == PlanetType.GasGiant || planet.Type == PlanetType.SubGasGiant || planet.Type == PlanetType.SubSubGasGiant)
            {
                return "-";
            }
            return String.Format("{0:0.00} F", UnitConversions.KelvinToFahrenheit(planet.BoilingPointWaterKelvin));
        }

        public static string GetGreenhouseRise(Planet planet)
        {
            return String.Format("{0:0.00} F", UnitConversions.KelvinToFahrenheit(planet.GreenhouseRiseKelvin));
        }

        public static string GetEscapeVelocity(Planet planet)
        {
            return String.Format("{0:0.00} km/sec", UnitConversions.CMToKM(planet.EscapeVelocityCMSec));
        }

        public static string GetPlanetTypeText(Planet planet)
        {
            var sb = new StringBuilder();
            switch (planet.Type)
            {
                case PlanetType.GasGiant:
                    sb.Append("Gas Giant");
                    break;
                case PlanetType.SubGasGiant:
                    sb.Append("Small Gas Giant");
                    break;
                case PlanetType.SubSubGasGiant:
                    sb.Append("Gas Dwarf");
                    break;
                default:
                    sb.Append(planet.Type);
                    break;
            }
            if (planet.HasResonantPeriod)
            {
                sb.Append(", Resonant Orbital Period");
            }
            if (planet.IsTidallyLocked)
            {
                sb.Append(", Tidally Locked");
            }
            if (planet.Atmosphere.SurfacePressure > 0 && planet.HasGreenhouseEffect)
            {
                sb.Append(", Runaway Greenhouse Effect");
            }
            switch (planet.Atmosphere.Breathability)
            {
                case Breathability.Breathable:
                case Breathability.Unbreathable:
                case Breathability.Poisonous:
                    sb.AppendFormat(", {0} Atmosphere", planet.Atmosphere.Breathability);
                    break;
                default:
                    sb.Append(", No Atmosphere");
                    break;
            }
            if (planet.IsEarthlike)
            {
                sb.Append(", Earthlike");
            }
            return sb.ToString();
        }

        public static string GetSurfaceGravityG(Planet planet)
        {
            if (planet.Type == PlanetType.GasGiant || planet.Type == PlanetType.SubGasGiant || planet.Type == PlanetType.SubSubGasGiant)
            {
                return "Oh yeah";
            }
            return String.Format("{0:0.00} G", planet.SurfaceGravityG);
        }

        public static string GetHydrosphere(Planet planet)
        {
            return String.Format("{0:0.0}%", planet.WaterCoverFraction * 100);
        }

        public static string GetIceCover(Planet planet)
        {
            return String.Format("{0:0.0}%", planet.IceCoverFraction * 100);
        }

        public static string GetCloudCover(Planet planet)
        {
            return String.Format("{0:0.0}%", planet.CloudCoverFraction * 100);
        }

        public static string GetDayTemp(Planet planet)
        {
            return String.Format("{0:0.0} F", UnitConversions.KelvinToFahrenheit(planet.DaytimeTempKelvin));
        }

        public static string GetNightTemp(Planet planet)
        {
            return String.Format("{0:0.0} F", UnitConversions.KelvinToFahrenheit(planet.NighttimeTempKelvin));
        }

        public static string GetExoTemp(Planet planet)
        {
            return String.Format("{0:0.0} K", planet.ExosphereTempKelvin);
        }

        public static string GetEstimatedHillSphereKM(Planet planet)
        {
            return String.Format("{0:n0} km", planet.HillSphereKM);
        }

        public static string GetLengthofDayHours(Planet planet)
        {
            if (planet.DayLengthHours > 24 * 7)
            {
                return string.Format("{0:0.0} days ({1:0.0} hours)", planet.DayLengthHours / 24, planet.DayLengthHours);
            }
            return String.Format("{0:0.0} hours", planet.DayLengthHours);
        }

        public static string GetOrbitalPeriodDay(Planet planet)
        {
            if (planet.OrbitalPeriodDays > 365 * 1.5)
            {
                return String.Format("{0:0.00} ({0:0.0} days)", planet.OrbitalPeriodDays / 365, planet.OrbitalPeriodDays);
            }
            return String.Format("{0:0.0} days", planet.OrbitalPeriodDays);
        }

        public static string GetOrbitalEccentricity(Planet planet)
        {
            return String.Format("{0:0.00}", planet.Eccentricity);
        }

        public static string GetOrbitalDistanceAU(Planet planet)
        {
            return String.Format("{0:0.00} AU", planet.SemiMajorAxisAU);
        }

        public static string GetPlanetNumber(Planet planet)
        {
            return String.Format("{0}.", planet.Position);
        }

        public static string GetRadiusKM(Planet planet)
        {
            return String.Format("{0:0} km", planet.RadiusKM);
        }

        public static string GetRadiusER(Planet planet)
        {
            return String.Format("{0:0.00} ER", planet.RadiusKM / GlobalConstants.KM_EARTH_RADIUS);
        }

        public static string GetMassStringEM(Planet planet)
        {
            return String.Format("{0:0.00} EM", UnitConversions.SolarMassesToEarthMasses(planet.MassSM));
        }

        public static string GetSurfacePressureStringAtm(Planet planet)
        {
            if (planet.Type == PlanetType.GasGiant || planet.Type == PlanetType.SubGasGiant || planet.Type == PlanetType.SubSubGasGiant)
            {
                return "Uh, a lot";
            }
            return String.Format("{0:0.000} atm", UnitConversions.MillibarsToAtm(planet.Atmosphere.SurfacePressure));
        }

        public static string GetAtmoStringPP(Planet planet)
        {
            if (planet.Type == PlanetType.GasGiant || planet.Type == PlanetType.SubGasGiant || planet.Type == PlanetType.SubSubGasGiant)
            {
                return "Yes";
            }
            if (planet.Atmosphere.Composition.Count == 0)
            {
                return "None";
            }
            var str = "";
            var orderedGases = planet.Atmosphere.Composition.OrderByDescending(g => g.surf_pressure).ToArray();
            if (orderedGases.Length == 0)
            {
                return "Trace gases only";
            }
            for (var i = 0; i < orderedGases.Length; i++)
            {
                var gas = orderedGases[i];
                var curGas = gas.GasType;
                str += String.Format("{0} [{1:0.0000} mb]", curGas.symbol, gas.surf_pressure);
                if (i < orderedGases.Length - 1)
                {
                    str += ", ";
                }
            }
            return str;
        }

        public static string GetPoisonString(Planet planet)
        {
            var str = "";
            var orderedGases = planet.Atmosphere.PoisonousGases.OrderByDescending(g => g.surf_pressure).ToList();
            for (var i = 0; i < orderedGases.Count; i++)
            {
                if (orderedGases[i].surf_pressure > 1)
                {
                    str += String.Format("{0:0.0000}mb {1}", orderedGases[i].surf_pressure, orderedGases[i].GasType.symbol);
                }
                else
                {
                    var ppm = UnitConversions.MillibarsToPPM(orderedGases[i].surf_pressure);
                    str += String.Format("{0:0.0000}ppm {1}", ppm, orderedGases[i].GasType.symbol);
                }
                if (i < orderedGases.Count - 1)
                {
                    str += ", ";
                }
            }
            return str;
        }

        public static string GetAtmoString(Planet planet, double minFraction = 1.0)
        {
            if (planet.Type == PlanetType.GasGiant || planet.Type == PlanetType.SubGasGiant || planet.Type == PlanetType.SubSubGasGiant)
            {
                return "Yes";
            }
            if (planet.Atmosphere.Composition.Count == 0)
            {
                return "None";
            }
            if (planet.Atmosphere.SurfacePressure < 0.0005)
            {
                return "Almost None";
            }

            var str = "";
            var orderedGases = planet.Atmosphere.Composition.Where(g => ((g.surf_pressure / planet.Atmosphere.SurfacePressure) * 100) > minFraction).OrderByDescending(g => g.surf_pressure).ToArray();
            for (var i = 0; i < orderedGases.Length; i++)
            {
                var gas = orderedGases[i];
                var curGas = gas.GasType;
                var pct = (gas.surf_pressure / planet.Atmosphere.SurfacePressure) * 100;
                str += String.Format("{0:0.0}% {1}", pct, curGas.symbol);
                if (i < orderedGases.Length - 1)
                {
                    str += ", ";
                }
            }
            if (orderedGases.Length < planet.Atmosphere.Composition.Count)
            {
                var traceGasSum = 0.0;
                foreach (var gas in planet.Atmosphere.Composition)
                {
                    var frac = (gas.surf_pressure / planet.Atmosphere.SurfacePressure) * 100;
                    if (frac <= minFraction)
                    {
                        traceGasSum += frac;
                    }
                }
                if (traceGasSum > 0.05)
                {
                    str += String.Format(", {0:0.0}% trace gases", traceGasSum);
                }
            }
            return str;
        }
    }
}
