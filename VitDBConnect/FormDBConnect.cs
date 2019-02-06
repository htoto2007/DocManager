using System;
using System.Windows.Forms;

namespace VitDBConnect
{
    public partial class FormDBConnect : Form
    {
        VitSettings.Properties.SettingsDataBase settings = VitSettings.Properties.SettingsDataBase.Default;

        public FormDBConnect()
        {
            InitializeComponent();
        }

        private void FormDBConnect_Load(object sender, EventArgs e)
        {
            textBoxHost.Text = settings.host;
            textBoxNameData.Text = settings.dataName;
            textBoxLogin.Text = settings.userLogin;
            textBoxPassword.Text = settings.userPassword;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            settings.host = textBoxHost.Text;
            settings.dataName = textBoxNameData.Text;
            settings.userLogin = textBoxLogin.Text;
            settings.userPassword = textBoxPassword.Text;
            settings.Save();
            Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}