namespace DLS.StarformNET
{
    using System;
    using Data;
    using System.Collections.Generic;

    public class Generator
    {
        private static double MIN_SUN_AGE = 1.0E9;
        private static double MAX_SUN_AGE = 6.0E9;

        public Planet InnermostPlanet { get; private set; }
        public List<Planet> Planets { get; private set; }

        private double _dustDensityCoeff = GlobalConstants.DUST_DENSITY_COEFF;
        private long _flagSeed = 0;
        private Accrete _accrete = new Accrete();
        private ChemType[] _gasTable;

        public Generator(ChemType[] g)
        {
            _gasTable = g;
        }

        public void GenerateStellarSystem(ref Star sun, Planet seedSystem, string flagChar, int systemNo, string systemName, bool doGases, bool doMoons)
        {
            _accrete = new Accrete();           
            Planets = new List<Planet>();

            // TODO why is this randomizing for high and low sun masses?
            if (sun.Mass < 0.2 || sun.Mass > 1.5)
            {
                sun.Mass = Utilities.RandomNumber(0.7, 1.4);
            }
            double outer_dust_limit = _accrete.stellar_dust_limit(sun.Mass);

            if (sun.Luminosity == 0)
            {
                sun.Luminosity = Environment.Luminosity(sun.Mass);
            }

            sun.EcosphereRadius = Math.Sqrt(sun.Luminosity);
            sun.Life = 1.0E10 * (sun.Mass / sun.Luminosity);
            
            if (seedSystem != null)
            {
                InnermostPlanet = seedSystem;
                sun.Age = 5.0E9;
            }
            else
            {
                sun.Age = Utilities.RandomNumber(
                    MIN_SUN_AGE,
                    sun.Life < MAX_SUN_AGE ? sun.Life : MAX_SUN_AGE);

                double outer_planet_limit = GetOuterLimit(sun);
                InnermostPlanet = _accrete.DistPlanetaryMasses(sun.Mass, 
                    sun.Luminosity, 0.0, outer_dust_limit, outer_planet_limit,
                    _dustDensityCoeff, seedSystem, doMoons);
            }

            GeneratePlanets(ref sun, seedSystem == null, flagChar, systemNo, systemName, doGases, doMoons);
        }

        private void GeneratePlanets(ref Star sun, bool useRandomTilt, string flatChar, int systemNo, string systemName, bool doGases, bool doMoons)
        {
            Planet planet;
            int planet_no = 0;
            Planet moon;
            int moons = 0;

            for (planet = InnermostPlanet, planet_no = 1; planet != null; planet = planet.NextPlanet, planet_no++)
            {
                string planet_id = String.Format("{0} (-{1} -{2}{3}) {4}", systemName, _flagSeed, flatChar, systemNo, planet_no);

                GeneratePlanet(planet, planet_no, ref sun, useRandomTilt, planet_id, doGases, doMoons, false);
                Planets.Add(planet);

                // Now we're ready to test for habitable planets,
                // so we can count and log them and such
                CheckPlanet(ref planet, planet_id, false);

                for (moon = planet.FirstMoon, moons = 1; moon != null;  moon = moon.NextPlanet, moons++)
                {
                    string moon_id = String.Format("{0}.{1}", planet_id, moons);

                    CheckPlanet(ref moon, moon_id, true);
                }
            }
        }

        private void GeneratePlanet(Planet planet, int planetNo, ref Star sun, bool useRandomTilt, string planetID, bool doGases, bool doMoons, bool isMoon)
        {
            // TODO deal with planet initialization
            planet.AtmosphericGases = null;
            planet.GasCount = 0;
            planet.SurfaceTemp = 0;
            planet.DaytimeTemp = 0;
            planet.NighttimeTemp = 0;
            planet.MaxTemp = 0;
            planet.MinTemp = 0;
            planet.GreenhouseRise = 0;
            planet.Position = planetNo;
            planet.Star = sun;
            planet.HasResonantPeriod = false;

            planet.OrbitZone = Environment.OrbitalZone(sun.Luminosity, planet.SemiMajorAxisAU);
            planet.OrbitalPeriod = Environment.Period(planet.SemiMajorAxisAU, planet.Mass, sun.Mass);
            if (useRandomTilt)
            {
                planet.AxialTilt = Environment.Inclination(planet.SemiMajorAxisAU);
            }
            planet.ExosphereTemp = GlobalConstants.EARTH_EXOSPHERE_TEMP / Utilities.Pow2(planet.SemiMajorAxisAU / sun.EcosphereRadius);
            planet.RMSVelocity = Environment.RMSVelocity(GlobalConstants.MOL_NITROGEN, planet.ExosphereTemp);
            planet.CoreRadius = Environment.KothariRadius(planet.DustMass, false, planet.OrbitZone);

            // Calculate the radius as a gas giant, to verify it will retain gas.
            // Then if mass > Earth, it's at least 5% gas and retains He, it's
            // some flavor of gas giant.

            planet.Density = Environment.EmpiricalDensity(planet.Mass, planet.SemiMajorAxisAU, sun.EcosphereRadius, true);
            planet.Radius = Environment.VolumeRadius(planet.Mass, planet.Density);

            planet.SurfaceAcceleration = Environment.Acceleration(planet.Mass, planet.Radius);
            planet.SurfaceGravity = Environment.Gravity(planet.SurfaceAcceleration);

            planet.MolecularWeightRetained = Environment.MinMolecularWeight(planet);

            // TODO remove call to MinMolecularWeight in condition
            if (((planet.Mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES) > 1.0) && ((planet.GasMass / planet.Mass) > 0.05) && (Environment.MinMolecularWeight(planet) <= 4.0))
            {
                if ((planet.GasMass / planet.Mass) < 0.20)
                {
                    planet.Type = PlanetType.SubSubGasGiant;
                }
                else if ((planet.Mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES) < 20.0)
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
                planet.Radius = Environment.KothariRadius(planet.Mass, false, planet.OrbitZone);
                planet.Density = Environment.VolumeDensity(planet.Mass, planet.Radius);

                planet.SurfaceAcceleration = Environment.Acceleration(planet.Mass, planet.Radius);
                planet.SurfaceGravity = Environment.Gravity(planet.SurfaceAcceleration);

                if ((planet.GasMass / planet.Mass) > 0.000001)
                {
                    double h2_mass = planet.GasMass * 0.85;
                    double he_mass = (planet.GasMass - h2_mass) * 0.999;

                    double h2_loss = 0.0;
                    double he_loss = 0.0;


                    double h2_life = Environment.GasLife(GlobalConstants.MOL_HYDROGEN, planet);
                    double he_life = Environment.GasLife(GlobalConstants.HELIUM, planet);

                    if (h2_life < sun.Age)
                    {
                        h2_loss = ((1.0 - (1.0 / Math.Exp(sun.Age / h2_life))) * h2_mass);

                        planet.GasMass -= h2_loss;
                        planet.Mass -= h2_loss;

                        planet.SurfaceAcceleration = Environment.Acceleration(planet.Mass, planet.Radius);
                        planet.SurfaceGravity = Environment.Gravity(planet.SurfaceAcceleration);
                    }

                    if (he_life < sun.Age)
                    {
                        he_loss = ((1.0 - (1.0 / Math.Exp(sun.Age / he_life))) * he_mass);

                        planet.GasMass -= he_loss;
                        planet.Mass -= he_loss;

                        planet.SurfaceAcceleration = Environment.Acceleration(planet.Mass, planet.Radius);
                        planet.SurfaceGravity = Environment.Gravity(planet.SurfaceAcceleration);
                    }
                }
            }

            planet.Day = Environment.DayLength(ref planet); // Modifies planet.resonant_period
            planet.EscapeVelocity = Environment.EscapeVelocity(planet.Mass, planet.Radius);

            if (planet.Type == PlanetType.GasGiant || planet.Type == PlanetType.SubGasGiant || planet.Type == PlanetType.SubSubGasGiant)
            {
                planet.HasGreenhouseEffect = false;
                planet.VolatileGasInventory = GlobalConstants.INCREDIBLY_LARGE_NUMBER;
                planet.SurfPressure = GlobalConstants.INCREDIBLY_LARGE_NUMBER;

                planet.BoilingPointWater = GlobalConstants.INCREDIBLY_LARGE_NUMBER;

                planet.SurfaceTemp = GlobalConstants.INCREDIBLY_LARGE_NUMBER;
                planet.GreenhouseRise = 0;
                planet.Albedo = Utilities.About(GlobalConstants.GAS_GIANT_ALBEDO, 0.1);
                planet.WaterCover = 1.0;
                planet.CloudCover = 1.0;
                planet.IceCover = 0.0;
                planet.SurfaceGravity = Environment.Gravity(planet.SurfaceAcceleration);
                planet.MolecularWeightRetained = Environment.MinMolecularWeight(planet);
                planet.SurfaceGravity = GlobalConstants.INCREDIBLY_LARGE_NUMBER;
                planet.EstimatedTemp = Environment.EstTemp(sun.EcosphereRadius, planet.SemiMajorAxisAU, planet.Albedo);
                planet.EstimatedTerrTemp = Environment.EstTemp(sun.EcosphereRadius, planet.SemiMajorAxisAU, GlobalConstants.EARTH_ALBEDO);

                {
                    double temp = planet.EstimatedTerrTemp;

                    // Indicates habitable (??) Jovian planet
                    if ((temp >= GlobalConstants.FREEZING_POINT_OF_WATER) && (temp <= GlobalConstants.EARTH_AVERAGE_KELVIN + 10.0) && (sun.Age > 2.0E9))
                    { }
                }
            }
            else
            {
                planet.EstimatedTemp = Environment.EstTemp(sun.EcosphereRadius, planet.SemiMajorAxisAU, GlobalConstants.EARTH_ALBEDO);
                planet.EstimatedTerrTemp = Environment.EstTemp(sun.EcosphereRadius, planet.SemiMajorAxisAU, GlobalConstants.EARTH_ALBEDO);

                planet.SurfaceGravity = Environment.Gravity(planet.SurfaceAcceleration);
                planet.MolecularWeightRetained = Environment.MinMolecularWeight(planet);

                planet.HasGreenhouseEffect = Environment.Greenhouse(sun.EcosphereRadius, planet.SemiMajorAxisAU);
                planet.VolatileGasInventory = Environment.VolatileInventory(
                    planet.Mass, planet.EscapeVelocity, planet.RMSVelocity, sun.Mass,
                    planet.OrbitZone, planet.HasGreenhouseEffect, (planet.GasMass / planet.Mass) > 0.000001);
                planet.SurfPressure = Environment.Pressure(
                    planet.VolatileGasInventory, planet.Radius, planet.SurfaceGravity);

                if ((planet.SurfPressure == 0.0))
                {
                    planet.BoilingPointWater = 0.0;
                }
                else
                {
                    planet.BoilingPointWater = Environment.BoilingPoint(planet.SurfPressure);
                }

                // Sets: planet.surf_temp, planet.greenhs_rise, planet.albedo, planet.hydrosphere,
                // planet.cloud_cover, planet.ice_cover
                Environment.IterateSurfaceTemp(ref planet);
                
                if (doGases)
                {
                    CalculateGases(planet);
                }

                if ( ((int)planet.Day == (int)(planet.OrbitalPeriod * 24.0)) || planet.HasResonantPeriod)
                {
                    planet.IsTidallyLocked = true;
                }

                // Assign planet type
                if (planet.SurfPressure < 1.0)
                {
                    if (!isMoon && ((planet.Mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES) < GlobalConstants.ASTEROID_MASS_LIMIT))
                    {
                        planet.Type = PlanetType.Asteroids;
                    }
                    else
                    {
                        planet.Type = PlanetType.Barren;
                    }
                }
                else if ((planet.SurfPressure > 6000.0) && (planet.MolecularWeightRetained <= 2.0)) // Retains Hydrogen
                {
                    planet.Type = PlanetType.SubSubGasGiant;
                    planet.GasCount = 0;
                    planet.AtmosphericGases = new Gas[0];
                }
                else
                {
                    // Atmospheres:
                    // TODO remove PlanetType enum entirely and replace it with a more flexible classification systme
                    if (planet.WaterCover >= 0.95) // >95% water
                    {
                        planet.Type = PlanetType.Water;
                    }
                    else if (planet.IceCover >= 0.95) // >95% ice
                    {
                        planet.Type = PlanetType.Ice;
                    }
                    else if (planet.WaterCover > 0.05) // Terrestrial
                    {
                        planet.Type = PlanetType.Terrestrial;
                    }
                    else if (planet.MaxTemp > planet.BoilingPointWater) // Hot = Venusian
                    {
                        planet.Type = PlanetType.Venusian;
                    }
                    else if ((planet.GasMass / planet.Mass) > 0.0001) // Accreted gas, but no greenhouse or liquid water make it an ice world
                    {
                        planet.Type = PlanetType.Ice;
                        planet.IceCover = 1.0;
                    }
                    else if (planet.SurfPressure <= 250.0) // Thin air = Martian
                    {
                        planet.Type = PlanetType.Martian;
                    }
                    else if (planet.SurfaceTemp < GlobalConstants.FREEZING_POINT_OF_WATER)
                    {
                        planet.Type = PlanetType.Ice;
                    }
                    else
                    {
                        planet.Type = PlanetType.Unknown;
                    }
                }
            }

            if (doMoons && !isMoon)
            {
                if (planet.FirstMoon != null)
                {
                    int n;
                    Planet ptr;

                    for (n = 0, ptr = planet.FirstMoon; ptr != null; ptr = ptr.NextPlanet)
                    {
                        if (ptr.Mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES > .000001)
                        {
                            ptr.SemiMajorAxisAU = planet.SemiMajorAxisAU;
                            ptr.Eccentricity = planet.Eccentricity;

                            n++;
                            
                            string moon_id = String.Format("{0}.{1}", planetID, n);

                            GeneratePlanet(ptr, n, ref sun, useRandomTilt, moon_id, doGases, doMoons, true);    // Adjusts ptr.density

                            double roche_limit = 2.44 * planet.Radius * Math.Pow((planet.Density / ptr.Density), (1.0 / 3.0));
                            double hill_sphere = planet.SemiMajorAxisAU * GlobalConstants.KM_PER_AU * Math.Pow((planet.Mass / (3.0 * sun.Mass)), (1.0 / 3.0));

                            if ((roche_limit * 3.0) < hill_sphere)
                            {
                                ptr.MoonSemiMajorAxisAU = Utilities.RandomNumber(roche_limit * 1.5, hill_sphere / 2.0) / GlobalConstants.KM_PER_AU;
                                ptr.MoonEccentricity = Utilities.RandomEccentricity();
                            }
                            else
                            {
                                ptr.MoonSemiMajorAxisAU = 0;
                                ptr.MoonEccentricity = 0;
                            }
                        }
                    }
                }
            }

        }

        // TODO this really should be in a separate class
        public void CalculateGases(Planet planet)
        {
            var sun = planet.Star;
            planet.GasCount = 0;
            planet.AtmosphericGases = new Gas[0];

            if (!(planet.SurfPressure > 0))
            {
                return;
            }

            double[] amount = new double[_gasTable.Length];
            double totamount = 0;
            double pressure = planet.SurfPressure / GlobalConstants.MILLIBARS_PER_BAR;
            int n = 0;

            // Determine the relative abundance of each gas in the planet's atmosphere
            for (var i = 0; i < _gasTable.Length; i++)
            {
                double yp = _gasTable[i].boil / (373.0 * ((Math.Log((pressure) + 0.001) / -5050.5) + (1.0 / 373.0)));

                // TODO move both of these conditions to separate methods
                if ((yp >= 0 && yp < planet.NighttimeTemp) && (_gasTable[i].weight >= planet.MolecularWeightRetained))
                {
                    double abund, react;
                    CheckForSpecialRules(out abund, out react, pressure, planet, _gasTable[i]);

                    double vrms = Environment.RMSVelocity(_gasTable[i].weight, planet.ExosphereTemp);
                    double pvrms = Math.Pow(1 / (1 + vrms / planet.EscapeVelocity), sun.Age / 1e9);

                    double fract = (1 - (planet.MolecularWeightRetained / _gasTable[i].weight));

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
                planet.GasCount = n;
                planet.AtmosphericGases = new Gas[n];

                n = 0;
                for (var i = 0; i < _gasTable.Length; i++)
                {
                    if (amount[i] > 0.0)
                    {
                        planet.AtmosphericGases[n] = new Gas(
                            _gasTable[i], planet.SurfPressure * amount[i] / totamount);

                        n++;
                    }
                }
            }
            
        }

        private void CheckForSpecialRules(out double abund, out double react, double pressure, Planet planet, ChemType gas)
        {
            var sun = planet.Star;
            var pres2 = 1.0;
            abund = gas.abunds;

            if (gas.symbol == "Ar")
            {
                react = .15 * sun.Age / 4e9;
            }
            else if (gas.symbol == "He")
            {
                abund = abund * (0.001 + (planet.GasMass / planet.Mass));
                pres2 = (0.75 + pressure);
                react = Math.Pow(1 / (1 + gas.reactivity), sun.Age / 2e9 * pres2);
            }
            else if ((gas.symbol == "O" || gas.symbol == "O2") && sun.Age > 2e9 && planet.SurfaceTemp > 270 && planet.SurfaceTemp < 400)
            {
                // pres2 = (0.65 + pressure/2); // Breathable - M: .55-1.4
                pres2 = (0.89 + pressure / 4);  // Breathable - M: .6 -1.8
                react = Math.Pow(1 / (1 + gas.reactivity), Math.Pow(sun.Age / 2e9, 0.25) * pres2);
            }
            else if (gas.symbol == "CO2" && sun.Age > 2e9 && planet.SurfaceTemp > 270 && planet.SurfaceTemp < 400)
            {
                pres2 = (0.75 + pressure);
                react = Math.Pow(1 / (1 + gas.reactivity), Math.Pow(sun.Age / 2e9, 0.5) * pres2);
                react *= 1.5;
            }
            else
            {
                pres2 = (0.75 + pressure);
                react = Math.Pow(1 / (1 + gas.reactivity), sun.Age / 2e9 * pres2);
            }
        }

        // TODO This should be moved out of this class entirely
        private void CheckPlanet(ref Planet planet, string planetID, bool is_moon)
        {
            planet.breathability = Environment.Breathability(planet);

            // TODO move this calculation to somewhere else. Also, what units is this in?
            planet.Illumination = Utilities.Pow2(1.0 / planet.SemiMajorAxisAU) * (planet.Star).Luminosity;

            planet.IsHabitable = Environment.IsHabitable(planet);
            planet.IsEarthlike = Environment.IsEarthlike(planet);
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
            double e = star.SemiMajorAxis;
            double e2 = Utilities.Pow2(e);
            double a = star.Eccentricity;

            return (0.464 + (-0.380 * mu) + (-0.631 * e) + (0.586 * mu * e) + (0.150 * e2) + (-0.198 * mu * e2)) * a;
        }
    }
}
