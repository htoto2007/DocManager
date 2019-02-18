using System;
using System.IO;
using System.Windows.Forms;
using VitFTP;
using VitUsers;

namespace VitTree
{
    public partial class FormTree : Form
    {
        public FormTree(string login, string password)
        {
            InitializeComponent();
            ClassTree classTree = new ClassTree();
            classTree.Init(treeView1, login, password);
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
            classTree.PreLoadNodesAsync(e.Node);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            buttonOk.Enabled = false;
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            int res = classFTP.getFileType(treeView1.SelectedNode.Name);
            if (treeView1.SelectedNode == null)
            {
                buttonOk.Enabled = false;
            }
            else if (res != 2)
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