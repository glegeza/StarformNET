namespace DLS.StarformNET.Display
{
    using System.Drawing;
    using System;
    using System.Collections.Generic;
    using Data;

    public class PlanetSpriteSheet
    {
        private static Dictionary<PlanetType, int> PlanetMapping = new Dictionary<PlanetType, int>()
        {
            { PlanetType.Asteroids,      2 },
            { PlanetType.GasGiant,       6 },
            { PlanetType.Ice,            5 },
            { PlanetType.Martian,        7 },
            { PlanetType.Barren,           2 },
            { PlanetType.SubGasGiant,    6 },
            { PlanetType.SubSubGasGiant, 6 },
            { PlanetType.Terrestrial,    0 },
            { PlanetType.Unknown,        8 },
            { PlanetType.Venusian,       4 },
            { PlanetType.Water,          3 }
        };

        public Size SpriteSize { get; set; }

        private int _hPadding;
        private int _vPadding;
        private int _planetTypes;
        private Image _image;
        private Point _upperLeft;

        public PlanetSpriteSheet(Image image, Point upperLeft, Size spriteSize,
            int hPadding, int vPadding, int planetTypes)
        {
            _hPadding = hPadding;
            _vPadding = vPadding;
            _planetTypes = planetTypes;
            _image = image;
            _upperLeft = upperLeft;
            SpriteSize = spriteSize;
        }

        public Sprite GetSprite(PlanetType type)
        {
            var planetNum = Utilities.RandomInt(0, _planetTypes - 1);
            var planetRow = PlanetMapping[type];
            var x = _upperLeft.X + (planetNum * SpriteSize.Width) + (planetNum * _hPadding);
            var y = _upperLeft.Y + (planetRow * SpriteSize.Height) + (planetRow * _vPadding);
            var rect = new Rectangle(x, y, SpriteSize.Width, SpriteSize.Height);
            return new Sprite(_image, rect);
        }
    }
}
