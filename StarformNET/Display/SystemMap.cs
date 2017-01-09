namespace DLS.StarformNET.Display
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Data;

    public class SystemMap : PictureBox
    {
        public PlanetSpriteSheet SpriteSheet;
        public int PlanetPadding { get; set; }

        private List<Sprite> _planetSprites = new List<Sprite>();
        public int SelectedPlanetIndex = -1;
        private Pen _selectionPen = new Pen(Color.White, 1);
        private Pen _focusedSelectionPen = new Pen(Color.Red, 1);

        public event EventHandler<EventArgs> PlanetClicked;

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
            foreach (var sprite in _planetSprites)
            {
                sprite.DrawSprite(e);
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
