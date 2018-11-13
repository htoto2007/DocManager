using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VitNotifyMessage;
using VitSubdivision;
using VitUserPositions;

namespace VitUsers
{
    public partial class FormUserProfile : Form
    {
        private ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
        private ClassUsers ClassUsers = new ClassUsers();

        public FormUserProfile()
        {
            InitializeComponent();
            init();
        }

        private void buttonEditPassword_Click(object sender, EventArgs e)
        {
            buttonOkPassword.Visible = true;
            textBoxOldPassword.ReadOnly = false;
            textBoxNewPassword.ReadOnly = false;
            textBoxRetryPassword.ReadOnly = false;
        }

        private void buttonEditUserPthoto_Click(object sender, EventArgs e)
        {
            if (pictureBox1.BackgroundImage != null)
            {
                pictureBox1.BackgroundImage.Dispose();
            }

            ClassUsers.updateImage(ClassUsers.getThisUser().id);
            init();
        }

        private void buttonOkPassword_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            if (classUsers.changePassword(textBoxOldPassword.Text, textBoxNewPassword.Text, textBoxRetryPassword.Text) == false)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.WARNING, "Пароли не совпадаются или старый пароль не верный!");
            }
            else
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.INFORMATION, "Пароль изменен успешно.");
                Application.Restart();
            }
        }

        private void init()
        {
            ClassUsers.UserColection userColection = ClassUsers.getThisUser();
            Console.WriteLine(userColection.imagePath);
            if (File.Exists(userColection.imagePath) == true)
            {
                //Console.WriteLine("ERR " + userColection.imagePath);
                pictureBox1.BackgroundImage = Image.FromFile(userColection.imagePath);
            }
            else
            {
                pictureBox1.BackgroundImage = VitIcons.Properties.ResourceColorImage.icons8_user_male_208;
            }
            richTextBoxUserName.Text = userColection.firstName + " " + userColection.lastName + " " + userColection.middleName;

            ClassUserPositions classUserPositions = new ClassUserPositions();
            richTextBoxPosition.Text = classUserPositions.getInfoById(userColection.idPosition).name;

            ClassSubdivision classSubdivision = new ClassSubdivision();
            richTextBoxSubdivision.Text = classSubdivision.getInfoById(userColection.idSubdivision).name;

            richTextBoxLogin.Text = userColection.login;
        }
    }
}