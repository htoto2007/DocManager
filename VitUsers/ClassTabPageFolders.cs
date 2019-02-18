using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitFTP;
using VitIcons;

namespace VitUsers
{
    class ClassTabPageFolders
    {
        private readonly ClassImageList ClassImageList = new ClassImageList();

        public void initTreeView(TreeView treeView)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            string[] directoryes = classFTP.ListDirectoryWithotFiles("/");

            treeView.ImageList = ClassImageList.imageList;
            treeView.Nodes.Clear();

            if (directoryes == null) return;
            foreach (string direcory in directoryes)
            {
                if (direcory.Contains("..")) continue;
                TreeNode treeNode = new TreeNode
                {
                    Name = direcory,
                    Text = direcory.Trim('/'),
                    ImageKey = "default_folder",
                    SelectedImageKey = "default_folder"
                };
                treeView.Nodes.Add(treeNode);
            }

            foreach (TreeNode treeNode in treeView.Nodes)
            {
                getSubdirectoryes(classFTP, treeNode, Path.GetFileName(treeNode.Name));
            }

        }

        private async Task getSubdirectoryes(ClassFTP classFTP, TreeNode treeNode, string directory)
        {
            string[] dsubirectoryes = classFTP.ListDirectoryWithotFiles(directory);
            foreach (string subdirectory in dsubirectoryes)
            {
                if (subdirectory.Contains("..")) continue;

                TreeNode tn = new TreeNode
                {
                    Name = subdirectory.Replace('\\', '/'),
                    Text = Path.GetFileName(subdirectory),
                    ImageKey = "default_folder",
                    SelectedImageKey = "default_folder"
                };
                treeNode.Nodes.Add(tn);
            }
        }
    }
}
