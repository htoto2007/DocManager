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
        }

        /// <summary>
        /// Включает или отключает возможность изменения размера окна
        /// </summary>
        public bool maximize
        {
            get;
            set;
        }

        /// <summary>
        /// Включает или отключает возможность сворачивания окна окна
        /// </summary>
        public bool minimize
        {
            get;
            set;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
        }

        private void buttonMaximize_Click(object sender, EventArgs e)
        {
            if (ParentForm.WindowState == FormWindowState.Maximized)
            {
                ParentForm.WindowState = FormWindowState.Normal;
                buttonMaximize.BackgroundImage = Properties.Resources.icons8_maximize_window_48;
            }
            else
            {
                ParentForm.WindowState = FormWindowState.Maximized;
                buttonMaximize.BackgroundImage = Properties.Resources.icons8_restore_window_48;
            }
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
            labelText.ForeColor = Color.Black;
            Width = ParentForm.Width;
            VitColors.ClassColors classColors = new VitColors.ClassColors();
            BackColor = classColors.getCollection().primary0;

            buttonMaximize.Visible = maximize;
        }
    }
}