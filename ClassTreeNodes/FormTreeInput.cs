using System;
using System.Windows.Forms;

namespace VitTree
{
    public partial class FormTreeInput : Form
    {
        public FormTreeInput()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Hide();
        }

        private void FormInput_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkField() == true)
            {
                Hide();
            }
        }

        private void FormTreeInput_StyleChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private bool checkField()
        {
            string str = textBox1.Text.Replace(" ", "");
            if (str == "")
            {
                MessageBox.Show("Название не может состоять из одних пробелов или быть пустым!");
                return false;
            }
            return true;
        }
    }
}