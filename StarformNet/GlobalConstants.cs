namespace DLS.StarformNET
{
    using System;

    public static class GlobalConstants
    {
        public static double RADIANS_PER_ROTATION         = (2.0 * Math.PI);

        public static double SUN_AGE_IN_YEARS             = (4600000000);
        public static double ECCENTRICITY_COEFF           = (0.077);                       // Dole's was 0.077			
        public static double PROTOPLANET_MASS             = (1.0E-15);                     // Units of solar masses	
        public static double CHANGE_IN_EARTH_ANG_VEL      = (-1.3E-15);                    // Units of radians/sec/year
        public static double SOLAR_MASS_IN_GRAMS          = (1.989E33);                    // Units of grams			
        public static double SOLAR_MASS_IN_KILOGRAMS      = (1.989E30);                    // Units of kg				
        public static double EARTH_MASS_IN_GRAMS          = (5.977E27);                    // Units of grams			
        public static double EARTH_RADIUS                 = (6.378E8);                     // Units of cm				
        public static double EARTH_DENSITY                = (5.52);                        // Units of g/cc			
        public static double KM_EARTH_RADIUS              = (6378.0);                      // Units of km				
        public static double EARTH_ACCELERATION           = (980.7);                       // Units of cm/sec2 (was 981.0)
        public static double EARTH_AXIAL_TILT             = (23.4);                        // Units of degrees			
        public static double EARTH_EXOSPHERE_TEMP         = (1273.0);                      // Units of degrees Kelvin	
        public static double SUN_MASS_IN_EARTH_MASSES     = (332775.64);
        public static double ASTEROID_MASS_LIMIT          = (0.001);                       // Units of Earth Masses	
        public static double EARTH_EFFECTIVE_TEMP         = (250.0);                       // Units of degrees Kelvin (was 255);	
        public static double CLOUD_COVERAGE_FACTOR        = (1.839E-8);                    // Km2/kg					
        public static double EARTH_WATER_MASS_PER_AREA    = (3.83E15);                     // grams per square km		
        public static double EARTH_SURF_PRES_IN_MILLIBARS = (1013.25);
        public static double EARTH_SURF_PRES_IN_MMHG      = (760.0);                       // Dole p. 15				
        public static double EARTH_SURF_PRES_IN_PSI       = (14.696);                      // Pounds per square inch	
        public static double MMHG_TO_MILLIBARS            = (EARTH_SURF_PRES_IN_MILLIBARS / EARTH_SURF_PRES_IN_MMHG);
        public static double PSI_TO_MILLIBARS             = (EARTH_SURF_PRES_IN_MILLIBARS / EARTH_SURF_PRES_IN_PSI);
        public static double H20_ASSUMED_PRESSURE         = (47.0 * MMHG_TO_MILLIBARS);    // Dole p. 15      
        public static double PPM_PRSSURE = (EARTH_SURF_PRES_IN_MILLIBARS / 1000000.0);
        
        // Maximum inspired partial pressures in mmHg for common atmospheric gases - Dole pg. 15-16
        public static double MIN_O2_IPP                   = UnitConversions.MMHGToMillibars(72.0);
        public static double MAX_O2_IPP                   = UnitConversions.MMHGToMillibars(400.0);
        public static double MAX_HE_IPP                   = UnitConversions.MMHGToMillibars(61000.0);
        public static double MAX_NE_IPP                   = UnitConversions.MMHGToMillibars(3900.0);
        public static double MAX_N2_IPP                   = UnitConversions.MMHGToMillibars(2330.0);
        public static double MAX_AR_IPP                   = UnitConversions.MMHGToMillibars(1220.0);
        public static double MAX_KR_IPP                   = UnitConversions.MMHGToMillibars(350.0); 
        public static double MAX_XE_IPP                   = UnitConversions.MMHGToMillibars(160.0);
        public static double MAX_CO2_IPP                  = UnitConversions.MMHGToMillibars(7.0);
        public static double MAX_HABITABLE_PRESSURE       = UnitConversions.MMHGToMillibars(118);

        // The next gases are listed as poisonous in parts per million by volume at 1 atm:
        public static double MAX_F_IPP                    = UnitConversions.PPMToMillibars(0.1);
        public static double MAX_CL_IPP                   = UnitConversions.PPMToMillibars(1.0);
        public static double MAX_NH3_IPP                  = UnitConversions.PPMToMillibars(100.0);
        public static double MAX_O3_IPP                   = UnitConversions.PPMToMillibars(0.1);
        public static double MAX_CH4_IPP                  = UnitConversions.PPMToMillibars(50000.0);

        public static double EARTH_CONVECTION_FACTOR      = (0.43);                        // from Hart, eq.20			
        public static double FREEZING_POINT_OF_WATER      = (273.15);                      // Units of degrees Kelvin (was 273.0)
        public static double EARTH_AVERAGE_CELSIUS        = (14.0);                        // Average Earth Temperature (was 15.5)
        public static double EARTH_AVERAGE_KELVIN         = (EARTH_AVERAGE_CELSIUS + FREEZING_POINT_OF_WATER);
        public static double DAYS_IN_A_YEAR               = (365.256);                     // Earth days per Earth year
        public static double GAS_RETENTION_THRESHOLD      = (6.0);                         // Ratio of esc vel to RMS vel (was 5.0)

        public static double ICE_ALBEDO                   = (0.7);
        public static double CLOUD_ALBEDO                 = (0.52);
        public static double GAS_GIANT_ALBEDO             = (0.5);                         // albedo of a gas giant	
        public static double AIRLESS_ICE_ALBEDO           = (0.5);
        public static double EARTH_ALBEDO                 = (0.3);                         // was .33 for a while 
        public static double GREENHOUSE_TRIGGER_ALBEDO    = (0.20);
        public static double ROCKY_ALBEDO                 = (0.15);
        public static double ROCKY_AIRLESS_ALBEDO         = (0.07);
        public static double WATER_ALBEDO                 = (0.04);

        public static double SECONDS_PER_HOUR             = (3600.0);
        public static double CM_PER_AU                    = (1.495978707E13);              // number of cm in an AU	
        public static double CM_PER_KM                    = (1.0E5);                       // number of cm in a km		
        public static double KM_PER_AU                    = (CM_PER_AU / CM_PER_KM);
        public static double CM_PER_METER                 = (100.0);
        public static double MILLIBARS_PER_BAR            = (1000.00);                     // was 1013.25

        public static double GRAV_CONSTANT                = (6.672E-8);                    // units of dyne cm2/gram2	
        public static double MOLAR_GAS_CONST              = (8314.41);                     // units: g*m2/ = (sec2*K*mol); 
        public static double K                            = (50.0);                        // K = gas/dust ratio		
        public static double B                            = (1.2E-5);                      // Used in Crit_mass calc	
        public static double DUST_DENSITY_COEFF           = (0.002);                       // A in Dole's paper		
        public static double ALPHA                        = (5.0);                         // Used in density calcs	
        public static double N                            = (3.0);                         // Used in density calcs	
        public static double J                            = (1.46E-19);                    // Used in day-length calcs (cm2/sec2 g) 

        // Now for a few molecular weights (used for RMS velocity calcs):
        // This table is from Dole's book "Habitable Planets for Man", p. 38
        public static double ATOMIC_HYDROGEN              = (1.0);                         // H
        public static double MOL_HYDROGEN                 = (2.0);                         // H2
        public static double HELIUM	                      = (4.0);                         // He
        public static double ATOMIC_NITROGEN              = (14.0);                        // N
        public static double ATOMIC_OXYGEN                = (16.0);                        // O
        public static double METHANE                      = (16.0);                        // CH4
        public static double AMMONIA                      = (17.0);                        // NH3
        public static double WATER_VAPOR                  = (18.0);                        // H2O
        public static double NEON                         = (20.2);                        // Ne
        public static double MOL_NITROGEN                 = (28.0);                        // N2
        public static double CARBON_MONOXIDE              = (28.0);                        // CO
        public static double NITRIC_OXIDE                 = (30.0);                        // NO
        public static double MOL_OXYGEN	                  = (32.0);                        // O2
        public static double HYDROGEN_SULPHIDE            = (34.1);                        // H2S
        public static double ARGON                        = (39.9);                        // Ar
        public static double CARBON_DIOXIDE               = (44.0);                        // CO2
        public static double NITROUS_OXIDE                = (44.0);                        // N2O
        public static double NITROGEN_DIOXIDE             = (46.0);                        // NO2
        public static double OZONE                        = (48.0);                        // O3
        public static double SULPH_DIOXIDE                = (64.1);                        // SO2
        public static double SULPH_TRIOXIDE	              = (80.1);                        // SO3
        public static double KRYPTON                      = (83.8);                        // Kr
        public static double XENON                        = (131.3);                       // Xe

        //	And atomic numbers, for use in ChemTable indexes
        public static int AN_H                         = 1;
        public static int AN_HE                        = 2;
        public static int AN_N                         = 7;
        public static int AN_O                         = 8;
        public static int AN_F                         = 9;
        public static int AN_NE                        = 10;
        public static int AN_P                         = 15;
        public static int AN_CL                        = 17;
        public static int AN_AR                        = 18;
        public static int AN_BR                        = 35;
        public static int AN_KR                        = 36;
        public static int AN_I                         = 53;
        public static int AN_XE                        = 54;
        public static int AN_HG                        = 80;
        public static int AN_AT                        = 85;
        public static int AN_RN                        = 86;
        public static int AN_FR                        = 87;
        public static int AN_NH3                       = 900;
        public static int AN_H2O                       = 901;
        public static int AN_CO2                       = 902;
        public static int AN_O3                        = 903;
        public static int AN_CH4                       = 904;
        public static int AN_CH3CH2OH                  = 905;

        // The following defines are used in the kothari_radius function in
        // file enviro.c.
        public static double A1_20                        = (6.485E12);                    // All units are in cgs system.
        public static double A2_20                        = (4.0032E-8);                   // ie: cm, g, dynes, etc.
        public static double BETA_20                      = (5.71E12);
        public static double JIMS_FUDGE                   = (1.004);

        // The following defines are used in determining the fraction of a planet
        // covered with clouds in function cloud_fraction in file enviro.c.
        public static double Q1_36                        = (1.258E19);                    // grams
        public static double Q2_36                        = (0.0698);                      // 1/Kelvin

        public static double INCREDIBLY_LARGE_NUMBER      = (9.9999E37);
    }
}
