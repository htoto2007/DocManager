using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VitUsers
{
    public partial class FormUserEdit : Form
    {
        public FormUserEdit()
        {
            InitializeComponent();
            init();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if( (textBoxLastName.Text.Length < 4) ||
                (textBoxFirstName.Text.Length < 4) ||
                (textBoxMiddleName.Text.Length < 4) ||
                (textBoxMail.Text.Length < 4) ||
                (textBoxMailPass.Text.Length < 4) ||
                (comboBoxAccessGroup.Text.Length < 4) ||
                (comboBoxPosition.Text.Length < 4) ||
                (comboBoxSubdivision.Text.Length < 4) ||
                (textBoxLogin.Text.Length < 4) ||
                (textBoxPassword.Text.Length < 4)){
                VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();
                classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.WARNING, "Все поля формы должны быть заполнены!");
                
                return;
            }


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
            Close();
        }

        private void init()
        {
            VitAccessGroup.ClassAccessGroup classAccessGroup = new VitAccessGroup.ClassAccessGroup();
            comboBoxAccessGroup.Items.Clear();
            var groups = classAccessGroup.getInfo();
            foreach (var group in groups) {
                comboBoxAccessGroup.Items.Add(group.name);
            }

            VitUserPositions.ClassUserPositions classUserPositions = new VitUserPositions.ClassUserPositions();
            comboBoxPosition.Items.Clear();
            var userPositions = classUserPositions.getAllInfo();
            foreach(var userPosition in userPositions)
            {
                comboBoxPosition.Items.Add(userPosition.name);
            }

            VitSubdivision.ClassSubdivision classSubdivision = new VitSubdivision.ClassSubdivision();
            comboBoxSubdivision.Items.Clear();
            var subdivisions = classSubdivision.getAllInfo();
            foreach(var subdivision in subdivisions)
            {
                comboBoxSubdivision.Items.Add(subdivision.name);
            }

            Random random = new Random();
            textBoxPassword.Text = random.Next(100000, 999999).ToString();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            ClassTranslit classTranslit = new ClassTranslit();
            textBoxLogin.Text = classTranslit.Front(textBoxLastName.Text);
            
        }
    }
}
