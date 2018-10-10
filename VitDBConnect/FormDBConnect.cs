using System;
using System.Windows.Forms;

namespace VitDBConnect
{
    public partial class FormDBConnect : Form
    {
        public FormDBConnect()
        {
            InitializeComponent();
        }

        private void FormDBConnect_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings1.Default.server;
            textBox2.Text = Properties.Settings1.Default.dbName;
            textBox3.Text = Properties.Settings1.Default.login;
            textBox4.Text = Properties.Settings1.Default.pass;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings1.Default.server = textBox1.Text;
            Properties.Settings1.Default.dbName = textBox2.Text;
            Properties.Settings1.Default.login = textBox3.Text;
            Properties.Settings1.Default.pass = textBox4.Text;
            Properties.Settings1.Default.Save();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}