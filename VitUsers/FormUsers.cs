using System;
using System.Windows.Forms;
using VitAccessGroup;
using VitSubdivision;
using VitUserPositions;

namespace VitUsers
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            Random random = new Random();
            textBoxPassword.Text = random.Next(100000, 999999).ToString();

            ClassSubdivision classSubdivision = new ClassSubdivision();
            ClassSubdivision.SubdivisionCollection[] subdivisionCollections = classSubdivision.getAllInfo();
            comboBoxSubdivision.Items.Clear();
            foreach (ClassSubdivision.SubdivisionCollection subdivisionCollection in subdivisionCollections)
            {
                comboBoxSubdivision.Items.Add(subdivisionCollection.name);
            }

            ClassUserPositions classUserPositions = new ClassUserPositions();
            ClassUserPositions.positionCollection[] positionCollections = classUserPositions.getAllInfo();
            comboBoxPosition.Items.Clear();
            foreach (ClassUserPositions.positionCollection positionCollection in positionCollections)
            {
                comboBoxPosition.Items.Add(positionCollection.name);
            }

            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            ClassAccessGroup.AccessGroupCollection[] accessGroupCollections = classAccessGroup.getInfo();
            comboBoxAccessGroup.Items.Clear();
            foreach (ClassAccessGroup.AccessGroupCollection accessGroupCollection in accessGroupCollections)
            {
                comboBoxAccessGroup.Items.Add(accessGroupCollection.name);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            classUsers.AddUser(
                textBoxLastName.Text,
                textBoxFirstName.Text,
                textBoxMiddleName.Text,
                textBoxMail.Text,
                textBoxMailPass.Text,
                comboBoxAccessGroup.Text,
                comboBoxPosition.Text,
                comboBoxSubdivision.Text,
                textBoxLogin.Text,
                textBoxPassword.Text);
        }

        private void buttonUSerEdit_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            classUsers.SendToEdit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxMiddleName_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxMail_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxMailPass_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxAccessGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxSubdivision_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBoxIdUser_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
        }
    }
}