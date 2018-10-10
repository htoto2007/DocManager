using System;
using System.Windows.Forms;

namespace VitUsers
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
            init();
            Random random = new Random();
            textBoxPassword.Text = random.Next(100000, 999999).ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            classUsers.AddUser();
        }

        private void buttonUSerEdit_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            classUsers.SendToEdit();
        }

        private void init()
        {
            /*
            Panel panelClose = (Panel)windowHeader1.Controls["panelButtonClose"];
            ClassColors classColors = new ClassColors();
            panelClose.BackgroundImage = VitIcons.Properties.ResourceColorImage.icons8_close_window_48;
            Panel panelMinimize = (Panel)windowHeader1.Controls["panelButtonMinimize"];
            panelMinimize.
            windowHeader1.BackColor = classColors.getCollection().primary1;
            */
        }
    }
}