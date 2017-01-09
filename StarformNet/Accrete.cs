namespace DLS.StarformNET
{
    using System;
    using Data;
    using System.Collections.Generic;

    public class Accrete
    {
        public double CloudEccentricity;
        public double GasDustRatio;

        private bool _dustLeft;
        private double _rInner;
        private double _rOuter;
        private double _reducedMass;
        private double _dustDensity;
        private double _cloudEccentricity;
        private DustRecord _dustHead;
        private PlanetSeed _planetHead;
        private Generation _histHead;

        public Accrete(double e, double gdr)
        {
            CloudEccentricity = e;
            GasDustRatio = gdr;
        }

        public static double GetStellarDustLimit(double stellarMassRatio)
        {
            return (200.0 * Math.Pow(stellarMassRatio, (1.0 / 3.0)));
        }

        // TODO documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stellarMassRatio"></param>
        /// <param name="stellarLumRatio"></param>
        /// <param name="innerDust"></param>
        /// <param name="outerDust"></param>
        /// <param name="outerPlanetLimit"></param>
        /// <param name="dustDensityCoeff"></param>
        /// <param name="seedSystem"></param>
        /// <param name="doMoons"></param>
        /// <returns></returns>
        public List<PlanetSeed> GetPlanetaryBodies(double stellarMassRatio, 
            double stellarLumRatio, double innerDust, double outerDust,
            double outerPlanetLimit, double dustDensityCoeff, 
            PlanetSeed seedSystem, bool doMoons)
        {
            SetInitialConditions(innerDust, outerDust);

            double planet_inner_bound = NearestPlanet(stellarMassRatio);
            double planet_outer_bound = outerPlanetLimit == 0 
                ? FarthestPlanet(stellarMassRatio) 
                : outerPlanetLimit;

            PlanetSeed seeds = seedSystem;
            while (_dustLeft)
            {
                double a, e;
                if (seeds != null)
                {
                    a = seeds.SemiMajorAxisAU;
                    e = seeds.Eccentricity;
                    seeds = seeds.NextPlanet;
                }
                else
                {
                    a = Utilities.RandomNumber(planet_inner_bound, planet_outer_bound);
                    e = Utilities.RandomEccentricity();
                }

                double mass = GlobalConstants.PROTOPLANET_MASS;
                double dust_mass = 0;
                double gas_mass = 0;

                //Trace.TraceInformation("Checking {0:0.00} AU", a);

                if (DustAvailable(InnerEffectLimit(a, e, mass), OuterEffectLimit(a, e, mass)))
                {
                    //Trace.TraceInformation("Injecting protoplanet at {0:0.00} AU", a);

                    _dustDensity = dustDensityCoeff * Math.Sqrt(stellarMassRatio) * Math.Exp(-GlobalConstants.ALPHA * Math.Pow(a, (1.0 / GlobalConstants.N)));
                    double crit_mass = CriticalLimit(a, e, stellarLumRatio);
                    AccreteDust(ref mass, ref dust_mass, ref gas_mass, a, e, crit_mass, planet_inner_bound, planet_outer_bound);

                    dust_mass += GlobalConstants.PROTOPLANET_MASS;

                    if (mass > GlobalConstants.PROTOPLANET_MASS)
                    {
                        CoalescePlanetesimals(a, e, mass, crit_mass,
                                               dust_mass, gas_mass,
                                               stellarLumRatio,
                                               planet_inner_bound, planet_outer_bound,
                                               doMoons);
                    }

                    //Trace.TraceInformation(".. failed due to large neighbor.");
                }

                //Trace.TraceInformation(".. failed");
            }

            var seedList = new List<PlanetSeed>();
            var next = _planetHead;
            while (next != null)
            {
                seedList.Add(next);
                next = next.NextPlanet;
            }
            return seedList;
        }

        private void SetInitialConditions(double inner_limit_of_dust, double outer_limit_of_dust)
        {
            _histHead = new Generation();

            _planetHead = null;
            _dustHead = new DustRecord();
            _dustHead.NextBand = null;
            _dustHead.OuterEdge = outer_limit_of_dust;
            _dustHead.InnerEdge = inner_limit_of_dust;
            _dustHead.DustPresent = true;
            _dustHead.GasPresent = true;
            _dustLeft = true;
            _cloudEccentricity = CloudEccentricity;

            _histHead.Dusts = _dustHead;
            _histHead.Planets = _planetHead;
            _histHead.Next = _histHead;
        }

        private double NearestPlanet(double stell_mass_ratio)
        {
            return (0.3 * Math.Pow(stell_mass_ratio, (1.0 / 3.0)));
        }

        private double FarthestPlanet(double stell_mass_ratio)
        {
            return (50.0 * Math.Pow(stell_mass_ratio, (1.0 / 3.0)));
        }

        private double InnerEffectLimit(double a, double e, double mass)
        {
            return (a * (1.0 - e) * (1.0 - mass) / (1.0 + _cloudEccentricity));
        }

        private double OuterEffectLimit(double a, double e, double mass)
        {
            return (a * (1.0 + e) * (1.0 + mass) / (1.0 - _cloudEccentricity));
        }

        private bool DustAvailable(double inside_range, double outside_range)
        {
            DustRecord current_dust_band;
            bool dust_here;

            current_dust_band = _dustHead;
            while (current_dust_band != null && current_dust_band.OuterEdge < inside_range)
            {
                current_dust_band = current_dust_band.NextBand;
            }

            if (current_dust_band == null)
            {
                dust_here = false;
            }
            else
            {
                dust_here = current_dust_band.DustPresent;
                while (current_dust_band != null && current_dust_band.InnerEdge < outside_range)
                {
                    dust_here = dust_here || current_dust_band.DustPresent;
                    current_dust_band = current_dust_band.NextBand;
                }
            }

            return dust_here;
        }

        private void UpdateDustLanes(double min, double max, double mass, double crit_mass, double body_inner_bound, double body_outer_bound)
        {
            bool gas;
            DustRecord node1 = null;
            DustRecord node2 = null;
            DustRecord node3 = null;

            _dustLeft = false;
            if (mass > crit_mass)
            {
                gas = false;
            }
            else
            {
                gas = true;
            }

            node1 = _dustHead;
            while (node1 != null)
            {
                if (node1.InnerEdge < min && node1.OuterEdge > max)
                {
                    node2 = new DustRecord();
                    node2.InnerEdge = min;
                    node2.OuterEdge = max;
                    if ((node1.GasPresent == true))
                    {
                        node2.GasPresent = gas;
                    }
                    else
                    {
                        node2.GasPresent = false;
                    }
                    node2.DustPresent = false;
                    node3 = new DustRecord();
                    node3.InnerEdge = max;
                    node3.OuterEdge = node1.OuterEdge;
                    node3.GasPresent = node1.GasPresent;
                    node3.DustPresent = node1.DustPresent;
                    node3.NextBand = node1.NextBand;
                    node1.NextBand = node2;
                    node2.NextBand = node3;
                    node1.OuterEdge = min;
                    node1 = node3.NextBand;
                }
                else if (node1.InnerEdge < max && node1.OuterEdge > max)
                {
                    node2 = new DustRecord();
                    node2.NextBand = node1.NextBand;
                    node2.DustPresent = node1.DustPresent;
                    node2.GasPresent = node1.GasPresent;
                    node2.OuterEdge = node1.OuterEdge;
                    node2.InnerEdge = max;
                    node1.NextBand = node2;
                    node1.OuterEdge = max;
                    if ((node1.GasPresent == true))
                    {
                        node1.GasPresent = gas;
                    }
                    else
                    {
                        node1.GasPresent = false;
                    }
                    node1.DustPresent = false;
                    node1 = node2.NextBand;
                }
                else if (node1.InnerEdge < min && node1.OuterEdge > min)
                {
                    node2 = new DustRecord();
                    node2.NextBand = node1.NextBand;
                    node2.DustPresent = false;
                    if ((node1.GasPresent == true))
                    {
                        node2.GasPresent = gas;
                    }
                    else
                    {
                        node2.GasPresent = false;
                    }
                    node2.OuterEdge = node1.OuterEdge;
                    node2.InnerEdge = min;
                    node1.NextBand = node2;
                    node1.OuterEdge = min;
                    node1 = node2.NextBand;
                }
                else if (node1.InnerEdge >= min && node1.OuterEdge <= max)
                {
                    if (node1.GasPresent == true)
                    {
                        node1.GasPresent = gas;
                    }
                    node1.DustPresent = false;
                    node1 = node1.NextBand;
                }
                else if (node1.OuterEdge < min || node1.InnerEdge > max)
                {
                    node1 = node1.NextBand;
                }
            }
            node1 = _dustHead;
            while (node1 != null)
            {
                if (node1.DustPresent && (node1.OuterEdge >= body_inner_bound && node1.InnerEdge <= body_outer_bound))
                {
                    _dustLeft = true;
                }

                node2 = node1.NextBand;
                if (node2 != null)
                {
                    if (node1.DustPresent == node2.DustPresent && node1.GasPresent == node2.GasPresent)
                    {
                        node1.OuterEdge = node2.OuterEdge;
                        node1.NextBand = node2.NextBand;
                        node2 = null;
                    }
                }
                node1 = node1.NextBand;
            }
        }

        private double CollectDust(double last_mass, ref double new_dust, ref double new_gas, double a, double e, double crit_mass, ref DustRecord dust_band)
        {
            double temp = last_mass / (1.0 + last_mass);
            _reducedMass = Math.Pow(temp, (1.0 / 4.0));
            _rInner = InnerEffectLimit(a, e, _reducedMass);
            _rOuter = OuterEffectLimit(a, e, _reducedMass);

            if (_rInner < 0.0)
            {
                _rInner = 0.0;
            }

            if (dust_band == null)
            {
                return (0.0);
            }
            else
            {
                double gas_density = 0.0;
                double temp_density;
                double mass_density;
                if (dust_band.DustPresent == false)
                {
                    temp_density = 0.0;
                }
                else
                {
                    temp_density = _dustDensity;
                }

                if (last_mass < crit_mass || dust_band.GasPresent == false)
                {
                    mass_density = temp_density;
                }
                else
                {
                    mass_density = GasDustRatio * temp_density / (1.0 + Math.Sqrt(crit_mass / last_mass) * (GasDustRatio - 1.0));
                    gas_density = mass_density - temp_density;
                }

                if (dust_band.OuterEdge <= _rInner || dust_band.InnerEdge >= _rOuter)
                {
                    return (CollectDust(last_mass, ref new_dust, ref new_gas, a, e, crit_mass, ref dust_band.NextBand));
                }
                else
                {
                    double bandwidth = (_rOuter - _rInner);

                    double temp1 = _rOuter - dust_band.OuterEdge;
                    if (temp1 < 0.0)
                    {
                        temp1 = 0.0;
                    }
                    double width = bandwidth - temp1;

                    double temp2 = dust_band.InnerEdge - _rInner;
                    if (temp2 < 0.0)
                    {
                        temp2 = 0.0;
                    }
                    width = width - temp2;

                    temp = 4.0 * Math.PI * Math.Pow(a, 2.0) * _reducedMass * (1.0 - e * (temp1 - temp2) / bandwidth);
                    double volume = temp * width;

                    double new_mass = volume * mass_density;
                    new_gas = volume * gas_density;
                    new_dust = new_mass - new_gas;

                    double next_dust = 0;
                    double next_gas = 0;
                    double next_mass = CollectDust(last_mass, ref next_dust, ref next_gas, a, e, crit_mass, ref dust_band.NextBand);

                    new_gas = new_gas + next_gas;
                    new_dust = new_dust + next_dust;

                    return (new_mass + next_mass);
                }
            }
        }

        private double CriticalLimit(double orb_radius, double eccentricity, double stell_luminosity_ratio)
        {
            double perihelion_dist = (orb_radius - orb_radius * eccentricity);
            double temp = perihelion_dist * Math.Sqrt(stell_luminosity_ratio);
            return GlobalConstants.B * Math.Pow(temp, -0.75);
        }

        private void AccreteDust(ref double seed_mass, ref double new_dust, ref double new_gas, double a, double e, double crit_mass, double body_inner_bound, double body_outer_bound)
        {
            double new_mass = seed_mass;
            double temp_mass;

            do
            {
                temp_mass = new_mass;
                new_mass = CollectDust(new_mass, ref new_dust, ref new_gas, a, e, crit_mass, ref _dustHead);
            }
            while (!(new_mass - temp_mass < 0.0001 * temp_mass));

            seed_mass = seed_mass + new_mass;
            UpdateDustLanes(_rInner, _rOuter, seed_mass, crit_mass, body_inner_bound, body_outer_bound);
        }

        private void CoalescePlanetesimals(double a, double e, double mass, double crit_mass, double dust_mass, double gas_mass, double stell_luminosity_ratio, double body_inner_bound, double body_outer_bound, bool do_moons)
        {
            PlanetSeed the_planet;
            PlanetSeed next_planet;
            PlanetSeed prev_planet;
            bool finished;

            finished = false;
            prev_planet = null;

            // First we try to find an existing planet with an over-lapping orbit.

            for (the_planet = _planetHead; the_planet != null; the_planet = the_planet.NextPlanet)
            {
                double diff = the_planet.SemiMajorAxisAU - a;
                double dist1;
                double dist2;

                if (diff > 0.0)
                {
                    dist1 = (a * (1.0 + e) * (1.0 + _reducedMass)) - a;
                    /* x aphelion	 */
                    _reducedMass = Math.Pow((the_planet.Mass / (1.0 + the_planet.Mass)), (1.0 / 4.0));
                    dist2 = the_planet.SemiMajorAxisAU
                        - (the_planet.SemiMajorAxisAU * (1.0 - the_planet.Eccentricity) * (1.0 - _reducedMass));
                }
                else
                {
                    dist1 = a - (a * (1.0 - e) * (1.0 - _reducedMass));
                    /* x perihelion */
                    _reducedMass = Math.Pow((the_planet.Mass / (1.0 + the_planet.Mass)), (1.0 / 4.0));
                    dist2 = (the_planet.SemiMajorAxisAU * (1.0 + the_planet.Eccentricity) * (1.0 + _reducedMass))
                        - the_planet.SemiMajorAxisAU;
                }

                if (Math.Abs(diff) <= Math.Abs(dist1) || Math.Abs(diff) <= Math.Abs(dist2))
                {
                    double new_dust = 0;
                    double new_gas = 0;
                    double new_a = (the_planet.Mass + mass) / ((the_planet.Mass / the_planet.SemiMajorAxisAU) + (mass / a));

                    double temp = the_planet.Mass * Math.Sqrt(the_planet.SemiMajorAxisAU) * Math.Sqrt(1.0 - Math.Pow(the_planet.Eccentricity, 2.0));
                    temp = temp + (mass * Math.Sqrt(a) * Math.Sqrt(Math.Sqrt(1.0 - Math.Pow(e, 2.0))));
                    temp = temp / ((the_planet.Mass + mass) * Math.Sqrt(new_a));
                    temp = 1.0 - Math.Pow(temp, 2.0);
                    if (temp < 0.0 || temp >= 1.0)
                    {
                        temp = 0.0;
                    }
                    e = Math.Sqrt(temp);

                    // TODO Consider breaking this off into another function?
                    if (do_moons)
                    {
                        double existing_mass = 0.0;

                        if (the_planet.FirstMoon != null)
                        {
                            //PlanetSeed m;

                            for (PlanetSeed m = the_planet.FirstMoon; m != null; m = m.NextPlanet)
                            {
                                existing_mass += m.Mass;
                            }
                        }

                        if (mass < crit_mass)
                        {
                            if (mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES < 2.5 && mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES > .0001 && existing_mass < the_planet.Mass * .05)
                            {
                                PlanetSeed the_moon = new PlanetSeed(0, 0, mass, dust_mass, gas_mass);

                                if (the_moon.DustMass + the_moon.GasMass > the_planet.DustMass + the_planet.GasMass)
                                {
                                    double temp_dust = the_planet.DustMass;
                                    double temp_gas = the_planet.GasMass;
                                    double temp_mass = the_planet.Mass;

                                    the_planet.DustMass = the_moon.DustMass;
                                    the_planet.GasMass = the_moon.GasMass;
                                    the_planet.Mass = the_moon.Mass;

                                    the_moon.DustMass = temp_dust;
                                    the_moon.GasMass = temp_gas;
                                    the_moon.Mass = temp_mass;
                                }

                                if (the_planet.FirstMoon == null)
                                {
                                    the_planet.FirstMoon = the_moon;
                                }
                                else
                                {
                                    the_moon.NextPlanet = the_planet.FirstMoon;
                                    the_planet.FirstMoon = the_moon;
                                }

                                finished = true;

                                //Trace.TraceInformation("Moon captured... {0:0.00} AU ({1:0.00}EM) <- {2:0.00} EM", 
                                //    the_planet.a, 
                                //    the_planet.mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                                //    mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES);
                            }
                            //else
                            //{
                            //    Trace.TraceInformation("Moon escapes... {0:0.00} AU ({1:0.00} EM){2} {3:0.00}L {4}", 
                            //        the_planet.a,
                            //        the_planet.mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                            //        existing_mass < (the_planet.mass * .05) ? "" : " (big moons)",
                            //        mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                            //        (mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES) >= 2.5 ? ", too big" : (mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES) <= .0001 ? ", too small" : "");
                            //}
                        }
                    }

                    if (!finished)
                    {
                        //Trace.TraceInformation("Collision between two planetesimals!\n {0:0.00} AU ({1:0.00}EM) + {2:0.00} AU ({3:0.00}EM = {4:0.00}EMd + {5:0.00}EMg [{6:0.00}EM]). {7:0.00} AU ({8:0.00})",
                        //    the_planet.a, the_planet.mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                        //    a, mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                        //    dust_mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                        //    gas_mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                        //    crit_mass * GlobalConstants.SUN_MASS_IN_EARTH_MASSES,
                        //    new_a, e);

                        temp = the_planet.Mass + mass;
                        AccreteDust(ref temp, ref new_dust, ref new_gas,
                                     new_a, e, stell_luminosity_ratio,
                                     body_inner_bound, body_outer_bound);

                        the_planet.SemiMajorAxisAU = new_a;
                        the_planet.Eccentricity = e;
                        the_planet.Mass = temp;
                        the_planet.DustMass += dust_mass + new_dust;
                        the_planet.GasMass += gas_mass + new_gas;
                        if (temp >= crit_mass)
                        {
                            the_planet.IsGasGiant = true;
                        }

                        while (the_planet.NextPlanet != null && the_planet.NextPlanet.SemiMajorAxisAU < new_a)
                        {
                            next_planet = the_planet.NextPlanet;

                            if (the_planet == _planetHead)
                            {
                                _planetHead = next_planet;
                            }
                            else
                            {
                                prev_planet.NextPlanet = next_planet;
                            }

                            the_planet.NextPlanet = next_planet.NextPlanet;
                            next_planet.NextPlanet = the_planet;
                            prev_planet = next_planet;
                        }
                    }

                    finished = true;
                    break;
                }
                else
                {
                    prev_planet = the_planet;
                }
            }

            if (!(finished)) // Planetesimals didn't collide. Make it a planet.
            {
                the_planet = new PlanetSeed(a, e, mass, dust_mass, gas_mass);
                
                //the_planet.SemiMajorAxisAU = a;
                //the_planet.Eccentricity = e;
                //the_planet.Mass = mass;
                //the_planet.DustMass = dust_mass;
                //the_planet.GasMass = gas_mass;

                if (mass >= crit_mass)
                {
                    the_planet.IsGasGiant = true;
                }
                else
                {
                    the_planet.IsGasGiant = false;
                }

                if (_planetHead == null)
                {
                    _planetHead = the_planet;
                    the_planet.NextPlanet = null;
                }
                else if (a < _planetHead.SemiMajorAxisAU)
                {
                    the_planet.NextPlanet = _planetHead;
                    _planetHead = the_planet;
                }
                else if (_planetHead.NextPlanet == null)
                {
                    _planetHead.NextPlanet = the_planet;
                    the_planet.NextPlanet = null;
                }
                else
                {
                    next_planet = _planetHead;
                    while (next_planet != null && next_planet.SemiMajorAxisAU < a)
                    {
                        prev_planet = next_planet;
                        next_planet = next_planet.NextPlanet;
                    }
                    the_planet.NextPlanet = next_planet;
                    prev_planet.NextPlanet = the_planet;
                }
            }
        }

    }
}