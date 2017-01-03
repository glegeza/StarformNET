
namespace DLS.StarformNET.Display
{
    using System.Collections.Generic;
    using Data;

    public class SystemInfoGroup : InfoGroup
    {
        public void SetSystem(Star star, List<Planet> planets)
        {
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
                StarText.GetLuminosityRel(star),
                StarText.GetMassRel(star),
                planets.Count.ToString()
            };

            SetText(labels, values);
        }
    }
}
