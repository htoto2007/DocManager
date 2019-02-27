using System;
using System.Windows.Forms;

namespace VitNotifyMessage
{
    partial class FormWarning : Form
    {
        public FormWarning(string str)
        {
            InitializeComponent();
            label1.Text = str;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void FormWarning_Shown(object sender, EventArgs e)
        {
            Update();
        }
    }
}
