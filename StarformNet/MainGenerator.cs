namespace DLS.StarformNET
{

    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.IO;
    using Display;
    using Data;
    using System.Collections.Generic;

    public partial class MainGenerator : Form
    {
        private static string ArtFolder = "Art";
        private static string PlanetsFile = "PixelPlanets.png";

        private ChemType[] _gases;
        private PlanetSpriteSheet _planetSprites;
        private List<Planet> _system;

        public MainGenerator()
        {
            AutoSize = true;
            AutoScaleMode = AutoScaleMode.Font;
            InitializeComponent();
            var spriteFile = Path.Combine(Directory.GetCurrentDirectory(), ArtFolder, PlanetsFile);
            _planetSprites = new PlanetSpriteSheet(Image.FromFile(spriteFile), new Point(77, 71), new Size(32, 32), 5,
                5, 6);
            _systemMap.SpriteSheet = _planetSprites;
            _systemMap.PlanetPadding = 10;
            _gases = ChemType.LoadFromFile(Path.Combine(Directory.GetCurrentDirectory(), "elements.dat"));

            GenerateSystem();
        }

        private void _regenButton_Click(object sender, EventArgs e)
        {
            GenerateSystem();
        }

        private void _planetSelector_Click(object sender, EventArgs e)
        {
            _planetInfoGroup.SetPlanet(_system[_planetSelector.SelectedIndex], _gases);
        }

        private void GenerateSystem()
        {
            var generator = new Generator(_gases);
            var star = new Star();
            _system = generator.GenerateStellarSystem(ref star, null, "p", 0, "whatever", true, true);
            _systemMap.SetNewSystem(_system);
            _planetSelector.Items.Clear();

            foreach (Planet planet in _system)
            {
                _planetSelector.Items.Add(String.Format("Planet {0}", planet.Position));
            }
            _planetSelector.SelectedIndex = 0;

            var text = PlanetText.GetSystemText(_system, _gases);
            //_descriptionBox.Text = text;
            _systemInfo.SetSystem(star, _system);
            _orbitMap.SetSystem(star, _system);
            _planetInfoGroup.TabSpacing = 160;
            _planetInfoGroup.SetPlanet(_system[0], _gases);
        }
    }
}
