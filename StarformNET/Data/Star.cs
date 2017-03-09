namespace DLS.StarformNET.Data
{
    using System;

    // TODO units?
    // UGLY Not comfortable with binary systems just having a second mass value

    [Serializable]
    public class Star
    {
        public string Name { get; set; }

        /// <summary>
        /// Age of the star in years.
        /// </summary>
        public double AgeYears { get; set; }

        /// <summary>
        /// The maximum lifetime of the star in years.
        /// </summary>
        public double Life { get; set; }

        /// <summary>
        /// The distance that the star's "ecosphere" (as far as I can tell,
        /// ye olden science speak for circumstellar habitable zone) is
        /// centered on. Given in AU. 
        /// </summary>
        public double EcosphereRadiusAU { get; set; }

        /// <summary>
        /// Luminosity of the star in solar luminosity units (L<sub>☉</sub>).
        /// The luminosity of the sun is 1.0.
        /// </summary>
        public double Luminosity { get; set; }

        /// <summary>
        /// Mass of the star in solar mass units (M<sub>☉</sub>). The mass of
        /// the sun is 1.0.
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// The mass of this star's companion star (if any) in solar mass
        /// units (M<sub>☉</sub>). 
        /// </summary>
        public double BinaryMass { get; set; }

        /// <summary>
        /// The semi-major axis of the companion star in au.
        /// </summary>
        public double SemiMajorAxisAU { get; set; }

        /// <summary>
        /// The eccentricity of the companion star's orbit.
        /// </summary>
        public double Eccentricity { get; set; }
    }
}
