namespace DLS.StarformNET.Data
{
    using System;
    using System.Collections.Generic;

    // TODO break this class up

    [Serializable]
    public class Planet : IEquatable<Planet>
    {
        public int Position;
        public Star Star { get; set; }
        public Atmosphere Atmosphere = new Atmosphere();

        // Orbit data
        public double SemiMajorAxisAU   { get; set; }   // semi-major axis of solar orbit (in AU)
        public double Eccentricity      { get; set; }   // eccentricity of solar orbit		 
        public double AxialTilt         { get; set; }   // units of degrees
        public int    OrbitZone         { get; set; }   // the 'zone' of the planet
        public double OrbitalPeriodDays { get; set; }   // length of the local year (days)
        public double DayLengthHours    { get; set; }   // length of the local day (hours)
        public double HillSphereKM      { get; set; }   // estimated hill sphere (km)

        // Size & mass data
        public double MassSolarMasses           { get; set; } // mass (in solar masses)			 
        public double DustMass                  { get; set; } // mass, ignoring gas				 
        public double GasMass                   { get; set; } // mass, ignoring dust
        public double EscapeVelocityCMSec       { get; set; } // units of cm/sec
        public double SurfaceAccelerationCMSec2 { get; set; } // units of cm/sec2
        public double SurfaceGravityG           { get; set; } // units of Earth gravities
        public double CoreRadiusKM              { get; set; } // radius of the rocky core (in km)
        public double RadiusKM                  { get; set; } // equatorial radius (in km)
        public double DensityGCC                { get; set; } // density (in g/cc)

        // Properties
        public bool IsGasGiant          { get; set; }
        public bool IsTidallyLocked     { get; set; }
        public bool IsEarthlike         { get; set; }
        public bool IsHabitable         { get; set; }
        public bool HasResonantPeriod   { get; set; }
        public bool HasGreenhouseEffect { get; set; }
        public PlanetType Type          { get; set; }

        // Moon data
        public List<Planet> Moons         { get; set; }
        public double MoonSemiMajorAxisAU { get; set; } // semi-major axis of lunar orbit (in AU)
        public double MoonEccentricity    { get; set; } // eccentricity of lunar orbit

        // Atmospheric data
        public double RMSVelocityCMSec        { get; set; } // units of cm/sec
        public double MolecularWeightRetained { get; set; } // smallest molecular weight retained
        public double VolatileGasInventory    { get; set; } 
        public double BoilingPointWaterKelvin { get; set; } // the boiling point of water (Kelvin)
        public double Albedo                  { get; set; } // albedo of the planet

        // Temperature data
        public double Illumination            { get; set; } // units?
        public double ExosphereTempKelvin     { get; set; } // units of degrees Kelvin
        public double EstimatedTempKelvin     { get; set; } // quick non-iterative estimate (K)
        public double EstimatedTerrTempKelvin { get; set; } // for terrestrial moons and the like
        public double SurfaceTempKelvin       { get; set; } // surface temperature in Kelvin
        public double GreenhouseRiseKelvin    { get; set; } // Temperature rise due to greenhouse
        public double DaytimeTempKelvin       { get; set; } // Day-time temperature
        public double NighttimeTempKelvin     { get; set; } // Night-time temperature
        public double MaxTempKelvin           { get; set; } // Summer/Day
        public double MinTempKelvin           { get; set; } // Winter/Night

        public double WaterCoverFraction { get; set; } // fraction of surface covered
        public double CloudCoverFraction { get; set; } // fraction of surface covered
        public double IceCoverFraction   { get; set; } // fraction of surface covered

        public Planet()
        {

        }

        public Planet(PlanetSeed seed, Star star, int num)
        {
            Star = star;
            Position = num;
            SemiMajorAxisAU = seed.SemiMajorAxisAU;
            Eccentricity = seed.Eccentricity;
            MassSolarMasses = seed.Mass;
            DustMass = seed.DustMass;
            GasMass = seed.GasMass;
            IsGasGiant = seed.IsGasGiant;
        }

        public bool Equals(Planet other)
        {
            return Position == other.Position &&
                Utilities.AlmostEqual(SemiMajorAxisAU, other.SemiMajorAxisAU) &&
                Utilities.AlmostEqual(Eccentricity, other.Eccentricity) &&
                Utilities.AlmostEqual(AxialTilt, other.AxialTilt) &&
                OrbitZone == other.OrbitZone &&
                Utilities.AlmostEqual(OrbitalPeriodDays, other.OrbitalPeriodDays) &&
                Utilities.AlmostEqual(DayLengthHours, other.DayLengthHours) &&
                Utilities.AlmostEqual(HillSphereKM, other.HillSphereKM) &&
                Utilities.AlmostEqual(MassSolarMasses, other.MassSolarMasses) &&
                Utilities.AlmostEqual(DustMass, other.DustMass) &&
                Utilities.AlmostEqual(GasMass, other.GasMass) &&
                Utilities.AlmostEqual(EscapeVelocityCMSec, other.EscapeVelocityCMSec) &&
                Utilities.AlmostEqual(SurfaceAccelerationCMSec2, other.SurfaceAccelerationCMSec2) &&
                Utilities.AlmostEqual(SurfaceGravityG, other.SurfaceGravityG) &&
                Utilities.AlmostEqual(CoreRadiusKM, other.CoreRadiusKM) &&
                Utilities.AlmostEqual(RadiusKM, other.RadiusKM) &&
                Utilities.AlmostEqual(DensityGCC, other.DensityGCC) &&
                Moons.Count == other.Moons.Count &&
                Utilities.AlmostEqual(RMSVelocityCMSec, other.RMSVelocityCMSec) &&
                Utilities.AlmostEqual(MolecularWeightRetained, other.MolecularWeightRetained) &&
                Utilities.AlmostEqual(VolatileGasInventory, other.VolatileGasInventory) &&
                Utilities.AlmostEqual(BoilingPointWaterKelvin, other.BoilingPointWaterKelvin) &&
                Utilities.AlmostEqual(Albedo, other.Albedo) &&
                Utilities.AlmostEqual(Illumination, other.Illumination) &&
                Utilities.AlmostEqual(ExosphereTempKelvin, other.ExosphereTempKelvin) &&
                Utilities.AlmostEqual(EstimatedTempKelvin, other.EstimatedTempKelvin) &&
                Utilities.AlmostEqual(EstimatedTerrTempKelvin, other.EstimatedTerrTempKelvin) &&
                Utilities.AlmostEqual(SurfaceTempKelvin, other.SurfaceTempKelvin) &&
                Utilities.AlmostEqual(GreenhouseRiseKelvin, other.GreenhouseRiseKelvin) &&
                Utilities.AlmostEqual(DaytimeTempKelvin, other.DaytimeTempKelvin) &&
                Utilities.AlmostEqual(NighttimeTempKelvin, other.NighttimeTempKelvin) &&
                Utilities.AlmostEqual(MaxTempKelvin, other.MaxTempKelvin) &&
                Utilities.AlmostEqual(MinTempKelvin, other.MinTempKelvin) &&
                Utilities.AlmostEqual(WaterCoverFraction, other.WaterCoverFraction) &&
                Utilities.AlmostEqual(CloudCoverFraction, other.CloudCoverFraction) &&
                Utilities.AlmostEqual(IceCoverFraction, other.IceCoverFraction);
        }
    }
}
