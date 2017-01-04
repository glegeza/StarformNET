namespace DLS.StarformNET.Display
{

    using System.Windows.Forms;
    using System;
    using System.Drawing;

    public class GrowLabel : Label
    {
        private bool _growing;

        public GrowLabel()
        {
            AutoSize = false;
        }

        private void ResizeLabel()
        {
            if (_growing) return;
            try
            {
                _growing = true;
                Size sz = new Size(this.Width, Int32.MaxValue);
                sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                Height = sz.Height;
            }
            finally
            {
                _growing = false;
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            ResizeLabel();
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            ResizeLabel();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ResizeLabel();
        }
    }

}