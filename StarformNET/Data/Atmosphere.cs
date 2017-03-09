namespace DLS.StarformNET.Data
{
    using System.Collections.Generic;
    using System;

    [Serializable]
    public class Atmosphere
    {
        public double SurfacePressure { get; set; }

        public Breathability Breathability { get; set; }

        public List<Gas> Composition { get; set; }

        public List<Gas> PoisonousGases { get; set; }

        public Atmosphere()
        {
            Composition = new List<Gas>();
            PoisonousGases = new List<Gas>();
        }
    }
}
