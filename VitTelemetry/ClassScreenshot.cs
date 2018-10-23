using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VitSettings;

namespace VitTelemetry
{
    public class ClassScreenshot
    {
        private readonly string pathScreans = "D:\\screans\\";

        public ClassScreenshot()
        {
            pathScreans = VitSettings.Properties.DevSettings.Default.screanShotsPath;
            if (!Directory.Exists(pathScreans))
            {
                Directory.CreateDirectory(pathScreans);
            }
        }

        public void start(int Width, int Height, int LocationX, int LocationY)
        {
            while (true)
            {
                Image image = CaptureScreen(Width, Height, LocationX, LocationY);
                double unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                string fileName = unixTimestamp.ToString() + ".png";
                //string path = classSettings.GetProperties().generalsSttings.programPath + "\\screans\\";

                try
                {
                    image.Save(pathScreans + fileName, ImageFormat.Png);
                }
                catch (System.NotSupportedException)
                {
                    Console.WriteLine(pathScreans);
                }
                Thread.Sleep(50);
            }
        }

        private Image CaptureScreen(int with, int height, int xLocation, int yLocation)
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

        public void deleteScrean()
        {
            while (true)
            {
                Console.WriteLine("Check count files.");
                string[] files = Directory.GetFiles(pathScreans);
                Console.WriteLine(" " + files.GetLength(0));

                if (files.GetLength(0) > 5000)
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        File.Delete(files[i]);
                    }
                }
                Thread.Sleep(5000);
            }
        }
    }
}