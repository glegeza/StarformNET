
namespace DLS.StarformNET.Display
{
    using System.Collections.Generic;
    using Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class SystemInfoGroup : GroupBox
    {
        public int LabelPadding = 20;
        public int TabSpacing = 80;
        public int XPadding = 4;
        public int YPadding = 12;

        public void SetSystem(Star star, List<Planet> planets)
        {
            ClearControls();

            var startY = (int)(Font.Size) * 2 + YPadding;
            var startX = XPadding;

            var x = startX;
            var y = startY;

            AddGroup(x, y, "Star Age:", StarText.GetAgeStringYearsSciN(star));
            y += LabelPadding;

            AddGroup(x, y, "Star Luminosity:", StarText.GetLuminosityRel(star));
            y += LabelPadding;

            AddGroup(x, y, "Star Mass:", StarText.GetMassRel(star));
            y += LabelPadding;

            AddGroup(x, y, "Planets:", planets.Count.ToString());
        }

        private void AddGroup(int x, int y, string label, string value)
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

        private void ClearControls()
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
