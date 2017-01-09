namespace DLS.StarformNET.Data
{
    public class PlanetSeed
    {
        public PlanetSeed NextPlanet = null;
        public PlanetSeed FirstMoon = null;

        public double SemiMajorAxisAU;
        public double Eccentricity;
        public double Mass;
        public double DustMass;
        public double GasMass;
        public bool IsGasGiant = false;

        public PlanetSeed(double a, double e, double mass, double dMass, double gMass)
        {
            SemiMajorAxisAU = a;
            Eccentricity = e;
            Mass = mass;
            DustMass = dMass;
            GasMass = gMass;
        }
    }
}
