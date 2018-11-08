using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VitCardPropsValue;
using VitFiles;
using VitFolder;
using VitRelationFolders;
using VitTextStringMask;
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

        /// <summary>
        /// класс формы загрузки и карточки фала
        /// </summary>
        private readonly FormFiles formFiles = new FormFiles();

        //private contextMenuCollection contMenu;
        /// <summary>
        /// диалоговая форма дерева
        /// </summary>
        private readonly FormTree formTree = new FormTree();

        /// <summary>
        /// сласс формы текстового ввода
        /// </summary>
        private readonly FormTreeInput formTreeInput = new FormTreeInput();

        private ClassFolder classFolder = new ClassFolder();

        /// <summary>
        /// Служит буфером для обмена узлами между функциями
        /// </summary>
        private TreeNode globalNode;

        /// <summary>
        /// Буфер для передачи объекта дерева между функциями
        /// </summary>
        private TreeView objectTreeView;

        private Thread threadTreeView;

        /// <exclude />
        public ClassTree()
        {
            //contMenu = ContextShow();
        }

        /// <summary>
        /// Создает набор каталогов для филиала
        /// </summary>
        public void AddBranch()
        {
            // получаем узел организации
            TreeNode treeNode = objectTreeView.Nodes[0].Nodes[0];
            // клонируем узел организации
            TreeNode treeNodeClone = (TreeNode)objectTreeView.Nodes[0].Nodes[0].Clone();
            // добавляем узел организации в корень дерева
            objectTreeView.Nodes[0].Nodes.Add(treeNodeClone);
            // получаем номер последнего узла организации
            int lastNumberNode = objectTreeView.Nodes[0].Nodes.Count - 1;
            // удаляем узлы фалов из нового дерева
            RecursiveDeleteFileNodes(objectTreeView.Nodes[0].Nodes[lastNumberNode]);
            // добавляем в название организации "филиал"
            objectTreeView.Nodes[0].Nodes[lastNumberNode].Text = "Филиал " + (lastNumberNode).ToString() + " " + treeNode.Text;
            // отправляем узел организации в базу
            int id = classFolder.CreateFolder(0, objectTreeView.Nodes[0].Nodes[lastNumberNode].Text);
            // задаем новый id папке, согласно базе
            objectTreeView.Nodes[0].Nodes[lastNumberNode].Name = "folder_" + id.ToString();
            int indexNode = id;
            // переходим к рекурсивной отпраке данных в базу о всей ветви новой организации
            RecursiveAddNodeToData(objectTreeView.Nodes[0].Nodes[lastNumberNode], indexNode);
        }

        /// <summary>
        /// Запускает средства для копирования директории
        /// </summary>
        public void CopyFolder(TreeView treeView)
        {
            // получаем копируемую папку
            TreeNode treeNodeFolder = treeView.SelectedNode;
            // вызываем диалоговую форму с деревом
            TreeViewDialog("", "");
            DialogResult dialogResult = formTree.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            //получаем из диалогового дерева место назначения для копирования
            TreeNode treeNodeLocation = formTree.treeView1.SelectedNode;
            // клоируем копируемую папку
            TreeNode treeNodeFolderClone = (TreeNode)treeNodeFolder.Clone();
            // ищем узел, в которы надо вставить папку
            TreeNode[] treeNodes = objectTreeView.Nodes.Find(treeNodeLocation.Name, true);
            // вставляем в найденый узел копию папки
            treeNodes[0].Nodes.Add(treeNodeFolderClone);
            treeNodes[0].Expand();
            // получаем индекс узла в который будет производиться копирование
            int idParent = Convert.ToInt32(treeNodes[0].Name.Split('_')[1]);
            // отправляем узел организации в базу
            int id = classFolder.CreateFolder(idParent, treeNodeFolderClone.Text);
            // задаем новый id папке, согласно базе
            treeNodeFolderClone.Name = "folder_" + id.ToString();
            // переходим к рекурсивной отпраке данных в базу о всей ветви новой организации
            RecursiveAddNodeToData(treeNodeFolderClone, id);
        }

        /// <summary>
        /// Выдает целое числовое значение номера
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public int GetIdNode(TreeNode treeNode)
        {
            return Convert.ToInt32(treeNode.Name.Split('_')[1]);
        }

        /// <summary>
        /// Выдает строковое название типа
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public string GetTypeNode(TreeNode treeNode)
        {
            return treeNode.Name.Split('_')[0];
        }

        /// <summary>
        /// Инициализирует и выводит дерево
        /// </summary>
        /// <param name="treeView">Принемает указанный объект в работу</param>
        /// <returns>результат инициализации</returns>
        public void InitTreeView(TreeView treeView)
        {
            /*
            treeView.BeginUpdate();
            treeView.Nodes.Clear();

            treeView.ImageList = ClassImageList.imageList;
            TreeViewFolder(treeView);
            TreeViewFile(treeView);
            treeView.Nodes[0].Expand();
            globalNode = (TreeNode)objectTreeView.Nodes[0].Clone();

            formTree.treeView1.Nodes.Clear();
            formTree.treeView1.Nodes.Insert(0, globalNode);
            treeView.EndUpdate();
            return objectTreeView;*/

            TW tW = new TW
            {
                TreeView = treeView
            };
            threadTreeView = new Thread(new ParameterizedThreadStart(InitTreeViewThread))
            {
                IsBackground = true,
                Priority = ThreadPriority.Highest
            };

            threadTreeView.Start(tW);
        }

        /// <summary>
        /// Инициализирует и выводит дерево
        /// </summary>
        /// <returns>результат инициализации</returns>
        public void InitTreeView()
        {
            TreeView treeView = formTree.treeView1;
            InitTreeView(treeView);
        }

        public void InitTreeViewThread(object tWObj)
        {
            TW tW = (TW)tWObj;
            TreeView treeView = tW.TreeView;

            treeView.BeginInvoke((MethodInvoker)delegate
            {
                treeView.BeginUpdate();
                treeView.Nodes.Clear();
                treeView.ImageList = ClassImageList.imageList;
                TreeViewFolder(treeView);
                TreeViewFile(treeView);
                treeView.Nodes[0].Expand();
                globalNode = (TreeNode)objectTreeView.Nodes[0].Clone();
                formTree.treeView1.Nodes.Clear();
                formTree.treeView1.Nodes.Insert(0, globalNode);
                treeView.EndUpdate();
            });
        }

        /// <summary>
        /// Событие нажатия на пункт котекстного меню файла (копировать документ)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TreeFilesCopyFile_click(object sender, EventArgs e)
        {
            Console.WriteLine("start copy");
            CopyFile();
        }

        public void TreeFilesDeleteFile(TreeView treeView)
        {
            TreeNode treeNode = treeView.SelectedNode;

            DialogResult dialogResult = MessageBox.Show(
                "Вы точно хотите удалить \"" + treeNode.Text + "\"?",
                "Удаление",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning
            );
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            string[] paramsName = treeNode.Name.Split('_');
            classFiles.Delete(Convert.ToInt32(paramsName[1]));

            InitTreeView(treeView);
        }

        /// <summary>
        /// Handles the click event of the TreeFilesDocumentCard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void TreeFilesDocumentCard_click(object sender, EventArgs e)
        {
            int idFile = Convert.ToInt32(objectTreeView.SelectedNode.Name.Split('_')[1]);
            ClassFiles.FileCollection fileCollection = classFiles.GetFileById(idFile);
            formFiles.textBoxNameFile.Text = fileCollection.name;
            formFiles.textBoxTreePath.Text = objectTreeView.SelectedNode.FullPath;
        }

        /// <summary>
        /// Событие контекстного меню файла.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TreeFilesMoveFile_click(object sender, EventArgs e)
        {
            formTree.Text = "Переместить файл";
            formTree.buttonOk.Text = "Переместить";
            TreeNode treeNode = objectTreeView.SelectedNode;
            formTree.ShowDialog();

            DialogResult dialogResult = MessageBox.Show(
                "Вы точно хотите переместить \"" + treeNode.Text + "\" в " + objectTreeView.SelectedNode.FullPath,
                "Удаление",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning
            );

            if (dialogResult == DialogResult.Yes)
            {
                treeNode = objectTreeView.SelectedNode;
                int idFile = Convert.ToInt32(treeNode.Name.Split('_')[1]);
                int idFolder = Convert.ToInt32(formTree.treeView1.SelectedNode.Name.Split('_')[1]);
                string newPath = formTree.treeView1.SelectedNode.FullPath;
                classFiles.Move(idFile, idFolder, newPath);
                InitTreeView(objectTreeView);
            }
        }

        /// <summary>
        /// Создает файл в базе по указанному узлу дерева
        /// </summary>
        /// <param name="treeView"></param>
        public void TreeFolderAddDocument(TreeView treeView)
        {
            TreeNode treeNode = treeView.SelectedNode;
            formFiles.Text = "Укажите имя файла";
            formFiles.textBoxTreePath.Text = treeNode.FullPath;
            formFiles.ShowDialog();

            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true
            };
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // разбираем имя узла, в который будем помещать документ
            string[] paramsName = treeNode.Name.Split('_');
            if (paramsName == null)
            {
                MessageBox.Show("Не выбрана папка для размещения фалов.");
                return;
            }

            // получаем получаем номер типа карточки документа по имени имав карточки
            int idTypeCard = classTypeCard.getIdByName(formFiles.comboBox1.Text.ToString());
            string pathSave = treeNode.FullPath;
            int idFolder = Convert.ToInt32(paramsName[1]);
            string fileName = formFiles.textBoxNameFile.Text;

            ClassTextStringMask classTextStringMask = new ClassTextStringMask();
            string[] fullFilePathes = ofd.FileNames;
            foreach (string fullFilePath in fullFilePathes)
            {
                // работаем с именем фалов
                string fName = classTextStringMask.DeterminationMask(fileName);
                if (fullFilePathes.GetLength(0) == 1)
                {
                    fName = fileName + "." + Path.GetExtension(fullFilePath);
                }
                else if (fName == null)
                {
                    fName = Path.GetFileName(fullFilePath);
                }
                else
                {
                    fName = fName + "." + Path.GetExtension(fullFilePath);
                }

                // создаем документ в программе
                int idFile = classFiles.Create(idFolder, idTypeCard, fName, fullFilePath, pathSave);
                if (idFile == 0)
                {
                    MessageBox.Show("Ошибка создания файла!");
                    return;
                }

                // Получаем все элементы полей карточки документа
                Control.ControlCollection controlCollection = formFiles.panel1.Controls;
                SendValueOfFields(controlCollection, idFile);
            }
            formFiles.Hide();
            formFiles.textBoxNameFile.Text = "";
            InitTreeView(treeView);
        }

        public void TreeFolderAddFolder(TreeView treeView)
        {
            TreeNode treeNode = treeView.SelectedNode;
            formTreeInput.Text = "Укажите имя папки";
            formTreeInput.buttonOk.Text = "Добавить";
            formTreeInput.Name = "formTreeFolderAddFolder";
            formTreeInput.ShowDialog();

            if (formTreeInput.textBox1.Text == "")
            {
                return;
            }

            if (treeNode == null)
            {
                MessageBox.Show("Не выбран узел дерева!");
                return;
            }

            string[] paramsName = treeNode.Name.Split('_');
            int id = classFolder.CreateFolder(Convert.ToInt32(paramsName[1]), formTreeInput.textBox1.Text);
            AddNode(treeView, paramsName[1], id.ToString(), formTreeInput.textBox1.Text, "folder");

            formTreeInput.textBox1.Text = "";
            formTreeInput.Name = "null";
            treeNode = null;
        }

        public void treeFolderDeleteFolder(TreeView treeView)
        {
            TreeNode treeNode = treeView.SelectedNode;

            DialogResult dialogResult = MessageBox.Show(
                "Вы точно хотите удалить \"" + treeNode.Text + "\"?",
                "Удаление",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning
            );
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            if (treeNode.Tag.ToString() != "folder")
            {
                MessageBox.Show("Удаляемый узел не является папкой!");
                return;
            }

            // Рекурсивно удаляем все файлы из леквидируемых каталогов
            RecursiveDeleteFiles(treeNode);
            // Удаляем папку с подкаталогами
            classFolder.DeleteFolder(GetIdNode(treeNode));
            treeView.SelectedNode.Remove();
        }

        /// <summary>
        /// Событие происходит при нажатии на пункт, контекстного меню дерева, "Переименовать папку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TreeFolderMoveFolder_click(object sender, EventArgs e)
        {
            // Задаем нужные надписи в диалоговом окне дерева
            formTree.Text = "Переместить папку";
            formTree.buttonOk.Text = "Переместить";
            //formTree.Name = "formTreeFolderMoveFolder";
            // получаем клон орсновного дерева
            TreeNode cloneNode = (TreeNode)objectTreeView.Nodes[0].Clone();
            formTree.treeView1.Nodes.Clear();
            // записываем клон в диалоговую форму
            formTree.treeView1.Nodes.Add(cloneNode);
            formTree.treeView1.Nodes[0].Expand();
            // запоминаем выделеный узел (переносимую папку)
            TreeNode folderNode = objectTreeView.SelectedNode;

            // Закрываем окно, если оно было открыто
            formTree.Hide();

            DialogResult dialogResult = formTree.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            // получаем из диалоговой формы выделенный узел (место перемещения)
            TreeNode moveToLocationNode = formTree.treeView1.SelectedNode;
            // Проверям куда переносим папку
            if (moveToLocationNode.Tag.ToString() != "folder")
            {
                MessageBox.Show("Перемещение возможно только в папку!");
                return;
            }

            int idFolder = Convert.ToInt32(folderNode.Name.Split('_')[1]);
            int idParent = Convert.ToInt32(moveToLocationNode.Name.Split('_')[1]);

            classFolder.MoveFolder(idFolder, idParent);

            TreeNode[] treeNodes = objectTreeView.Nodes.Find(moveToLocationNode.Name, true);
            treeNodes[0].Nodes.Add((TreeNode)folderNode.Clone());
            folderNode.Remove();

            //InitTreeView(objectTreeView);
        }

        /// <summary>
        /// Событие происходит при нажатии на пункт, контекстного меню дерева, "Переименовать папку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TreeFolderRenameFolder_click(object sender, EventArgs e)
        {
            TreeNode treeNode = objectTreeView.SelectedNode;

            formTreeInput.Text = "Переименовать папку";
            formTreeInput.buttonOk.Text = "Переименовать";
            formTreeInput.textBox1.Text = treeNode.Text;
            formTreeInput.ShowDialog();

            if (formTreeInput.textBox1.Text == "")
            {
                return;
            }

            if (treeNode == null)
            {
                MessageBox.Show("Не выбран узел дерева!");
                return;
            }

            int id = Convert.ToInt32(treeNode.Name.Split('_')[1]);
            classFolder.RenameFolder(id, formTreeInput.textBox1.Text);
            RenameNode(objectTreeView, treeNode.Name, formTreeInput.textBox1.Text);

            formTreeInput.textBox1.Text = "";
            formTreeInput.Name = "null";
            treeNode = null;
        }

        /// <summary>
        /// Вызывает модальное диалоговое окно дерева
        /// </summary>
        /// <param name="formName">Текст заголовка окна</param>
        /// <param name="buttonOkText">Текст кнопеи ok</param>
        /// <returns></returns>
        public TreeNode TreeViewDialog(string formName, string buttonOkText)
        {
            InitTreeView();
            formTree.Text = formName;
            formTree.buttonOk.Text = buttonOkText;
            formTree.ShowDialog();
            return formTree.treeView1.SelectedNode;
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="treeView">The tree view.</param>
        /// <param name="idParent">The identifier parent.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="type">The type.</param>
        private void AddNode(TreeView treeView, string idParent, string id, string text, string type)
        {
            TreeNode treeNode = new TreeNode
            {
                Text = text,
                Name = type + "_" + id,
                Tag = type
            };
            switch (type)
            {
                case "folder":
                    treeNode.ImageKey = "default_folder";
                    treeNode.SelectedImageKey = "default_folder";
                    //globalNode.ContextMenuStrip = contMenu.treeFolder;
                    break;

                case "file":
                    string imageKey = ClassImageList.addIconFile(classFiles.GetFileById(Convert.ToInt32(id)).path);
                    treeNode.ImageKey = imageKey;
                    treeNode.SelectedImageKey = imageKey;
                    // globalNode.ContextMenuStrip = contMenu.treeFiles;
                    break;

                default:
                    MessageBox.Show("Не известный тип узла: " + type);
                    break;
            }
            treeView.HideSelection = false;
            TreeNode[] treeNodes = treeView.Nodes.Find("folder_" + idParent, true);
            treeNodes[0].Nodes.Add(treeNode);
            treeNode = null;
        }

        /// <summary>
        /// Вызывает средства для копирования файла
        /// </summary>
        private void CopyFile()
        {
            Console.WriteLine("0");
            // полчаем узел копируемого файла
            TreeNode treeNodeCopyFile = objectTreeView.SelectedNode;
            // получаем родителя (папку) копируемого файла
            TreeNode treeNodeParentCopyFile = objectTreeView.SelectedNode.Parent;
            int idFile = Convert.ToInt32(treeNodeCopyFile.Name.Split('_')[1]);
            Console.WriteLine("1");

            DialogResult dialogResult = formTree.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                Console.WriteLine("2");
                return;
            }
            Console.WriteLine("3");
            // получаем копию узла файла
            TreeNode treeNodeFileClone = (TreeNode)objectTreeView.SelectedNode.Clone();
            TreeNode treeNodeFileLocation = formTree.treeView1.SelectedNode;
            int idNewFolder = Convert.ToInt32(treeNodeFileLocation.Name.Split('_')[1]);
            string newPath = treeNodeFileLocation.FullPath;
            TreeNode[] treeNodes = objectTreeView.Nodes.Find(treeNodeFileLocation.Name, true);
            treeNodes[0].Nodes.Add(treeNodeFileClone);
            classFiles.Copy(idFile, idNewFolder, newPath);
        }

        private void FormTreeButtonOk_click(object sender, EventArgs e)
        {
            switch (formTree.Name)
            {
                case "formTreeFileChangeLocation":
                    TreeNode treeNode = formTree.treeView1.SelectedNode;
                    formFiles.textBoxTreePath.Text = treeNode.FullPath;
                    break;

                default:
                    MessageBox.Show("Не правильная операция переноса или копирования!");
                    break;
            }
            formTree.Hide();
        }

        private void RecursiveAddNodeToData(TreeNode treeNode, int indexParent)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // отправляем узел в базу
                int id = classFolder.CreateFolder(indexParent, tn.Text);
                // задаем новый id папке, согласно базе
                tn.Name = "folder_" + id.ToString();
                RecursiveAddNodeToData(tn, id);
            }
        }

        private void RecursiveDeleteFileNodes(TreeNode treeNode)
        {
            try
            {
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    if (tn.Name.Contains("file"))
                    {
                        tn.Remove();
                    }

                    RecursiveDeleteFileNodes(tn);
                }
            }
            catch (System.NullReferenceException)
            {
            }
        }

        private void RecursiveDeleteFiles(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Name.Contains("file"))
                {
                    classFiles.Delete(GetIdNode(tn));
                }

                RecursiveDeleteFiles(tn);
            }
        }

        private void renameFile()
        {
            formTreeInput.textBox1.Text = objectTreeView.SelectedNode.Text;
            formTreeInput.Text = "Переименовать файл";
            formTreeInput.ShowDialog();
            string newName = formTreeInput.textBox1.Text;
            if (newName == "")
            {
                return;
            }

            objectTreeView.SelectedNode.Text = newName;
            int idFile = Convert.ToInt32(objectTreeView.SelectedNode.Name.Split('_')[1]);

            classFiles.Rename(idFile, newName);
        }

        private void RenameNode(TreeView treeView, string name, string text)
        {
            TreeNode[] treeNodes = treeView.Nodes.Find(name, true);
            treeNodes[0].Text = text;
        }

        /// <summary>
        /// Отправляет значение полей формы в карточку документа
        /// </summary>
        /// <param name="controlCollection"Коллекция полей формы></param>
        /// <param name="idFile">номер файла, которому принадлежит карточка</param>
        private void SendValueOfFields(Control.ControlCollection controlCollection, int idFile)
        {
            // идем по полуеным элементам полей карточки документа
            foreach (Control control in controlCollection)
            {
                Console.Write(control.GetType().ToString() + " ");
                Console.WriteLine(control.Name);
                string[] arrNameControl;
                int idCardProps = 0;
                string value = "";
                switch (control.GetType().ToString())
                {
                    case "System.Windows.Forms.TextBox":
                        // разбиваем имя элемента
                        arrNameControl = ((TextBox)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = ((TextBox)control).Text;
                        classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.NumericUpDown":
                        // разбиваем имя элемента
                        arrNameControl = ((NumericUpDown)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = ((NumericUpDown)control).Text;
                        classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.MaskedTextBox":
                        // разбиваем имя элемента
                        arrNameControl = ((MaskedTextBox)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = ((MaskedTextBox)control).Text;
                        classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.CheckBox":
                        // разбиваем имя элемента
                        arrNameControl = ((CheckBox)control).Name.Split('_');

                        idCardProps = Convert.ToInt32(arrNameControl[1]);
                        value = Convert.ToInt32(((CheckBox)control).Checked).ToString();
                        classCardPropsValue.createValue(idCardProps, value, idFile);
                        break;

                    case "System.Windows.Forms.Label":
                        // Пропускаем этот элемент
                        break;

                    default:
                        MessageBox.Show("Не известный тип элемента поля " + control.GetType().ToString());
                        break;
                }
            }
        }

        /// <summary>
        /// Подтягивает из базы информацию о файлах и выводит в дерево
        /// </summary>
        /// <param name="treeView"></param>
        private void TreeViewFile(TreeView treeView)
        {
            ClassFiles.FileCollection[] fileCollection = classFiles.selectAllFiles();
            foreach (ClassFiles.FileCollection file in fileCollection)
            {
                TreeNode treeNode = new TreeNode();
                string imageKey = "";//ClassImageList.addIconFile(classFiles.GetFileById(file.id).path);
                treeNode.Text = file.name;
                treeNode.Name = "file_" + file.id.ToString();
                treeNode.Tag = "file";
                treeNode.ImageKey = imageKey;
                treeNode.SelectedImageKey = imageKey;

                TreeNode[] tNode = treeView.Nodes.Find("folder_" + file.idFolder.ToString(), true);
                if (tNode.Length > 0)
                {
                    tNode[0].Nodes.Add(treeNode);
                }
            }
            objectTreeView = treeView;
        }

        /// <summary>
        /// Обеспечивает подгрузку информации о папках из базы и организации ее в дерево
        /// </summary>
        /// <param name="treeView"></param>
        private void TreeViewFolder(TreeView treeView)
        {
            ClassFolder.FolderCollection[] foldersCollection = classFolder.GetAllFolders(false);

            TreeNode treeNode = new TreeNode
            {
                Text = VitSettings.Properties.GeneralsSettings.Default.repositoryRootFolderName,
                Name = "folder_0",
                Tag = "folder",
                ImageKey = "root",
                SelectedImageKey = "root",
                StateImageKey = "root",
            };
            treeView.HideSelection = false;
            treeView.Nodes.Add(treeNode);
            treeNode = null;

            foreach (ClassFolder.FolderCollection folder in foldersCollection)
            {
                if (folder.parentId == 0)
                {
                    treeNode = new TreeNode
                    {
                        Text = folder.name,
                        Name = "folder_" + folder.id.ToString(),
                        Tag = "folder",
                        ImageKey = "default_folder",
                        SelectedImageKey = "default_folder",
                        StateImageKey = "default_folder",
                    };
                    treeView.Nodes[0].Nodes.Add(treeNode);
                }
                else
                {
                    treeNode = new TreeNode
                    {
                        Text = folder.name,
                        Name = "folder_" + folder.id.ToString(),
                        Tag = "folder",
                        ImageKey = "default_folder",
                        SelectedImageKey = "default_folder",
                        StateImageKey = "default_folder",
                        //ContextMenuStrip = contMenu.treeFolder
                    };
                    TreeNode[] tNode = treeView.Nodes.Find("folder_" + folder.parentId.ToString(), true);
                    if (tNode.Length > 0)
                    {
                        tNode[0].Nodes.Add(treeNode);
                    }
                }
                treeNode = null;
            }
            objectTreeView = treeView;
        }

        /// <summary>
        /// Struct contextMenuCollection
        /// </summary>
        public struct contextMenuCollection
        {
            /// <summary>
            /// The tree files
            /// </summary>
            public ContextMenuStrip treeFiles;

            /// <summary>
            /// The tree folder
            /// </summary>
            public ContextMenuStrip treeFolder;
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