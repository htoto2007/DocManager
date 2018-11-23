﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VitAccess;
using VitAccessGroup;
using VitDBConnect;
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
            formCreatTypeCard.Show();
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
            classTree.init(treeView1);
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
            classTree.DeleteNode(treeView1);
        }

        private void ToolStripMenuItemMove_Click(object sender, EventArgs e)
        {
            classTree.move(treeView1);
        }

        private void ToolStripMenuItemRename_Click(object sender, EventArgs e)
        {
        }

        private void ToolStripMenuItemSendToDesctop_Click(object sender, EventArgs e)
        {
        }

        private void ToolStripMenuItemSendToEmail_Click(object sender, EventArgs e)
        {
        }

        private void ToolStripMenuItemSendToFolder_Click(object sender, EventArgs e)
        {
        }

        private void ToolStripMenuItemSendToPrint_Click(object sender, EventArgs e)
        {
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
        private void ToolStripMenuItemWithoutCard_ClickAsync(object sender, EventArgs e)
        {
            classTree.AddFileNode(treeView1);
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            classTree.preLoadNodes(e.Node);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //classTree.preLoadNodes(treeView1);

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
        }

        private void Version()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string programName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build);
            string displayableVersion = $"{version.Major + "." + (version.Build)}";
            Text = programName;
        }
    }
}