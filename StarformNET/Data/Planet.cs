namespace DLS.StarformNET.Data
{
    using System;
    using System.Collections.Generic;

    // TODO break this class up
    // TODO orbit zone is supposedly no longer used anywhere. Check references and possibly remove.

    [Serializable]
    public class Planet : IEquatable<Planet>
    {
        public int Position;
        public Star Star { get; set; }
        public Atmosphere Atmosphere = new Atmosphere();

        #region Orbit data
        public double SemiMajorAxisAU
        { get; set; }

        public double Eccentricity
        { get; set; }

        public double AxialTiltDegrees
        { get; set; }

        // the 'zone' of the planet
        public int OrbitZone
        { get; set; }

        public double OrbitalPeriodDays
        { get; set; }

        public double DayLengthHours
        { get; set; }

        public double HillSphereKM
        { get; set; }
        #endregion

        #region Size & mass data
        public double MassSolarMasses
        { get; set; } 

        public double DustMass
        { get; set; }

        public double GasMass
        { get; set; }

        public double EscapeVelocityCMSec
        { get; set; }

        public double SurfaceAccelerationCMSec2
        { get; set; }

        public double SurfaceGravityG
        { get; set; }

        public double CoreRadiusKM
        { get; set; }

        public double RadiusKM
        { get; set; }

        public double DensityGCC
        { get; set; }
        #endregion

        #region Planet properties
        public PlanetType Type
        { get; set; }

        public bool IsGasGiant
        { get; set; }

        public bool IsTidallyLocked
        { get; set; }

        public bool IsEarthlike
        { get; set; }

        public bool IsHabitable
        { get; set; }

        public bool HasResonantPeriod
        { get; set; }

        public bool HasGreenhouseEffect
        { get; set; }
        #endregion

        #region Moon data
        public List<Planet> Moons
        { get; set; }

        public double MoonSemiMajorAxisAU
        { get; set; }

        public double MoonEccentricity
        { get; set; }
        #endregion

        #region Atmospheric data
        public double RMSVelocityCMSec
        { get; set; }

        public double MolecularWeightRetained
        { get; set; } // smallest molecular weight retained

        public double VolatileGasInventory
        { get; set; } 

        public double BoilingPointWaterKelvin
        { get; set; }

        public double Albedo
        { get; set; }
        #endregion

        #region Temperature data
        public double Illumination { get; set; } // units?

        public double ExosphereTempKelvin { get; set; }

        public double EstimatedTempKelvin { get; set; } // quick non-iterative estimate (K)

        public double EstimatedTerrTempKelvin { get; set; } // for terrestrial moons and the like

        public double SurfaceTempKelvin { get; set; }

        public double GreenhouseRiseKelvin { get; set; }

        public double DaytimeTempKelvin { get; set; }

        public double NighttimeTempKelvin { get; set; }

        public double MaxTempKelvin { get; set; }

        public double MinTempKelvin { get; set; }
        #endregion

        #region Surface coverage
        public double WaterCoverFraction { get; set; }

        public double CloudCoverFraction { get; set; }

        public double IceCoverFraction { get; set; }
        #endregion

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
                Utilities.AlmostEqual(AxialTiltDegrees, other.AxialTiltDegrees) &&
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
