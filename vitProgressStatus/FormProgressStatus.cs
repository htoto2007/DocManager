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
    public partial class FormProgressStatus : Form
    {
        public FormProgressStatus(int min, int max)
        {
            InitializeComponent();
            progressBar1.Minimum = min;
            progressBar1.Maximum = max;
            progressBar1.Step = 1;
            progressBar1.Value = min;
            Show();
        }

        public void iterator(int value, string info)
        {
            progressBar1.PerformStep();
            labelPercent.Text = (progressBar1.Maximum / 100 * value).ToString() + "%";
            labelInfo.Text = info;
            if(value == progressBar1.Maximum)
            {
                Close();
            }
        }
    }
}
