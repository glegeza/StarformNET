namespace DLS.StarformNET.Data
{
    using System;

    // TODO units?
    // UGLY Not comfortable with binary systems just having a second mass value

    [Serializable]
    public class Star
    {
        public string Name { get; set; }
        public double Life { get; set; }
        public double Age { get; set; }                   // Years
        public double EcosphereRadius { get; set; }
        public double Luminosity { get; set; }            // Sun = 1.0
        public double Mass { get; set; }                  // Sun = 1.0
        public double BinaryMass { get; set; }
        public double SemiMajorAxis { get; set; }
        public double Eccentricity { get; set; }
    }
}
