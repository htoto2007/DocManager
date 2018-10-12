using System;
using System.Windows.Forms;

namespace VitSettings
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void buttonRepositoryToDocuments_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                VitSettings.Properties.GeneralsSettings.Default.repositiryPayh = folderBrowserDialog.SelectedPath;
                VitSettings.Properties.GeneralsSettings.Default.Save();
                init();
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            init();
        }

        private void init()
        {
            linkLabelRepositoryToDocuments.Text = VitSettings.Properties.GeneralsSettings.Default.repositiryPayh;
        }
    }
}