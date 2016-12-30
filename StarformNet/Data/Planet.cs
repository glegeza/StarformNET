namespace DLS.StarformNET.Data
{
    // TODO break this class up

    public class Planet
    {
        public int Position;
        public Star Star { get; set; }
        public Planet NextPlanet { get; set; } // this should be considered deprecated

        // Orbit data
        public double SemiMajorAxisAU { get; set; } // semi-major axis of solar orbit (in AU)
        public double Eccentricity { get; set; }    // eccentricity of solar orbit		 
        public double AxialTilt { get; set; }       // units of degrees
        public int OrbitZone { get; set; }          // the 'zone' of the planet
        public double OrbitalPeriod { get; set; }   // length of the local year (days)
        public double Day { get; set; }             // length of the local day (hours)

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
        public int MinorMoonCount { get; set; }
        public Planet FirstMoon { get; set; }
        public double MoonSemiMajorAxisAU { get; set; } // semi-major axis of lunar orbit (in AU)
        public double MoonEccentricity { get; set; }    // eccentricity of lunar orbit

        // Atmospheric data
        public double RMSVelocity { get; set; }             // units of cm/sec
        public double MolecularWeightRetained { get; set; } // smallest molecular weight retained
        public double VolatileGasInventory { get; set; } 
        public double SurfPressure { get; set; }            // units of millibars (mb)
        public double BoilingPointWater { get; set; }       // the boiling point of water (Kelvin)
        public double Albedo { get; set; }                  // albedo of the planet
        public int GasCount { get; set; }                   // Count of gases in the atmosphere:
        public Gas[] AtmosphericGases { get; set; }
        public Breathability breathability { get; set; }

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
    }
}
