namespace DLS.StarformNET.Display
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class InfoGroup : GroupBox
    {
        public int LabelPadding = 20;
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
                y += LabelPadding;
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
            var valueLabel = new Label()
            {
                Text = value,
                Location = new Point(x + TabSpacing, y),
                AutoSize = true
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
