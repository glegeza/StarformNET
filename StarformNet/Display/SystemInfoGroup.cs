
namespace DLS.StarformNET.Display
{
    using System.Collections.Generic;
    using Data;

    public class SystemInfoGroup : InfoGroup
    {
        public void SetSystem(List<Planet> planets)
        {
            if (planets == null || planets[0] == null)
            {
                return;
            }

            var star = planets[0].Star;

            var labels = new List<string>()
            {
                "Star Age:",
                "Star Luminosity:",
                "Star Mass:",
                "Planets:"
            };

            var values = new List<string>()
            {
                StarText.GetAgeStringYearsSciN(star),
                StarText.GetLuminosityPercent(star),
                StarText.GetMassPercent(star),
                planets.Count.ToString()
            };

            SetText(labels, values);
        }
    }
}
