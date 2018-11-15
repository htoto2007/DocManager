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
        private ClassUsers classUsers = new ClassUsers();

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

            classUsers.updateImage(classUsers.getThisUser().id);
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
            ClassUsers.UserColection userColection = classUsers.getThisUser();
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
            labelUserName.Text = userColection.firstName + " " + userColection.lastName + " " + userColection.middleName;

            ClassUserPositions classUserPositions = new ClassUserPositions();
            labelPosition.Text = classUserPositions.getInfoById(userColection.idPosition).name;

            ClassSubdivision classSubdivision = new ClassSubdivision();
            labelSubdivision.Text = classSubdivision.getInfoById(userColection.idSubdivision).name;

            labelLogin.Text = userColection.login;
        }

        private void buttonDeletePhoto_Click(object sender, EventArgs e)
        {
            if (pictureBox1.BackgroundImage != null)
            {
                pictureBox1.BackgroundImage.Dispose();
            }
            classUsers.deleteImage(classUsers.getThisUser().id);
            init();
        }
    }
}