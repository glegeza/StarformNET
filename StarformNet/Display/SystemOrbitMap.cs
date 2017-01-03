namespace DLS.StarformNET.Display
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class SystemOrbitMap : PictureBox
    {
        private class OrbitParameters
        {
            public double a; // semi-major axis
            public double b; // semi-minor axis
            public double e; // eccentricity

            public OrbitParameters(Planet planet)
            {
                a = Math.Sqrt(planet.SemiMajorAxisAU);
                e = planet.Eccentricity;
                b = Utilities.GetSemiMinorAxis(a, e);
            }
        }

        public int OuterEdgePadding = 20;

        private List<OrbitParameters> _orbits = new List<OrbitParameters>();
        private Pen _pen = new Pen(Color.White, 1);

        public void SetSystem(Star star, List<Planet> system)
        {
            _orbits.Clear();
            foreach (var planet in system)
            {
                _orbits.Add(new OrbitParameters(planet));
            }
            Refresh();

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (!_orbits.Any())
            {
                return;
            }
            var center = new Point(Size.Width / 2, Size.Height / 2);
            var max = _orbits.Max(o => o.b);
            foreach (var orbit in _orbits)
            {
                // scale orbit sizes to window
                var sA = (orbit.a / max) * ((Size.Height - OuterEdgePadding) / 2);
                var sB = (orbit.b / max) * ((Size.Height - OuterEdgePadding) / 2);
                var x = (int)(center.X - sA);
                var y = (int)(center.Y - sB);
                var width = (int)(sA * 2);
                var height = (int)(sB * 2);
                e.Graphics.DrawEllipse(_pen, x, y, width, height);
            }
        }
    }
}
