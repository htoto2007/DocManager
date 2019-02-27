using System;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFileCard;
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
        private readonly ClassCardPropValue classCardPropsValue = new ClassCardPropValue();
        private readonly ClassFiles classFiles = new ClassFiles();
        private readonly ClassImageList ClassImageList = new ClassImageList();
        private readonly ClassTypeCard classTypeCard = new ClassTypeCard();
        private readonly FormCompanents formCompanents = new FormCompanents();
        //private readonly FormProgressStatus formProgressStatus = new FormProgressStatus();

        public async Task AddFileNodeWithoutCardAsync(TreeView treeView)
        {
            // получаем путь загрузки на сервер
            string targetPath = treeView.SelectedNode.Name;

            // открываем окно выбора фалов на загрузку
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            // загружаем файл на сервер
            string[] files = await classFiles.CreateFileAsync(openFileDialog.FileNames, targetPath);
            if (files == null) return;

            // Добавляем узлы дерева из загруженых документов
            foreach (var file in files) {
                TreeNode[] treeNodes = treeView.SelectedNode.Nodes.Find(file, false);
                if (treeNodes.GetLength(0) > 0) continue;
                TreeNode treeNodeFile = new TreeNode();
                treeNodeFile.Name = targetPath + "/" + Path.GetFileName(file);
                treeNodeFile.Text = Path.GetFileName(file);
                treeNodeFile.ImageKey = Path.GetExtension(file).Trim('.');
                treeNodeFile.SelectedImageKey = Path.GetExtension(file).Trim('.');

                treeView.SelectedNode.Nodes.Add(treeNodeFile);
            }
        }

        public void AddBranch(TreeView treeView)
        {
            string[] directories = Directory.GetDirectories(VitSettings.Properties.GeneralsSettings.Default.programPath + "\\originalDirectories\\Организация\\");
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            int dirCount = classFTP.ListDirectory("/").GetLength(0);
            string dirName = "Организация " + dirCount.ToString();
            classFTP.CreateDirectory("/" + dirName);
            classFTP.UploadAsync(directories[0], "/" + dirName + "/", false );
            
            Init(treeView, classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.sessionClose();
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

        /// <summary>
        /// Обеспечивает загрузку файла на сервер и создание карточки файла
        /// </summary>
        /// <param name="treeView">Дерево в котором производим выбор папки для загрузки файла</param>
        public async void AddFileNodeWithCardAsync(TreeView treeView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            FormFileCard formFileCard = new FormFileCard();
            ClassFiles classFiles = new ClassFiles();
            
            // выводим окно карточки файла
            if (formFileCard.ShowDialog() != DialogResult.OK) return;
            var propsCollection = formFileCard.GetProps();
            // выводим окно выбора файлов
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            // создаем пути к папке на сервере
            string targetPath = treeView.SelectedNode.Name;
            // загружаем файлы
            string[] files = null;
            files = await classFiles.CreateFileAsync(openFileDialog.FileNames, targetPath);
            // проверяем есть ли загруженные файлы
            if (files == null) return;

            
            FormProgressStatus formProgressStatus = new FormProgressStatus(0, files.GetLength(0));
            formProgressStatus.buttonCancel.Enabled = false;
            await Task.Run(() => { Thread.Sleep(2000); });
            int iterator = 0;
            foreach (var file in files)
            {
                formProgressStatus.Iterator(
                    iterator,
                    file,
                    "Запись в базу данных",
                    iterator.ToString() + "/" + files.GetLength(0).ToString(),
                    "Создание карточки");

                string path = targetPath + "/" + Path.GetFileName(file);
                int idFile = classFiles.getInfoByFilePath(path).id;
                if (idFile < 1)
                {
                    Console.WriteLine("Файл в базе не найден! " + path);
                    continue;
                }
                foreach (var prop in propsCollection)
                    await Task.Run(() => { classCardPropsValue.createValue(prop.idProp, prop.text, idFile); }) ;

                TreeNode treeNodeFile = new TreeNode();
                treeNodeFile.Name = path;
                treeNodeFile.Text = Path.GetFileName(file);
                treeNodeFile.ImageKey = Path.GetExtension(file).Trim('.');
                treeNodeFile.SelectedImageKey = Path.GetExtension(file).Trim('.');
                treeView.SelectedNode.Nodes.Add(treeNodeFile);
                if(files.GetLength(0) == 1) treeView.SelectedNode = treeNodeFile;
                iterator++;
            }
            formProgressStatus.Close();
            formProgressStatus.Dispose();
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
            classFTP.SessionOpen();
            bool result = classFTP.CreateDirectory(path);
            classFTP.sessionClose();
            if (!result)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось создать папку!");
                return;
            }
            
            TreeNode treeNode = new TreeNode
            {
                Name = path,
                Text = formTreeInput.textBox1.Text,
                ImageKey = "default_folder",
                SelectedImageKey = "default_folder"
            };

            treeView.SelectedNode.Nodes.Add(treeNode);
            treeView.SelectedNode = treeNode;
            //treeView.Sort();
        }

        public void copy(TreeView treeView, string login, string password)
        {
            string sourcePath = treeView.SelectedNode.Name;
            // выводим окно выбора местан директории назначения 
            FormTree formTree = new FormTree(login, password);
            formTree.Text = "Копирование";
            formTree.textBoxSelectedPath.Text = sourcePath;
            if (formTree.ShowDialog() != DialogResult.OK) return;

            string targetPath = formTree.treeView1.SelectedNode.Name;
            string[] copyFilesOk = classFiles.Copy(new string[] { sourcePath }, targetPath);
            if (copyFilesOk.GetLength(0) < 1)
            {
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, "Не получилось скопироватиь файл.");
                return;
            }

            TreeNode[] treeNodes = treeView.Nodes.Find(formTree.treeView1.SelectedNode.Name, true);
            if (treeNodes.GetLength(0) > 0)
            {
                TreeNode treeNodeClone = (TreeNode)treeView.SelectedNode.Clone();
                treeNodeClone.Name = copyFilesOk[0];
                treeNodeClone.Text = Path.GetFileName(copyFilesOk[0]);
                treeNodes[0].Nodes.Add(treeNodeClone);
                treeView.SelectedNode = treeNodeClone;
            }
        }

        /// <summary>
        /// Производит вызов функций для удаления файла или папки по узлу дерева
        /// </summary>
        /// <param name="treeView">Дерево над которым проводим операцию</param>
        public async void DeleteNode(TreeView treeView)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            // запрашиваем подтверждение выполнения операции
            DialogResult dialogResult = classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.QUESTION, "Вы точно хотите удалить " + Path.GetFileName(treeView.SelectedNode.FullPath) + "?");
            if (dialogResult == DialogResult.No) return;

            // делаем запрос на удаление
            string[] filesok = null;
            filesok = await classFiles.DeleteFiles(new string[] { treeView.SelectedNode.Name });

            // проверяем актуальность списка удалкееых файлов
            if (filesok.GetLength(0) < 1)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Есть не удаленные файлы!");
                return;
            }

            // сверяемся с результатами удаления
            if (treeView.SelectedNode.Name == filesok[0]) treeView.SelectedNode.Remove();
            else classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Есть не удаленные файлы! '" + filesok[0] + "'");
        }

        /// <summary>
        /// Производит получение файлов из указанной директории на сервере и выводит их список в дерево
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="classFTP"></param>
        public void getNextNodes(TreeNode treeNode, ClassFTP classFTP)
        {
            // получаем список файлов директории
            var files = classFTP.ListDirectoryDetail(treeNode.Name);
            if (files.GetLength(0) < 1) return;

            foreach (var file in files)
            {
                // отсеиваем путь возврата
                if (file.path.Contains("..")) continue;
                // проверяем на наличие повторяющихся узлов
                TreeNode[] treeNodes = treeNode.Nodes.Find(file.path, true);
                if (treeNodes.GetLength(0) > 0) continue;
                TreeNode tn = new TreeNode
                {
                    Name = file.path,
                    Text = Path.GetFileName(file.path)
                };
                // присваиваем иконку узлу
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
            classFTP.SessionOpen();
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
                if ((direcory.isDirectory) && (!direcory.path.Contains("..")))
                    getSubdirectoryes(classFTP, treeNode, Path.GetFileName(treeNode.Name));
            }
            classFTP.sessionClose();
            if (treeView.Nodes.Count > 0) treeView.SelectedNode = treeView.Nodes[0];
        }

        /// <summary>
        /// Обращается к функции перемещения файлов на сервере и отправляет ей данные.
        /// </summary>
        /// <param name="treeView">Дерево в котором находится нужный файл для перемещения</param>
        public async Task MoveAsync(TreeView treeView, string login, string password)
        {
            string sourcePath = treeView.SelectedNode.Name;
            // выводим форму для выбора директории назначения
            FormTree formTree = new FormTree(login, password);
            formTree.Text = "Перемещение";
            formTree.textBoxSelectedPath.Text = sourcePath;
            if (formTree.ShowDialog() != DialogResult.OK) return;
            string targetPath = formTree.treeView1.SelectedNode.Name.Replace("/", "\\");

            // производим перемещение
            string[] completeFiles = await classFiles.MoveFileAsync(new string[] { sourcePath }, targetPath);
            if (completeFiles == null) return;
            if (completeFiles.GetLength(0) < 1) return;

            // выводим в дерево
            TreeNode treeNodeClone = (TreeNode)treeView.SelectedNode.Clone();
            treeNodeClone.Name = targetPath.Replace("\\", "/") + "/" + Path.GetFileName(completeFiles[0]);
            treeView.SelectedNode.Remove();
            TreeNode[] treeNodes = treeView.Nodes.Find(formTree.treeView1.SelectedNode.Name, true);
            treeNodes[0].Nodes.Add(treeNodeClone);
            treeView.SelectedNode = treeNodeClone;
        }

        /// <summary>
        /// Подгружает внутреннии узлы переданного узла
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public void PreLoadNodesAsync(TreeNode treeNode)
        {
            TreeNode tNode;
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            var files = classFTP.ListDirectoryDetail(treeNode.Name);
            if (files.GetLength(0) < 1) return;
            Console.WriteLine("Открытие папки " + treeNode.Name);
            for (int i = 0; i < files.GetLength(0); i++)
            {
                // отсеиваем путь в предыдущую директорию
                if (files[i].path.Contains("..")) continue;
                // точно определяем тип представления
                if (!files[i].isDirectory) continue;
                // проверяем уже загруженные папки на совпадение со списком папок с сервеа
                TreeNode[] treeNodes = treeNode.Nodes.Find(files[i].path, true);
                if (treeNodes == null) continue;
                if (treeNodes.GetLength(0) < 1) continue;

                tNode = treeNodes[0];
                getNextNodes(tNode, classFTP);
            }
            classFTP.sessionClose();
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

        /// <summary>
        /// Запускает операцию переименования узла
        /// </summary>
        /// <param name="treeView">Делево над которым производим операцию</param>
        public void rename(TreeView treeView)
        {
            // выводим форму ввода для нового названия
            FormTreeInput formTreeInput = new FormTreeInput();
            formTreeInput.Text = "Переименовать";
            formTreeInput.textBox1.Text = Path.GetFileName(treeView.SelectedNode.Name);
            formTreeInput.buttonOk.Text = "Переименовать";
            DialogResult dialogResult = formTreeInput.ShowDialog();
            if (dialogResult != DialogResult.OK) return;

            // обращаемся к FTP для переименования
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            string res = classFTP.Rename(treeView.SelectedNode.FullPath, formTreeInput.textBox1.Text);
            classFTP.sessionClose();

            // проверяем результат операции
            if (res.Contains("250") == true)
            {
                treeView.SelectedNode.Text = formTreeInput.textBox1.Text;
                treeView.SelectedNode.Name = "/" + treeView.SelectedNode.FullPath.Replace("\\", "/");
                return;
            }
            
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось переименовать!");
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