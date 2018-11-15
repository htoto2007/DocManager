using System;
using System.Windows.Forms;

namespace VitTree
{
    public partial class FormTree : Form
    {
        public FormTree()
        {
            InitializeComponent();
            buttonOk.Enabled = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            treeView1.SelectedNode = null;
            Hide();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                buttonOk.Enabled = false;
            }
            else
            {
                buttonOk.Enabled = true;
            }
        }
    }
}