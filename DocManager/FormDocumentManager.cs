using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VitAccess;
using VitAccessGroup;
using VitCardPropsValue;
using VitDBConnect;
using VitListView;
using VitNotifyMessage;
using VitSearcher;
using VitSendToProgram;
using VitSettings;
using VitTree;
using VitTypeCard;
using VitUsers;
using VitVerifycationFiles;

namespace DocManager
{
    public partial class FormDocumentManager : Form
    {
        private ClassAccess classAccess = new ClassAccess();
        private ClassSearcher classSearcher = new ClassSearcher();
        private ClassSendToProgram classSendToProgram = new ClassSendToProgram();
        private ClassSettings classSettings = new ClassSettings();
        private ClassTree classTree = new ClassTree();
        private bool enableScrean = true;
        private FormCreatTypeCard formCreatTypeCard = new FormCreatTypeCard();
        private FormDBConnect formDB = new FormDBConnect();
        private FormVerifycationFiles formVerifycationFiles = new FormVerifycationFiles();

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

        private void buttonAddBranch_Click(object sender, EventArgs e)
        {
            //classTree.AddBranch();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            classUsers.logOut();
            Application.Restart();
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            if (twain32.SelectSource())
            {
                twain32.Acquire();
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

        private void buttonSettingsConnectToDB_Click(object sender, EventArgs e)
        {
            formDB.Show();
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

        private void contextMenuStripTreeView_Opened(object sender, EventArgs e)
        {
            if (contextMenuStripTreeView.SourceControl.GetType() == listView1.GetType())
            {
                listView1ContextMenu();
            }
            if (contextMenuStripTreeView.SourceControl.GetType() == treeView1.GetType())
            {
                
            }
            contextMenuStripTreeView.Update();
            Console.WriteLine(contextMenuStripTreeView.SourceControl.GetType());
            lastRequireContextMenu = contextMenuStripTreeView.SourceControl.GetType();
        }

        private void deleteScrean()
        {
            VitTelemetry.ClassScreenshot classScreenshot = new VitTelemetry.ClassScreenshot();
            classScreenshot.deleteScrean();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Enabled = false;
            classTree.Init(treeView1);
            Enabled = true;
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

        private void initAccessToForm()
        {
            // выводим имя пользователя в форму
            ClassUsers classUsers = new ClassUsers();
            ClassUsers.UserColection userColection = classUsers.getThisUser();
            string firstName = userColection.firstName;
            string lastName = userColection.lastName;
            string middleName = userColection.middleName;
            textBoxUserName.Text =  lastName + " " + firstName + " " + middleName;

            // работем с вывордом пользовательского меню.
            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            ClassAccessGroup.AccessGroupCollection accessGroupCollection = classAccessGroup.getInfoById(userColection.idAccessGroup);
            if (accessGroupCollection.rank != ClassAccessGroup.Ranks.ADMIN)
            {
                flowLayoutPanelUserMenu.Visible = false;
                ToolStripMenuItemAdministration.Visible = false;
            }
        }

        private void InitControlsStyle()
        {
            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ClassNotifyMessage classNotifyMessage = new ClassNotifyMessage();
            if (listView1.SelectedItems.Count == 1)
            {
                if (listView1.SelectedItems[0].SubItems["path"] == null) return;
                string openFilePath = VitSettings.Properties.FTPSettings.Default.openFilePath + "\\" + Path.GetFileName(listView1.SelectedItems[0].SubItems["path"].Text);
                string remoteFilePath = listView1.SelectedItems[0].SubItems["path"].Text;
                ClassUsers classUsers = new ClassUsers();

                VitFTP.ClassFTP classFTP = new VitFTP.ClassFTP(classUsers.getThisUser().login, classUsers.getThisUser().password);
                if (classFTP.FileExist(remoteFilePath)) return;
                if (Path.GetExtension(remoteFilePath) == "") return;
                classFTP.DownloadFile(remoteFilePath, openFilePath);
                
                if (!File.Exists(openFilePath))
                {
                    classNotifyMessage.showDialog(ClassNotifyMessage.TypeMessage.SYSTEM_ERROR, "Не удалось получить файл с сервера!");
                    return;
                }
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

        private void listView1ContextMenu()
        {
            if (listView1.SelectedItems.Count < 1)
            {
                ToolStripMenuItemRequestOriginal.Enabled = false;
                ToolStripMenuItemAddFolder.Enabled = false;
                ToolStripMenuItemAddDocument.Enabled = false;
                ToolStripMenuItemSend.Enabled = false;
                ToolStripMenuItemDelete.Enabled = false;
                ToolStripMenuItemCopy.Enabled = false;
                ToolStripMenuItemMove.Enabled = false;
                ToolStripMenuItemRename.Enabled = false;
            }
            else if (listView1.SelectedItems.Count == 1)
            {
                if (listView1.SelectedItems[0].SubItems["type"].Text == ClassTree.TypeNodeCollection.FILE)
                {
                    ToolStripMenuItemRequestOriginal.Enabled = true;
                    ToolStripMenuItemAddFolder.Enabled = false;
                    ToolStripMenuItemAddDocument.Enabled = false;
                    ToolStripMenuItemSend.Enabled = true;
                    ToolStripMenuItemDelete.Enabled = true;
                    ToolStripMenuItemCopy.Enabled = true;
                    ToolStripMenuItemMove.Enabled = true;
                    ToolStripMenuItemRename.Enabled = true;
                }
                else if (listView1.SelectedItems[0].SubItems["type"].Text == ClassTree.TypeNodeCollection.FOLDER)
                {
                    ToolStripMenuItemRequestOriginal.Enabled = false;
                    ToolStripMenuItemAddFolder.Enabled = true;
                    ToolStripMenuItemAddDocument.Enabled = true;
                    ToolStripMenuItemSend.Enabled = true;
                    ToolStripMenuItemDelete.Enabled = true;
                    ToolStripMenuItemCopy.Enabled = true;
                    ToolStripMenuItemMove.Enabled = true;
                    ToolStripMenuItemRename.Enabled = true;
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
            }
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
            ToolStripMenuItemUserMenu.Checked = VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelUserMenu;
            panelUserMenu.Visible = VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelUserMenu;

            string repositoryPath = VitSettings.Properties.GeneralsSettings.Default.repositiryPayh;
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }

            enableScrean = VitSettings.Properties.DevSettings.Default.screanShotsEnable;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                listView1.Items.Clear();
            }

            timerSearcher.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            return;
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void ToolStripMenuItemAddDocumentWithCard_Click(object sender, EventArgs e)
        {
            classTree.AddFileNodeWithCard(treeView1);
        }

        private void ToolStripMenuItemAddFolder_Click(object sender, EventArgs e)
        {
            classTree.addNodeFolder(treeView1);
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            classTree.copy(treeView1);
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
                    TreeNode[] treeNodes = treeView1.Nodes.Find("/" + listViewItem.SubItems["path"].Text, true);
                    Console.WriteLine("find " + treeNodes.GetLength(0) + " files");
                    if (treeNodes.GetLength(0) > 0)
                    {
                        treeNodes[0].Remove();
                    }
                }
            }
            
        }

        private void ToolStripMenuItemMove_Click(object sender, EventArgs e)
        {
            classTree.move(treeView1);
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
            formDB.Show();
        }

        private void ToolStripMenuItemSettingsDocumentCard_Click(object sender, EventArgs e)
        {
            formCreatTypeCard.Show();
        }

        private void ToolStripMenuItemUserMenu_CheckedChanged(object sender, EventArgs e)
        {
            panelUserMenu.Visible = ToolStripMenuItemUserMenu.Checked;
            VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelUserMenu = ToolStripMenuItemUserMenu.Checked;
            VitSettings.Properties.ControlsSettings.Default.Save();
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
        private void ToolStripMenuItemWithoutCard_ClickAsync(object sender, EventArgs e)
        {
            classTree.AddFileNodeWithoutCard(treeView1);
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            classTree.preLoadNodes(e.Node);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //classTree.preLoadNodes(treeView1);
            Console.WriteLine(treeView1.SelectedNode.Name);
            VitListView.ClassLisView classLisView = new VitListView.ClassLisView();
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
            foreach(ListViewItem listViewItem in listView1.Items)
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

        private void ToolStripMenuItemDocumentCard_Click(object sender, EventArgs e)
        {
            MessageBox.Show(treeView1.SelectedNode.Name);
            FormEditFileCard formEditFileCard = new FormEditFileCard(treeView1.SelectedNode.Name);
            formEditFileCard.ShowDialog();
        }
    }
}