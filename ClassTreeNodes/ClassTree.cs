using System;
using System.Drawing.Printing;
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
        //private readonly FormProgressStatus formProgressStatus = new FormProgressStatus();

        public void AddFileNodeWithoutCard(TreeView treeView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            var files = classFiles.createFileWithoutCard(openFileDialog.FileNames, treeView.SelectedNode.FullPath);
            if (files == null) return;

            // Добавляем узлы дерева из загруженых документов
            foreach (var file in files) {
                TreeNode[] treeNodes = treeView.SelectedNode.Nodes.Find(file, false);
                if (treeNodes.GetLength(0) > 0) continue;
                TreeNode treeNodeFile = new TreeNode();
                treeNodeFile.Name = file;
                treeNodeFile.Text = Path.GetFileName(file);
                treeNodeFile.ImageKey = Path.GetExtension(file).Trim('.');

                treeView.SelectedNode.Nodes.Add(treeNodeFile);
            }
        }

        public void sendToDesctop(TreeView treeView)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            string dest = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + Path.GetFileName(treeView.SelectedNode.FullPath);
            classFTP.DownloadFile(treeView.SelectedNode.FullPath, dest);
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            if (File.Exists(dest)) classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.INFORMATION, "Файд отправлен на ваш рабочий стол.");
            else classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Файд не удалось отправить на ваш рабочий стол.");
        }

        public void sendToPrint(TreeView treeView)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            string dest = VitSettings.Properties.FTPSettings.Default.pathTnp + "\\" + Path.GetFileName(treeView.SelectedNode.FullPath);
            classFTP.DownloadFile(treeView.SelectedNode.FullPath, dest);
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            if (File.Exists(dest)) classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.INFORMATION, "Файд отправлен на печать.");
            else
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Файд не удалось отправить на печать.");
                return;
            }
            PrintDocument PrintD = new PrintDocument();
            PrintD.DocumentName = dest;
            PrintD.Print();
        }

        public void sendToAnyFolder(TreeView treeView)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                ClassUsers classUsers = new ClassUsers();
                ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                string dest = folderBrowserDialog.SelectedPath + "\\" + Path.GetFileName(treeView.SelectedNode.FullPath);
                classFTP.DownloadFile(treeView.SelectedNode.FullPath, dest);
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                if (File.Exists(dest)) classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.INFORMATION, "Файд отправлен в " + dest);
                else classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Файд не удалось отправить в " + dest);
            }
        }

        public void AddFileNodeWithCard(TreeView treeView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                VitFiles.ClassFiles classFiles = new VitFiles.ClassFiles();
                string[] files = classFiles.createFileWithCard(openFileDialog.FileNames, treeView.SelectedNode.FullPath);
                if (files == null) return;
                foreach (var file in files)
                {
                    TreeNode treeNodeFile = new TreeNode();
                    treeNodeFile.Name = file;
                    treeNodeFile.Text = Path.GetFileName(file);
                    treeNodeFile.ImageKey = Path.GetExtension(file).Trim('.');
                }
                treeView.Sort();
            }
        }

        public void addNodeFolder(TreeView treeView)
        {
            string path = "";
            if (treeView.SelectedNode != null)
            {
                path = treeView.SelectedNode.FullPath;
            }

            FormTreeInput formTreeInput = new FormTreeInput();
            formTreeInput.Text = "Создание папки";
            formTreeInput.buttonOk.Text = "Создать";
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
                string targetPath = formTree.treeView1.SelectedNode.FullPath + "/копия - " + Path.GetFileName(treeView.SelectedNode.FullPath);
                string[] copyFilesOk = classFiles.Copy(new string[] { sourcePath }, targetPath);
                if(copyFilesOk.GetLength(0) < 1)
                {
                    ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, "Не получилось скопироватиь файл.");
                    return;
                }
                TreeNode[] treeNodes = treeView.Nodes.Find(formTree.treeView1.SelectedNode.Name, true);
                if(treeNodes.GetLength(0) > 0)
                {
                    TreeNode treeNodeClone = (TreeNode)treeView.SelectedNode.Clone();
                    treeNodeClone.Name = targetPath;
                    //treeNodeClone.ImageKey = Path.GetExtension(treeView.SelectedNode.FullPath);
                    //treeNodeClone.SelectedImageKey = Path.GetExtension(treeView.SelectedNode.FullPath);
                    //treeNodeClone.StateImageKey = Path.GetExtension(treeView.SelectedNode.FullPath);
                    treeNodeClone.Text = "копия - " + Path.GetFileName(treeView.SelectedNode.FullPath);
                    treeNodes[0].Nodes.Add(treeNodeClone);
                }
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
            //classFTP.ChangeWorkingDirectory(treeNode.FullPath);
            string[] files = classFTP.ListDirectory2("/" + treeNode.FullPath);
            foreach (string file in files)
            {
                if (file.Contains("..")) continue;
                TreeNode tn = new TreeNode
                {
                    Name = file,
                    Text = Path.GetFileName(file)
                };
                addIcon(tn);
                treeNode.Nodes.Add(tn);
            }
        }

        public async void Init(TreeView treeView)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            string[] directoryes = classFTP.ListDirectory2("/");

            treeView.ImageList = ClassImageList.imageList;
            //treeView.HideSelection = true;
            //treeView.FullRowSelect = true;
            treeView.Sort();
            treeView.Nodes.Clear();
            await Task.Run(async () =>
            {
                foreach (string direcory in directoryes)
                {
                    if (direcory.Contains("..")) continue;
                    TreeNode treeNode = new TreeNode
                    {
                        Name = direcory,
                        Text = direcory.Trim('/')
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
                TreeNode treeNodeClone = (TreeNode)treeView.SelectedNode.Clone();
                treeNodeClone.Name = targetPath + "\\" + Path.GetFileName(sourcePath);
                treeView.SelectedNode.Remove();
                TreeNode[] treeNodes = treeView.Nodes.Find(formTree.treeView1.SelectedNode.Name, true);
                treeNodes[0].Nodes.Add(treeNodeClone);
                //update(treeView);
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
            //classFTP.ChangeWorkingDirectory(directory);
            //Console.WriteLine(classFTP.ChangeWorkingDirectory(""));
            string[] dsubirectoryes = classFTP.ListDirectory2(directory);
            foreach (string subdirectory in dsubirectoryes)
            {
                if (subdirectory.Contains("..")) continue;
                
                Console.WriteLine(subdirectory.Replace('\\', '/'));
                TreeNode tn = new TreeNode
                {
                    Name = subdirectory.Replace('\\', '/'),
                    Text = Path.GetFileName(subdirectory)
                };
                addIcon(tn);
                treeNode.Nodes.Add(tn);
            }
            //classFTP.ChangeWorkingDirectory("..");
            //Console.WriteLine(classFTP.ChangeWorkingDirectory(""));
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