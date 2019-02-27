using System;
using System.Windows.Forms;

namespace vitProgressStatus
{
    public partial class FormProgressStatus : Form
    {
        private int iterator = 0;
        private string persent = "";
        private string infoFrom = "";
        private string infoTo = "";
        private string infoCount = "";
        private string infoProcessName = "";

        public FormProgressStatus(int min, int max)
        {
            InitializeComponent();
            progressBar1.Minimum = min;
            progressBar1.Maximum = max;
            progressBar1.Step = 1;
            progressBar1.Value = min;
            progressBar1.Style = ProgressBarStyle.Marquee;
            Show();
            
            UpdateStyles();
            Update();
        }
        

        public void Iterator(int value, string infoFrom, string infoTo, string infoCount, string infoProcessName)
        {
            //Activate();
            //progressBar1.PerformStep();
            progressBar1.Style = ProgressBarStyle.Continuous;
            this.iterator = value;
            this.infoCount = infoCount;
            this.infoTo = infoTo;
            this.infoFrom = infoFrom;
            this.infoProcessName = infoProcessName;
            this.persent = Convert.ToInt32((((double)100 / (double)progressBar1.Maximum) * value)).ToString() + "%";
            progressBar1.Value = iterator;
            labelCount.Text = infoCount;
            labelTo.Text = infoTo;
            labelFrom.Text = infoFrom;
            labelPercent.Text = persent;
            labelProcessName.Text = infoProcessName;
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
            labelTo.Text = infoTo;
            labelFrom.Text = infoFrom;
            labelCount.Text = infoCount;
            labelPercent.Text = persent;
            labelProcessName.Text = infoProcessName;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
