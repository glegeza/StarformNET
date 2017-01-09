namespace DLS.StarformNET.Display
{

    using System.Drawing;
    using System.Windows.Forms;

    public class Sprite
    {
        public Image Image { get; private set; }
        public Rectangle SourceRect { get; private set; }
        public Point DrawLocation { get; set; }

        public Sprite(Image image, Rectangle srcRect)
        {
            Image = image;
            SourceRect = srcRect;
        }

        public void DrawSprite(PaintEventArgs e)
        {
            e.Graphics.DrawImage(Image, DrawLocation.X, DrawLocation.Y, SourceRect, GraphicsUnit.Pixel);
        }
    }
}
