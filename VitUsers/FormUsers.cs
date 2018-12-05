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
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormUserAdd formUserAdd = new FormUserAdd();
            formUserAdd.Show();
        }
    }
}