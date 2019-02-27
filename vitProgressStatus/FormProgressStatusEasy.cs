using System.Windows.Forms;

namespace vitProgressStatus
{
    public partial class FormProgressStatusEasy : Form
    {
        public FormProgressStatusEasy(string info)
        {
            InitializeComponent();
            labelInfo.Text = info;
            
        }


    }
}
