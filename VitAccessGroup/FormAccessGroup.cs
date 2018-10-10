using System.Windows.Forms;

namespace VitAccessGroup
{
    public partial class FormAccessGroup : Form
    {
        public FormAccessGroup()
        {
            InitializeComponent();
        }

        private void FormAccessGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
        }
    }
}