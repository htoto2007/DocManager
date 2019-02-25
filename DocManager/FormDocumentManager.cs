using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitAccess;
using VitAccessGroup;
using VitDBConnect;
using VitFTP;
using VitListView;
using VitNotifyMessage;
using vitProgressStatus;
using VitSearcher;
using VitSendToProgram;
using VitSettings;
using VitTree;
using VitTypeCard;
using VitUserPositions;
using VitUsers;

namespace DocManager
{
    /// <summary>
    /// Основная форма программы
    /// </summary>
    public partial class FormDocumentManager : Form
    {
        private ClassAccess classAccess = new ClassAccess();
        private ClassSearcher classSearcher = new ClassSearcher();
        private ClassSendToProgram classSendToProgram = new ClassSendToProgram();
        private ClassSettings classSettings = new ClassSettings();
        private ClassTree classTree = new ClassTree();
        private bool enableScrean = true;
        private FormCreatTypeCard formCreatTypeCard = new FormCreatTypeCard();
        

        /// <summary>
        /// Хранит последний задействованый контрол
        /// </summary>
        private Control lastControl = null;

        /// <summary>
        /// Хранит в себе тип последнего пользовательского элемента, который вызвал контекстное меню
        /// </summary>
        private Type lastRequireContextMenu;

        /// <summary>
        /// Этот поток служит для удаление устареаших скриншотов
        /// </summary>
        private Thread threadDelete;

        /// <summary>
        /// Вь данном потоке создаются скриншоты области окна программы
        /// </summary>
        private Thread threadScrean;

        /// <summary>
        /// Класс для работы со сканером
        /// </summary>
        private VitTwain.Twain32 twain32 = new VitTwain.Twain32();

        /// <summary>
        /// Инициализация основной формы
        /// </summary>
        /// <param name="args"></param>
        public FormDocumentManager(string[] args)
        {
            InitializeComponent();
            Settings();
            if (enableScrean == true)
            {
                threadScrean = new Thread(Screan);
                threadScrean.Start();
                threadDelete = new Thread(deleteScrean);
                threadDelete.Start();
            }

            Version();
            Login();
            SendToProgram(args);
            //formVerifycationFiles.Show();

            InitControlsStyle();
            initAccessToForm();
            timerSearcher.Enabled = true;

            twain32.AcquireCompleted += new EventHandler(scanEvent);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VitUsers.FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.ShowDialog();
        }

        /// <summary>
        /// Событие кнопки по созданию филиала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddBranch_Click(object sender, EventArgs e)
        {
            //treeView1.BeginUpdate();
            classTree.AddBranch(treeView1);
            //treeView1.EndUpdate();
        }

        /// <summary>
        /// Событие кнопки прекращения сеанса пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            classUsers.logOut();
            Application.Restart();
        }

        /// <summary>
        /// Событие кнопки сканирования документа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonScan_Click(object sender, EventArgs e)
        {
            Enabled = false;
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            try
            {
                if (twain32.SelectSource())
                    twain32.Acquire();
                Enabled = true;
            }
            catch (VitTwain.TwainException)
            {
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.USER_ERROR, "Сканер не найден!");
                Enabled = true;
            }
            
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }

        private void buttonSettingsAccessGroup_Click(object sender, EventArgs e)
        {
            VitAccessGroup.FormAccessGroup formAccessGroup = new VitAccessGroup.FormAccessGroup();
            formAccessGroup.ShowDialog();
        }

        private void buttonSettingsDocumentCard_Click(object sender, EventArgs e)
        {
            FormTypeCard formTypeCard = new FormTypeCard();
            formTypeCard.ShowDialog();
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            FormUsers formUsers = new FormUsers();
            formUsers.ShowDialog();
        }

        /// <summary>
        /// Происходит при открытии контехтного меню в listview иди Treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripTreeView_Opened(object sender, EventArgs e)
        {
            if (contextMenuStripTreeView.SourceControl.GetType() == listView1.GetType())
            {
                listViewContextMenu();
            }
            if (contextMenuStripTreeView.SourceControl.GetType() == treeView1.GetType())
            {
                TreeViewContextMenu();
            }
            contextMenuStripTreeView.Update();
            lastRequireContextMenu = contextMenuStripTreeView.SourceControl.GetType();
        }

        /// <summary>
        /// удаляет не актуальные скрины экрана
        /// </summary>
        private void deleteScrean()
        {
            VitTelemetry.ClassScreenshot classScreenshot = new VitTelemetry.ClassScreenshot();
            classScreenshot.deleteScrean();
        }

        private void FormDocumentManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                threadDelete.Abort();
                threadScrean.Abort();
            }
            catch (System.NullReferenceException) { }
        }

        /// <summary>
        /// Определяем доступные элементы для данного пользователя
        /// </summary>
        private void initAccessToForm()
        {
            // выводим имя пользователя в форму
            ClassUsers classUsers = new ClassUsers();
            ClassUsers.UserColection userColection = classUsers.getThisUser();
            string firstName = userColection.firstName;
            string lastName = userColection.lastName;
            string middleName = userColection.middleName;
            textBoxUserName.Text =  lastName + " " + firstName + " " + middleName;
            ClassUserPositions classUserPositions = new ClassUserPositions();
            textBoxUserPosition.Text = classUserPositions.getInfoById(userColection.idPosition).name;

            // работаем с вывордом пользовательского меню.
            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            ClassAccessGroup.AccessGroupCollection accessGroupCollection = classAccessGroup.getInfoById(userColection.idAccessGroup);
            
            if (accessGroupCollection.rank != ClassAccessGroup.Ranks.ADMIN)
            {
                ToolStripMenuItemAdministration.Enabled = false;
                ToolStripMenuItemAdministration.Visible = false;
                buttonAdminSettings.Enabled = false;
                buttonAdminSettings.Visible = false;
                buttonAdminSettingsDocumentCard.Enabled = false;
                buttonAdminSettingsDocumentCard.Visible = false;
                buttonAdminUsers.Enabled = false;
                buttonAdminUsers.Visible = false;
            }
        }

        /// <summary>
        /// Производим инициализацию параметров элементов главного окна
        /// </summary>
        private void InitControlsStyle()
        {
            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            // проверяем по какому колличеству элементов кликают
            if (listView1.SelectedItems.Count == 1)
            {
                // если у элемента нет пути то выходим из функции
                if (listView1.SelectedItems[0].SubItems["path"] == null)
                {
                    Console.WriteLine("У элемента нет пути!");
                    return;
                }
                // формируем путь для скачивания файла на открытие
                string openFilePath = VitSettings.Properties.FTPSettings.Default.openFilePath + "\\" + Path.GetFileName(listView1.SelectedItems[0].SubItems["path"].Text);
                // формируем путь к удаленному файлу на скачивание
                string remoteFilePath = listView1.SelectedItems[0].SubItems["path"].Text;

                
                ClassUsers classUsers = new ClassUsers();
                ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                classFTP.SessionOpen();
                // проверяем наличие файла на удаленном сервере
                if (!classFTP.FileExist(remoteFilePath))
                {
                    Console.WriteLine("Файл '" + remoteFilePath + "' не найден на сервере.");
                    return;
                }
                if (classFTP.getFileType(remoteFilePath) != 1)
                {
                    Console.WriteLine("открываемый элемент не является файлом.");
                    return;
                }
                var res = classFTP.DownloadFile2(remoteFilePath, openFilePath);

                if(!res)
                {
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось получить файл с сервера! \n " + remoteFilePath);
                    return;
                }

                classFTP.sessionClose();
                // проверяем скачался ли файл
                if (!File.Exists(openFilePath))
                {
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось получить файл с сервера!");
                    return;
                }
                // запускаем файл
                Process.Start(VitSettings.Properties.FTPSettings.Default.openFilePath + "\\" + Path.GetFileName(remoteFilePath));
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listView1.SelectedItems;

            try
            {
                Process.Start(items[0].SubItems[4].Text);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Определяет актуальные пункты в зависимости от выделенного объекта в ListView
        /// </summary>
        private void listViewContextMenu()
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            if (listView1.SelectedItems.Count < 1) // если ничего не выделено
            {
                ToolStripMenuItemRequestOriginal.Enabled = false;
                ToolStripMenuItemAddFolder.Enabled = true;
                ToolStripMenuItemAddDocument.Enabled = false;
                ToolStripMenuItemSend.Enabled = false;
                ToolStripMenuItemDelete.Enabled = false;
                ToolStripMenuItemCopy.Enabled = false;
                ToolStripMenuItemMove.Enabled = false;
                ToolStripMenuItemRename.Enabled = false;
                ToolStripMenuItemSendToPrint.Enabled = false;
                toolStripMenuItemScanToThisFolder.Enabled = true;
            }
            else if (listView1.SelectedItems.Count == 1) // если выделен один объект
            {
                if (classFTP.getFileType(listView1.SelectedItems[0].SubItems["path"].Text) == 1) // если файл
                {
                    ToolStripMenuItemRequestOriginal.Enabled = true; 
                    ToolStripMenuItemAddFolder.Enabled = false;
                    ToolStripMenuItemAddDocument.Enabled = false;
                    ToolStripMenuItemSend.Enabled = true;
                    ToolStripMenuItemDelete.Enabled = true;
                    ToolStripMenuItemCopy.Enabled = true;
                    ToolStripMenuItemMove.Enabled = true;
                    ToolStripMenuItemRename.Enabled = true;
                    ToolStripMenuItemSendToPrint.Enabled = true;
                    toolStripMenuItemScanToThisFolder.Enabled = false;
                }
                else if (classFTP.getFileType(listView1.SelectedItems[0].SubItems["path"].Text) == 2) // если папка
                {
                    ToolStripMenuItemRequestOriginal.Enabled = false;
                    ToolStripMenuItemAddFolder.Enabled = true;
                    ToolStripMenuItemAddDocument.Enabled = true;
                    ToolStripMenuItemSend.Enabled = true;
                    ToolStripMenuItemDelete.Enabled = true;
                    ToolStripMenuItemCopy.Enabled = true;
                    ToolStripMenuItemMove.Enabled = true;
                    ToolStripMenuItemRename.Enabled = true;
                    ToolStripMenuItemSendToPrint.Enabled = false;
                }
            }
            else if (listView1.SelectedItems.Count > 1)
            {
                ToolStripMenuItemRequestOriginal.Enabled = false;
                ToolStripMenuItemAddFolder.Enabled = false;
                ToolStripMenuItemAddDocument.Enabled = false;
                ToolStripMenuItemSend.Enabled = true;
                ToolStripMenuItemDelete.Enabled = true;
                ToolStripMenuItemCopy.Enabled = true;
                ToolStripMenuItemMove.Enabled = true;
                ToolStripMenuItemRename.Enabled = false;
                ToolStripMenuItemSendToPrint.Enabled = false;
            }
            classFTP.sessionClose();
        }

        /// <summary>
        /// Определяет актуальные пункты в зависимости от выделенного объекта в ListView
        /// </summary>
        private void TreeViewContextMenu()
        {
            ClassUsers classUsers = new ClassUsers();
            ClassFTP classFTP = new ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
            classFTP.SessionOpen();
            if (treeView1.SelectedNode != null) // если выделен один объект
            {

                if (classFTP.getFileType(treeView1.SelectedNode.Name) == 1) // если файл
                {
                    ToolStripMenuItemRequestOriginal.Enabled = true;
                    ToolStripMenuItemAddFolder.Enabled = false;
                    ToolStripMenuItemAddDocument.Enabled = false;
                    ToolStripMenuItemSend.Enabled = true;
                    ToolStripMenuItemDelete.Enabled = true;
                    ToolStripMenuItemCopy.Enabled = true;
                    ToolStripMenuItemMove.Enabled = true;
                    ToolStripMenuItemRename.Enabled = true;
                    ToolStripMenuItemSendToPrint.Enabled = true;
                    toolStripMenuItemScanToThisFolder.Enabled = false;
                }
                else if (classFTP.getFileType(treeView1.SelectedNode.Name) == 2) // если папка
                {
                    ToolStripMenuItemRequestOriginal.Enabled = false;
                    ToolStripMenuItemAddFolder.Enabled = true;
                    ToolStripMenuItemAddDocument.Enabled = true;
                    ToolStripMenuItemSend.Enabled = true;
                    ToolStripMenuItemDelete.Enabled = true;
                    ToolStripMenuItemCopy.Enabled = true;
                    ToolStripMenuItemMove.Enabled = true;
                    ToolStripMenuItemRename.Enabled = true;
                    ToolStripMenuItemSendToPrint.Enabled = false;
                }
            }
            else
            {
                ToolStripMenuItemRequestOriginal.Enabled = false;
                ToolStripMenuItemAddFolder.Enabled = true;
                ToolStripMenuItemAddDocument.Enabled = false;
                ToolStripMenuItemSend.Enabled = false;
                ToolStripMenuItemDelete.Enabled = false;
                ToolStripMenuItemCopy.Enabled = false;
                ToolStripMenuItemMove.Enabled = false;
                ToolStripMenuItemRename.Enabled = false;
                ToolStripMenuItemSendToPrint.Enabled = false;
                toolStripMenuItemScanToThisFolder.Enabled = true;
            }
            classFTP.sessionClose();
        }

        private void Login()
        {
            ClassUsers classUsers = new ClassUsers();
            while (true)
            {
                int res = classUsers.Login();
                if (res == 0)
                {
                    MessageBox.Show("Не правильный логин или пароль!");
                }

                if (res == 1)
                {
                    break;
                }

                if (res == 2)
                {
                    Environment.Exit(0);
                }
            }
        }

        private void SaveScan(Image[] images)
        {
            MessageBox.Show(images.GetLength(0).ToString());
            if (images.GetLength(0) < 1)
            {
                return;
            }
        }

        private void scanEvent(object sender, EventArgs e)
        {
            Image[] images = new Image[twain32.ImageCount];
            for (int i = 0; i < twain32.ImageCount; i++)
            {
                images[i] = twain32.GetImage(i);
            }
            SaveScan(images);
        }

        private void Screan()
        {
            VitTelemetry.ClassScreenshot classScreenshot = new VitTelemetry.ClassScreenshot();
            classScreenshot.start(Size.Width, Size.Height, Location.X, Location.Y);
        }

        private void SendToProgram(string[] args)
        {
            classSendToProgram.createMenuItem();
            classSendToProgram.Definition(args);
            if (args.GetLength(0) > 0)
            {
                Application.Exit();
            }
        }

        private void Settings()
        {
            ToolStripMenuItemShowUserMenu.Checked = VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelUserMenu;
            panelUserMenu.Visible = VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelUserMenu;

            string repositoryPath = VitSettings.Properties.GeneralsSettings.Default.repositiryPayh;
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }

            enableScrean = VitSettings.Properties.DevSettings.Default.screanShotsEnable;
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private async void ToolStripMenuItemAddDocumentWithCard_Click(object sender, EventArgs e)
        {
            await classTree.AddFileNodeWithCardAsync(treeView1);
        }

        private void ToolStripMenuItemAddFolder_Click(object sender, EventArgs e)
        {
            if(lastControl.GetType() == typeof(TreeView))
                classTree.addNodeFolder(treeView1);
            else if (lastControl.GetType() == typeof(ListView))
            {
                ClassLisView classLisView = new ClassLisView();
            }
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (lastControl.GetType() == typeof(TreeView))
            {
                ClassUsers classUsers = new ClassUsers();
                classTree.copy(treeView1, classUsers.getThisUser().login, classUsers.getThisUser().password);
            }else if (lastControl.GetType() == typeof(ListView))
            {
                ClassLisView classLisView = new ClassLisView();
                
            }
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (lastControl.GetType() == typeof(TreeView))
            {
                classTree.DeleteNode(treeView1);
            }else if(lastControl.GetType() == typeof(ListView))
            {
                ClassLisView classLisView = new ClassLisView();

                ListViewItem[] listViewItems = classLisView.deleteFiles(listView1);
                if (listViewItems == null)
                {
                    Console.WriteLine("listViewItems " + listViewItems);
                    return;
                }

                foreach (ListViewItem listViewItem in listViewItems)
                {
                    TreeNode[] treeNodes = treeView1.Nodes.Find(listViewItem.SubItems["path"].Text, true);
                    if (treeNodes.GetLength(0) > 0)
                        treeNodes[0].Remove();
                }
            }
            
        }

        private void ToolStripMenuItemMove_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            classTree.Move(treeView1, classUsers.getThisUser().login, classUsers.getThisUser().password);
        }

        private void ToolStripMenuItemRename_Click(object sender, EventArgs e)
        {
            classTree.rename(treeView1);
        }

        private void ToolStripMenuItemSendToDesctop_Click(object sender, EventArgs e)
        {
            if(lastControl.GetType() == typeof(TreeView))
            {
                classTree.sendToDesctop(treeView1);
            }
        }

        private void ToolStripMenuItemSendToEmail_Click(object sender, EventArgs e)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.INFORMATION, "В демо версии эта функция недоступна.");
        }

        private void ToolStripMenuItemSendToFolder_Click(object sender, EventArgs e)
        {
            if (lastControl.GetType() == typeof(TreeView))
            {
                classTree.sendToAnyFolder(treeView1);
            }
        }

        private void ToolStripMenuItemSendToPrint_Click(object sender, EventArgs e)
        {
            if (lastControl.GetType() == typeof(TreeView))
            {
                classTree.sendToPrint(treeView1);
            }
            if (lastControl.GetType() == typeof(ListView))
            {
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Отправка файла из списка на печать недоступна.");
            }
        }

        private void ToolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }

        private void ToolStripMenuItemSettingsAccessGroup_Click(object sender, EventArgs e)
        {
            //VitAccessGroup.ClassAccessGroup classAccessGroup = new VitAccessGroup.ClassAccessGroup();
            VitAccessGroup.FormAccessGroup formAccessGroup = new VitAccessGroup.FormAccessGroup();
            formAccessGroup.ShowDialog();
        }

        private void ToolStripMenuItemSettingsConnectToDataBase_Click(object sender, EventArgs e)
        {
            FormDBConnect formDB = new FormDBConnect();
            formDB.ShowDialog();
        }

        private void ToolStripMenuItemSettingsDocumentCard_Click(object sender, EventArgs e)
        {
            formCreatTypeCard.Show();
        }

        private void ToolStripMenuItemUserMenu_CheckedChanged(object sender, EventArgs e)
        {
            panelUserMenu.Visible = ToolStripMenuItemShowUserMenu.Checked;
            VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelUserMenu = ToolStripMenuItemShowUserMenu.Checked;
            VitSettings.Properties.ControlsSettings.Default.Save();
            //flowLayoutPanelUserMenu.Visible = ToolStripMenuItemShowUserMenu.Checked;
        }

        private void ToolStripMenuItemUsers_Click(object sender, EventArgs e)
        {
            FormUsers formUsers = new FormUsers();
            formUsers.ShowDialog();
        }

        /// <summary>
        /// Добавляет файл в базу с пустой картой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToolStripMenuItemWithoutCard_ClickAsync(object sender, EventArgs e)
        {
            Enabled = false;
            await classTree.AddFileNodeWithoutCardAsync(treeView1);
            Enabled = true;
        }

        private async void treeView1_AfterExpandAsync(object sender, TreeViewEventArgs e)
        {
            //Enabled = false;
            //treeView1.BeginUpdate();
            classTree.PreLoadNodesAsync(e.Node);
            //treeView1.EndUpdate();
            //Enabled = true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Console.WriteLine(treeView1.SelectedNode.Name);
            ClassLisView classLisView = new ClassLisView();
            classLisView.FromTreeVuew(treeView1, listView1);
        }

        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                return;
            }
        }

        private void Version()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string programName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build);
            string displayableVersion = $"{version.Major + "." + (version.Build)}";
            Text = programName + " 1.0.1";
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isExist = File.Exists(VitSettings.Properties.GeneralsSettings.Default.programPath + "\\help\\AEархив.html");
            if(isExist == false)
            {
                ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
                classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Помощь отсутствует или повреждена!");
                return;
            }
            Process.Start(VitSettings.Properties.GeneralsSettings.Default.programPath + "\\help\\AEархив.html");
        }

        private void ToolStripMenuItemSelectAll_Click(object sender, EventArgs e)
        {
            listView1.Focus();
            foreach (ListViewItem listViewItem in listView1.Items)
            {
                listViewItem.Selected = true;
            }
        }

        private void treeView1_MouseHover(object sender, EventArgs e)
        {
            lastControl = (Control)sender;
        }

        private void listView1_MouseHover(object sender, EventArgs e)
        {
            lastControl = (Control)sender;
        }

        private void vitButtonUpdateInfo_Click(object sender, EventArgs e)
        {
            //Enabled = false;
            ClassUsers classUsers = new ClassUsers();
            classTree.Init(treeView1, classUsers.getThisUser().login, classUsers.getThisUser().password);
            //Enabled = true;
        }

        private void ToolStripMenuItemConnectToData_Click(object sender, EventArgs e)
        {
            FormDBConnect formDBConnect = new FormDBConnect();
            formDBConnect.ShowDialog();
        }

        private void FormDocumentManager_Load(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            //treeView1.BeginUpdate();
            classTree.Init(treeView1, classUsers.getThisUser().login, classUsers.getThisUser().password);
            //treeView1.EndUpdate();
            //treeView1.Update();
        }

        private async void textBoxSearch_KeyUpAsync(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ClassSearcher.fileIdCollection[] idFileCollections = null;
                idFileCollections = classSearcher.Start(textBoxSearch.Text);
                VitFiles.ClassFiles classFiles = new VitFiles.ClassFiles();
                VitFiles.ClassFiles.FileCollection[] fileCollections = new VitFiles.ClassFiles.FileCollection[idFileCollections.GetLength(0)];
                for (int i = 0; i < idFileCollections.GetLength(0); i++)
                {
                    Console.WriteLine(idFileCollections[i].id);
                    fileCollections[i] = classFiles.getInfoById(idFileCollections[i].id);
                }
                ClassLisView classLisView = new ClassLisView();
                classLisView.FromSearch(fileCollections, listView1);
            }
        }

        private void FormDocumentManager_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.F) textBoxSearch.Focus();
            if (e.Shift && e.KeyCode == Keys.S) treeView1.Sort();
        }

        private void FormDocumentManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                ClassUsers classUsers = new ClassUsers();
                classTree.Init(treeView1, classUsers.getThisUser().login, classUsers.getThisUser().password);
            }
        }

        private void vitButtonTreeSort_Click(object sender, EventArgs e)
        {
            treeView1.Sort();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //listView1.Columns[e.Column];
            //listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
            for (int i = 0; i < listView1.Columns.Count; i++) {
                listView1.Columns[i].Text = listView1.Columns[i].Text.Trim(char.ConvertFromUtf32(0x2193).ToCharArray(0, 1));
                listView1.Columns[i].Text = listView1.Columns[i].Text.Trim(char.ConvertFromUtf32(0x2191).ToCharArray(0, 1));
            }
            if (listView1.Sorting != SortOrder.Ascending)
            {
                listView1.Sorting = SortOrder.Ascending;
                listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + char.ConvertFromUtf32(0x2193);
            }
            else
            {
                listView1.Sorting = SortOrder.Descending;
                listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + char.ConvertFromUtf32(0x2191);
            }
            listView1.Sort();
        }

        // Implements the manual sorting of items by columns.
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        private void toolStripMenuItemScanToThisFolder_Click(object sender, EventArgs e)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.INFORMATION, "В демо версии эта функция недоступна.");
        }

        private void карточкаДокументаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}