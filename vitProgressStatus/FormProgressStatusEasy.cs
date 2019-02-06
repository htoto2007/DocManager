using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
