using System;
using System.Windows.Forms;
using VitNotifyMessage;

namespace VitSettings
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            InitConnactToData();
            InitConnectToFTP();
            InitConnectToMail();
        }

        private void InitConnactToData()
        {
            var settings = VitSettings.Properties.SettingsDataBase.Default;
            textBoxConnectDataBaseHost.Text = settings.host;
            textBoxConnectDataBaseDataBaseName.Text = settings.dataName;
            textBoxConnectDataBaseLogin.Text = settings.userLogin;
            textBoxConnectDataBasePassword.Text = settings.userPassword;
            textBoxConnectDataBasePort.Text = settings.port.ToString();
        }

        private void SaveConnactToData()
        {
            var settings = VitSettings.Properties.SettingsDataBase.Default;
            settings.host = textBoxConnectDataBaseHost.Text;
            settings.dataName = textBoxConnectDataBaseDataBaseName.Text;
            settings.userLogin = textBoxConnectDataBaseLogin.Text;
            settings.userPassword = textBoxConnectDataBasePassword.Text;
            settings.port = Convert.ToInt32(textBoxConnectDataBasePort.Text);
            settings.Save();
        }

        private void InitConnectToFTP()
        {
            var settings = VitSettings.Properties.FTPSettings.Default;
            textBoxFTPHost.Text = settings.host;
            textBoxFTPPort.Text = settings.port.ToString();
            textBoxFTPPathForOpenFile.Text = settings.openFilePath;
            textBoxFTPPathForTmp.Text = settings.pathTnp;
        }

        private void SaveConnectToFTP()
        {
            var settings = VitSettings.Properties.FTPSettings.Default;
            settings.host = textBoxFTPHost.Text;
            settings.port = Convert.ToInt32(textBoxFTPPort.Text);
            settings.openFilePath = textBoxFTPPathForOpenFile.Text;
            settings.pathTnp = textBoxFTPPathForTmp.Text;
            settings.Save();
        }

        private void InitConnectToMail()
        {
            var settings = VitSettings.Properties.SettingsMail.Default;
            textBoxMailServerInAdres.Text = settings.serverInAdres;
            textBoxMailServerInPort.Text = settings.serverInPort.ToString();
            textBoxMailServerOutAdres.Text = settings.serverOutAdres;
            textBoxMailServerOutPort.Text = settings.serverOutPort.ToString();
        }

        private void SaveConnectToMail()
        {
            var settings = VitSettings.Properties.SettingsMail.Default;
            settings.serverInAdres = textBoxMailServerInAdres.Text;
            settings.serverInPort = Convert.ToInt32(textBoxMailServerInPort.Text);
            settings.serverOutAdres = textBoxMailServerOutAdres.Text;
            settings.serverOutPort = Convert.ToInt32(textBoxMailServerOutPort.Text);
            settings.Save();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SaveConnactToData();
            SaveConnectToFTP();
            SaveConnectToMail();
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.INFORMATION, "Параметры сохранены.");
            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonFTPChooseFolderForOpenFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFTPPathForOpenFile.Text = folderBrowserDialog.SelectedPath;
            }
            folderBrowserDialog.Dispose();
        }

        private void buttonFTPChooseFolderForTmp_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFTPPathForTmp.Text = folderBrowserDialog.SelectedPath;
            }
            folderBrowserDialog.Dispose();
        }

        private void buttonGeneralsDefault_Click(object sender, EventArgs e)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            DialogResult dialogResult = classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.QUESTION, "Вы действительно хотите сделать все настройки по умолчанию?");
            if (dialogResult != DialogResult.Yes) return;

            Properties.SettingsDataBase.Default.Reload();
            Properties.ControlsSettings.Default.Reload();
            Properties.DevSettings.Default.Reload();
            Properties.FTPSettings.Default.Reload();
            Properties.GeneralsSettings.Default.Reload();
            Properties.SettingsDataBase.Default.Reload();
            Properties.SettingsMail.Default.Reload();
        }
    }
}