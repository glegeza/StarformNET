namespace DLS.StarformNET.Display
{
    using Data;
    using System.Collections.Generic;

    public class PlanetInfoGroup : InfoGroup
    {
        public void SetPlanet(Planet planet, ChemType[] gases)
        {
            var labels = new List<string>()
            {
                "Traits:",
                "Orbital Distance:",
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
                "Surface Pressure",
                "Atmosphere:"
            };

            var values = new List<string>()
            {
                PlanetText.GetPlanetTypeText(planet),
                PlanetText.GetOrbitalDistanceAU(planet),
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
                PlanetText.GetSurfacePressureStringAtm(planet),
                PlanetText.GetAtmoString(planet, gases)
            };

            SetText(labels, values);
        }
    }
}
