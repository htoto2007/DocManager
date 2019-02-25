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

        private void buttonEditUserPthoto_Click(object sender, EventArgs e)
        {
            if (pictureBox1.BackgroundImage != null)
            {
                pictureBox1.BackgroundImage.Dispose();
            }

            classUsers.updateImage(classUsers.getThisUser().id);
            init();
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
                pictureBox1.BackgroundImage = VitIcons.Properties.ResourceColorImage.user_person_people_6100;
            }
            richTextBox1.Text = userColection.lastName + " " + userColection.firstName + " " + userColection.middleName;

            ClassUserPositions classUserPositions = new ClassUserPositions();
            labelPosition.Text = classUserPositions.getInfoById(userColection.idPosition).name;

            ClassSubdivision classSubdivision = new ClassSubdivision();
            labelSubdivision.Text = classSubdivision.getInfoById(userColection.idSubdivision).name;

            if (userColection.gender) richTextBoxGender.Text = "муж.";
            else richTextBoxGender.Text = "жен.";

            richTextBoxDBO.Text = userColection.birthday.ToString("d");
            richTextBoxDataStartWork.Text = userColection.DateOfEmployment.ToString("d");

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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonQueryPassword_Click(object sender, EventArgs e)
        {
            //ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.INFORMATION, "В демо версии эта функция недоступна.");
            //classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не могу запросить смену пароля!");
        }
    }
}