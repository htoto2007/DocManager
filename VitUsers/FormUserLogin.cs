using System;
using System.Windows.Forms;

namespace VitUsers
{
    public partial class FormUserLogin : Form
    {
        public FormUserLogin()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void comboBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)(sender)).DroppedDown = true;
            if ((char.IsControl(e.KeyChar)))
            {
                return;
            }

            string Str = ((ComboBox)(sender)).Text.Substring(0, ((ComboBox)(sender)).SelectionStart) + e.KeyChar;
            int Index = ((ComboBox)(sender)).FindStringExact(Str);
            if (Index == -1)
            {
                Index = ((ComboBox)(sender)).FindString(Str);
            } ((ComboBox)sender).SelectedIndex = Index;
            ((ComboBox)(sender)).SelectionStart = Str.Length;
            ((ComboBox)(sender)).SelectionLength = ((ComboBox)(sender)).Text.Length - ((ComboBox)(sender)).SelectionStart;
            e.Handled = true;
        }

        private void FormUserLogin_Shown(object sender, EventArgs e)
        {
            comboBoxLogin.Focus();
        }

        private void GetKeyboardLayout()
        {
            InputLanguage myCurrentLanguage = InputLanguage.CurrentInputLanguage;
            labelInputLanguage.Text = myCurrentLanguage.Culture.ThreeLetterISOLanguageName;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetKeyboardLayout();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}