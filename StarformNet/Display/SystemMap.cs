using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLS.StarformNet.Display
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Data;

    public class SystemMap : PictureBox
    {
        public PlanetSpriteSheet SpriteSheet;
        public int PlanetPadding { get; set; }

        private Planet _systemHead;
        private List<Sprite> _planetSprites = new List<Sprite>();

        public void SetNewSystem(Planet systemHead)
        {
            _systemHead = systemHead;
            _planetSprites.Clear();
            var next = _systemHead;
            while (next != null)
            {
                var sprite = SpriteSheet.GetSprite(next.type);
                next = next.next_planet;
                _planetSprites.Add(sprite);
            }

            var y = Size.Height / 2 - SpriteSheet.SpriteSize.Width / 2;
            var totalWidth = SpriteSheet.SpriteSize.Width * _planetSprites.Count + PlanetPadding * (_planetSprites.Count - 1);
            var x = Size.Width / 2 - totalWidth / 2;
            var xIncr = SpriteSheet.SpriteSize.Width + PlanetPadding;
            for (var i = 0; i < _planetSprites.Count; i ++)
            {
                _planetSprites[i].DrawLocation = new Point(x, y);
                x += xIncr;
            }

            Refresh();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (_systemHead == null)
            {
                return;
            }
            foreach (var sprite in _planetSprites)
            {
                sprite.DrawSprite(e);
            }
        }
    }
}
