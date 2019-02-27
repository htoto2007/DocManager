using System.Drawing;
using System.Windows.Forms;

namespace VitControls
{
    public partial class WindowResizer : UserControl
    {
        private bool isMouseDown = false;
        private Point mouseOffset;

        public WindowResizer()
        {
            InitializeComponent();
        }

        private void WindowResizer_MouseDown(object sender, MouseEventArgs e)
        {
            if (ParentForm.WindowState == FormWindowState.Maximized) return;
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = e.X - ParentForm.Location.X - (Width / 3);
                yOffset = e.Y - ParentForm.Location.Y - (Height / 3);
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void WindowResizer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                ParentForm.Size = new Size(mousePos);
            }
        }

        private void WindowResizer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        
    }
}
