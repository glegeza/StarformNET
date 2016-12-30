namespace DLS.StarformNET.Data
{
    // TODO KnownPlanets should probably be a list? Or leaving it as a linked list might be fine too.
    // TODO units?
    // UGLY Not comfortable with binary systems just having a second mass value

    public class Star
    {
        public string Name { get; set; }
        public double Life { get; set; }
        public double Age { get; set; }
        public double EcosphereRadius { get; set; }
        public double Luminosity { get; set; }
        public double Mass { get; set; }
        public double BinaryMass { get; set; }
        public double SemiMajorAxis { get; set; }
        public double Eccentricity { get; set; }
    }
}
