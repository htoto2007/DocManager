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

        private readonly ClassImageList ClassImageList = new ClassImageList();

        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();

        private readonly FormCompanents formCompanents = new FormCompanents();
        //private readonly FormProgressStatus formProgressStatus = new FormProgressStatus();

        public async Task AddFileNodeWithoutCardAsync(TreeView treeView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            string[] files = await classFiles.CreateFileWithoutCardAsync(openFileDialog.FileNames, treeView.SelectedNode.FullPath.Replace("\\", "/"));
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

        public void AddBranch(TreeView treeView)
        {
            string[] directories = Directory.GetDirectories(VitSettings.Properties.GeneralsSettings.Default.programPath + "\\originalDirectories\\Организация\\");
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            int dirCount = classFTP.ListDirectory("/").GetLength(0);
            string dirName = "Организация " + dirCount.ToString();
            classFTP.CreateDirectory("/" + dirName);
            classFTP.Upload2Async(directories[0], "/" + dirName + "/", false );
            Init(treeView, classUsers.getThisUser().login, classUsers.getThisUser().password);
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

        public async Task AddFileNodeWithCardAsync(TreeView treeView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                VitFiles.ClassFiles classFiles = new VitFiles.ClassFiles();
                string[] files = await classFiles.CreateFileWithCardAsync(openFileDialog.FileNames, treeView.SelectedNode.FullPath.Replace('\\', '/'));
                
                if (files == null) return;
                foreach (var file in files)
                {
                    TreeNode treeNodeFile = new TreeNode();
                    treeNodeFile.Name = file;
                    treeNodeFile.Text = Path.GetFileName(file);
                    treeNodeFile.ImageKey = Path.GetExtension(file).Trim('.');
                    treeView.SelectedNode.Nodes.Add(treeNodeFile);
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

        public void copy(TreeView treeView, string login, string password)
        {
            string sourcePath = treeView.SelectedNode.FullPath;
            FormTree formTree = new FormTree(login, password);
            formTree.Text = "Копирование";
            formTree.textBoxSelectedPath.Text = sourcePath;
            if (formTree.ShowDialog() == DialogResult.OK)
            {
                string targetPath = formTree.treeView1.SelectedNode.FullPath;
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
                    treeNodeClone.Name = copyFilesOk[0];
                    treeNodeClone.Text = Path.GetFileName(copyFilesOk[0]);
                    treeNodes[0].Nodes.Add(treeNodeClone);
                    treeView.Sort();
                }
            }
        }

        public void DeleteNode(TreeView treeView)
        {
            VitNotifyMessage.ClassNotifyMessage classNotifyMessage = new VitNotifyMessage.ClassNotifyMessage();

            DialogResult dialogResult = classNotifyMessage.showDialog(VitNotifyMessage.ClassNotifyMessage.TypeMessage.QUESTION, "Вы точно хотите удалить" + treeView.SelectedNode.FullPath + "?");
            if (dialogResult == DialogResult.No) return;

            string[] filesok = classFiles.DeleteFiles(new string[] { treeView.SelectedNode.Name.Replace("\\", "/") });

            if (filesok.GetLength(0) < 1)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Есть не удаленные файлы!");
                return;
            }

            if (treeView.SelectedNode.Name == filesok[0]) treeView.SelectedNode.Remove();
            else classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Есть не удаленные файлы! '" + filesok[0] + "'");
        }

        public void getNextNodes(TreeNode treeNode)
        {
            if (treeNode.Nodes.Count > 0) return;

            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            string[] files = classFTP.ListDirectory("/" + treeNode.FullPath);
            foreach (string file in files)
            {
                if (file.Contains("..")) continue;
                TreeNode tn = new TreeNode
                {
                    Name = file,
                    Text = Path.GetFileName(file)
                };
                addIconAsync(tn);
                treeNode.Nodes.Add(tn);
            }
        }

        public async void Init(TreeView treeView, string login, string password)
        {
            //ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(login, password);
            string[] directoryes = classFTP.ListDirectory("/");
            
            treeView.ImageList = ClassImageList.imageList;
            treeView.Nodes.Clear();

            if (directoryes == null) return;
            foreach (string direcory in directoryes)
            {
                if (direcory.Contains("..")) continue;
                TreeNode treeNode = new TreeNode
                {
                    Name = direcory,
                    Text = direcory.Trim('/')
                };
                addIconAsync(treeNode);
                //treeView.Invoke((Action)(() =>
                //{
                    treeView.Nodes.Add(treeNode);
                //}));
            }
            
            foreach (TreeNode treeNode in treeView.Nodes)
            {
                if (Path.GetExtension(treeNode.Name) == "")
                    if (await Task.Run(() => classFTP.getFileType(treeNode.Name) == 2)) 
                        getSubdirectoryes(classFTP, treeNode, Path.GetFileName(treeNode.Name));
            }
        }

        /// <summary>
        /// Обращается к функции перемещения файлов на сервере и отправляет ей данные.
        /// </summary>
        /// <param name="treeView">Дерево в котором находится нужный файл для перемещения</param>
        public async Task MoveAsync(TreeView treeView, string login, string password)
        {
            string sourcePath = treeView.SelectedNode.FullPath;
            FormTree formTree = new FormTree(login, password);
            formTree.Text = "Перемещение";
            formTree.textBoxSelectedPath.Text = sourcePath;
            if (formTree.ShowDialog() == DialogResult.OK)
            {
                string targetPath = formTree.treeView1.SelectedNode.FullPath.Replace("/", "\\");
                string[] completeFiles = await classFiles.MoveFileAsync(new string[] { sourcePath }, targetPath);
                if (completeFiles == null) return;

                TreeNode treeNodeClone = (TreeNode)treeView.SelectedNode.Clone();
                treeNodeClone.Name = completeFiles[0];
                treeView.SelectedNode.Remove();
                TreeNode[] treeNodes = treeView.Nodes.Find(formTree.treeView1.SelectedNode.Name, true);
                treeNodes[0].Nodes.Add(treeNodeClone);
            }
        }

        public async Task preLoadNodesAsync(TreeNode treeNode)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // проверяем наличие поуздлв
                //if (tn.Nodes.Count == 0) 
                    //continue;
                // проверяем есть ли расщирение в имени узлов
                if (Path.GetExtension(tn.FullPath) != "")
                    continue;
                // точно определяем тип представления
                if (await Task.Run(() => ( classFTP.getFileType("/" + tn.Name))) == 2)
                    getNextNodes(tn);
            }
        }

        public async Task update(TreeView treeView)
        {
            treeView.SelectedNode.Nodes.Clear();
            getNextNodes(treeView.SelectedNode);
            preLoadNodesAsync(treeView.SelectedNode);
            treeView.SelectedNode.Expand();
        }

        private async void addIconAsync(TreeNode treeNode)
        {
            if (Path.GetExtension(treeNode.Name) != "")
            {
                bool res = await Task.Run(() => ( ClassImageList.imageList.Images.ContainsKey(Path.GetExtension(treeNode.Name).TrimStart('.'))) );
                if (res == true)
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
                treeNode.StateImageKey = "default_folder_no_empty";
            }
        }

        private async Task getSubdirectoryes(ClassFTP classFTP, TreeNode treeNode, string directory)
        {
            string[] dsubirectoryes = classFTP.ListDirectory(directory);
            foreach (string subdirectory in dsubirectoryes)
            {
                if (subdirectory.Contains("..")) continue;
                
                TreeNode tn = new TreeNode
                {
                    Name = subdirectory.Replace('\\', '/'),
                    Text = Path.GetFileName(subdirectory)
                };
                addIconAsync(tn);
                treeNode.Nodes.Add(tn);
            }
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