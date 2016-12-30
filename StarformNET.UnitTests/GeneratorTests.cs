namespace DLS.StarformNET.UnitTests
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StarformNET;
    using StarformNET.Data;
    using System.Linq;
    using System.Collections.Generic;

    class GeneratorTests
    {
        [TestClass]
        public class CalculateGasesTest
        {
            private double DELTA = 0.0001;

            private Star GetTestStar()
            {
                return new Star()
                {
                    Luminosity = 1.0,
                    Mass = 1.0,
                    Age = 4600000000
                };
            }

            private Planet GetTestPlanetAtmosphere()
            {
                var planet = new Planet();
                planet.Star = GetTestStar();
                planet.Star.EcosphereRadius = System.Math.Sqrt(planet.Star.Luminosity);
                planet.SemiMajorAxisAU = 0.723332;
                planet.Eccentricity = 0.0067;
                planet.AxialTilt = 2.8;
                planet.OrbitZone = Environment.OrbitalZone(planet.Star.Luminosity, planet.SemiMajorAxisAU);
                planet.Day = 2802;
                planet.OrbitalPeriod = 225;

                planet.Mass = 0.000002447;
                planet.GasMass = 2.41E-10;
                planet.DustMass = planet.Mass - planet.GasMass;
                planet.Radius = 6051.8;
                planet.Density = Environment.EmpiricalDensity(planet.Mass, planet.SemiMajorAxisAU, planet.Star.EcosphereRadius, true);
                planet.ExosphereTemp = GlobalConstants.EARTH_EXOSPHERE_TEMP / Utilities.Pow2(planet.SemiMajorAxisAU / planet.Star.EcosphereRadius);
                planet.SurfaceAcceleration = Environment.Acceleration(planet.Mass, planet.Radius);
                planet.EscapeVelocity = Environment.EscapeVelocity(planet.Mass, planet.Radius);

                planet.IsGasGiant = false;
                planet.SurfPressure = 92000;
                planet.DaytimeTemp = 737;
                planet.NighttimeTemp = 737;
                planet.SurfaceTemp = 737;
                planet.SurfaceGravity = 0.9;
                planet.MolecularWeightRetained = Environment.MinMolecularWeight(planet);

                return planet;
            }

            private Planet GetTestPlanetNoAtmosphere()
            {
                var star = GetTestStar();
                var planet = new Planet();
                planet.Star = star;
                planet.SurfPressure = 0;
                return planet;
            }

            [TestCategory("Atmosphere")]
            [TestMethod]
            public void TestEmptyPlanet()
            {
                var planet = new Planet();
                var sun = GetTestStar();
                planet.Star = sun;
                var generator = new Generator(new ChemTable[0]);
                generator.CalculateGases(planet);

                Assert.AreEqual(0, planet.GasCount);
                Assert.AreEqual(0, planet.AtmosphericGases.Length);
            }

            [TestCategory("Atmosphere")]
            [TestMethod]
            public void TestEmptyChemTable()
            {
                var generator = new Generator(new ChemTable[0]);
                var planet = GetTestPlanetAtmosphere();
                var star = planet.Star;
                var chemTable = ChemTable.GetDefaultTable();
                generator.CalculateGases(planet);

                Assert.AreEqual(0, planet.GasCount);
                Assert.AreEqual(0, planet.AtmosphericGases.Length);
            }

            [TestCategory("Atmosphere")]
            [TestMethod]
            public void TestAtmosphereDefaultChemTable()
            {
                var expected = new Dictionary<string, double>()
                {
                    {"Ar", 87534.0399 },
                    {"CO2", 2702.8010 },
                    {"H2O", 1316.9480 },
                    {"Kr", 339.5423 },
                    {"Ne", 65.7954 },
                    {"Xe", 40.8734 },
                    {"NH3", 0.0000 },
                    {"CH4", 0.0000 },
                    {"O3", 0.0000 },
                    {"O", 0.0000 }
                };

                var planet = GetTestPlanetAtmosphere();
                var star = planet.Star;
                var chemTable = ChemTable.GetDefaultTable();
                var generator = new Generator(chemTable);
                generator.CalculateGases(planet);

                Assert.AreEqual(expected.Count, planet.GasCount);

                foreach (var gas in planet.AtmosphericGases)
                {
                    var chem = chemTable.First(c => c.num == gas.num);
                    Assert.AreEqual(expected[chem.symbol], gas.surf_pressure, DELTA);
                }
            }

            [TestCategory("Atmosphere")]
            [TestMethod]
            public void TestNoAtmosphereDefaultChemTable()
            {
                var planet = GetTestPlanetNoAtmosphere();
                var generator = new Generator(ChemTable.GetDefaultTable());
                var star = planet.Star;
                generator.CalculateGases(planet);

                Assert.AreEqual(0, planet.GasCount);
                Assert.AreEqual(0, planet.AtmosphericGases.Length);
            }
        }
    }
}
