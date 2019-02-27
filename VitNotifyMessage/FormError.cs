using System.Windows.Forms;

namespace VitNotifyMessage
{
    partial class FormError : Form
    {
        public FormError(string mess, string title)
        {
            InitializeComponent();
            label1.Text = mess;
            Text = title;
        }
    }
}
