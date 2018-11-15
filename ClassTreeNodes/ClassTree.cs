using System;
using System.IO;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFiles;
using VitFTP;
using VitIcons;
using VitRelationFolders;
using VitTypeCard;

namespace VitTree
{
    /// <summary>
    /// Класс включающий в себя методы по работе с деревом.
    /// </summary>
    public class ClassTree
    {
        private readonly ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();

        private readonly ClassFiles classFiles = new ClassFiles();

        private readonly VitIcons.ClassImageList ClassImageList = new VitIcons.ClassImageList();

        private readonly ClassRelationFolders classRelationFolders = new ClassRelationFolders();

        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();

        private readonly VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();

        public void deleteNode(TreeView treeView)
        {
            ClassFTP classFTP = new ClassFTP();
            classFTP.ChangeWorkingDirectory("");

            string result = "";
            if (Path.GetExtension(treeView.SelectedNode.FullPath) == "")
            {
                result = classFTP.RemoveDirectory(treeView.SelectedNode.FullPath);
            }
            else
            {
                result = classFTP.DeleteFile(treeView.SelectedNode.FullPath);
            }

            if (result.Split(' ')[0] == "250")
            {
                treeView.SelectedNode.Remove();
            }
        }

        public void getNextNodes(TreeNode treeNode)
        {
            if (treeNode.Nodes.Count > 0)
            {
                return;
            }

            ClassFTP classFTP = new ClassFTP();

            Console.WriteLine(treeNode.FullPath);
            string[] arrDirectoryesName = treeNode.FullPath.Split('\\');
            Console.WriteLine(classFTP.ChangeWorkingDirectory(""));

            foreach (string folderName in arrDirectoryesName)
            {
                Console.WriteLine(folderName);
                classFTP.ChangeWorkingDirectory(folderName);
            }

            string[] directoryes = classFTP.ListDirectory();
            foreach (string direcory in directoryes)
            {
                TreeNode tn = new TreeNode
                {
                    Name = direcory,
                    ImageKey = "icons8-folder-48.png",
                    Text = Path.GetFileName(direcory)
                };

                getSubdirectoryes(classFTP, tn, Path.GetFileName(direcory));
                treeNode.Nodes.Add(tn);
            }
        }

        public void init(TreeView treeView)
        {
            ClassFTP classFTP = new ClassFTP();
            string[] directoryes = classFTP.ListDirectory();

            treeView.ImageList = ClassImageList.imageList;
            treeView.Nodes.Clear();
            foreach (string direcory in directoryes)
            {
                TreeNode treeNode = new TreeNode
                {
                    Name = direcory,
                    ImageKey = "icons8-folder-48.png",
                    Text = direcory
                };

                getSubdirectoryes(classFTP, treeNode, Path.GetFileName(direcory));
                treeView.Nodes.Add(treeNode);
            }
        }

        public void preLoadNodes(TreeView treeView)
        {
            foreach (TreeNode treeNode in treeView.SelectedNode.Nodes)
            {
                if ((treeNode.Nodes.Count == 0) && (Path.GetExtension(treeNode.Name) == ""))
                {
                    getNextNodes(treeNode);
                }
            }
        }

        private void getSubdirectoryes(ClassFTP classFTP, TreeNode treeNode, string directory)
        {
            classFTP.ChangeWorkingDirectory(directory);
            Console.WriteLine(classFTP.ChangeWorkingDirectory(""));
            string[] dsubirectoryes = classFTP.ListDirectory();
            foreach (string subdirecory in dsubirectoryes)
            {
                if (Path.GetExtension(subdirecory) == "")
                {
                    treeNode.Nodes.Add(subdirecory, Path.GetFileName(subdirecory), "icons8-folder-48.png");
                }
                else
                {
                    treeNode.Nodes.Add(subdirecory, Path.GetFileName(subdirecory), "icons8-folder-48.png");
                }
            }
            classFTP.ChangeWorkingDirectory("..");
            Console.WriteLine(classFTP.ChangeWorkingDirectory(""));
        }

        public struct TypeNodeCollection
        {
            public const string FILE = "file";
            public const string FOLDER = "folder";
        }
    }

    /// <summary>
    /// Объект содержащий <see cref="TreeView"/>
    /// </summary>
    public class TW
    {
        public TreeView TreeView;
    }
}