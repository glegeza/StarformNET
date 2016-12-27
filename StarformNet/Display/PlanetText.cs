
namespace DLS.StarformNet.Display
{
    using System;
    using System.Linq;
    using System.Text;
    using Data;

    public static class PlanetText
    {
        public static string GetSystemText(Planet head, ChemTable[] gases)
        {
            var sb = new StringBuilder();
            var sun = head.sun;
            sb.AppendLine(GetSunText(sun, gases));
            sb.AppendLine();

            var next = head;
            while (next != null)
            {
                sb.AppendLine(GetPlanetText(next, gases));
                sb.AppendLine();
                sb.AppendLine();
                next = next.next_planet;
            }
            return sb.ToString();
        }

        public static string GetSunText(Star star, ChemTable[] gases)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Star");
            sb.AppendLine("=========================");
            sb.AppendFormat("Luminosity: {0:0.00}", star.luminosity);
            sb.AppendLine();
            sb.AppendFormat("Mass: {0:0.00} SM", star.mass);
            sb.AppendLine();
            sb.AppendFormat("Age: {0:E2} years", star.age);
            return sb.ToString();
        }

        public static string GetPlanetText(Planet planet, ChemTable[] gases)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", GetPlanetNumber(planet), GetPlanetTypeText(planet));
            sb.AppendLine();
            sb.AppendLine("-------------------------");
            sb.AppendLine();
            sb.AppendFormat("Orbital Distance: {0}\n", GetOrbitalDistanceAU(planet));
            sb.AppendLine();
            sb.AppendFormat("Equatorial Radius: {0}\n", GetRadiusKM(planet));
            sb.AppendLine();
            sb.AppendFormat("Surface Gravity: {0}\n", GetSurfaceGravityG(planet));
            sb.AppendLine();
            sb.AppendFormat("Escape Velocity: {0}\n", GetEscapeVelocity(planet));
            sb.AppendLine();
            sb.AppendFormat("Mass: {0}\n", GetMassStringEM(planet));
            sb.AppendLine();
            sb.AppendFormat("Length of Year: {0}\n", GetOrbitalPeriodDay(planet));
            sb.AppendLine();
            sb.AppendFormat("Length of Day: {0}\n", GetLengthofDayHours(planet));
            sb.AppendLine();
            sb.AppendFormat("Average Day Temperature: {0}\n", GetDayTemp(planet));
            sb.AppendLine();
            sb.AppendFormat("Average Night Temperature: {0}\n", GetNightTemp(planet));
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
            sb.AppendFormat("Atmosphere: {0}\n", GetAtmoString(planet, gases));

            return sb.ToString();
        }

        private static string GetGreenhouseRise(Planet planet)
        {
            return String.Format("{0:0.00} F", planet.greenhs_rise * (9.0 / 5.0) - 459.67);
        }

        private static string GetEscapeVelocity(Planet planet)
        {
            return String.Format("{0:0.00} km/sec", planet.esc_velocity / GlobalConstants.CM_PER_KM);
        }

        private static string GetPlanetTypeText(Planet planet)
        {
            var sb = new StringBuilder();
            switch (planet.type)
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
                    sb.Append(planet.type);
                    break;
            }
            if (planet.one_face)
            {
                sb.Append(", Tidally Locked");
            }
            if (planet.surf_pressure > 0 && planet.greenhouse_effect)
            {
                sb.Append(", Runaway Greenhouse Effect");
            }
            switch (planet.breathability)
            {
                case Breathability.Breathable:
                    sb.Append(", Breathable Atmosphere");
                    break;
                case Breathability.Unbreathable:
                    sb.Append(", Unbreathable Atmosphere");
                    break;
                case Breathability.Poisonous:
                    sb.AppendFormat(", Poisonous Atmosphere ({0})", planet.PoisonGases[0]);
                    break;
                default:
                    sb.Append(", No Atmosphere");
                    break;
            }
            if (planet.earthlike)
            {
                sb.Append(", Earthlike");
            }
            return sb.ToString();
        }

        private static string GetMoonCount(Planet planet)
        {
            var moons = 0;
            var moon = planet.first_moon;
            while (moon != null)
            {
                moons++;
                moon = moon.next_planet;
            }
            return moons.ToString();
        }

        private static string GetSurfaceGravityG(Planet planet)
        {
            if (planet.type == PlanetType.GasGiant || planet.type == PlanetType.SubGasGiant || planet.type == PlanetType.SubSubGasGiant)
            {
                return "Oh yeah";
            }
            return String.Format("{0:0.00} G", planet.surf_grav);
        }

        static string GetHydrosphere(Planet planet)
        {
            return String.Format("{0:0.0}%", planet.hydrosphere * 100);
        }

        static string GetIceCover(Planet planet)
        {
            return String.Format("{0:0.0}%", planet.ice_cover * 100);
        }

        static string GetCloudCover(Planet planet)
        {
            return String.Format("{0:0.0}%", planet.cloud_cover * 100);
        }

        static string GetDayTemp(Planet planet)
        {
            return String.Format("{0:0.0} F", planet.high_temp * (9.0/5.0) - 459.67);
        }

        static string GetNightTemp(Planet planet)
        {
            return String.Format("{0:0.0} F", planet.low_temp * (9.0 / 5.0) - 459.67);
        }

        static string GetLengthofDayHours(Planet planet)
        {
            if (planet.day > 24 * 7)
            {
                return string.Format("{0:0.0} days ({1:0.0} hours)", planet.day / 24, planet.day);
            }
            return String.Format("{0:0.0} hours", planet.day);
        }

        static string GetOrbitalPeriodDay(Planet planet)
        {
            if (planet.orb_period > 365 * 1.5)
            {
                return String.Format("{0:0.00} ({0:0.0} days)", planet.orb_period / 365, planet.orb_period);
            }
            return String.Format("{0:0.0} days", planet.orb_period);
        }

        static string GetOrbitalDistanceAU(Planet planet)
        {
            return String.Format("{0:0.00} AU", planet.a);
        }

        static string GetPlanetNumber(Planet planet)
        {
            return String.Format("{0}.", planet.planet_no);
        }

        static string GetRadiusKM(Planet planet)
        {
            return String.Format("{0:0} km", planet.radius);
        }

        static string GetMassStringEM(Planet planet)
        {
            return String.Format("{0:0.00} EM", planet.mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES);
        }

        static string GetSurfacePressureStringAtm(Planet planet)
        {
            if (planet.type == PlanetType.GasGiant || planet.type == PlanetType.SubGasGiant || planet.type == PlanetType.SubSubGasGiant)
            {
                return "Uh, a lot";
            }
            return String.Format("{0:0.000} atm", planet.surf_pressure / GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS);
        }

        static string GetAtmoString(Planet planet, ChemTable[] gases)
        {
            if (planet.type == PlanetType.GasGiant || planet.type == PlanetType.SubGasGiant || planet.type == PlanetType.SubSubGasGiant)
            {
                return "Yes";
            }
            if (planet.gases == 0)
            {
                return "None";
            }
            var str = "";
            //var orderedGases = planet.atmosphere.Where(g => ((g.surf_pressure / planet.surf_pressure) * 100) > 0.01).OrderByDescending(g => g.surf_pressure).ToArray();
            var orderedGases = planet.atmosphere.OrderByDescending(g => g.surf_pressure).ToArray();
            if (orderedGases.Length == 0)
            {
                return "Trace gases only";
            }
            for (var i = 0; i < orderedGases.Length; i++)
            {
                var gas = orderedGases[i];
                var curGas = gases.First(g => g.num == gas.num);
                var pct = (gas.surf_pressure / planet.surf_pressure) * 100;
                str += String.Format("{0} [{1:0.00}%]", curGas.symbol, pct);
                if (i < orderedGases.Length - 1)
                {
                    str += ", ";
                }
            }
            return str;
        }
    }
}
