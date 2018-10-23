using System;
using System.Drawing;
using System.Windows.Forms;
using VitColors;

namespace VitControls
{
    public partial class Button : UserControl
    {
        private struct bgImages
        {
            //private const Image yes = Properties.Resources.icons8_checkmark_48;
        };

        public Button()
        {
            InitializeComponent();
            //BackgroundImage = Properties.Resources.icons8_checkmark_48
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            ClassColors classColors = new ClassColors();
            BackColor = classColors.getCollection().secondaryA0;
            BorderStyle = BorderStyle.FixedSingle;
            //Size = new Size(34, 34);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            BackColor = Color.Transparent;
            BorderStyle = BorderStyle.None;
            //Size = new Size(32, 32);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
    }
}