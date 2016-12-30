namespace DLS.StarformNET.Display
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Data;

    public class SystemMap : PictureBox
    {
        public PlanetSpriteSheet SpriteSheet;
        public int PlanetPadding { get; set; }

        private List<Sprite> _planetSprites = new List<Sprite>();

        public void SetNewSystem(List<Planet> planets)
        {
            _planetSprites.Clear();
            foreach (var p in planets)
            {
                var sprite = SpriteSheet.GetSprite(p.Type);
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
            foreach (var sprite in _planetSprites)
            {
                sprite.DrawSprite(e);
            }
        }
    }
}
