namespace DLS.StarformNET.Data
{
    using System;

    [Serializable]
    public class Gas
    {
        public ChemType GasType { get; set; }
        public double surf_pressure { get; set; }

        public Gas(ChemType gType, double pressure)
        {
            GasType = gType;
            surf_pressure = pressure;
        }
    }
}
