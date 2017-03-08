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
            public float angle;
            public float size;
            public Brush color;

            public OrbitParameters(Planet planet, float angle)
            {
                a = Math.Sqrt(planet.SemiMajorAxisAU);
                e = planet.Eccentricity;
                b = Utilities.GetSemiMinorAxis(a, e);
                this.angle = angle;
                size = (float)(planet.RadiusKM / GlobalConstants.KM_EARTH_RADIUS);
                color = Brushes.White;
                switch (planet.Type)
                {
                    case PlanetType.GasGiant:
                    case PlanetType.SubGasGiant:
                    case PlanetType.SubSubGasGiant:
                        color = Brushes.Tan;
                        break;
                    case PlanetType.Ice:
                        color = Brushes.LightSteelBlue;
                        break;
                    case PlanetType.Barren:
                    case PlanetType.Asteroids:
                        color = Brushes.SaddleBrown;
                        break;
                    case PlanetType.Martian:
                        color = Brushes.Red;
                        break;
                    case PlanetType.Terrestrial:
                        color = Brushes.YellowGreen;
                        break;
                    case PlanetType.Venusian:
                        color = Brushes.Yellow;
                        break;
                    case PlanetType.Water:
                        color = Brushes.Blue;
                        break;
                }
            }
        }

        public int OuterEdgePadding = 20;

        private List<OrbitParameters> _orbits = new List<OrbitParameters>();
        private Pen _pen = new Pen(Color.White, 1);
        private Pen _selectedPen = new Pen(Color.Red, 1);
        private int _selectedIndex = -1;
        private int _scaleFactor = 1;
        private int _maxScale = 8;

        public void SelectPlanet(int index)
        {
            _selectedIndex = index;
        }

        public void ZoomIn()
        {
            _scaleFactor *= 2;
            if (_scaleFactor > _maxScale)
            {
                _scaleFactor = _maxScale;
            }
            Refresh();
        }

        public void ZoomOut()
        {
            if (_scaleFactor != 1)
            {
                _scaleFactor /= 2;
            }
            Refresh();
        }

        public void SetSystem(List<Planet> system)
        {
            _orbits.Clear();
            foreach (var planet in system)
            {
                _orbits.Add(new OrbitParameters(planet, (float)(Utilities.RandomNumber() * 2 * Math.PI)));
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
            for (var i = 0; i < _orbits.Count; i++)
            {
                var orbit = _orbits[i];
                // scale orbit sizes to window
                var sA = (orbit.a / max) * ((Size.Height - OuterEdgePadding) / 2) * _scaleFactor;
                var sB = (orbit.b / max) * ((Size.Height - OuterEdgePadding) / 2) * _scaleFactor;
                var x = (int)(center.X - sA);
                var y = (int)(center.Y - sB);
                var width = (int)(sA * 2);
                var height = (int)(sB * 2);

                var pen = i == _selectedIndex ? _selectedPen : _pen;

                e.Graphics.DrawEllipse(pen, x, y, width, height);

                var size = (float)Math.Sqrt(orbit.size) * 8;
                var sx = center.X + sA * Math.Cos(orbit.angle);
                var sy = center.Y + sB * Math.Sin(orbit.angle);

                if (i == _selectedIndex)
                {
                    e.Graphics.FillEllipse(Brushes.White, (float)(sx - size / 2) - 1, (float)(sy - size / 2) - 1, size + 2, size + 2);
                }

                e.Graphics.FillEllipse(orbit.color, (float)(sx - size / 2), (float)(sy - size / 2), size, size);
            }
        }
    }
}
