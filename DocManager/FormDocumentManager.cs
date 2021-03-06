﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VitAccess;
using VitAccessGroup;
using VitDBConnect;
using VitFiles;
using VitFolder;
using VitFTP;
using VitMail;
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
            ClassFTP classFTP = new ClassFTP
            {
                FTP_SERVER = "192.168.0.4",
                FTP_PORT = 21,
                FTP_USER = "user",
                FTP_PASSWORD = "123"
            };
            //Console.WriteLine("ftp: " + classFTP.connectToServer());
            //classFTP.getDirectoryesAndFiles();

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
            classTree.AddBranch();
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

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            ClassUsers classUsers = new ClassUsers();
            classUsers.showDialog();
        }

        private void contextMenuStripTreeView_Opened(object sender, EventArgs e)
        {
            if (contextMenuStripTreeView.SourceControl.GetType() == listView1.GetType())
            {
                listView1ContextMenu();
            }
            if (contextMenuStripTreeView.SourceControl.GetType() == treeView1.GetType())
            {
                treeView1ContextMenu();
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
            classTree.InitTreeView(treeView1);
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
            ClassUsers classUsers = new ClassUsers();
            ClassUsers.UserColection userColection = classUsers.getThisUser();
            string firstName = userColection.firstName;
            string lastName = userColection.lastName;
            string middleName = userColection.middleName;
            labelUserName.Text = firstName + " " + lastName + " " + middleName;

            ClassAccessGroup classAccessGroup = new ClassAccessGroup();
            ClassAccessGroup.AccessGroupCollection accessGroupCollection = classAccessGroup.getInfoById(userColection.idAccessGroup);
            if (accessGroupCollection.rank != ClassAccessGroup.Ranks.ADMIN)
            {
                flowLayoutPanelAdmin.Visible = false;
                ToolStripMenuItemAdministration.Visible = false;
            }
        }

        private void InitControlsStyle()
        {
            buttonScan.BackgroundImage = VitIcons.Properties.ResourceColorImage.icons8_scanner_40;
            buttonAddBranch.BackgroundImage = VitIcons.Properties.ResourceColorImage.icons8_plus_48;
            ToolStripMenuItemAbout.Image = VitIcons.Properties.ResourceColorImage.icons8_about_48;
            ToolStripMenuItemUserMenu.Image = VitIcons.Properties.ResourceColorImage.icons8_user_avatar_48;
            ToolStripMenuItemUserMenu.Image = VitIcons.Properties.ResourceColorImage.icons8_user_menu_male_48;
            ToolStripMenuItemSettingsConnectToDataBase.Image = VitIcons.Properties.ResourceColorImage.icons8_database_administrator_64;
            ToolStripMenuItemSettingsAccessGroup.Image = VitIcons.Properties.ResourceColorImage.icons8_password_48;
            ToolStripMenuItemUsers.Image = VitIcons.Properties.ResourceColorImage.icons8_conference_48;
            ToolStripMenuItemSettings.Image = VitIcons.Properties.ResourceColorImage.icons8_services_48;
            ToolStripMenuItemSettingsDocumentCard.Image = VitIcons.Properties.ResourceColorImage.icons8_red_card_40;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                foreach (ListViewItem listViewItem in listView1.SelectedItems)
                {
                    //Console.WriteLine(listViewItem.SubItems["path"].Text);
                    Process.Start(listViewItem.SubItems["path"].Text);
                }
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

            TreeNode treeNode = classTree.TreeViewDialog("Выберите папку", "Выбрать");
            ClassFiles classFiles = new ClassFiles();
            int idFolder = Convert.ToInt32(treeNode.Name.Split('_')[1]);
            ClassTypeCard classTypeCard = new ClassTypeCard();
            int idCard = classTypeCard.getIdByName(ClassTypeCard.EMPTY_CARD);
            string pathSave = treeNode.FullPath;
            string tmpPath = classSettings.GetProperties().generalsSttings.programPath + @"\tmp";
            Directory.CreateDirectory(tmpPath);

            int iterator = 0;
            foreach (Image image in images)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Size = new System.Drawing.Size(800, 600),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = image
                };

                if (pictureBox.Image == null)
                {
                    pictureBox.Dispose();
                    continue;
                }

                string fileName = "image_" + DateTime.UtcNow.ToString().Replace(":", "-") + "_" + iterator + ".jpg";
                pictureBox.Image.Save(tmpPath + @"\" + fileName);
                classFiles.Create(idFolder, idCard, fileName, tmpPath + @"\" + fileName, pathSave);
                classTree.InitTreeView(treeView1);

                iterator++;
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
            Console.WriteLine("timer start");
            timerSearcher.Enabled = false;

            ClassFiles.FileCollection[] fileCollections = null;
            fileCollections = classSearcher.start(textBox1.Text);

            if (fileCollections == null)
            {
                Console.WriteLine("fileCollection == null");
                Console.WriteLine("timer return");
                listView1.Items.Clear();
                return;
            }

            VitListView.ClassLisView classLisView = new VitListView.ClassLisView();
            classLisView.FromSearch(fileCollections, listView1);

            return;
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void ToolStripMenuItemAddDocumentWithCard_Click(object sender, EventArgs e)
        {
            classTree.TreeFolderAddDocument(treeView1);
        }

        private void ToolStripMenuItemAddFolder_Click(object sender, EventArgs e)
        {
            classTree.TreeFolderAddFolder(treeView1);
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (lastRequireContextMenu == treeView1.GetType())
            {
                if (classTree.GetTypeNode(treeView1.SelectedNode) == ClassTree.TypeNodeCollection.FILE)
                {
                    ClassFiles classFiles = new ClassFiles();
                    int idFile = Convert.ToInt32(treeView1.SelectedNode.Name.Split('_')[1]);

                    FormTree formTree = new FormTree();
                    formTree.treeView1.ImageList = treeView1.ImageList;
                    classTree.TreeViewFolder(formTree.treeView1);
                    DialogResult dialogResult = formTree.ShowDialog();
                    if (dialogResult != DialogResult.OK)
                    {
                        return;
                    }

                    if (formTree.treeView1.SelectedNode == null)
                    {
                        return;
                    }

                    int idNewFolder = Convert.ToInt32(formTree.treeView1.SelectedNode.Name.Split('_')[1]);
                    ClassFolder classFolder = new ClassFolder();
                    ClassFolder.FolderCollection folderCollection = classFolder.GetFolderById(idNewFolder, true);
                    int id = classFiles.Copy(idFile, idNewFolder, folderCollection.pathWithoutRoot);

                    TreeNode treeNode = (TreeNode)treeView1.SelectedNode.Clone();
                    treeNode.Name = "file_" + id.ToString();
                    TreeNode[] treeNodes = treeView1.Nodes.Find(formTree.treeView1.SelectedNode.Name, true);
                    treeNodes[0].Nodes.Add(treeNode);
                }
                else if (classTree.GetTypeNode(treeView1.SelectedNode) == ClassTree.TypeNodeCollection.FOLDER)
                {
                    classTree.CopyFolder(treeView1);
                }
            }
            else if (lastRequireContextMenu == listView1.GetType())
            {
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    Console.WriteLine(i + " of " + listView1.SelectedItems.Count);
                    if (listView1.SelectedItems[i].SubItems["type"].Text == ClassTree.TypeNodeCollection.FILE)
                    {
                        //id = Convert.ToInt32(listView1.SelectedItems[i].SubItems["id"].Text);
                        //classFiles.Delete(id);
                    }
                    else if (listView1.SelectedItems[i].SubItems["type"].Text == ClassTree.TypeNodeCollection.FOLDER)
                    {
                        //id = Convert.ToInt32(listView1.SelectedItems[i].SubItems["id"].Text);
                        //classFolder.DeleteFolder(id);
                    }
                }
                listView1.SelectedItems.Clear();

                classTree.InitTreeView(treeView1);
            }
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedListViewItemCollection = listView1.SelectedItems;
            foreach (ListViewItem listViewItem in selectedListViewItemCollection)
            {
                if (listViewItem.SubItems["type"].Text == "file")
                {
                    ClassFiles classFiles = new ClassFiles();
                    classFiles.Delete(Convert.ToInt32(listViewItem.SubItems["id"]));
                }

                if (listViewItem.SubItems["type"].Text == "folder")
                {
                    // classFiles.Delete(Convert.ToInt32(listViewItem.SubItems["id"]))
                }
            }
        }

        private void ToolStripMenuItemDelete_Click_1(object sender, EventArgs e)
        {
            ClassFiles classFiles = new ClassFiles();
            ClassFolder classFolder = new ClassFolder();
            int id = 0;

            if (lastRequireContextMenu == treeView1.GetType())
            {
                if (classTree.GetTypeNode(treeView1.SelectedNode) == ClassTree.TypeNodeCollection.FILE)
                {
                    classTree.TreeFilesDeleteFile(treeView1);
                }
                else if (classTree.GetTypeNode(treeView1.SelectedNode) == ClassTree.TypeNodeCollection.FOLDER)
                {
                    classTree.treeFolderDeleteFolder(treeView1);
                }
            }
            else if (lastRequireContextMenu == listView1.GetType())
            {
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    Console.WriteLine(i + " of " + listView1.SelectedItems.Count);
                    if (listView1.SelectedItems[i].SubItems["type"].Text == ClassTree.TypeNodeCollection.FILE)
                    {
                        id = Convert.ToInt32(listView1.SelectedItems[i].SubItems["id"].Text);
                        classFiles.Delete(id);
                    }
                    else if (listView1.SelectedItems[i].SubItems["type"].Text == ClassTree.TypeNodeCollection.FOLDER)
                    {
                        id = Convert.ToInt32(listView1.SelectedItems[i].SubItems["id"].Text);
                        classFolder.DeleteFolder(id);
                    }
                }
                listView1.SelectedItems.Clear();

                classTree.InitTreeView(treeView1);
            }
        }

        private void ToolStripMenuItemSendToDesctop_Click(object sender, EventArgs e)
        {
            ClassFiles classFiles = new ClassFiles();
            int fileId = Convert.ToInt32(treeView1.SelectedNode.Name.Split('_')[1]);
            ClassFiles.FileCollection fileCollection = classFiles.GetFileById(fileId);
            string fileName = Path.GetFileName(fileCollection.path);
            string desctopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.Copy(fileCollection.path, desctopPath + "\\" + fileName);
        }

        private void ToolStripMenuItemSendToEmail_Click(object sender, EventArgs e)
        {
            ClassMail classMail = new ClassMail();
            ClassFiles classFiles = new ClassFiles();
            int fileId = Convert.ToInt32(treeView1.SelectedNode.Name.Split('_')[1]);
            ClassFiles.FileCollection fileCollection = classFiles.GetFileById(fileId);
            classMail.Send("htoto2007@mail.ru", "Test", "Tested message....", fileCollection.path);
        }

        private void ToolStripMenuItemSendToFolder_Click(object sender, EventArgs e)
        {
            ClassFiles classFiles = new ClassFiles();
            int fileId = Convert.ToInt32(treeView1.SelectedNode.Name.Split('_')[1]);
            ClassFiles.FileCollection fileCollection = classFiles.GetFileById(fileId);
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            string fileName = Path.GetFileName(fileCollection.path);
            File.Copy(fileCollection.path, folderBrowserDialog.SelectedPath + "\\" + fileName);
        }

        private void ToolStripMenuItemSendToPrint_Click(object sender, EventArgs e)
        {
            ClassFiles classFiles = new ClassFiles();
            int fileId = Convert.ToInt32(treeView1.SelectedNode.Name.Split('_')[1]);
            ClassFiles.FileCollection fileCollection = classFiles.GetFileById(fileId);
            PrintDialog printDialog = new PrintDialog();
            printDialog.ShowDialog();
            PrintDocument printDocument = new PrintDocument
            {
                DocumentName = fileCollection.path,
                PrinterSettings = printDialog.PrinterSettings
            };
            printDocument.Print();// Чтобы распечатать
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
            ClassUsers classUsers = new ClassUsers();
            classUsers.showDialog();
        }

        /// <summary>
        /// Добавляет файл в базу с пустой картой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemWithoutCard_Click(object sender, EventArgs e)
        {
            int idFolder = 0;
            int idTypeCard = 0;
            string fileName = "";
            //string fullFilePath = "";
            string pathSave = "";

            string repositiryPayh = VitSettings.Properties.GeneralsSettings.Default.repositiryPayh;
            string repositoryRootFolderName = VitSettings.Properties.GeneralsSettings.Default.repositoryRootFolderName;

            ClassFiles classFiles = new ClassFiles();
            if (lastRequireContextMenu == treeView1.GetType())
            {
                idFolder = classTree.GetIdNode(treeView1.SelectedNode);
            }
            else if (lastRequireContextMenu == listView1.GetType())
            {
                idFolder = Convert.ToInt32(listView1.SelectedItems[0].SubItems["id"].Text);
            }

            ClassFolder classFolder = new ClassFolder();
            ClassFolder.FolderCollection folderCollection = classFolder.GetFolderById(idFolder, true);
            pathSave = folderCollection.pathWithoutRoot;

            ClassTypeCard classTypeCard = new ClassTypeCard();
            idTypeCard = classTypeCard.getIdByName(ClassTypeCard.EMPTY_CARD);

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (idFolder == 0)
            {
                return;
            }

            progressBar1.Value = 0;
            progressBar1.Maximum = openFileDialog.FileNames.GetLength(0);
            labelStatus.Text = "Загрузка файлов: ";
            foreach (string fullFilePath in openFileDialog.FileNames)
            {
                fileName = Path.GetFileName(fullFilePath);
                if (classFiles.Create(idFolder, idTypeCard, fileName, fullFilePath, pathSave) == 0)
                {
                    Console.WriteLine("Ошибка загрузки файла: " + fullFilePath);
                }
                progressBar1.Increment(1);
            }
            classTree.InitTreeView(treeView1);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
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

        private void treeView1ContextMenu()
        {
            TreeNode treeNode = treeView1.SelectedNode;
            if (classTree.GetTypeNode(treeNode) == ClassTree.TypeNodeCollection.FILE)
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
            else if (classTree.GetTypeNode(treeNode) == ClassTree.TypeNodeCollection.FOLDER)
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

        private void Version()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string programName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build);
            string displayableVersion = $"{version.Major + "." + (version.Build)}";
            Text = programName + " 1.0.1";
        }

        private void buttonSettingsConnectToDB_Click(object sender, EventArgs e)
        {
            formDB.Show();
        }

        private void buttonSettingsDocumentCard_Click(object sender, EventArgs e)
        {
            formCreatTypeCard.Show();
        }

        private void buttonSettingsAccessGroup_Click(object sender, EventArgs e)
        {
            VitAccessGroup.FormAccessGroup formAccessGroup = new VitAccessGroup.FormAccessGroup();
            formAccessGroup.ShowDialog();
        }

        private void ToolStripMenuItemMove_Click(object sender, EventArgs e)
        {
            ClassFiles classFiles = new ClassFiles();
            ClassFolder classFolder = new ClassFolder();

            if (lastRequireContextMenu == treeView1.GetType())
            {
                if (classTree.GetTypeNode(treeView1.SelectedNode) == ClassTree.TypeNodeCollection.FILE)
                {
                    classTree.TreeFilesMoveFile(treeView1);
                }
                else if (classTree.GetTypeNode(treeView1.SelectedNode) == ClassTree.TypeNodeCollection.FOLDER)
                {
                    classTree.TreeFolderMoveFolder(treeView1);
                }
            }
            else if (lastRequireContextMenu == listView1.GetType())
            {
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    Console.WriteLine(i + " of " + listView1.SelectedItems.Count);
                    if (listView1.SelectedItems[i].SubItems["type"].Text == ClassTree.TypeNodeCollection.FILE)
                    {
                        //id = Convert.ToInt32(listView1.SelectedItems[i].SubItems["id"].Text);
                        //classFiles.Delete(id);
                    }
                    else if (listView1.SelectedItems[i].SubItems["type"].Text == ClassTree.TypeNodeCollection.FOLDER)
                    {
                        //id = Convert.ToInt32(listView1.SelectedItems[i].SubItems["id"].Text);
                        //classFolder.DeleteFolder(id);
                    }
                }
                listView1.SelectedItems.Clear();

                classTree.InitTreeView(treeView1);
            }
        }

        private void ToolStripMenuItemRename_Click(object sender, EventArgs e)
        {
            ClassFiles classFiles = new ClassFiles();
            ClassFolder classFolder = new ClassFolder();

            if (lastRequireContextMenu == treeView1.GetType())
            {
                if (classTree.GetTypeNode(treeView1.SelectedNode) == ClassTree.TypeNodeCollection.FILE)
                {
                    classTree.renameFile(treeView1);
                }
                else if (classTree.GetTypeNode(treeView1.SelectedNode) == ClassTree.TypeNodeCollection.FOLDER)
                {
                    classTree.TreeFolderRenameFolder(treeView1);
                }
            }
            else if (lastRequireContextMenu == listView1.GetType())
            {
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    Console.WriteLine(i + " of " + listView1.SelectedItems.Count);
                    if (listView1.SelectedItems[i].SubItems["type"].Text == ClassTree.TypeNodeCollection.FILE)
                    {
                        //id = Convert.ToInt32(listView1.SelectedItems[i].SubItems["id"].Text);
                        //classFiles.Delete(id);
                    }
                    else if (listView1.SelectedItems[i].SubItems["type"].Text == ClassTree.TypeNodeCollection.FOLDER)
                    {
                        //id = Convert.ToInt32(listView1.SelectedItems[i].SubItems["id"].Text);
                        //classFolder.DeleteFolder(id);
                    }
                }
                listView1.SelectedItems.Clear();

                classTree.InitTreeView(treeView1);
            }
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(VitSettings.Properties.GeneralsSettings.Default.programPath + "/help/AEархив.html");
        }
    }
}