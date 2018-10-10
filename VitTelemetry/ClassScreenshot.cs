using System.Drawing;
using System.Windows.Forms;
using VitSettings;

namespace VitTelemetry
{
    public class ClassScreenshot
    {
        public Image CaptureScreen(int with, int height, int xLocation, int yLocation)
        {
            Rectangle screenSize = Screen.PrimaryScreen.Bounds;
            ClassSettings classSettings = new ClassSettings();
            Bitmap target = new Bitmap(with, height);

            int cursorX = Cursor.Position.X - xLocation;
            int cursorY = Cursor.Position.Y - yLocation;
            using (Graphics g = Graphics.FromImage(target))
            {
                g.CopyFromScreen(xLocation, yLocation, 0, 0, new Size(screenSize.Width, screenSize.Height));
                g.DrawImage(Image.FromFile(classSettings.GetProperties().generalsSttings.programPath + "\\icons\\color\\icons8-cursor-32.png"), cursorX, cursorY);
            }
            return target;
        }
    }
}