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
            private Gas[] GetMockBreathableAtmo()
            {
                return new Gas[]
                {
                    new Gas() { num = 8, surf_pressure = GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS * 0.21 },   // Oxygen
                    new Gas() { num = 7, surf_pressure = GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS * 0.78 }    // Nitrogen
                };
            }

            private Gas[] GetMockPoisonousAtmo()
            {
                return new Gas[]
                {
                    new Gas() { num = 902, surf_pressure = GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS } // CO2
                };
            }

            private Gas[] GetMockUnbreathableAtmo()
            {
                return new Gas[]
                {
                    new Gas() {num = 7, surf_pressure = GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS } // Nitrogen
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
                var breathe = StarformNET.Environment.Breathability(null, ChemTable.GetDefaultTable());
            }

            [TestCategory("Breathability")]
            [ExpectedException(typeof(ArgumentNullException))]
            [TestMethod]
            public void TestNullChemTable()
            {
                var planet = GetMockPlanet(GetMockPoisonousAtmo);
                var breathe = StarformNET.Environment.Breathability(planet, null);
            }

            [TestCategory("Breathability")]
            [TestMethod]
            public void TestNoAtmoPlanet()
            {
                var planet = GetMockPlanet(GetMockNoAtmo);
                var breathe = StarformNET.Environment.Breathability(planet, ChemTable.GetDefaultTable());
                Assert.AreEqual(Breathability.None, breathe);
            }

            [TestCategory("Breathability")]
            [TestMethod]
            public void TestBreathablePlanet()
            {
                var planet = GetMockPlanet(GetMockBreathableAtmo);
                var breathe = StarformNET.Environment.Breathability(planet, ChemTable.GetDefaultTable());
                Assert.AreEqual(Breathability.Breathable, breathe);
            }

            [TestCategory("Breathability")]
            [TestMethod]
            public void TestUnbreathablePlanet()
            {
                var planet = GetMockPlanet(GetMockUnbreathableAtmo);
                var breathe = StarformNET.Environment.Breathability(planet, ChemTable.GetDefaultTable());
                Assert.AreEqual(Breathability.Unbreathable, breathe);
            }

            [TestCategory("Breathability")]
            [TestMethod]
            public void TestPoisonousPlanet()
            {
                var planet = GetMockPlanet(GetMockPoisonousAtmo);
                var breathe = StarformNET.Environment.Breathability(planet, ChemTable.GetDefaultTable());
                Assert.AreEqual(Breathability.Poisonous, breathe);
            }
        }
    }
}
