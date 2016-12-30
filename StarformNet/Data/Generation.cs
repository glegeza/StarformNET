namespace DLS.StarformNET.Data
{
    public class Generation
    {
        public DustRecord Dusts { get; set; }
        public Planet     Planets { get; set; }
        public Generation Next { get; set; }
    }
}
