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
        private StellarSystem _system;
        private StellarGroup _group;
        private int _systemsToGenerate = 100;
        private int _seed = 0;

        public MainGenerator()
        {
            AutoSize = true;
            AutoScaleMode = AutoScaleMode.Font;
            InitializeComponent();

            _systemMap.PlanetClicked += _systemMap_Click;

            var spriteFile = Path.Combine(Directory.GetCurrentDirectory(), ArtFolder, PlanetsFile);
            _planetSprites = new PlanetSpriteSheet(Image.FromFile(spriteFile), new Point(77, 71), new Size(32, 32), 5,
                5, 6);
            _systemMap.SpriteSheet = _planetSprites;
            _systemMap.PlanetPadding = 10;
            _gases = ChemType.LoadFromFile(Path.Combine(Directory.GetCurrentDirectory(), "elements.dat"));
            
            _seedSelector.Value = 0;
            _eccentricitySelector.Value = 0.25M;
            _gasRatioSelector.Value = 50;
            _dustDensitySelector.Value = 0.002M;
            _countSelector.Value = 100;

            GenerateGroup();
        }

        private SystemGenerationOptions GetSelectedOptions()
        {
            return new SystemGenerationOptions()
            {
                CloudEccentricity = (double)_eccentricitySelector.Value,
                DustDensityCoeff = (double)_dustDensitySelector.Value,
                GasDensityRatio = (double)_gasRatioSelector.Value,
                GasTable = _gases
            };
        }

        private void _regenButton_Click(object sender, EventArgs e)
        {
            GenerateSystem();
        }

        private void _systemMap_Click(object sender, EventArgs e)
        {
            SelectPlanet(_systemMap.SelectedPlanetIndex);
        }

        private void _planetSelector_Click(object sender, EventArgs e)
        {
            SelectPlanet(_planetSelector.SelectedIndex);
        }

        private void SelectPlanet(int s)
        {
            if (_planetSelector.SelectedIndex != s)
            {
                _planetSelector.SelectedIndex = s;
            }
            _planetInfoGroup.SetPlanet(_system.Planets[s]);
            _systemMap.SelectPlanet(s);
            _orbitMap.SelectPlanet(s);
            Refresh();
        }

        private void SetSystem(StellarSystem system)
        {
            if (system == null)
            {
                return;
            }
            _system = system;
            _systemMap.SetNewSystem(_system.Planets);
            _planetSelector.Items.Clear();

            foreach (Planet planet in _system.Planets)
            {
                _planetSelector.Items.Add(String.Format("Planet {0}", planet.Position));
            }
            _planetSelector.SelectedIndex = 0;

            var text = PlanetText.GetSystemText(_system.Planets);
            _systemInfo.SetSystem(_system.Planets);
            _orbitMap.SetSystem(_system.Planets);
            _planetInfoGroup.TabSpacing = 160;
            _planetInfoGroup.SetPlanet(_system.Planets[0]);
            _orbitMap.SelectPlanet(0);
            _systemMap.SelectPlanet(0);
        }

        private void GenerateSystem()
        {
            Utilities.InitRandomSeed((int)_seedSelector.Value);
            var curIdx = _systemListBox.SelectedIndex;
            var newSystem = Generator.GenerateStellarSystem(_system.Name, GetSelectedOptions());
            _group.Systems[curIdx] = newSystem;
            _systemListBox.Items[curIdx] = newSystem;
            SetSystem(newSystem);
        }

        private void _zoomInButton_Click(object sender, EventArgs e)
        {
            _orbitMap.ZoomIn();
        }

        private void _zoomOutButton_Click(object sender, EventArgs e)
        {
            _orbitMap.ZoomOut();
        }

        private void _systemListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SetSystem(_systemListBox.SelectedItem as StellarSystem);
        }

        private void GenerateGroup()
        {
            _systemListBox.Items.Clear();
            var options = GetSelectedOptions();
            _group = Generator.GenerateStellarGroup((int)_seedSelector.Value, (int)_countSelector.Value, options);
            foreach (var system in _group.Systems)
            {
                _systemListBox.Items.Add(system);
            }

            _systemListBox.SelectedIndex = 0;
        }

        private void _regenAllButton_Click(object sender, EventArgs e)
        {
            GenerateGroup();
        }
    }
}
