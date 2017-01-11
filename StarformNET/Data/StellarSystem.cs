namespace DLS.StarformNET.Data
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class StellarSystem
    {
        public string Name { get; set; }
        public Star Star { get; set; }
        public List<Planet> Planets { get; set; }
        public SystemGenerationOptions Options { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
