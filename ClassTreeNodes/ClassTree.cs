using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFiles;
using VitFTP;
using VitIcons;
using vitProgressStatus;
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
        private readonly FormProgressStatus formProgressStatus = new FormProgressStatus();

        public async void AddFileNode(TreeView treeView)
        {
            string paths = treeView.SelectedNode.FullPath;
            TreeNode treeNode = treeView.SelectedNode;
            ClassFTP classFTP = new ClassFTP();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                await Task.Run(() =>
                {
                    treeView.Invoke((Action)(() =>
                    {
                        classFTP.Upload2(openFileDialog.FileNames, paths + "/");
                    }));
                });
            }
            treeView.Invoke((Action)(async () =>
            {
                await update(treeView);
            }));
        }

        public void copy(TreeView treeView)
        {
            FormTree formTree = new FormTree();
            if (formTree.ShowDialog() == DialogResult.OK)
            {
                ClassFTP classFTP = new ClassFTP();
                classFTP.copyAsync(treeView.SelectedNode.FullPath, formTree.treeView1.SelectedNode.FullPath + "/");
                update(treeView);
            }
        }

        public void DeleteNode(TreeView treeView)
        {
            ClassFTP classFTP = new ClassFTP();
            classFTP.ChangeWorkingDirectory("");

            string result = "";
            if (Path.GetExtension(treeView.SelectedNode.FullPath) == "")
            {
                result = classFTP.RemoveDirecroty2(treeView.SelectedNode.FullPath).ToString();
            }
            else
            {
                result = classFTP.DeleteFile(treeView.SelectedNode.FullPath);
            }

            Console.WriteLine(result);

            if ((result.Split(' ')[0] == "250") || (result.Equals("True")))
            {
                treeView.SelectedNode.Remove();
            }
        }

        public void getNextNodes(TreeNode treeNode)
        {
            if (Path.GetExtension(treeNode.Name) != "")
            {
                return;
            }

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

        public async void init(TreeView treeView)
        {
            ClassFTP classFTP = new ClassFTP();
            string[] directoryes = classFTP.ListDirectory();

            treeView.ImageList = ClassImageList.imageList;
            treeView.Sort();
            treeView.Nodes.Clear();
            await Task.Run(async () =>
            {
                foreach (string direcory in directoryes)
                {
                    TreeNode treeNode = new TreeNode
                    {
                        Name = direcory,
                        ImageKey = "icons8-folder-48.png",
                        Text = direcory
                    };
                    if (Path.GetExtension(treeNode.Name) == "")
                    {
                        await getSubdirectoryes(classFTP, treeNode, Path.GetFileName(direcory));
                    }
                    treeView.Invoke((Action)(() =>
                    {
                        treeView.Nodes.Add(treeNode);
                    }));
                }
            });
        }

        public void preLoadNodes(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if ((tn.Nodes.Count == 0) && (Path.GetExtension(tn.FullPath) == ""))
                {
                    getNextNodes(tn);
                }
            }
        }

        public async Task update(TreeView treeView)
        {
            if ((treeView.SelectedNode.Nodes.Count == 0) && (Path.GetExtension(treeView.SelectedNode.FullPath) == ""))
            {
                getNextNodes(treeView.SelectedNode);
            }
        }

        private async Task getSubdirectoryes(ClassFTP classFTP, TreeNode treeNode, string directory)
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