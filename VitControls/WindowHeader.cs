using System;
using System.Drawing;
using System.Windows.Forms;

namespace VitControls
{
    public partial class WindowHeader : UserControl
    {
        private bool isMouseDown = false;
        private Point mouseOffset;

        public WindowHeader()
        {
            InitializeComponent();
            VitColors.ClassColors classColors = new VitColors.ClassColors();
            BackColor = classColors.getCollection().primary0;
            buttonClose.BackgroundImage = VitIcons.Properties.ResourceColorImage.icons8_close_window_48;
            buttonMinimize.BackgroundImage = VitIcons.Properties.ResourceColorImage.icons8_minimize_window_48;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
        }

        private void ButtonMinimize_Click(object sender, EventArgs e)
        {
            ParentForm.WindowState = FormWindowState.Minimized;
        }

        private void WindowHeader_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void WindowHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                ParentForm.Location = mousePos;
            }
        }

        private void WindowHeader_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void WindowHeader_Paint(object sender, PaintEventArgs e)
        {
            labelText.Text = ParentForm.Text;
            labelText.Font = ParentForm.Font;
            labelText.ForeColor = ParentForm.ForeColor;
            Width = ParentForm.Width;
        }
    }
}