namespace DLS.StarformNET.Data
{
    public class Gas
    {
        public ChemTable GasType { get; set; }
        public double surf_pressure { get; set; }

        public Gas(ChemTable gType, double pressure)
        {
            GasType = gType;
            surf_pressure = pressure;
        }
    }
}
