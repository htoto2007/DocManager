using System;
using System.Drawing;
using System.Windows.Forms;

namespace VitControls
{
    public partial class WindowHeader : UserControl
    {
        private VitColors.ClassColors classColors = new VitColors.ClassColors();

        private bool isMouseDown = false;

        private Point mouseOffset;

        public WindowHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Включает или отключает возможность закрыть окно
        /// </summary>
        public bool close
        {
            get;
            set;
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

        /// <summary>
        /// Скрывает все элементы управления окном
        /// </summary>
        public bool showInTaskbar
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
            ParentForm.Update();
        }

        private void ButtonMinimize_Click(object sender, EventArgs e)
        {
            ParentForm.WindowState = FormWindowState.Minimized;
            ParentForm.Update();
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
            //Width = ParentForm.Width;

            BackColor = classColors.getCollection().blue;
            ParentForm.BackColor = classColors.getCollection().darckBlue;
            ParentForm.Padding = new Padding(1, 1, 1, 1);
            ParentForm.TransparencyKey = Color.Empty;
            ParentForm.Update();
            Update();

            buttonMaximize.Visible = maximize;
            buttonMinimize.Visible = minimize;
            buttonClose.Visible = close;
            //ParentForm.ShowInTaskbar = showInTaskbar;
        }
    }
}