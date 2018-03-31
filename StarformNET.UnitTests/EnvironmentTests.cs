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
        public class GasLifeTest
        {
            // Note these expected values aren't based on anything. They are
            // just the output from the function expected given certain input
            // values. They are intended for regression testing, not to prove
            // 'correctness' of the function.

            private static readonly double[] Weights =
            {
                GlobalConstants.ATOMIC_HYDROGEN,
                GlobalConstants.MOL_HYDROGEN,
                GlobalConstants.ATOMIC_NITROGEN,
                GlobalConstants.MOL_NITROGEN,
                GlobalConstants.MOL_OXYGEN,
                GlobalConstants.WATER_VAPOR
            };

            [TestCategory("GasLife")]
            [TestMethod]
            public void TestGasesEarth()
            {
                double[] expected =
                {
                    0.000886904496537799,
                    0.0469963194268096,
                    double.MaxValue,
                    double.MaxValue,
                    double.MaxValue,
                    double.MaxValue
                };
                CheckGasValues(1500, 1.0, 6371, expected, Weights);
            }

            [TestCategory("GasLife")]
            [TestMethod]
            public void TestGasesHighExoTemp()
            {
                double[] expected =
                {
                    0.000442164077775748,
                    0.000215237230333243,
                    0.000539283973908679,
                    0.0167717184670422,
                    0.0493293473613224,
                    0.00132928020274972
                };
                CheckGasValues(4500, 0.5, 2440, expected, Weights);
            }

            [TestCategory("GasLife")]
            [TestMethod]
            public void TestReturnNaNZeroTemp()
            {
                Assert.AreEqual(double.NaN, StarformNET.Environment.GasLife(
                    GlobalConstants.ATOMIC_NITROGEN, 0, 0.5, 2440));
            }

            [TestCategory("GasLife")]
            [TestMethod]
            public void TestReturnNaNNegativeTemp()
            {
                Assert.AreEqual(double.NaN, StarformNET.Environment.GasLife(
                    GlobalConstants.ATOMIC_NITROGEN, -100, 0.5, 2440));
            }

            private static void CheckGasValues(double exo, double surfG, double radius, 
                IReadOnlyList<double> expected, IReadOnlyList<double> weights)
            {
                for (var i = 0; i < expected.Count; i++)
                {
                    var e = expected[i];
                    var w = weights[i];
                    Assert.AreEqual(e, StarformNET.Environment.GasLife(
                        w, exo, surfG, radius), 0.000001);
                }
            }
        }

        [TestClass]
        public class EcosphereTests
        {
            [TestCategory("Ecosphere")]
            [TestMethod]
            public void TestSunEcosphere()
            {
                const double expectedValue = 1.0;
                const double sunLuminosity = 1.0;

                Assert.AreEqual(expectedValue, StarformNET.Environment.StarEcosphereRadiusAU(sunLuminosity), 0.0001);
            }
        }

        [TestClass]
        public class IlluminationTests
        {
            [TestCategory("Illumination")]
            [TestMethod]
            public void TestSunIllumination()
            {
                var expectedValue = 1.0;
                var sunLuminosity = 1.0;
                var earthSemiMajorAxis = 1.0;

                Assert.AreEqual(expectedValue, StarformNET.Environment.MinimumIllumination(earthSemiMajorAxis, sunLuminosity));
            }
        }

        [TestClass]
        public class HillSphereTests
        {
            // Expected hill sphere value from:
            // http://orbitsimulator.com/formulas/hillsphere.html

            public static double SunMass = 1;
            public static double EarthMass = 0.000003003;
            public static double MercuryMass = 0.0000001652;
            public static double VenusMass = 0.000002447;
            public static double JupiterMass = 0.0009543;

            public static double EarthSemiMajorAxisKM = 149600000;
            public static double EarthSemiMajorAxisAU = EarthSemiMajorAxisKM / GlobalConstants.KM_PER_AU;
            public static double MercurySemiMajorAxisKM = 57909050;
            public static double MercurySemiMajorAxisAU = MercurySemiMajorAxisKM / GlobalConstants.KM_PER_AU;
            public static double VenusSemiMajorAxisKM = 108208000;
            public static double VenusSemiMajorAxisAU = VenusSemiMajorAxisKM / GlobalConstants.KM_PER_AU;
            public static double JupiterSemiMajorAxisKM = 778297882;
            public static double JupiterSemiMajorAxisAU = JupiterSemiMajorAxisKM / GlobalConstants.KM_PER_AU;

            [TestCategory("Hill Sphere")]
            [TestMethod]
            public void TestSunEarthHillSphere()
            {
                var earthSphereKM = 1496498;
                var earthSphereAU = earthSphereKM / GlobalConstants.KM_PER_AU;

                var hAU = StarformNET.Environment.SimplifiedHillSphereAU(SunMass, EarthMass, EarthSemiMajorAxisAU);
                Assert.AreEqual(earthSphereAU, hAU, 0.001);

                var hKM = StarformNET.Environment.SimplifiedHillSphereKM(SunMass, EarthMass, EarthSemiMajorAxisAU);
                Assert.AreEqual(earthSphereKM, hKM, 0.99);
            }

            [TestCategory("Hill Sphere")]
            [TestMethod]
            public void TestSunMercuryHillSphere()
            {
                var mercurySphereKM = 220314;
                var mercurySphereAU = mercurySphereKM / GlobalConstants.KM_PER_AU;

                var hAU = StarformNET.Environment.SimplifiedHillSphereAU(SunMass, MercuryMass, MercurySemiMajorAxisAU);
                Assert.AreEqual(mercurySphereAU, hAU, 0.001);

                var hKM = StarformNET.Environment.SimplifiedHillSphereKM(SunMass, MercuryMass, MercurySemiMajorAxisAU);
                Assert.AreEqual(mercurySphereKM, hKM, 0.99);
            }

            [TestCategory("Hill Sphere")]
            [TestMethod]
            public void TestSunVenusHillSphere()
            {
                var venusSphereKM = 1011028;
                var venusSphereAU = venusSphereKM / GlobalConstants.KM_PER_AU;

                var hAU = StarformNET.Environment.SimplifiedHillSphereAU(SunMass, VenusMass, VenusSemiMajorAxisAU);
                Assert.AreEqual(venusSphereAU, hAU, 0.001);

                var hKM = StarformNET.Environment.SimplifiedHillSphereKM(SunMass, VenusMass, VenusSemiMajorAxisAU);
                Assert.AreEqual(venusSphereKM, hKM, 0.99);
            }

            [TestCategory("Hill Sphere")]
            [TestMethod]
            public void TestSunJupiterHillSphere()
            {
                var jupiterSphereKM = 53129256;
                var jupiterSphereAU = jupiterSphereKM / GlobalConstants.KM_PER_AU;

                var hAU = StarformNET.Environment.SimplifiedHillSphereAU(SunMass, JupiterMass, JupiterSemiMajorAxisAU);
                Assert.AreEqual(jupiterSphereAU, hAU, 0.001);

                var hKM = StarformNET.Environment.SimplifiedHillSphereKM(SunMass, JupiterMass, JupiterSemiMajorAxisAU);
                Assert.AreEqual(jupiterSphereKM, hKM, 0.99);
            }
        }

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
