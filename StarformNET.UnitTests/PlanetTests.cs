
namespace DLS.StarformNET.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StarformNET;
    
    public class PlanetTests
    {
        [TestClass]
        public class EqualTests
        {
            [TestCategory("Planet.Equals")]
            [TestMethod]
            public void TestGeneratedEquality()
            {
                Utilities.InitRandomSeed(0);
                var system1 = Generator.GenerateStellarSystem("system1");

                Utilities.InitRandomSeed(0);
                var system2 = Generator.GenerateStellarSystem("system2");

                Assert.IsTrue(system1[0].Equals(system2[0]));
            }

            [TestCategory("Planet.Equals")]
            [TestMethod]
            public void TestGeneratedInequality()
            {
                Utilities.InitRandomSeed(0);
                var system1 = Generator.GenerateStellarSystem("system1");

                Utilities.InitRandomSeed(1);
                var system2 = Generator.GenerateStellarSystem("system2");

                Assert.IsFalse(system1[0].Equals(system2[0]));
            }
        }
    }
}
