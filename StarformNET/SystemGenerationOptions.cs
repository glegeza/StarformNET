namespace DLS.StarformNET
{
    using Data;

    public class SystemGenerationOptions
    {
        public static SystemGenerationOptions DefaultOptions = new SystemGenerationOptions();

        public double MinSunAge = 1.0E9;
        public double MaxSunAge = 6.0E9;

        public double DustDensityCoeff = GlobalConstants.DUST_DENSITY_COEFF;
        public double CloudEccentricity = GlobalConstants.CLOUD_ECCENTRICITY;
        public double GasDensityRatio = GlobalConstants.K;

        public ChemType[] GasTable = new ChemType[0];
    }
}
