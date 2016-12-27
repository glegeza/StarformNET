namespace DLS.StarformNet.Data
{
    // TODO KnownPlanets should probably be a list? Or leaving it as a linked list might be fine too.

    public class Star
    {
        public string name         { get; set; }
        public double life { get; set; }
        public double age { get; set; }
        public double r_ecosphere { get; set; }
        public double luminosity   { get; set; }
        public double mass         { get; set; }
        public double m2           { get; set; }
        public double e            { get; set; }
        public double a            { get; set; }
        public Planet known_planets { get; set; }
        public string desig  { get; set; }
        public int    in_celestia   { get; set; }
    }
}
