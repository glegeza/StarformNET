using System.Collections.Generic;

namespace DLS.StarformNet.Data
{
    // TODO Create orbit class to store orbit parameters
    // TODO Switch fields to properties
    // TODO Class for temperature and other climate data?

    public class Planet
    {
        public Planet()
        {
            PoisonGases = new List<string>();
        }

        public int planet_no;
        public double a                            { get; set; } // semi-major axis of solar orbit (in AU)
        public double e                            { get; set; } // eccentricity of solar orbit		 
        public double axial_tilt                    { get; set; } // units of degrees					 
        public double mass                         { get; set; } // mass (in solar masses)			 
        public bool gas_giant                       { get; set; } // TRUE if the planet is a gas giant 
        public double dust_mass                     { get; set; } // mass, ignoring gas				 
        public double gas_mass                      { get; set; } // mass, ignoring dust
        public bool one_face { get; set; }		 
        public bool earthlike { get; set; }
        public Breathability breathability { get; set; }
        public List<string> PoisonGases { get; set; }
        
        //   ZEROES start here               
        public double moon_a                        { get; set; } // semi-major axis of lunar orbit (in AU)
        public double moon_e                        { get; set; } // eccentricity of lunar orbit		 
        public double core_radius                   { get; set; } // radius of the rocky core (in km)
        public double radius                       { get; set; } // equatorial radius (in km)
        public int    orbit_zone                    { get; set; } // the 'zone' of the planet			 
        public double density                      { get; set; } // density (in g/cc)
        public double orb_period                { get; set; } // length of the local year (days)
        public double day                          { get; set; } // length of the local day (hours)
        public bool   resonant_period               { get; set; } // TRUE if in resonant rotation
        public double esc_velocity               { get; set; } // units of cm/sec		 
        public double surf_accel          { get; set; } // units of cm/sec2
        public double surf_grav               { get; set; } // units of Earth gravities
        public double rms_velocity                  { get; set; } // units of cm/sec
        public double molec_weight              { get; set; } // smallest molecular weight retained
        public double volatile_gas_inventory         { get; set; } 
        public double surf_pressure              { get; set; } // units of millibars (mb)
        public bool   greenhouse_effect             { get; set; } // runaway greenhouse effect?
        public double boil_point                 { get; set; } // the boiling point of water (Kelvin)
        public double albedo                       { get; set; } // albedo of the planet
        public double exospheric_temp               { get; set; } // units of degrees Kelvin
        public double estimated_temp                { get; set; } // quick non-iterative estimate (K)
        public double estimated_terr_temp     { get; set; } // for terrestrial moons and the like
        public double surf_temp                  { get; set; } // surface temperature in Kelvin
        public double greenhs_rise               { get; set; } // Temperature rise due to greenhouse
        public double high_temp                     { get; set; } // Day-time temperature
        public double low_temp                      { get; set; } // Night-time temperature
        public double max_temp                      { get; set; } // Summer/Day
        public double min_temp                      { get; set; } // Winter/Night
        public double hydrosphere                  { get; set; } // fraction of surface covered
        public double cloud_cover                   { get; set; } // fraction of surface covered
        public double ice_cover                     { get; set; } // fraction of surface covered
        public int    gases                        { get; set; } // Count of gases in the atmosphere:
        public Gas[]  atmosphere                   { get; set; } 
        public PlanetType type                     { get; set; }

        public Star sun                             { get; set; } 
        public int minor_moons                      { get; set; } 
        public Planet first_moon                    { get; set; }
        //   ZEROES end here               

        public Planet next_planet                   { get; set; }
    }
}
