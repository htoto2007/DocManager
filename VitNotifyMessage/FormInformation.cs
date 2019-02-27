using System.Windows.Forms;

namespace VitNotifyMessage
{
    partial class FormInformation : Form
    {
        public FormInformation(string str)
        {
            InitializeComponent();
            label1.Text = str;
        }
    }
}
