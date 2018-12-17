using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFiles;
using VitFTP;
using VitIcons;
using VitNotifyMessage;
using vitProgressStatus;
using VitTypeCard;
using VitUsers;

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

        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();

        private readonly VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
        private readonly FormProgressStatus formProgressStatus = new FormProgressStatus();

        public async void AddFileNode(TreeView treeView)
        {
            string paths = treeView.SelectedNode.FullPath;
            TreeNode treeNode = treeView.SelectedNode;
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
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
                        classFTP.Upload2Async(openFileDialog.FileNames, paths + "/");
                    }));
                });
            }
            treeView.Invoke((Action)(async () =>
            {
                await update(treeView);
            }));
        }

        public async void AddFileNodeWithCard(TreeView treeView)
        {
            string paths = treeView.SelectedNode.FullPath;
            TreeNode treeNode = treeView.SelectedNode;
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
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
                        classFTP.Upload2Async(openFileDialog.FileNames, paths + "/");
                    }));
                });
            }
            treeView.Invoke((Action)(async () =>
            {
                await update(treeView);
            }));
        }

        public void addNodeFolder(TreeView treeView)
        {
            string path = "";
            if (treeView.SelectedNode != null)
            {
                path = treeView.SelectedNode.FullPath;
            }

            FormTreeInput formTreeInput = new FormTreeInput();
            if (formTreeInput.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.CreateDirectory(path + "/" + formTreeInput.textBox1.Text);

            if (classFTP.FileExist(path + "/" + formTreeInput.textBox1.Text))
            {
                TreeNode treeNode = new TreeNode
                {
                    Name = formTreeInput.textBox1.Text,
                    Text = formTreeInput.textBox1.Text,
                    ImageKey = "default_folder",
                };

                treeView.SelectedNode.Nodes.Add(treeNode);
            }
        }

        public void copy(TreeView treeView)
        {
            string sourcePath = treeView.SelectedNode.FullPath;
            FormTree formTree = new FormTree();
            if (formTree.ShowDialog() == DialogResult.OK)
            {
                string targetPath = formTree.treeView1.SelectedNode.FullPath;
                Console.WriteLine(sourcePath + "    ->    " + targetPath);
                ClassUsers classUsers = new ClassUsers();
                ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                classFTP.copyAsync(sourcePath, targetPath);
                update(treeView);
            }
        }

        public void DeleteNode(TreeView treeView)
        {
            VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();

            DialogResult dialogResult = classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.QUESTION, "Вы точно хотите удалить" + treeView.SelectedNode.FullPath + "?");
            if (dialogResult == DialogResult.No) return;

            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.ChangeWorkingDirectory("");

            string result = "";
            result = classFTP.RemoveDirecroty2(treeView.SelectedNode.FullPath).ToString();

            Console.WriteLine(result);

            if ((result.Split(' ')[0] == "250") || (result.Equals("True")))
            {
                treeView.SelectedNode.Remove();
            }
            else
            {
                classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Ошибка, не получилось удалить объект!");
            }
        }

        public void getNextNodes(TreeNode treeNode)
        {
            if (treeNode.Nodes.Count > 0)
            {
                return;
            }

            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.ChangeWorkingDirectory(treeNode.FullPath);
            foreach (string directory in classFTP.ListDirectory())
            {
                TreeNode tn = new TreeNode
                {
                    Name = directory,
                    Text = Path.GetFileName(directory)
                };
                addIcon(tn);
                treeNode.Nodes.Add(tn);
            }
        }

        public async void init(TreeView treeView)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
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
                        Text = direcory
                    };
                    addIcon(treeNode);

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

        public void move(TreeView treeView)
        {
            string sourcePath = treeView.SelectedNode.FullPath;
            FormTree formTree = new FormTree();
            if (formTree.ShowDialog() == DialogResult.OK)
            {
                string targetPath = formTree.treeView1.SelectedNode.FullPath;
                Console.WriteLine(sourcePath + "    ->    " + targetPath);
                ClassUsers classUsers = new ClassUsers();
                ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                classFTP.moveAsync(sourcePath, targetPath);
                update(treeView);
            }
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
            treeView.SelectedNode.Nodes.Clear();
            getNextNodes(treeView.SelectedNode);
            preLoadNodes(treeView.SelectedNode);
            treeView.SelectedNode.Expand();
        }

        private void addIcon(TreeNode treeNode)
        {
            if (Path.GetExtension(treeNode.Name) != "")
            {
                if (ClassImageList.imageList.Images.ContainsKey(Path.GetExtension(treeNode.Name).TrimStart('.')))
                {
                    treeNode.ImageKey = Path.GetExtension(treeNode.Name).TrimStart('.');
                    treeNode.SelectedImageKey = Path.GetExtension(treeNode.Name).TrimStart('.');
                }
                else
                {
                    treeNode.ImageKey = "default_file";
                    treeNode.SelectedImageKey = "default_file";
                }
            }
            else
            {
                treeNode.ImageKey = "default_folder";
                treeNode.SelectedImageKey = "default_folder";
            }
        }

        private async Task getSubdirectoryes(ClassFTP classFTP, TreeNode treeNode, string directory)
        {
            classFTP.ChangeWorkingDirectory(directory);
            Console.WriteLine(classFTP.ChangeWorkingDirectory(""));
            string[] dsubirectoryes = classFTP.ListDirectory();
            foreach (string subdirecory in dsubirectoryes)
            {
                TreeNode tn = new TreeNode
                {
                    Name = subdirecory,
                    Text = Path.GetFileName(subdirecory)
                };
                addIcon(tn);

                treeNode.Nodes.Add(tn);
            }
            classFTP.ChangeWorkingDirectory("..");
            Console.WriteLine(classFTP.ChangeWorkingDirectory(""));
        }

        public void rename(TreeView treeView)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            FormTreeInput formTreeInput = new FormTreeInput();
            formTreeInput.Text = "Переименовать";
            formTreeInput.textBox1.Text = Path.GetFileName(treeView.SelectedNode.FullPath);
            formTreeInput.buttonOk.Text = "Переименовать";
            DialogResult dialogResult = formTreeInput.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            string res = classFTP.Rename(treeView.SelectedNode.FullPath, formTreeInput.textBox1.Text);
            if (res.Contains("250") == true) treeView.SelectedNode.Text = formTreeInput.textBox1.Text;
            else
            {
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось переименовать!");
            }
            treeView.Sort();
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