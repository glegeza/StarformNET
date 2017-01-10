namespace DLS.StarformNET.Display
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Data;

    public class SystemMap : PictureBox
    {
        public static int MOONS_PER_COLUMN = 4;

        public PlanetSpriteSheet SpriteSheet;
        public int PlanetPadding { get; set; }

        private List<Sprite> _planetSprites = new List<Sprite>();
        private List<Planet> _planets;
        public int SelectedPlanetIndex = -1;
        private Pen _selectionPen = new Pen(Color.White, 1);
        private Pen _focusedSelectionPen = new Pen(Color.Red, 1);

        public event EventHandler<EventArgs> PlanetClicked;

        public void SetNewSystem(List<Planet> planets)
        {
            _planets = planets;
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            switch (keyData)
            {
                case Keys.Left:
                    SelectedPlanetIndex -= 1;
                    if (SelectedPlanetIndex < 0)
                    {
                        SelectedPlanetIndex = _planetSprites.Count - 1;
                    }
                    PlanetClicked?.Invoke(this, EventArgs.Empty);
                    return true;
                case Keys.Right:
                    SelectedPlanetIndex += 1;
                    if (SelectedPlanetIndex >= _planetSprites.Count)
                    {
                        SelectedPlanetIndex = 0;
                    }
                    PlanetClicked?.Invoke(this, EventArgs.Empty);
                    return true;
            }
            return (base.ProcessCmdKey(ref msg, keyData));
        }

        public void SelectPlanet(int index)
        {
            SelectedPlanetIndex = index;
            Refresh();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Focus();
            var me = e as MouseEventArgs;
            var localPos = me.Location;
            for (var i = 0; i < _planetSprites.Count; i++)
            {
                var sprite = _planetSprites[i];
                var rect = new Rectangle(sprite.DrawLocation, sprite.SourceRect.Size);
                if (rect.Contains(localPos))
                {
                    SelectedPlanetIndex = i;
                    PlanetClicked?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Refresh();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Refresh();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            for (var i = 0; i < _planetSprites.Count; i++)
            {
                _planetSprites[i].DrawSprite(e);
                var rectPen = new Pen(Color.White);

                var moonYOffset = _planetSprites[i].SourceRect.Height + 2;
                var moonStartY = _planetSprites[i].DrawLocation.Y + moonYOffset;
                var columns = (int)Math.Ceiling((float)_planets[i].Moons.Count / MOONS_PER_COLUMN);
                var moonStartX = _planetSprites[i].DrawLocation.X + (int)((_planetSprites[i].SourceRect.Width / 2.0f) - (4 * columns) + 2);
                var x = moonStartX;
                var y = moonStartY;
                for (var k = 0; k < _planets[i].Moons.Count; k++)
                {
                    if (k % MOONS_PER_COLUMN == 0)
                    {
                        x += 4;
                        y = moonStartY;
                    }
                    e.Graphics.DrawRectangle(rectPen, x, y,
                        2, 2);
                    y += 4;
                }
            }

            if (SelectedPlanetIndex < 0 || SelectedPlanetIndex >= _planetSprites.Count)
            {
                return;
            }
            var selected = _planetSprites[SelectedPlanetIndex];
            var pen = Focused ? _focusedSelectionPen : _selectionPen;
            e.Graphics.DrawRectangle(pen, selected.DrawLocation.X - 1, selected.DrawLocation.Y - 1, selected.SourceRect.Width + 2, selected.SourceRect.Height + 2);
        }
    }
}
