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
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            
            
            // выводим окно ввода имени новой папки
            FormTreeInput formTreeInput = new FormTreeInput();
            formTreeInput.Text = "Создание папки";
            formTreeInput.buttonOk.Text = "Создать";
            if (formTreeInput.ShowDialog() != DialogResult.OK) return;

            string path = "";
            // получаем путь выделенного элемента в дереве
            if (treeView.SelectedNode == null) path = "/";
            else path = treeView.SelectedNode.Name + "/" + formTreeInput.textBox1.Text;

            // создаем папку на сервере
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            bool result = classFTP.CreateDirectory(path);
            if (!result) return;
            
            TreeNode treeNode = new TreeNode
            {
                Name = path,
                Text = formTreeInput.textBox1.Text,
                ImageKey = "default_folder",
                SelectedImageKey = "default_folder"
            };

            treeView.SelectedNode.Nodes.Add(treeNode);
            treeView.Sort();
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
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();

            DialogResult dialogResult = classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.QUESTION, "Вы точно хотите удалить " + Path.GetFileName(treeView.SelectedNode.FullPath) + "?");
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
            var files = classFTP.ListDirectoryDetail(treeNode.Name);
            foreach (var file in files)
            {
                if (file.path.Contains("..")) continue;
                TreeNode tn = new TreeNode
                {
                    Name = file.path,
                    Text = Path.GetFileName(file.path)
                };
                addIconAsync(tn, file.isDirectory);
                treeNode.Nodes.Add(tn);
            }
        }

        /// <summary>
        /// Производит очистку TreeView и выводит все элементы репозитория на сервере
        /// </summary>
        /// <param name="treeView">TreeView в который нужно вывести файлы и папки</param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public async void Init(TreeView treeView, string login, string password)
        {
            ClassFTP classFTP = new ClassFTP(login, password);
            var fileCollections = classFTP.ListDirectoryDetail("/");
            
            treeView.ImageList = ClassImageList.imageList;
            treeView.Nodes.Clear();

            if (fileCollections == null) return;
            foreach (var direcory in fileCollections)
            {
                if (direcory.path.Contains("..")) continue;
                TreeNode treeNode = new TreeNode
                {
                    Name = direcory.path,
                    Text = Path.GetFileName(direcory.path)
                };
                
                addIconAsync(treeNode, direcory.isDirectory);
                treeView.Nodes.Add(treeNode);
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
            string sourcePath = treeView.SelectedNode.Name;
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

        /// <summary>
        /// Подгружает внутреннии узлы переданного узла
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public async Task PreLoadNodesAsync(TreeNode treeNode)
        {
            TreeNode tNode;
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            var files = classFTP.ListDirectoryDetail(treeNode.Name);
            
            for (int i = 0; i < files.GetLength(0); i++)
            {
                if (files[i].path.Contains("..")) continue;
                // точно определяем тип представления
                if (!files[i].isDirectory) continue;
                // проверяем уже загруженные папки на совпадение со списком папок с сервеа
                
                TreeNode[] treeNodes = treeNode.Nodes.Find(files[i].path, false);
                
                if (treeNodes == null) continue;
                if (treeNodes.GetLength(0) < 1) continue;
                tNode = treeNodes[0];
                getNextNodes(tNode);
            }
        }

        public async Task update(TreeView treeView)
        {
            treeView.SelectedNode.Nodes.Clear();
            getNextNodes(treeView.SelectedNode);
            PreLoadNodesAsync(treeView.SelectedNode);
            treeView.SelectedNode.Expand();
        }

        private async void addIconAsync(TreeNode treeNode, bool isDirectory)
        {
            if (!isDirectory)
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
            }
        }

        private async Task getSubdirectoryes(ClassFTP classFTP, TreeNode treeNode, string directory)
        {
            var dsubirectoryes = classFTP.ListDirectoryDetail(directory);
            foreach (var subdirectory in dsubirectoryes)
            {
                if (subdirectory.path.Contains("..")) continue;
                
                TreeNode tn = new TreeNode
                {
                    Name = subdirectory.path,
                    Text = Path.GetFileName(subdirectory.path)
                };
                addIconAsync(tn, subdirectory.isDirectory);
                treeNode.Nodes.Add(tn);
            }
        }

        public void rename(TreeView treeView)
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            FormTreeInput formTreeInput = new FormTreeInput();
            formTreeInput.Text = "Переименовать";
            formTreeInput.textBox1.Text = Path.GetFileName(treeView.SelectedNode.Name);
            formTreeInput.buttonOk.Text = "Переименовать";
            DialogResult dialogResult = formTreeInput.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            
            string res = classFTP.Rename(treeView.SelectedNode.FullPath, formTreeInput.textBox1.Text);
            if (res.Contains("250") == true)
            {
                treeView.SelectedNode.Text = formTreeInput.textBox1.Text;
                treeView.SelectedNode.Name = "/" + treeView.SelectedNode.FullPath.Replace("\\", "/");
            }
            else
            {
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось переименовать!");
            }
            //treeView.Sort();
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