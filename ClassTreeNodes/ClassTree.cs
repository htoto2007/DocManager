using System;
using System.IO;
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
        private readonly ClassRelationFolders classRelationFolders = new ClassRelationFolders();
        private ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
        private ClassFiles classFiles = new ClassFiles();
        private ClassFolder classFolder = new ClassFolder();
        private ClassTypeCard classTypeCard = new ClassTypeCard();
        private contextMenuCollection contMenu;

        /// <summary>
        /// класс формы загрузки и карточки фала
        /// </summary>
        private FormFiles formFiles = new FormFiles();

        /// <summary>
        /// диалоговая форма дерева
        /// </summary>
        private FormTree formTree = new FormTree();

        /// <summary>
        /// сласс формы текстового ввода
        /// </summary>
        private FormTreeInput formTreeInput = new FormTreeInput();

        /// <summary>
        /// Служит буфером для обмена узлами между функциями
        /// </summary>
        private TreeNode globalNode;

        /// <summary>
        /// Буфер для передачи объекта дерева между функциями
        /// </summary>
        private TreeView objectTreeView;

        /// <exclude />
        public ClassTree()
        {
            contMenu = ContextShow();
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
        /// Contexts the show.
        /// </summary>
        /// <returns>contextMenuCollection.</returns>
        public contextMenuCollection ContextShow()
        {
            ToolStripMenuItem treeFolderAddFolder = new ToolStripMenuItem();
            ToolStripMenuItem treeFolderDeleteFolder = new ToolStripMenuItem();
            ToolStripMenuItem treeFolderRenameFolder = new ToolStripMenuItem();
            ToolStripMenuItem treeFolderMoveFolder = new ToolStripMenuItem();
            ToolStripMenuItem treeFolderCopyFolder = new ToolStripMenuItem();
            ToolStripMenuItem treeFolderAddDocument = new ToolStripMenuItem();

            treeFolderAddFolder.Text = "Добавить папку";
            treeFolderAddFolder.Image = global::VitIcons.Properties.Resource1.folder_plus;
            treeFolderAddFolder.Click += new EventHandler(TreeFolderAddFolder_click);

            treeFolderDeleteFolder.Text = "Удалть папку";
            treeFolderDeleteFolder.Image = global::VitIcons.Properties.Resource1.trash_alt;
            treeFolderDeleteFolder.Click += new EventHandler(treeFolderDeleteFolder_click);

            treeFolderRenameFolder.Text = "Переименовать";
            treeFolderRenameFolder.Image = global::VitIcons.Properties.Resource1.italic;
            treeFolderRenameFolder.Click += new EventHandler(TreeFolderRenameFolder_click);

            treeFolderMoveFolder.Text = "Переместить";
            treeFolderMoveFolder.Image = global::VitIcons.Properties.Resource1.arrow_alt_circle_right;
            treeFolderMoveFolder.Click += new EventHandler(TreeFolderMoveFolder_click);

            treeFolderCopyFolder.Text = "Копировать";
            treeFolderCopyFolder.Image = global::VitIcons.Properties.Resource1.copy;
            treeFolderCopyFolder.Click += new EventHandler(TreeFolderCopyFolder_click);

            treeFolderAddDocument.Text = "Добавить документ";
            treeFolderAddDocument.Image = global::VitIcons.Properties.Resource1.file_medical;
            treeFolderAddDocument.Click += new EventHandler(TreeFolderAddDocument_click);

            contMenu.treeFolder = new ContextMenuStrip();

            contMenu.treeFolder.Items.AddRange(new ToolStripMenuItem[] {
                treeFolderAddFolder,
                treeFolderDeleteFolder,
                treeFolderRenameFolder,
                treeFolderMoveFolder,
                treeFolderCopyFolder,
                treeFolderAddDocument
            });

            ToolStripMenuItem treeFilesDeleteFile = new ToolStripMenuItem();
            ToolStripMenuItem treeFilesDocumentCard = new ToolStripMenuItem();
            ToolStripMenuItem treeFilesMove = new ToolStripMenuItem();
            ToolStripMenuItem treeFilesCopy = new ToolStripMenuItem();
            ToolStripMenuItem treeFilesRename = new ToolStripMenuItem();

            treeFilesDeleteFile.Text = "Удалить документ";
            treeFilesDeleteFile.ShortcutKeys = Keys.Delete;
            treeFilesDeleteFile.ShowShortcutKeys = true;
            treeFilesDeleteFile.Image = global::VitIcons.Properties.Resource1.trash_alt;
            treeFilesDeleteFile.Click += new EventHandler(TreeFilesDeleteFile_click);

            treeFilesDocumentCard.Text = "Карточка документа";
            treeFilesDocumentCard.Image = global::VitIcons.Properties.Resource1.list_alt;
            treeFilesDocumentCard.Click += new EventHandler(TreeFilesDocumentCard_click);

            treeFilesMove.Text = "Переместить документ";
            treeFilesMove.Image = global::VitIcons.Properties.Resource1.arrow_alt_circle_right;
            treeFilesMove.Click += new EventHandler(TreeFilesMoveFile_click);

            treeFilesCopy.Text = "Копировать документа";
            treeFilesCopy.Image = global::VitIcons.Properties.Resource1.copy;
            treeFilesCopy.Click += new EventHandler(TreeFilesCopyFile_click);

            treeFilesRename.Text = "Переименовать";
            treeFilesRename.Image = VitIcons.Properties.Resource1.italic;
            treeFilesRename.Click += new EventHandler(treeFilesRename_Click);

            contMenu.treeFiles = new ContextMenuStrip();

            contMenu.treeFiles.Items.AddRange(new ToolStripMenuItem[] {
                treeFilesDeleteFile,
                treeFilesDocumentCard,
                treeFilesMove,
                treeFilesCopy,
                treeFilesRename
            });

            return contMenu;
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
        public TreeView InitTreeView(TreeView treeView)
        {
            treeView.Nodes.Clear();
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            treeView.ImageList = formCompanents.imageListColor;
            TreeViewFolder(treeView);
            TreeViewFile(treeView);
            treeView.Nodes[0].Expand();
            globalNode = (TreeNode)objectTreeView.Nodes[0].Clone();

            formTree.treeView1.Nodes.Clear();
            formTree.treeView1.Nodes.Insert(0, globalNode);
            return objectTreeView;
        }

        public TreeView InitTreeViewThread(TreeView treeView)
        {
            //treeView.Invoke((MethodInvoker)delegate
            //{
            treeView.Nodes.Clear();
            VitIcons.FormCompanents formCompanents = new VitIcons.FormCompanents();
            treeView.ImageList = formCompanents.imageListColor;
            TreeViewFolder(treeView);
            TreeViewFile(treeView);
            treeView.Nodes[0].Expand();
            //});
            globalNode = (TreeNode)objectTreeView.Nodes[0].Clone();
            formTree.treeView1.Nodes.Clear();
            formTree.treeView1.Nodes.Insert(0, globalNode);
            return objectTreeView;
        }

        /// <summary>
        /// Инициализирует и выводит дерево
        /// </summary>
        /// <returns>результат инициализации</returns>
        public TreeView InitTreeView()
        {
            TreeView treeView = formTree.treeView1;
            return InitTreeView(treeView);
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

        /// <summary>
        /// Handles the click event of the TreeFilesDeleteFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void TreeFilesDeleteFile_click(object sender, EventArgs e)
        {
            globalNode = objectTreeView.SelectedNode;

            DialogResult dialogResult = MessageBox.Show(
                "Вы точно хотите удалить \"" + globalNode.Text + "\"?",
                "Удаление",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning
            );
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            string[] paramsName = globalNode.Name.Split('_');
            classFiles.Delete(Convert.ToInt32(paramsName[1]));

            objectTreeView = InitTreeView(objectTreeView);
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
            globalNode = objectTreeView.SelectedNode;
            formTree.ShowDialog();

            DialogResult dialogResult = MessageBox.Show(
                "Вы точно хотите переместить \"" + globalNode.Text + "\" в " + objectTreeView.SelectedNode.FullPath,
                "Удаление",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning
            );

            if (dialogResult == DialogResult.Yes)
            {
                globalNode = objectTreeView.SelectedNode;
                int idFile = Convert.ToInt32(globalNode.Name.Split('_')[1]);
                int idFolder = Convert.ToInt32(formTree.treeView1.SelectedNode.Name.Split('_')[1]);
                string newPath = formTree.treeView1.SelectedNode.FullPath;
                classFiles.Move(idFile, idFolder, newPath);
                InitTreeView(objectTreeView);
            }
        }

        /// <summary>
        /// Событие происходит при нажатии на пункт, контекстного меню дерева, "Создать документ"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TreeFolderAddDocument_click(object sender, EventArgs e)
        {
            globalNode = objectTreeView.SelectedNode;
            formFiles.Text = "Укажите имя файла";
            formFiles.textBoxTreePath.Text = globalNode.FullPath;
            formFiles.ShowDialog();

            // проверяем введено ли новое имя файла
            if (formFiles.textBoxNameFile.Text == "")
            {
                //MessageBox.Show("Имя файла не может быть пустым");
                //return;
            }

            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true
            };
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // разбираем имя узла, в который будем помещать документ
            string[] paramsName = globalNode.Name.Split('_');

            // получаем получаем номер типа карточки документа по имени имав карточки
            int idTypeCard = classTypeCard.getIdByName(formFiles.comboBox1.Text.ToString());
            string pathSave = globalNode.FullPath;
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
                sendValueOfFields(controlCollection, idFile);
            }
            formFiles.Hide();
            formFiles.textBoxNameFile.Text = "";
            InitTreeView(objectTreeView);
        }

        /// <summary>
        /// Handles the click event of the TreeFolderAddFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void TreeFolderAddFolder_click(object sender, EventArgs e)
        {
            globalNode = objectTreeView.SelectedNode;
            formTreeInput.Text = "Укажите имя папки";
            formTreeInput.buttonOk.Text = "Добавить";
            formTreeInput.Name = "formTreeFolderAddFolder";
            formTreeInput.ShowDialog();

            if (formTreeInput.textBox1.Text == "")
            {
                return;
            }

            if (globalNode == null)
            {
                MessageBox.Show("Не выбран узел дерева!");
                return;
            }

            string[] paramsName = globalNode.Name.Split('_');
            int id = classFolder.CreateFolder(Convert.ToInt32(paramsName[1]), formTreeInput.textBox1.Text);
            AddNode(objectTreeView, paramsName[1], id.ToString(), formTreeInput.textBox1.Text, "folder");

            formTreeInput.textBox1.Text = "";
            formTreeInput.Name = "null";
            globalNode = null;
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
            globalNode = objectTreeView.SelectedNode;

            formTreeInput.Text = "Переименовать папку";
            formTreeInput.buttonOk.Text = "Переименовать";
            formTreeInput.textBox1.Text = globalNode.Text;
            formTreeInput.ShowDialog();

            if (formTreeInput.textBox1.Text == "")
            {
                return;
            }

            if (globalNode == null)
            {
                MessageBox.Show("Не выбран узел дерева!");
                return;
            }

            int id = Convert.ToInt32(globalNode.Name.Split('_')[1]);
            classFolder.RenameFolder(id, formTreeInput.textBox1.Text);
            RenameNode(objectTreeView, globalNode.Name, formTreeInput.textBox1.Text);

            formTreeInput.textBox1.Text = "";
            formTreeInput.Name = "null";
            globalNode = null;
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
            globalNode = new TreeNode
            {
                Text = text,
                Name = type + "_" + id,
                Tag = type
            };
            switch (type)
            {
                case "folder":
                    globalNode.ImageKey = "icons8-folder-48.png";
                    globalNode.SelectedImageKey = "icons8-folder-48.png";
                    globalNode.ContextMenuStrip = contMenu.treeFolder;
                    break;

                case "file":
                    globalNode.ImageKey = "icons8-document-48.png";
                    globalNode.SelectedImageKey = "icons8-document-48.png";
                    globalNode.ContextMenuStrip = contMenu.treeFiles;
                    break;

                default:
                    MessageBox.Show("Не известный тип узла: " + type);
                    break;
            }
            treeView.HideSelection = false;
            TreeNode[] treeNodes = treeView.Nodes.Find("folder_" + idParent, true);
            treeNodes[0].Nodes.Add(globalNode);
            globalNode = null;
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

        /// <summary>
        /// Запускает средства для копирования директории
        /// </summary>
        private void CopyFolder()
        {
            // получаем копируемую папку
            TreeNode treeNodeFolder = objectTreeView.SelectedNode;
            // вызываем диалоговую форму с деревом
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

        private void FormTreeButtonOk_click(object sender, EventArgs e)
        {
            switch (formTree.Name)
            {
                case "formTreeFileChangeLocation":
                    globalNode = formTree.treeView1.SelectedNode;
                    formFiles.textBoxTreePath.Text = globalNode.FullPath;
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
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Name.Contains("file"))
                {
                    tn.Remove();
                }

                RecursiveDeleteFileNodes(tn);
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

        private void sendValueOfFields(Control.ControlCollection controlCollection, int idFile)
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

        private void treeFilesRename_Click(object sender, EventArgs e)
        {
            renameFile();
        }

        private void TreeFolderCopyFolder_click(object sender, EventArgs e)
        {
            CopyFolder();
        }

        private void treeFolderDeleteFolder_click(object sender, EventArgs e)
        {
            globalNode = objectTreeView.SelectedNode;

            DialogResult dialogResult = MessageBox.Show(
                "Вы точно хотите удалить \"" + globalNode.Text + "\"?",
                "Удаление",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Warning
            );
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            if (globalNode.Tag.ToString() != "folder")
            {
                MessageBox.Show("Удаляемый узел не является папкой!");
                return;
            }

            // Рекурсивно удаляем все файлы из леквидируемых каталогов
            RecursiveDeleteFiles(globalNode);
            // Удаляем папку с подкаталогами
            classFolder.DeleteFolder(GetIdNode(globalNode));
            objectTreeView.SelectedNode.Remove();
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
                globalNode = new TreeNode
                {
                    Text = file.name,
                    Name = "file_" + file.id.ToString(),
                    Tag = "file",
                    ImageKey = "icons8-document-48.png",
                    SelectedImageKey = "icons8-document-48.png",
                    ContextMenuStrip = contMenu.treeFiles
                };
                TreeNode[] tNode = treeView.Nodes.Find("folder_" + file.idFolder.ToString(), true);
                if (tNode.Length > 0)
                {
                    tNode[0].Nodes.Add(globalNode);
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
            //treeView.Invoke((MethodInvoker)delegate
            //{
            ClassFolder.FoldersCollection[] foldersCollection = classFolder.GetAllFolders(false);

            globalNode = new TreeNode
            {
                Text = "r",
                Name = "folder_0",
                Tag = "folder",
                ImageKey = "icons8-tree-structure-40.png",
                SelectedImageKey = "icons8-tree-structure-40.png",
                StateImageKey = "icons8-tree-structure-40.png",
                ContextMenuStrip = contMenu.treeFolder,
            };
            treeView.HideSelection = false;
            treeView.Nodes.Add(globalNode);
            globalNode = null;

            foreach (ClassFolder.FoldersCollection folder in foldersCollection)
            {
                if (folder.parentId == 0)
                {
                    globalNode = new TreeNode
                    {
                        Text = folder.name,
                        Name = "folder_" + folder.id.ToString(),
                        Tag = "folder",
                        ImageKey = "icons8-folder-48.png",
                        SelectedImageKey = "icons8-folder-48.png",
                        StateImageKey = "icons8-folder-48.png",
                        ContextMenuStrip = contMenu.treeFolder
                    };
                    treeView.Nodes[0].Nodes.Add(globalNode);
                }
                else
                {
                    globalNode = new TreeNode
                    {
                        Text = folder.name,
                        Name = "folder_" + folder.id.ToString(),
                        Tag = "folder",
                        ImageKey = "icons8-folder-48.png",
                        SelectedImageKey = "icons8-folder-48.png",
                        StateImageKey = "icons8-folder-48.png",
                        ContextMenuStrip = contMenu.treeFolder
                    };
                    TreeNode[] tNode = treeView.Nodes.Find("folder_" + folder.parentId.ToString(), true);
                    if (tNode.Length > 0)
                    {
                        tNode[0].Nodes.Add(globalNode);
                    }
                }
                globalNode = null;
            }
            objectTreeView = treeView;
            //});
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
}