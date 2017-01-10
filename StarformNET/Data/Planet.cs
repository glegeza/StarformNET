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
        public double SemiMajorAxisAU { get; set; } // semi-major axis of solar orbit (in AU)
        public double Eccentricity { get; set; }    // eccentricity of solar orbit		 
        public double AxialTilt { get; set; }       // units of degrees
        public int OrbitZone { get; set; }          // the 'zone' of the planet
        public double OrbitalPeriod { get; set; }   // length of the local year (days)
        public double Day { get; set; }             // length of the local day (hours)
        public double HillSphere { get; set; }      // estimated hill sphere (km)

        // Size & mass data
        public double Mass { get; set; }                // mass (in solar masses)			 
        public double DustMass { get; set; }            // mass, ignoring gas				 
        public double GasMass { get; set; }             // mass, ignoring dust
        public double EscapeVelocity { get; set; }      // units of cm/sec
        public double SurfaceAcceleration { get; set; } // units of cm/sec2
        public double SurfaceGravity { get; set; }      // units of Earth gravities
        public double CoreRadius { get; set; }          // radius of the rocky core (in km)
        public double Radius { get; set; }              // equatorial radius (in km)
        public double Density { get; set; }             // density (in g/cc)

        // Properties
        public bool IsGasGiant { get; set; }
        public bool IsTidallyLocked { get; set; }
        public bool IsEarthlike { get; set; }
        public bool IsHabitable { get; set; }
        public bool HasResonantPeriod { get; set; }
        public bool HasGreenhouseEffect { get; set; }
        public PlanetType Type { get; set; }

        // Moon data
        public List<Planet> Moons { get; set; }
        public double MoonSemiMajorAxisAU { get; set; } // semi-major axis of lunar orbit (in AU)
        public double MoonEccentricity { get; set; }    // eccentricity of lunar orbit

        // Atmospheric data
        public double RMSVelocity { get; set; }             // units of cm/sec
        public double MolecularWeightRetained { get; set; } // smallest molecular weight retained
        public double VolatileGasInventory { get; set; } 
        public double BoilingPointWater { get; set; }       // the boiling point of water (Kelvin)
        public double Albedo { get; set; }                  // albedo of the planet

        // Temperature data
        public double Illumination { get; set; }      // units?
        public double ExosphereTemp { get; set; }     // units of degrees Kelvin
        public double EstimatedTemp { get; set; }     // quick non-iterative estimate (K)
        public double EstimatedTerrTemp { get; set; } // for terrestrial moons and the like
        public double SurfaceTemp { get; set; }       // surface temperature in Kelvin
        public double GreenhouseRise { get; set; }    // Temperature rise due to greenhouse
        public double DaytimeTemp { get; set; }       // Day-time temperature
        public double NighttimeTemp { get; set; }     // Night-time temperature
        public double MaxTemp { get; set; }           // Summer/Day
        public double MinTemp { get; set; }           // Winter/Night
        public double WaterCover { get; set; }        // fraction of surface covered
        public double CloudCover { get; set; }        // fraction of surface covered
        public double IceCover { get; set; }          // fraction of surface covered

        public Planet()
        {

        }

        public Planet(PlanetSeed seed, Star star, int num)
        {
            Star = star;
            Position = num;
            SemiMajorAxisAU = seed.SemiMajorAxisAU;
            Eccentricity = seed.Eccentricity;
            Mass = seed.Mass;
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
                Utilities.AlmostEqual(OrbitalPeriod, other.OrbitalPeriod) &&
                Utilities.AlmostEqual(Day, other.Day) &&
                Utilities.AlmostEqual(HillSphere, other.HillSphere) &&
                Utilities.AlmostEqual(Mass, other.Mass) &&
                Utilities.AlmostEqual(DustMass, other.DustMass) &&
                Utilities.AlmostEqual(GasMass, other.GasMass) &&
                Utilities.AlmostEqual(EscapeVelocity, other.EscapeVelocity) &&
                Utilities.AlmostEqual(SurfaceAcceleration, other.SurfaceAcceleration) &&
                Utilities.AlmostEqual(SurfaceGravity, other.SurfaceGravity) &&
                Utilities.AlmostEqual(CoreRadius, other.CoreRadius) &&
                Utilities.AlmostEqual(Radius, other.Radius) &&
                Utilities.AlmostEqual(Density, other.Density) &&
                Moons.Count == other.Moons.Count &&
                Utilities.AlmostEqual(RMSVelocity, other.RMSVelocity) &&
                Utilities.AlmostEqual(MolecularWeightRetained, other.MolecularWeightRetained) &&
                Utilities.AlmostEqual(VolatileGasInventory, other.VolatileGasInventory) &&
                Utilities.AlmostEqual(BoilingPointWater, other.BoilingPointWater) &&
                Utilities.AlmostEqual(Albedo, other.Albedo) &&
                Utilities.AlmostEqual(Illumination, other.Illumination) &&
                Utilities.AlmostEqual(ExosphereTemp, other.ExosphereTemp) &&
                Utilities.AlmostEqual(EstimatedTemp, other.EstimatedTemp) &&
                Utilities.AlmostEqual(EstimatedTerrTemp, other.EstimatedTerrTemp) &&
                Utilities.AlmostEqual(SurfaceTemp, other.SurfaceTemp) &&
                Utilities.AlmostEqual(GreenhouseRise, other.GreenhouseRise) &&
                Utilities.AlmostEqual(DaytimeTemp, other.DaytimeTemp) &&
                Utilities.AlmostEqual(NighttimeTemp, other.NighttimeTemp) &&
                Utilities.AlmostEqual(MaxTemp, other.MaxTemp) &&
                Utilities.AlmostEqual(MinTemp, other.MinTemp) &&
                Utilities.AlmostEqual(WaterCover, other.WaterCover) &&
                Utilities.AlmostEqual(CloudCover, other.CloudCover) &&
                Utilities.AlmostEqual(IceCover, other.IceCover);
        }
    }
}
