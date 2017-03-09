namespace DLS.StarformNET
{
    using System;
    using Data;
    using System.Collections.Generic;
    
    public class Generator
    {
        public static StellarGroup GenerateStellarGroup(int seed, int numSystems, SystemGenerationOptions genOptions = null)
        {
            Utilities.InitRandomSeed(seed);
            genOptions = genOptions ?? SystemGenerationOptions.DefaultOptions;
            var group = new StellarGroup() { Seed = seed, GenOptions = genOptions, Systems = new List<StellarSystem>() };
            for (var i = 0; i < numSystems; i++)
            {
                var name = String.Format("System {0}", i);
                group.Systems.Add(GenerateStellarSystem(name, genOptions));
            }
            return group;
        }

        public static StellarSystem GenerateStellarSystem(string systemName, SystemGenerationOptions genOptions = null, Star sun=null, List<PlanetSeed> seedSystem=null)
        {
            genOptions = genOptions ?? SystemGenerationOptions.DefaultOptions;
            sun = sun ?? StarGenerator.GetDefaultStar();
            var useRandomTilt = seedSystem == null;

            var accrete = new Accrete(genOptions.CloudEccentricity, genOptions.GasDensityRatio);
            double outer_planet_limit = GetOuterLimit(sun);
            double outer_dust_limit = GetStellarDustLimit(sun.Mass);
            seedSystem = seedSystem ?? accrete.GetPlanetaryBodies(sun.Mass, 
                sun.Luminosity, 0.0, outer_dust_limit, outer_planet_limit,
                genOptions.DustDensityCoeff, null, true);
            
            var planets = GeneratePlanets(sun, seedSystem, useRandomTilt, genOptions);
            return new StellarSystem()
            {
                Options = genOptions,
                Planets = planets,
                Name = systemName,
                Star = sun
            };
        }

        private static List<Planet> GeneratePlanets(Star sun, List<PlanetSeed> seeds, bool useRandomTilt, SystemGenerationOptions genOptions)
        {
            var planets = new List<Planet>();
            for (var i = 0; i < seeds.Count; i++)
            {
                var planetNo = i + 1; // start counting planets at 1
                var seed = seeds[i];

                string planet_id = planetNo.ToString();

                var planet = GeneratePlanet(seed, planetNo, ref sun, useRandomTilt, planet_id, false, genOptions);
                planets.Add(planet);

                // Now we're ready to test for habitable planets,
                // so we can count and log them and such
                CheckPlanet(planet, planet_id, false);
                
                for (var m = 0; m < planet.Moons.Count; m++)
                {
                    string moon_id = String.Format("{0}.{1}", planet_id, m);
                    CheckPlanet(planet.Moons[m], moon_id, true);
                }
            }

            return planets;
        }

        private static Planet GeneratePlanet(PlanetSeed seed, int planetNo, ref Star sun, bool useRandomTilt, string planetID, bool isMoon, SystemGenerationOptions genOptions)
        {
            var planet = new Planet(seed, sun, planetNo);

            planet.OrbitZone = Environment.OrbitalZone(sun.Luminosity, planet.SemiMajorAxisAU);
            planet.OrbitalPeriodDays = Environment.Period(planet.SemiMajorAxisAU, planet.MassSM, sun.Mass);
            if (useRandomTilt)
            {
                planet.AxialTiltDegrees = Environment.Inclination(planet.SemiMajorAxisAU);
            }
            planet.ExosphereTempKelvin = GlobalConstants.EARTH_EXOSPHERE_TEMP / Utilities.Pow2(planet.SemiMajorAxisAU / sun.EcosphereRadiusAU);
            planet.RMSVelocityCMSec = Environment.RMSVelocity(GlobalConstants.MOL_NITROGEN, planet.ExosphereTempKelvin);
            planet.CoreRadiusKM = Environment.KothariRadius(planet.DustMassSM, false, planet.OrbitZone);

            // Calculate the radius as a gas giant, to verify it will retain gas.
            // Then if mass > Earth, it's at least 5% gas and retains He, it's
            // some flavor of gas giant.

            planet.DensityGCC = Environment.EmpiricalDensity(planet.MassSM, planet.SemiMajorAxisAU, sun.EcosphereRadiusAU, true);
            planet.RadiusKM = Environment.VolumeRadius(planet.MassSM, planet.DensityGCC);

            planet.SurfaceAccelerationCMSec2 = Environment.Acceleration(planet.MassSM, planet.RadiusKM);
            planet.SurfaceGravityG = Environment.Gravity(planet.SurfaceAccelerationCMSec2);

            planet.MolecularWeightRetained = Environment.MinMolecularWeight(planet);
            
            // Is the planet a gas giant?
            if (((planet.MassSM * GlobalConstants.SUN_MASS_IN_EARTH_MASSES) > 1.0) && ((planet.GasMassSM / planet.MassSM) > 0.05) && (planet.MolecularWeightRetained <= 4.0))
            {
                if ((planet.GasMassSM / planet.MassSM) < 0.20)
                {
                    planet.Type = PlanetType.SubSubGasGiant;
                }
                else if ((planet.MassSM * GlobalConstants.SUN_MASS_IN_EARTH_MASSES) < 20.0)
                {
                    planet.Type = PlanetType.SubGasGiant;
                }
                else
                {
                    planet.Type = PlanetType.GasGiant;
                }
            }
            else // If not, it's rocky.
            {
                planet.RadiusKM = Environment.KothariRadius(planet.MassSM, false, planet.OrbitZone);
                planet.DensityGCC = Environment.VolumeDensity(planet.MassSM, planet.RadiusKM);

                planet.SurfaceAccelerationCMSec2 = Environment.Acceleration(planet.MassSM, planet.RadiusKM);
                planet.SurfaceGravityG = Environment.Gravity(planet.SurfaceAccelerationCMSec2);

                if ((planet.GasMassSM / planet.MassSM) > 0.000001)
                {
                    double h2_mass = planet.GasMassSM * 0.85;
                    double he_mass = (planet.GasMassSM - h2_mass) * 0.999;

                    double h2_loss = 0.0;
                    double he_loss = 0.0;


                    double h2_life = Environment.GasLife(GlobalConstants.MOL_HYDROGEN, planet);
                    double he_life = Environment.GasLife(GlobalConstants.HELIUM, planet);

                    if (h2_life < sun.AgeYears)
                    {
                        h2_loss = ((1.0 - (1.0 / Math.Exp(sun.AgeYears / h2_life))) * h2_mass);

                        planet.GasMassSM -= h2_loss;
                        planet.MassSM -= h2_loss;

                        planet.SurfaceAccelerationCMSec2 = Environment.Acceleration(planet.MassSM, planet.RadiusKM);
                        planet.SurfaceGravityG = Environment.Gravity(planet.SurfaceAccelerationCMSec2);
                    }

                    if (he_life < sun.AgeYears)
                    {
                        he_loss = ((1.0 - (1.0 / Math.Exp(sun.AgeYears / he_life))) * he_mass);

                        planet.GasMassSM -= he_loss;
                        planet.MassSM -= he_loss;

                        planet.SurfaceAccelerationCMSec2 = Environment.Acceleration(planet.MassSM, planet.RadiusKM);
                        planet.SurfaceGravityG = Environment.Gravity(planet.SurfaceAccelerationCMSec2);
                    }
                }
            }

            planet.DayLengthHours = Environment.DayLength(ref planet); // Modifies planet.resonant_period
            planet.EscapeVelocityCMSec = Environment.EscapeVelocity(planet.MassSM, planet.RadiusKM);
            planet.HillSphereKM = Environment.SimplifiedHillSphereKM(sun.Mass, planet.MassSM, planet.SemiMajorAxisAU);

            if (planet.Type == PlanetType.GasGiant || planet.Type == PlanetType.SubGasGiant || planet.Type == PlanetType.SubSubGasGiant)
            {
                planet.HasGreenhouseEffect = false;
                planet.VolatileGasInventory = GlobalConstants.INCREDIBLY_LARGE_NUMBER;
                planet.Atmosphere.SurfacePressure = GlobalConstants.INCREDIBLY_LARGE_NUMBER;

                planet.BoilingPointWaterKelvin = GlobalConstants.INCREDIBLY_LARGE_NUMBER;

                planet.SurfaceTempKelvin = GlobalConstants.INCREDIBLY_LARGE_NUMBER;
                planet.GreenhouseRiseKelvin = 0;
                planet.Albedo = Utilities.About(GlobalConstants.GAS_GIANT_ALBEDO, 0.1);
                planet.WaterCoverFraction = 1.0;
                planet.CloudCoverFraction = 1.0;
                planet.IceCoverFraction = 0.0;
                planet.SurfaceGravityG = Environment.Gravity(planet.SurfaceAccelerationCMSec2);
                planet.MolecularWeightRetained = Environment.MinMolecularWeight(planet);
                planet.SurfaceGravityG = GlobalConstants.INCREDIBLY_LARGE_NUMBER;
            }
            else
            {
                planet.SurfaceGravityG = Environment.Gravity(planet.SurfaceAccelerationCMSec2);
                planet.MolecularWeightRetained = Environment.MinMolecularWeight(planet);

                planet.HasGreenhouseEffect = Environment.Greenhouse(sun.EcosphereRadiusAU, planet.SemiMajorAxisAU);
                planet.VolatileGasInventory = Environment.VolatileInventory(
                    planet.MassSM, planet.EscapeVelocityCMSec, planet.RMSVelocityCMSec, sun.Mass,
                    planet.OrbitZone, planet.HasGreenhouseEffect, (planet.GasMassSM / planet.MassSM) > 0.000001);
                planet.Atmosphere.SurfacePressure = Environment.Pressure(
                    planet.VolatileGasInventory, planet.RadiusKM, planet.SurfaceGravityG);

                if ((planet.Atmosphere.SurfacePressure == 0.0))
                {
                    planet.BoilingPointWaterKelvin = 0.0;
                }
                else
                {
                    planet.BoilingPointWaterKelvin = Environment.BoilingPoint(planet.Atmosphere.SurfacePressure);
                }

                // Sets: planet.surf_temp, planet.greenhs_rise, planet.albedo, planet.hydrosphere,
                // planet.cloud_cover, planet.ice_cover
                Environment.IterateSurfaceTemp(ref planet);
                
                CalculateGases(planet, genOptions.GasTable);

                planet.IsTidallyLocked = Environment.IsTidallyLocked(planet);

                // Assign planet type
                if (planet.Atmosphere.SurfacePressure < 1.0)
                {
                    if (!isMoon && ((planet.MassSM * GlobalConstants.SUN_MASS_IN_EARTH_MASSES) < GlobalConstants.ASTEROID_MASS_LIMIT))
                    {
                        planet.Type = PlanetType.Asteroids;
                    }
                    else
                    {
                        planet.Type = PlanetType.Barren;
                    }
                }
                else if ((planet.Atmosphere.SurfacePressure > 6000.0) && (planet.MolecularWeightRetained <= 2.0)) // Retains Hydrogen
                {
                    planet.Type = PlanetType.SubSubGasGiant;
                    planet.Atmosphere.Composition = new List<Gas>();
                }
                else
                {
                    // Atmospheres:
                    // TODO remove PlanetType enum entirely and replace it with a more flexible classification systme
                    if (planet.WaterCoverFraction >= 0.95) // >95% water
                    {
                        planet.Type = PlanetType.Water;
                    }
                    else if (planet.IceCoverFraction >= 0.95) // >95% ice
                    {
                        planet.Type = PlanetType.Ice;
                    }
                    else if (planet.WaterCoverFraction > 0.05) // Terrestrial
                    {
                        planet.Type = PlanetType.Terrestrial;
                    }
                    else if (planet.MaxTempKelvin > planet.BoilingPointWaterKelvin) // Hot = Venusian
                    {
                        planet.Type = PlanetType.Venusian;
                    }
                    else if ((planet.GasMassSM / planet.MassSM) > 0.0001) // Accreted gas, but no greenhouse or liquid water make it an ice world
                    {
                        planet.Type = PlanetType.Ice;
                        planet.IceCoverFraction = 1.0;
                    }
                    else if (planet.Atmosphere.SurfacePressure <= 250.0) // Thin air = Martian
                    {
                        planet.Type = PlanetType.Martian;
                    }
                    else if (planet.SurfaceTempKelvin < GlobalConstants.FREEZING_POINT_OF_WATER)
                    {
                        planet.Type = PlanetType.Ice;
                    }
                    else
                    {
                        planet.Type = PlanetType.Unknown;
                    }
                }
            }

            // Generate moons
            planet.Moons = new List<Planet>();
            if (!isMoon)
            {
                var curMoon = seed.FirstMoon;
                var n = 0;
                while (curMoon != null)
                {
                    if (curMoon.Mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES > .000001)
                    {
                        curMoon.SemiMajorAxisAU = planet.SemiMajorAxisAU;
                        curMoon.Eccentricity = planet.Eccentricity;

                        n++;

                        string moon_id = String.Format("{0}.{1}", planetID, n);

                        var generatedMoon = GeneratePlanet(curMoon, n, ref sun, useRandomTilt, moon_id, true, genOptions);

                        double roche_limit = 2.44 * planet.RadiusKM * Math.Pow((planet.DensityGCC / generatedMoon.DensityGCC), (1.0 / 3.0));
                        double hill_sphere = planet.SemiMajorAxisAU * GlobalConstants.KM_PER_AU * Math.Pow((planet.MassSM / (3.0 * sun.Mass)), (1.0 / 3.0));

                        if ((roche_limit * 3.0) < hill_sphere)
                        {
                            generatedMoon.MoonSemiMajorAxisAU = Utilities.RandomNumber(roche_limit * 1.5, hill_sphere / 2.0) / GlobalConstants.KM_PER_AU;
                            generatedMoon.MoonEccentricity = Utilities.RandomEccentricity();
                        }
                        else
                        {
                            generatedMoon.MoonSemiMajorAxisAU = 0;
                            generatedMoon.MoonEccentricity = 0;
                        }
                        planet.Moons.Add(generatedMoon);
                    }
                    curMoon = curMoon.NextPlanet;
                }
            }

            return planet;
        }

        // TODO this really should be in a separate class
        public static void CalculateGases(Planet planet, ChemType[] gasTable)
        {
            var sun = planet.Star;
            planet.Atmosphere.Composition = new List<Gas>();

            if (!(planet.Atmosphere.SurfacePressure > 0))
            {
                return;
            }

            double[] amount = new double[gasTable.Length];
            double totamount = 0;
            double pressure = planet.Atmosphere.SurfacePressure / GlobalConstants.MILLIBARS_PER_BAR;
            int n = 0;

            // Determine the relative abundance of each gas in the planet's atmosphere
            for (var i = 0; i < gasTable.Length; i++)
            {
                double yp = gasTable[i].boil / (373.0 * ((Math.Log((pressure) + 0.001) / -5050.5) + (1.0 / 373.0)));

                // TODO move both of these conditions to separate methods
                if ((yp >= 0 && yp < planet.NighttimeTempKelvin) && (gasTable[i].weight >= planet.MolecularWeightRetained))
                {
                    double abund, react;
                    CheckForSpecialRules(out abund, out react, pressure, planet, gasTable[i]);

                    double vrms = Environment.RMSVelocity(gasTable[i].weight, planet.ExosphereTempKelvin);
                    double pvrms = Math.Pow(1 / (1 + vrms / planet.EscapeVelocityCMSec), sun.AgeYears / 1e9);

                    double fract = (1 - (planet.MolecularWeightRetained / gasTable[i].weight));

                    // Note that the amount calculated here is unitless and doesn't really mean
                    // anything except as a relative value
                    amount[i] = abund * pvrms * react * fract;
                    totamount += amount[i];
                    if (amount[i] > 0.0)
                    {
                        n++;
                    }
                }
                else
                {
                    amount[i] = 0.0;
                }
            }

            // For each gas present, calculate its partial pressure
            if (n > 0)
            {
                planet.Atmosphere.Composition = new List<Gas>();

                n = 0;
                for (var i = 0; i < gasTable.Length; i++)
                {
                    if (amount[i] > 0.0)
                    {
                        planet.Atmosphere.Composition.Add(
                            new Gas(gasTable[i], planet.Atmosphere.SurfacePressure * amount[i] / totamount));
                    }
                }
            }
            
        }

        private static void CheckForSpecialRules(out double abund, out double react, double pressure, Planet planet, ChemType gas)
        {
            var sun = planet.Star;
            var pres2 = 1.0;
            abund = gas.abunds;

            if (gas.symbol == "Ar")
            {
                react = .15 * sun.AgeYears / 4e9;
            }
            else if (gas.symbol == "He")
            {
                abund = abund * (0.001 + (planet.GasMassSM / planet.MassSM));
                pres2 = (0.75 + pressure);
                react = Math.Pow(1 / (1 + gas.reactivity), sun.AgeYears / 2e9 * pres2);
            }
            else if ((gas.symbol == "O" || gas.symbol == "O2") && sun.AgeYears > 2e9 && planet.SurfaceTempKelvin > 270 && planet.SurfaceTempKelvin < 400)
            {
                // pres2 = (0.65 + pressure/2); // Breathable - M: .55-1.4
                pres2 = (0.89 + pressure / 4);  // Breathable - M: .6 -1.8
                react = Math.Pow(1 / (1 + gas.reactivity), Math.Pow(sun.AgeYears / 2e9, 0.25) * pres2);
            }
            else if (gas.symbol == "CO2" && sun.AgeYears > 2e9 && planet.SurfaceTempKelvin > 270 && planet.SurfaceTempKelvin < 400)
            {
                pres2 = (0.75 + pressure);
                react = Math.Pow(1 / (1 + gas.reactivity), Math.Pow(sun.AgeYears / 2e9, 0.5) * pres2);
                react *= 1.5;
            }
            else
            {
                pres2 = (0.75 + pressure);
                react = Math.Pow(1 / (1 + gas.reactivity), sun.AgeYears / 2e9 * pres2);
            }
        }

        // TODO This should be moved out of this class entirely
        private static void CheckPlanet(Planet planet, string planetID, bool is_moon)
        {
            planet.Atmosphere.Breathability = Environment.Breathability(planet);

            // TODO move this calculation to somewhere else. Also, what units is this in?
            planet.Illumination = Utilities.Pow2(1.0 / planet.SemiMajorAxisAU) * (planet.Star).Luminosity;

            planet.IsHabitable = Environment.IsHabitable(planet);
            planet.IsEarthlike = Environment.IsEarthlike(planet);
        }

        private static double GetStellarDustLimit(double stellarMassRatio)
        {
            return (200.0 * Math.Pow(stellarMassRatio, (1.0 / 3.0)));
        }

        private static double GetOuterLimit(Star star)
        {
            if (star.BinaryMass < .001)
            {
                return 0.0;
            }

            // The following is Holman & Wiegert's equation 1 from
            // Long-Term Stability of Planets in Binary Systems
            // The Astronomical Journal, 117:621-628, Jan 1999
            double m1 = star.Mass;
            double m2 = star.BinaryMass;
            double mu = m2 / (m1 + m2);
            double e = star.SemiMajorAxisAU;
            double e2 = Utilities.Pow2(e);
            double a = star.Eccentricity;

            return (0.464 + (-0.380 * mu) + (-0.631 * e) + (0.586 * mu * e) + (0.150 * e2) + (-0.198 * mu * e2)) * a;
        }
    }
}