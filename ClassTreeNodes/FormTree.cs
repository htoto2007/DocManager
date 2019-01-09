using System;
using System.Windows.Forms;

namespace VitTree
{
    public partial class FormTree : Form
    {
        public FormTree()
        {
            InitializeComponent();
            VitTree.ClassTree classTree = new ClassTree();
            classTree.Init(treeView1);
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

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            VitTree.ClassTree classTree = new ClassTree();
            classTree.preLoadNodes(e.Node);
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