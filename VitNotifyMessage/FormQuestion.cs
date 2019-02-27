using System.Windows.Forms;

namespace VitNotifyMessage
{
    partial class FormQuestion : Form
    {
        public FormQuestion(string str)
        {
            InitializeComponent();
            label1.Text = str;
        }
    }
}
