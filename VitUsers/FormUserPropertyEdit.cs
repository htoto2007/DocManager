using System;
using System.Windows.Forms;

namespace VitUsers
{
    public partial class FormUserPropertyEdit : Form
    {
        public FormUserPropertyEdit(int id)
        {
            InitializeComponent();
            init(id);
        }

        private void init(int id)
        {
            VitUsers.ClassUsers classUsers = new ClassUsers();
            var user = classUsers.GetUserByid(id);
            textBoxFirstName.Text = user.firstName;
            textBoxLastName.Text = user.lastName;
            textBoxMiddleName.Text = user.middleName;
            textBoxLogin.Text = user.login;
            textBoxMail.Text = user.mail;
            textBoxPassword.Text = user.password;
            textBoxMailPass.Text = user.mailPass;
            textBoxIdUser.Text = user.id.ToString();

            VitAccessGroup.ClassAccessGroup classAccessGroup = new VitAccessGroup.ClassAccessGroup();
            var accessGroups = classAccessGroup.getInfo();
            comboBoxAccessGroup.Items.Clear();
            foreach(var accessGroup in accessGroups)
            {
                comboBoxAccessGroup.Items.Add(accessGroup.name);
            }
            comboBoxAccessGroup.Text = classAccessGroup.getInfoById(user.idAccessGroup).name;

            VitUserPositions.ClassUserPositions classUserPositions = new VitUserPositions.ClassUserPositions();
            var userPositions = classUserPositions.getAllInfo();
            comboBoxPosition.Items.Clear();
            foreach(var userPosition in userPositions)
            {
                comboBoxPosition.Items.Add(userPosition.name);
            }
            comboBoxPosition.Text = classUserPositions.getInfoById(user.idPosition).name;

            VitSubdivision.ClassSubdivision classSubdivision = new VitSubdivision.ClassSubdivision();
            var subdivisions = classSubdivision.getAllInfo();
            comboBoxSubdivision.Items.Clear();
            foreach(var subdivision in subdivisions)
            {
                comboBoxSubdivision.Items.Add(subdivision.name);
            }
            comboBoxSubdivision.Text = classSubdivision.getInfoById(user.idSubdivision).name;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if ((textBoxLastName.Text.Length < 4) ||
                (textBoxFirstName.Text.Length < 4) ||
                (textBoxMiddleName.Text.Length < 4) ||
                (textBoxMail.Text.Length < 4) ||
                (textBoxMailPass.Text.Length < 4) ||
                (comboBoxAccessGroup.Text.Length < 4) ||
                (comboBoxPosition.Text.Length < 4) ||
                (comboBoxSubdivision.Text.Length < 4) ||
                (textBoxLogin.Text.Length < 4) ||
                (textBoxPassword.Text.Length < 4))
            {
                VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
                classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.WARNING, "Все поля формы должны быть заполнены!");

                return;
            }


            ClassUsers classUsers = new ClassUsers();
            classUsers.UpdateUser(
                textBoxLastName.Text,
                textBoxFirstName.Text,
                textBoxMiddleName.Text,
                textBoxMail.Text,
                textBoxMailPass.Text,
                comboBoxAccessGroup.Text,
                comboBoxPosition.Text,
                comboBoxSubdivision.Text,
                textBoxLogin.Text,
                textBoxPassword.Text,
                textBoxIdUser.Text
                );
            Close();
        }
    }
}
