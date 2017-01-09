namespace DLS.StarformNET.UnitTests
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StarformNET;
    using Data;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    class EnvironmentTests
    {
        [TestClass]
        public class RocheLimitTests
        {
            public static double SunDensity = 1.408;
            public static double SunRadius = 696000000;
            public static double EarthDensity = 5.513;
            public static double EarthRadius = 6378137;
            public static double MoonDensity = 3.346;
            public static double MoonRadius = 1737100;
            public static double JupiterDensity = 1.326;
            public static double JupiterRadius = 71493000;
            public static double SaturnDensity = 0.687;
            public static double SaturnRadius = 60267000;
            public static double AvgCometDensity = .5;

            [TestCategory("Roche Limit")]
            [TestMethod]
            public void TestEarthMoonRocheLimit()
            {
                var earthMoonKM = 9492;
                var earthMoonAU = earthMoonKM / GlobalConstants.KM_PER_AU;

                var dAU = StarformNET.Environment.RocheLimitAU(EarthRadius, EarthDensity, MoonDensity);
                Assert.AreEqual(earthMoonAU, dAU, 0.99);

                var dKM = StarformNET.Environment.RocheLimitKM(EarthRadius, EarthDensity, MoonDensity);
                Assert.AreEqual(earthMoonKM, dKM, 0.99);
            }

            [TestCategory("Roche Limit")]
            [TestMethod]
            public void TestEarthAverageCometRocheLimit()
            {
                var earthAvgCometKM = 17887;
                var earthAvgCometAU = earthAvgCometKM / GlobalConstants.KM_PER_AU;

                var dAU = StarformNET.Environment.RocheLimitAU(EarthRadius, EarthDensity, AvgCometDensity);
                Assert.AreEqual(earthAvgCometAU, dAU, 0.99);

                var dKM = StarformNET.Environment.RocheLimitKM(EarthRadius, EarthDensity, AvgCometDensity);
                Assert.AreEqual(earthAvgCometKM, dKM, 0.99);
            }

            [TestCategory("Roche Limit")]
            [TestMethod]
            public void TestSunEarthRocheLimit()
            {
                var sunEarthKM = 556397;
                var sunEarthAU = sunEarthKM / GlobalConstants.KM_PER_AU;

                var dAU = StarformNET.Environment.RocheLimitAU(SunRadius, SunDensity, EarthDensity);
                Assert.AreEqual(sunEarthAU, dAU, 0.99);

                var dKM = StarformNET.Environment.RocheLimitKM(SunRadius, SunDensity, EarthDensity);
                Assert.AreEqual(sunEarthKM, dKM, 0.99);
            }

            [TestCategory("Roche Limit")]
            [TestMethod]
            public void TestSunMoonRocheLimit()
            {
                var sunMoonKM = 657161;
                var sunMoonAU = sunMoonKM / GlobalConstants.KM_PER_AU;

                var dAU = StarformNET.Environment.RocheLimitAU(SunRadius, SunDensity, MoonDensity);
                Assert.AreEqual(sunMoonAU, dAU, 0.99);

                var dKM = StarformNET.Environment.RocheLimitKM(SunRadius, SunDensity, MoonDensity);
                Assert.AreEqual(sunMoonKM, dKM, 0.99);
            }

            [TestCategory("Roche Limit")]
            [TestMethod]
            public void TestSunJupiterRocheLimit()
            {
                var sunJupiterKM = 894677;
                var sunJupiterAU = sunJupiterKM / GlobalConstants.KM_PER_AU;

                var dAU = StarformNET.Environment.RocheLimitAU(SunRadius, SunDensity, JupiterDensity);
                Assert.AreEqual(sunJupiterAU, dAU, 0.99);

                var dKM = StarformNET.Environment.RocheLimitKM(SunRadius, SunDensity, JupiterDensity);
                Assert.AreEqual(sunJupiterKM, dKM, 0.99);
            }
        }

        [TestClass]
        public class BreathabilityTests
        {
            private static Dictionary<string, ChemType> TestGases = new Dictionary<string, ChemType>()
            {
                {"N", new ChemType(GlobalConstants.AN_N,  "N",    "N<SUB><SMALL>2</SMALL></SUB>",  "Nitrogen",        14.0067,  63.34,  77.40,  0.0012506, 1.99526e-05, 3.13329,       0,     GlobalConstants.MAX_N2_IPP ) },
                {"O", new ChemType(GlobalConstants.AN_O,  "O",    "O<SUB><SMALL>2</SMALL></SUB>",  "Oxygen",          15.9994,  54.80,  90.20,  0.001429,  0.501187,    23.8232,       10,    GlobalConstants.MAX_O2_IPP ) },
                {"CO2", new ChemType(GlobalConstants.AN_CO2, "CO2", "CO<SUB><SMALL>2</SMALL></SUB>", "CarbonDioxide",   44.0000, 194.66, 194.66,  0.001,     0.01,        0.0005,        0,     GlobalConstants.MAX_CO2_IPP) },
            };

            private Gas[] GetMockBreathableAtmo()
            {
                return new Gas[]
                {
                    new Gas(TestGases["O"], GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS * 0.21 ),
                    new Gas(TestGases["N"], GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS * 0.78 )
                };
            }

            private Gas[] GetMockPoisonousAtmo()
            {
                return new Gas[]
                {
                    new Gas(TestGases["CO2"], GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS )
                };
            }

            private Gas[] GetMockUnbreathableAtmo()
            {
                return new Gas[]
                {
                    new Gas(TestGases["N"], GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS )
                };
            }

            private Gas[] GetMockNoAtmo()
            {
                return new Gas[0];
            }

            private Planet GetMockPlanet(Func<Gas[]> mockAtmoGen)
            {
                var planet = new Planet();
                planet.Atmosphere.Composition = mockAtmoGen().ToList();
                foreach (var gas in planet.Atmosphere.Composition)
                {
                    planet.Atmosphere.SurfacePressure += gas.surf_pressure;
                }
                return planet;
            }

            [TestCategory("Breathability")]
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void TestNullPlanet()
            {
                var breathe = StarformNET.Environment.Breathability(null);
            }


            [TestCategory("Breathability")]
            [TestMethod]
            public void TestNoAtmoPlanet()
            {
                var planet = GetMockPlanet(GetMockNoAtmo);
                var breathe = StarformNET.Environment.Breathability(planet);
                Assert.AreEqual(Breathability.None, breathe);
            }

            [TestCategory("Breathability")]
            [TestMethod]
            public void TestBreathablePlanet()
            {
                var planet = GetMockPlanet(GetMockBreathableAtmo);
                var breathe = StarformNET.Environment.Breathability(planet);
                Assert.AreEqual(Breathability.Breathable, breathe);
            }

            [TestCategory("Breathability")]
            [TestMethod]
            public void TestUnbreathablePlanet()
            {
                var planet = GetMockPlanet(GetMockUnbreathableAtmo);
                var breathe = StarformNET.Environment.Breathability(planet);
                Assert.AreEqual(Breathability.Unbreathable, breathe);
            }

            [TestCategory("Breathability")]
            [TestMethod]
            public void TestPoisonousPlanet()
            {
                var planet = GetMockPlanet(GetMockPoisonousAtmo);
                var breathe = StarformNET.Environment.Breathability(planet);
                Assert.AreEqual(Breathability.Poisonous, breathe);
            }
        }
    }
}
