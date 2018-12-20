using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vitProgressStatus
{
    public partial class FormProgressStatus : Form
    {
        private int iterator = 0;
        private string persent = "";
        private string info = "";

        public FormProgressStatus(int min, int max)
        {
            InitializeComponent();
            progressBar1.Minimum = min;
            progressBar1.Maximum = max;
            progressBar1.Step = 1;
            progressBar1.Value = min;
            Show();
            
            UpdateStyles();
            Update();
        }
        

        public void Iterator(int value, string info)
        {
            Activate();
            //progressBar1.PerformStep();
            this.iterator = value;
            this.info = info;
            this.persent = Convert.ToInt32((((double)100 / (double)progressBar1.Maximum) * value)).ToString() + "%";
            progressBar1.Value = iterator;
            labelInfo.Text = info;
            labelPercent.Text = persent;
            if (value == progressBar1.Maximum)
            {
                Close();
            }
            UpdateStyles();
            Update();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("t");
            progressBar1.Value = iterator;
            labelInfo.Text = info;
            labelPercent.Text = persent;
        }
    }
}
