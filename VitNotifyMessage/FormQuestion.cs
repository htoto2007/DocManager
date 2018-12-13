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
    public partial class FormQuestion : Form
    {
        public FormQuestion(string str)
        {
            InitializeComponent();
            label1.Text = str;
        }
    }
}
