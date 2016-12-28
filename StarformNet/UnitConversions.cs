namespace DLS.StarformNet
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class UnitConversions
    {
        public static double KelvinToFahrenheit(double tempK)
        {
            return tempK * (9.0 / 5.0) - 459.67;
        }
    }
}
