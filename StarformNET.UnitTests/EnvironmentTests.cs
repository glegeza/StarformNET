namespace DLS.StarformNET.UnitTests
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StarformNET;
    using StarformNET.Data;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    class EnvironmentTests
    {
        [TestClass]
        public class BreathabilityTests
        {
            private static Dictionary<string, ChemTable> TestGases = new Dictionary<string, ChemTable>()
            {
                {"N", new ChemTable(GlobalConstants.AN_N,  "N",    "N<SUB><SMALL>2</SMALL></SUB>",  "Nitrogen",        14.0067,  63.34,  77.40,  0.0012506, 1.99526e-05, 3.13329,       0,     GlobalConstants.MAX_N2_IPP ) },
                {"O", new ChemTable(GlobalConstants.AN_O,  "O",    "O<SUB><SMALL>2</SMALL></SUB>",  "Oxygen",          15.9994,  54.80,  90.20,  0.001429,  0.501187,    23.8232,       10,    GlobalConstants.MAX_O2_IPP ) },
                {"CO2", new ChemTable(GlobalConstants.AN_CO2, "CO2", "CO<SUB><SMALL>2</SMALL></SUB>", "CarbonDioxide",   44.0000, 194.66, 194.66,  0.001,     0.01,        0.0005,        0,     GlobalConstants.MAX_CO2_IPP) },
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
                planet.AtmosphericGases = mockAtmoGen();
                planet.GasCount = planet.AtmosphericGases.Length;
                foreach (var gas in planet.AtmosphericGases)
                {
                    planet.SurfPressure += gas.surf_pressure;
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
