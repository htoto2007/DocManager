using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
