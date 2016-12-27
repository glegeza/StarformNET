namespace DLS.StarformNet.Data
{
    public class DustRecord
    {
        public double InnerEdge;
        public double OuterEdge;
        public bool DustPresent;
        public bool GasPresent;
        public DustRecord NextBand;
    }
}
