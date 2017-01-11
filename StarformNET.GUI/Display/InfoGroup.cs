namespace DLS.StarformNET.Display
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class InfoGroup : GroupBox
    {
        public int LabelPadding = 4;
        public int TabSpacing = 80;
        public int XPadding = 4;
        public int YPadding = 12;

        protected void SetText(List<string> labels, List<string> values)
        {
            ClearControls();

            var startY = (int)(Font.Size) * 2 + YPadding;
            var startX = XPadding;

            var x = startX;
            var y = startY;

            for (var i = 0; i < labels.Count; i++)
            {
                AddGroup(x, y, labels[i], values[i]);
                var lastValue = Controls[Controls.Count - 1];
                var lastLabel = Controls[Controls.Count - 2];
                var bottom = Math.Max(lastValue.Bottom, lastLabel.Bottom);
                y = bottom + LabelPadding;
            }
        }

        protected void AddGroup(int x, int y, string label, string value)
        {
            var labelLabel = new Label()
            {
                Text = label,
                Location = new Point(x, y),
                AutoSize = true
            };
            var valueLabel = new GrowLabel()
            {
                Size = new Size(200, 0),
                Text = value,
                Location = new Point(x + TabSpacing, y),
            };
            Controls.Add(labelLabel);
            Controls.Add(valueLabel);
        }

        protected void ClearControls()
        {
            Visible = false;
            while (Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            Visible = true;
        }
    }
}
