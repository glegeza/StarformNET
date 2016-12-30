namespace DLS.StarformNET.UnitTests
{

    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StarformNet;

    public class UnitConversionsTest
    {
        public static double DELTA = 0.01;

        [TestClass]
        public class KelvinToFahrenheitTests
        {

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertAbsoluteZero()
            {
                double resultZero = UnitConversions.KelvinToFahrenheit(0.0);
                Assert.AreEqual(resultZero, -459.67, DELTA, "Absolute zero converted incorrectly.");
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertPositiveKelvin()
            {
                var tempK = new double[]
                {
                    20,
                    78,
                    120,
                    1000,
                    1234,
                    36000
                };

                var expectedTemp = new double[]
                {
                    -423.67,   // 20 K
                    -319.27,   // 78 K
                    -243.67,   // 120 K
                    1340.33,   // 1000 K
                    1761.53,   // 1234 K
                    64340.33   // 36000 K
                };

                for (var i = 0; i < tempK.Length; i++)
                {
                    var result = UnitConversions.KelvinToFahrenheit(tempK[i]);
                    Assert.AreEqual(result, expectedTemp[i], DELTA,
                        String.Format("Incorrect result converting {0} K. Expected {1}, received {2}",
                        tempK[i], expectedTemp[i], result));
                }
            }
        }

        [TestClass]
        public class MMHGToMillibarsTests
        {
            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertZeroPressure()
            {
                var result = UnitConversions.MMHGToMillibars(0);
                Assert.AreEqual(result, 0.0, DELTA, "Incorrect conversion for zero mmHg");
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertPositivePressureLessThanOne()
            {
                var presMmHg = new double[]
                {
                    0.20,
                    0.78,
                    0.120,
                    0.1,
                    0.1234,
                    0.0005,
                    0.002
                };

                var expectedMb = new double[]
                {
                    0.266645,     // 0.2 mmHg
                    1.039915,     // 0.78 mmHg
                    0.1599869,    // 0.120 mmHg
                    0.133322,     // 0.1 mmHg
                    0.164519826,  // 0.1234 mmHg
                    0.0066661194, // 0.0005 mmHg
                    0.0026664477  // 0.002 mmHg
                };

                for (var i = 0; i < presMmHg.Length; i++)
                {
                    var result = UnitConversions.MMHGToMillibars(presMmHg[i]);
                    Assert.AreEqual(result, expectedMb[i], DELTA,
                        String.Format("Incorrect result converting {0} mmHg. Expected {1}, received {2}",
                        presMmHg[i], expectedMb[i], result));
                }
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertPositivePressureWholeNumbers()
            {
                var presMmHg = new double[]
                {
                    20,
                    78,
                    120,
                    1000,
                    1234,
                    36000,
                    120000
                };

                var expectedMb = new double[]
                {
                    26.6645,    // 20 mmHg
                    103.991,    // 78 mmHg
                    159.987,    // 120 mmHg
                    1333.22,    // 1000 mmHg
                    1645.198,   // 1234 mmHg
                    47996.059,  // 36000 mmHg
                    159986.865  // 120000 mmHg
                };

                for (var i = 0; i < presMmHg.Length; i++)
                {
                    var result = UnitConversions.MMHGToMillibars(presMmHg[i]);
                    Assert.AreEqual(result, expectedMb[i], DELTA,
                        String.Format("Incorrect result converting {0} mmHg. Expected {1}, received {2}",
                        presMmHg[i], expectedMb[i], result));
                }
            }
        }

        [TestClass]
        public class MillibarsToAtmTests
        {
            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertZeroPressure()
            {
                var result = UnitConversions.MillibarsToAtm(0);
                Assert.AreEqual(result, 0.0, DELTA, "Incorrect conversion for zero mmHg");
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertEarthSurfacePressure()
            {
                var result = UnitConversions.MillibarsToAtm(GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS);
                Assert.AreEqual(result, 1.0, DELTA, "Incorrect conversion for Earth surface pressure.");
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertPositivePressureLessThanOne()
            {
                var presMb = new double[]
                {
                    0.20,
                    0.78,
                    0.120,
                    0.1,
                    0.1234,
                    0.0005,
                    0.002,
                    0.9
                };

                var expectedAtm = new double[]
                {
                    0.000197385,     // 0.2 mb
                    0.0007698001,    // 0.78 mb
                    0.0001184308,    // 0.120 mb
                    9.8692e-5,       // 0.1 mb
                    0.000121786331,  // 0.1234 mb
                    4.9346163e-7,    // 0.0005 mb
                    1.973847e-6,     // 0.002 mb
                    0.000888231      // 0.9 mb
                };

                for (var i = 0; i < presMb.Length; i++)
                {
                    var result = UnitConversions.MillibarsToAtm(presMb[i]);
                    Assert.AreEqual(result, expectedAtm[i], DELTA,
                        String.Format("Incorrect result converting {0} mb. Expected {1}atm, received {2}atm",
                        presMb[i], expectedAtm[i], result));
                }
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertPositivePressureWholeNumbers()
            {
                var presMb = new double[]
                {
                    20,
                    78,
                    120,
                    1000,
                    1234,
                    36000,
                    120000
                };

                var expectedAtm = new double[]
                {
                    0.0198,    // 20 mb
                    0.0770,    // 78 mb
                    0.1185,    // 120 mb
                    0.9870,    // 1000 mb
                    1.2179,    // 1234 mb
                    35.5293,   // 36000 mb
                    118.4308   // 120000 mb
                };

                for (var i = 0; i < presMb.Length; i++)
                {
                    var result = UnitConversions.MillibarsToAtm(presMb[i]);
                    Assert.AreEqual(result, expectedAtm[i], DELTA,
                        String.Format("Incorrect result converting {0} mb. Expected {1}atm, received {2}atm",
                        presMb[i], expectedAtm[i], result));
                }
            }
        }

        [TestClass]
        public class PPMToMillibars
        {
            private static double[] ATMOS = new double[] { 1, 1.5, 0.5, 4, 0.1 };
            private static double[] PPM_VALUES = new double[]
            {
                20,
                78,
                120,
                1,
                1234,
                55000,
                100000,
                25000
            };

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertZeroPPM()
            {
                var expected = new double[]
                {
                    0.0,  // 1atm
                    0.0,  // 1.5atm
                    0.0,  // 0.5atm
                    0.0,  // 4atm
                    0.0   // 0.1atm
                };

                for (var i = 0; i < ATMOS.Length; i++)
                {
                    var result = UnitConversions.PPMToMillibars(0, ATMOS[i]);
                    Assert.AreEqual(expected[i], result, DELTA);
                }
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void Convert1MPPM()
            {
                var expected = new double[]
                {
                    1013.25,   // 1 atm
                    1519.875,  // 1.5 atm
                    506.625,   // 0.5 atm
                    4053,      // 4 atm
                    101.325    // 0.1 atm
                };

                for (var i = 0; i < ATMOS.Length; i++)
                {
                    var result = UnitConversions.PPMToMillibars(1000000, ATMOS[i]);
                    Assert.AreEqual(result, GlobalConstants.EARTH_SURF_PRES_IN_MILLIBARS * ATMOS[i], DELTA);
                }
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertPositivePPMWholeNumbers1Atm()
            {
                var expected = new double[]
                {
                    0.0203,    // 20ppm
                    0.0791,    // 78ppm
                    0.1216,    // 120ppm
                    0.0010,    // 1ppm
                    1.2504,    // 1234ppm
                    55.7288,   // 55000ppm
                    101.3250,  // 100000ppm
                    25.3313    // 25000ppm
                };

                for (var i = 0; i < PPM_VALUES.Length; i++)
                {
                    var result = UnitConversions.PPMToMillibars(PPM_VALUES[i]);
                    Assert.AreEqual(expected[i], result, DELTA,
                        String.Format("expected: {0} ppm @ {1} atm -> {2} mb, result: {3}",
                        PPM_VALUES[i], 1.0, expected[i], result));
                }
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertPositivePPMWholeNumbersBelow1Atm()
            {
                var expected = new double[]
                {
                    0.0102,  // 20ppm
                    0.0396,  // 78ppm
                    0.0608,  // 120ppm
                    0.0005,  // 1ppm
                    0.6252,  // 1234ppm
                    27.8644, // 55000ppm
                    50.6625, // 100000ppm
                    12.6657  // 25000ppm
                };

                for (var i = 0; i < PPM_VALUES.Length; i++)
                {
                    var result = UnitConversions.PPMToMillibars(PPM_VALUES[i], 0.5);
                    Assert.AreEqual(expected[i], result, DELTA,
                        String.Format("expected: {0} ppm @ {1} atm -> {2} mb, result: {3}",
                        PPM_VALUES[i], 0.5, expected[i], result));
                }
            }

            [TestCategory("Unit Conversions")]
            [TestMethod]
            public void ConvertPositivePPMWholeNumbersAbove1Atm()
            {
                var expected = new double[]
                {
                    0.0406,   // 20ppm
                    0.1582,   // 78ppm
                    0.2432,   // 120ppm
                    0.0020,   // 1ppm
                    2.5008,   // 1234ppm
                    111.4576, // 55000ppm
                    202.6500, // 100000ppm
                    50.6626   // 25000ppm
                };

                for (var i = 0; i < PPM_VALUES.Length; i++)
                {
                    var result = UnitConversions.PPMToMillibars(PPM_VALUES[i], 2.0);
                    Assert.AreEqual(expected[i], result, DELTA,
                        String.Format("expected: {0} ppm @ {1} atm -> {2} mb, result: {3}",
                        PPM_VALUES[i], 2.0, expected[i], result));
                }
            }
        }
    }
}
