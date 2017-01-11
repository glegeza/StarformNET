namespace DLS.StarformNET.Display
{
    using Data;
    using System.Collections.Generic;

    public class PlanetInfoGroup : InfoGroup
    {
        public void SetPlanet(Planet planet)
        {
            var labels = new List<string>()
            {
                "Traits:",
                "Orbital Distance:",
                "Orbital Eccentricity:",
                "Estimated Hill Sphere:",
                "Equatorial Radius:",
                "Surface Gravity:",
                "Escape Velocity:",
                "Mass:",
                "Density:",
                "Length of Year:",
                "Length of Day:",
                "Average Day Temperature:",
                "Average Night Temperature:",
                "Boiling Point",
                "Water Cover",
                "Ice Cover",
                "Cloud Cover",
                "Moons:",
                "Surface Pressure",
                "Atmosphere:"
            };

            var values = new List<string>()
            {
                PlanetText.GetPlanetTypeText(planet),
                PlanetText.GetOrbitalDistanceAU(planet),
                PlanetText.GetOrbitalEccentricity(planet),
                PlanetText.GetEstimatedHillSphereKM(planet),
                PlanetText.GetRadiusER(planet),
                PlanetText.GetSurfaceGravityG(planet),
                PlanetText.GetEscapeVelocity(planet),
                PlanetText.GetMassStringEM(planet),
                PlanetText.GetDensity(planet),
                PlanetText.GetOrbitalPeriodDay(planet),
                PlanetText.GetLengthofDayHours(planet),
                PlanetText.GetDayTemp(planet),
                PlanetText.GetNightTemp(planet),
                PlanetText.GetBoilingPoint(planet),
                PlanetText.GetHydrosphere(planet),
                PlanetText.GetIceCover(planet),
                PlanetText.GetCloudCover(planet),
                planet.Moons.Count.ToString(),
                PlanetText.GetSurfacePressureStringAtm(planet),
                PlanetText.GetAtmoString(planet)
            };

            if (planet.Atmosphere.Breathability == Breathability.Poisonous)
            {
                labels.Add("Poison Gases:");
                values.Add(PlanetText.GetPoisonString(planet));
            }

            SetText(labels, values);
        }
    }
}
