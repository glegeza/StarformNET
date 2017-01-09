namespace DLS.StarformNET.Data
{
    public class Generation
    {
        public DustRecord Dusts { get; set; }
        public PlanetSeed Planets { get; set; }
        public Generation Next { get; set; }
    }
}
