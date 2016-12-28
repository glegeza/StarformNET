namespace DLS.StarformNet
{
    using System;
    using Data;

    public class Generator
    {
        private Accrete _accrete = new Accrete();
        private ChemTable[] gases;
        int max_gas;

        public Planet innermost_planet;
        double dust_density_coeff = GlobalConstants.DUST_DENSITY_COEFF;
        long flag_seed = 0;

        int earthlike = 0;
        int total_earthlike = 0;
        int habitable = 0;
        int habitable_jovians = 0;
        int total_habitable = 0;

        double min_sun_age = 1.0E9;
        double max_sun_age = 6.0E9;
        double min_breathable_terrestrial_g = 1000.0;
        double min_breathable_g = 1000.0;
        double max_breathable_terrestrial_g = 0.0;
        double max_breathable_g = 0.0;
        double min_breathable_temp = 1000.0;
        double max_breathable_temp = 0.0;
        double min_breathable_p = 100000.0;
        double max_breathable_p = 0.0;
        double min_breathable_terrestrial_l = 1000.0;
        double min_breathable_l = 1000.0;
        double max_breathable_terrestrial_l = 0.0;
        double max_breathable_l = 0.0;
        double max_moon_mass = 0.0;

        int[] type_counts = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int type_count = 0;
        int max_type_count = 0;

        public Generator(ChemTable[] g)
        {
            gases = g;
            max_gas = gases.Length;
        }

        public void GenerateStellarSystem(ref Star sun, Planet seedSystem, string flagChar, int systemNo, string systemName, bool doGases, bool doMoons)
        {
            _accrete = new Accrete();           

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
                innermost_planet = seedSystem;
                sun.Age = 5.0E9;
            }
            else
            {
                sun.Age = Utilities.RandomNumber(
                    min_sun_age,
                    sun.Life < max_sun_age ? sun.Life : max_sun_age);

                double outer_planet_limit = GetOuterLimit(sun);
                innermost_planet = _accrete.DistPlanetaryMasses(sun.Mass, 
                    sun.Luminosity, 0.0, outer_dust_limit, outer_planet_limit,
                    dust_density_coeff, seedSystem, doMoons);
            }

            GeneratePlanets(ref sun, seedSystem == null, flagChar, systemNo, systemName, doGases, doMoons);
        }

        private void GeneratePlanets(ref Star sun, bool useRandomTilt, string flatChar, int systemNo, string systemName, bool doGases, bool doMoons)
        {
            Planet planet;
            int planet_no = 0;
            Planet moon;
            int moons = 0;

            for (planet = innermost_planet, planet_no = 1; planet != null; planet = planet.NextPlanet, planet_no++)
            {
                //sprintf(planet_id, "%s (-s%ld -%c%d) %d", system_name, flag_seed, flag_char, sys_no, planet_no);
                string planet_id = String.Format("{0} (-{1} -{2}{3}) {4}", systemName, flag_seed, flatChar, systemNo, planet_no);

                GeneratePlanet(planet, planet_no, ref sun, useRandomTilt, planet_id, doGases, doMoons, false);

                // Now we're ready to test for habitable planets,
                // so we can count and log them and such
                CheckPlanet(ref planet, planet_id, false);

                for (moon = planet.FirstMoon, moons = 1; moon != null;  moon = moon.NextPlanet, moons++)
                {
                    //sprintf(moon_id, "%s.%d", planet_id, moons);
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

                    // TODO logging
                    //if (((h2_loss + he_loss) > .000001) && (flag_verbose & 0x0080))
                    //    fprintf(stderr, "%s\tLosing gas: H2: %5.3Lf EM, He: %5.3Lf EM\n",
                    //             planet_id,
                    //             h2_loss * GlobalConstants.SUN_MASS_IN_EARTH_MASSES, he_loss * GlobalConstants.SUN_MASS_IN_EARTH_MASSES);
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

                    if ((temp >= GlobalConstants.FREEZING_POINT_OF_WATER) && (temp <= GlobalConstants.EARTH_AVERAGE_KELVIN + 10.0) && (sun.Age > 2.0E9))
                    {
                        habitable_jovians++;

                        // TODO logging
                        //if (flag_verbose & 0x8000)
                        //{
                        //    fprintf(stderr, "%s\t%s (%4.2LfEM %5.3Lf By)%s with earth-like temperature (%.1Lf C, %.1Lf F, %+.1Lf C Earth).\n",
                        //             planet_id,
                        //             planet.type == PlanetType.GasGiant ? "Jovian" :
                        //             planet.type == PlanetType.SubGasGiant ? "Sub-Jovian" :
                        //             planet.type == PlanetType.SubSubGasGiant ? "Gas Dwarf" :
                        //             "Big",
                        //             planet.mass * SUN_MASS_IN_EARTH_MASSES,
                        //             sun.age / 1.0E9,
                        //             planet.first_moon == null ? "" : " WITH MOON",
                        //             temp - FREEZING_POINT_OF_WATER,
                        //             32 + ((temp - FREEZING_POINT_OF_WATER) * 1.8),
                        //             temp - EARTH_AVERAGE_KELVIN);
                        //}
                    }
                }
            }
            else
            {
                planet.EstimatedTemp = Environment.EstTemp(sun.EcosphereRadius, planet.SemiMajorAxisAU, GlobalConstants.EARTH_ALBEDO);
                planet.EstimatedTerrTemp = Environment.EstTemp(sun.EcosphereRadius, planet.SemiMajorAxisAU, GlobalConstants.EARTH_ALBEDO);

                planet.SurfaceGravity = Environment.Gravity(planet.SurfaceAcceleration);
                planet.MolecularWeightRetained = Environment.MinMolecularWeight(planet);

                planet.HasGreenhouseEffect = Environment.Greenhouse(sun.EcosphereRadius, planet.SemiMajorAxisAU);
                planet.VolatileGasInventory = Environment.VolumeInventory(
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

                // GL: Atmosphere was only being calculated for planets with liquid water for some reason?
                //if (do_gases && (planet.max_temp >= GlobalConstants.FREEZING_POINT_OF_WATER) && (planet.min_temp <= planet.boil_point))
                if (doGases)
                {
                    CalculateGases(ref sun, planet, planetID);
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

                        // TODO logging
                        //if (flag_verbose & 0x0001)
                        //    fprintf(stderr, "%12s\tp=%4.2Lf\tm=%4.2Lf\tg=%4.2Lf\tt=%+.1Lf\t%s\t Unknown %s\n",
                        //                    type_string(planet.type),
                        //                    planet.surf_pressure,
                        //                    planet.mass * SUN_MASS_IN_EARTH_MASSES,
                        //                    planet.surf_grav,
                        //                    planet.surf_temp - EARTH_AVERAGE_KELVIN,
                        //                    planet_id,
                        //                    ((int)planet.day == (int)(planet.orb_period * 24.0) ||
                        //                     (planet.resonant_period)) ? "(1-Face)" : ""
                        //             );
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

                            //sprintf(moon_id, "%s.%d", planet_id, n);
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

                            // TODO logging
                            //if (flag_verbose & 0x40000)
                            //{
                            //    fprintf(stderr,
                            //                "   Roche limit: R = %4.2Lg, rM = %4.2Lg, rm = %4.2Lg . %.0Lf km\n"
    
                            //                "   Hill Sphere: a = %4.2Lg, m = %4.2Lg, M = %4.2Lg . %.0Lf km\n"
    
                            //                "%s Moon orbit: a = %.0Lf km, e = %.0Lg\n",
                            //                planet.radius, planet.density, ptr.density,
                            //                roche_limit,
                            //                planet.a * KM_PER_AU, planet.mass * SOLAR_MASS_IN_KILOGRAMS, sun.mass * SOLAR_MASS_IN_KILOGRAMS,
                            //                hill_sphere,
                            //                moon_id,
                            //                ptr.moon_a * KM_PER_AU, ptr.moon_e
                            //            );
                            //}

                            //if (flag_verbose & 0x1000)
                            //{
                            //    fprintf(stderr, "  %s: (%7.2LfEM) %d %4.2LgEM\n",
                            //        planet_id,
                            //        planet.mass * SUN_MASS_IN_EARTH_MASSES,
                            //        n,
                            //        ptr.mass * SUN_MASS_IN_EARTH_MASSES);
                            //}
                        }
                    }
                }
            }

        }

        private void CalculateGases(ref Star sun, Planet planet, string planet_id)
        {
            if (planet.SurfPressure > 0)
            {
                //Trace.TraceInformation("Calculating surface gases.");

                double[] amount = new double[max_gas + 1];
                double totamount = 0;
                double pressure = planet.SurfPressure / GlobalConstants.MILLIBARS_PER_BAR;
                int n = 0;
                int i;

                for (i = 0; i < max_gas; i++)
                {
                    double yp = gases[i].boil / (373.0 * ((Math.Log((pressure) + 0.001) / -5050.5) + (1.0 / 373.0)));

                    if ((yp >= 0 && yp < planet.NighttimeTemp) && (gases[i].weight >= planet.MolecularWeightRetained))
                    {
                        double vrms = Environment.RMSVelocity(gases[i].weight, planet.ExosphereTemp);
                        double pvrms = Math.Pow(1 / (1 + vrms / planet.EscapeVelocity), sun.Age / 1e9);
                        double abund = gases[i].abunds; // gases[i].abunde
                        double react = 1.0;
                        double fract = 1.0;
                        double pres2 = 1.0;

                        if (gases[i].symbol == "Ar")
                        {
                            react = .15 * sun.Age / 4e9;
                        }
                        else if (gases[i].symbol == "He")
                        {
                            abund = abund * (0.001 + (planet.GasMass / planet.Mass));
                            pres2 = (0.75 + pressure);
                            react = Math.Pow(1 / (1 + gases[i].reactivity), sun.Age / 2e9 * pres2);
                        }
                        else if ((gases[i].symbol == "O" || gases[i].symbol == "O2") && sun.Age > 2e9 && planet.SurfaceTemp > 270 && planet.SurfaceTemp < 400)
                        {
                            // pres2 = (0.65 + pressure/2); // Breathable - M: .55-1.4
                            pres2 = (0.89 + pressure / 4);  // Breathable - M: .6 -1.8
                            react = Math.Pow(1 / (1 + gases[i].reactivity), Math.Pow(sun.Age / 2e9, 0.25) * pres2);
                        }
                        else if (gases[i].symbol == "CO2" && sun.Age > 2e9 && planet.SurfaceTemp > 270 && planet.SurfaceTemp < 400)
                        {
                            pres2 = (0.75 + pressure);
                            react = Math.Pow(1 / (1 + gases[i].reactivity), Math.Pow(sun.Age / 2e9, 0.5) * pres2);
                            react *= 1.5;
                        }
                        else
                        {
                            pres2 = (0.75 + pressure);
                            react = Math.Pow(1 / (1 + gases[i].reactivity), sun.Age / 2e9 * pres2);
                        }

                        fract = (1 - (planet.MolecularWeightRetained / gases[i].weight));

                        amount[i] = abund * pvrms * react * fract;

                        //if (gases[i].symbol == "O" || gases[i].symbol == "N" || gases[i].symbol == "Ar" || gases[i].symbol == "He" || gases[i].symbol == "CO2")
                        //{
                        //    Trace.TraceInformation("{0} {1}s, {2} = a {3} * p {4} * r {5} * p2 {6} * f {7}\t({8}%)\n",
                        //              planet.mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                        //              gases[i].symbol,
                        //              amount[i],
                        //              abund,
                        //              pvrms,
                        //              react,
                        //              pres2,
                        //              fract,
                        //              100.0 * (planet.gas_mass / planet.mass)
                        //             );
                        //}

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

                if (n > 0)
                {
                    planet.GasCount = n;
                    planet.AtmosphericGases = new Gas[n];

                    for (i = 0, n = 0; i < max_gas; i++)
                    {
                        if (amount[i] > 0.0)
                        {
                            planet.AtmosphericGases[n] = new Gas();
                            planet.AtmosphericGases[n].num = gases[i].num;
                            planet.AtmosphericGases[n].surf_pressure = planet.SurfPressure
                                                                * amount[i] / totamount;

                            //if ((planet.atmosphere[n].num == GlobalConstants.AN_O) && Environment.InspiredPartialPressure(planet.surf_pressure, planet.atmosphere[n].surf_pressure) > gases[i].max_ipp)
                            //{
                            //    Trace.TraceInformation("{0}\t Poisoned by O2", planet_id);
                            //}

                            n++;
                        }
                    }
                }
            }
        }

        private void CheckPlanet(ref Planet planet, string planetID, bool is_moon)
        {
            
            int tIndex = 0;

            switch (planet.Type)
            {
                case PlanetType.Unknown: tIndex = 0; break;
                case PlanetType.Barren: tIndex = 1; break;
                case PlanetType.Venusian: tIndex = 2; break;
                case PlanetType.Terrestrial: tIndex = 3; break;
                case PlanetType.SubSubGasGiant: tIndex = 4; break;
                case PlanetType.SubGasGiant: tIndex = 5; break;
                case PlanetType.GasGiant: tIndex = 6; break;
                case PlanetType.Martian: tIndex = 7; break;
                case PlanetType.Water: tIndex = 8; break;
                case PlanetType.Ice: tIndex = 9; break;
                case PlanetType.Asteroids: tIndex = 10; break;
            }

            if (type_counts[tIndex] == 0)
                ++type_count;

            ++type_counts[tIndex];

            // Check for and list planets with breathable atmospheres
            // TODO break this out into another function?
            Breathability breathe = Environment.Breathability(ref planet, max_gas, gases);

            // Option needed? <-- what is this referring to?
            if ((breathe == Breathability.Breathable) && (!planet.HasResonantPeriod) && ((int)planet.Day != (int)(planet.OrbitalPeriod * 24.0)))
            {
                //bool list_it = false;
                double illumination = Utilities.Pow2(1.0 / planet.SemiMajorAxisAU)
                                            * (planet.Star).Luminosity;

                habitable++;

                if (min_breathable_temp > planet.SurfaceTemp)
                {
                    min_breathable_temp = planet.SurfaceTemp;

                    //if (flag_verbose & 0x0002)
                    //    list_it = true;
                }

                if (max_breathable_temp < planet.SurfaceTemp)
                {
                    max_breathable_temp = planet.SurfaceTemp;

                    //if (flag_verbose & 0x0002)
                    //    list_it = true;
                }

                if (min_breathable_g > planet.SurfaceGravity)
                {
                    min_breathable_g = planet.SurfaceGravity;

                    //if (flag_verbose & 0x0002)
                    //    list_it = true;
                }

                if (max_breathable_g < planet.SurfaceGravity)
                {
                    max_breathable_g = planet.SurfaceGravity;

                    //if (flag_verbose & 0x0002)
                    //    list_it = true;
                }

                if (min_breathable_l > illumination)
                {
                    min_breathable_l = illumination;

                    //if (flag_verbose & 0x0002)
                    //    list_it = true;
                }

                if (max_breathable_l < illumination)
                {
                    max_breathable_l = illumination;

                    //if (flag_verbose & 0x0002)
                    //    list_it = true;
                }

                if (planet.Type == PlanetType.Terrestrial)
                {
                    if (min_breathable_terrestrial_g > planet.SurfaceGravity)
                    {
                        min_breathable_terrestrial_g = planet.SurfaceGravity;

                        //if (flag_verbose & 0x0002)
                        //    list_it = true;
                    }

                    if (max_breathable_terrestrial_g < planet.SurfaceGravity)
                    {
                        max_breathable_terrestrial_g = planet.SurfaceGravity;

                        //if (flag_verbose & 0x0002)
                        //    list_it = true;
                    }

                    if (min_breathable_terrestrial_l > illumination)
                    {
                        min_breathable_terrestrial_l = illumination;

                        //if (flag_verbose & 0x0002)
                        //    list_it = true;
                    }

                    if (max_breathable_terrestrial_l < illumination)
                    {
                        max_breathable_terrestrial_l = illumination;

                        //if (flag_verbose & 0x0002)
                        //    list_it = true;
                    }
                }

                if (min_breathable_p > planet.SurfPressure)
                {
                    min_breathable_p = planet.SurfPressure;

                    //if (flag_verbose & 0x0002)
                    //    list_it = true;
                }

                if (max_breathable_p < planet.SurfPressure)
                {
                    max_breathable_p = planet.SurfPressure;

                    //if (flag_verbose & 0x0002)
                    //    list_it = true;
                }

                //if (flag_verbose & 0x0004)
                //    list_it = true;

                //if (list_it)
                //    fprintf(stderr, "%12s\tp=%4.2Lf\tm=%4.2Lf\tg=%4.2Lf\tt=%+.1Lf\tl=%4.2Lf\t%s\n",
                //                type_string(planet.type),
                //                planet.surf_pressure,
                //                planet.mass * SUN_MASS_IN_EARTH_MASSES,
                //                planet.surf_grav,
                //                planet.surf_temp - EARTH_AVERAGE_KELVIN,
                //                illumination,
                //                planet_id);
            }
            

            if (is_moon && max_moon_mass < planet.Mass)
            {
                max_moon_mass = planet.Mass;

                //if (flag_verbose & 0x0002)
                //    fprintf(stderr, "%12s\tp=%4.2Lf\tm=%4.2Lf\tg=%4.2Lf\tt=%+.1Lf\t%s Moon Mass\n",
                //            type_string(planet.type),
                //            planet.surf_pressure,
                //            planet.mass * SUN_MASS_IN_EARTH_MASSES,
                //            planet.surf_grav,
                //            planet.surf_temp - EARTH_AVERAGE_KELVIN,
                //            planet_id);
            }

            //if ((flag_verbose & 0x0800) && (planet.dust_mass * SUN_MASS_IN_EARTH_MASSES >= 0.0006) && (planet.gas_mass * SUN_MASS_IN_EARTH_MASSES >= 0.0006) && (planet.type != tGasGiant) && (planet.type != tSubGasGiant))
            //{
            //    int core_size = (int)((50. * planet.core_radius) / planet.radius);

            //    if (core_size <= 49)
            //    {
            //        fprintf(stderr, "%12s\tp=%4.2Lf\tr=%4.2Lf\tm=%4.2Lf\t%s\t%d\n",
            //                type_string(planet.type),
            //                planet.core_radius,
            //                planet.radius,
            //                planet.mass * SUN_MASS_IN_EARTH_MASSES,
            //                planet_id,
            //                50 - core_size);
            //    }
            //}

            
            double rel_temp = (planet.SurfaceTemp - GlobalConstants.FREEZING_POINT_OF_WATER) - GlobalConstants.EARTH_AVERAGE_CELSIUS;
            double seas = (planet.WaterCover * 100.0);
            double clouds = (planet.CloudCover * 100.0);
            double pressure = (planet.SurfPressure / GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS);
            double ice = (planet.IceCover * 100.0);
            double gravity = planet.SurfaceGravity;
            breathe = Environment.Breathability(ref planet, max_gas, gases);

            // TODO this needs to be a separate function
            // is the world earthlike? 
            if ((gravity >= .8) && (gravity <= 1.2) && (rel_temp >= -2.0) && (rel_temp <= 3.0) && (ice <= 10.0) && (pressure >= 0.5) && (pressure <= 2.0) && (clouds >= 40.0) && (clouds <= 80.0) && (seas >= 50.0) && (seas <= 80.0) && (planet.Type != PlanetType.Water) && (breathe == Breathability.Breathable))
            {
                planet.IsEarthlike = true;
                earthlike++;
                //if (flag_verbose & 0x0008)
                //{
                //    fprintf(stderr, "%12s\tp=%4.2Lf\tm=%4.2Lf\tg=%4.2Lf\tt=%+.1Lf\t%d %s\tEarth-like\n",
                //                    type_string(planet.type),
                //                    planet.surf_pressure,
                //                    planet.mass * SUN_MASS_IN_EARTH_MASSES,
                //                    planet.surf_grav,
                //                    planet.surf_temp - EARTH_AVERAGE_KELVIN,
                //                    habitable,
                //                    planet_id);
                //}
            }

            planet.breathability = breathe;
            //else if ((flag_verbose & 0x0008) && (breathe == BREATHABLE) && (gravity > 1.3) && (habitable > 1) && ((rel_temp < -2.0) || (ice > 10.0)))
            //{
            //    fprintf(stderr, "%12s\tp=%4.2Lf\tm=%4.2Lf\tg=%4.2Lf\tt=%+.1Lf\t%s\tSphinx-like\n",
            //            type_string(planet.type),
            //            planet.surf_pressure,
            //            planet.mass * SUN_MASS_IN_EARTH_MASSES,
            //            planet.surf_grav,
            //            planet.surf_temp - EARTH_AVERAGE_KELVIN,
            //            planet_id);
            //}
        }
                
        private double GetOuterLimit(Star star)
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
