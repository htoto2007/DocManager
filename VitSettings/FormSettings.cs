using System;
using System.Windows.Forms;

namespace VitSettings
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            initConnactToData();
            initConnectToFTP();
            initConnectToMail();
        }

        private void initConnactToData()
        {
            var settings = VitSettings.Properties.SettingsDataBase.Default;
            textBoxConnectDataBaseHost.Text = settings.host;
            textBoxConnectDataBaseDataBaseName.Text = settings.dataName;
            textBoxConnectDataBaseLogin.Text = settings.userLogin;
            textBoxConnectDataBasePassword.Text = settings.userPassword;
            textBoxConnectDataBasePort.Text = settings.port.ToString();
        }

        private void initConnectToFTP()
        {
            var settings = VitSettings.Properties.FTPSettings.Default;
            textBoxFTPHost.Text = settings.host;
            textBoxFTPPort.Text = settings.port.ToString();
            textBoxFTPPathForOpenFile.Text = settings.openFilePath;
            textBoxFTPPathForTmp.Text = settings.pathTnp;
        }

        private void initConnectToMail()
        {
            var settings = VitSettings.Properties.SettingsMail.Default;
            textBoxMailServerInAdres.Text = settings.serverInAdres;
            textBoxMailServerInPort.Text = settings.serverInPort.ToString();
            textBoxMailServerOutAdres.Text = settings.serverOutAdres;
            textBoxMailServerOutPort.Text = settings.serverOutPort.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }
    }
}