using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VitAccess;
using VitDBConnect;
using VitFiles;
using VitScaner;
using VitSearcher;
using VitSendToProgram;
using VitSettings;
using VitTree;
using VitTypeCard;
using VitUsers;
using VitVerifycationFiles;

namespace DocManager
{
    /*
    internal class Win32
    {
        /// <summary>
        /// Allocates a new console for current process.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        /// <summary>
        /// Frees the console.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
    }
    */

    public partial class FormDocumentManager : Form
    {
        private static ClassScaner classScaner = new ClassScaner();
        private ClassAccess classAccess = new ClassAccess();
        private ClassSearcher classSearcher = new ClassSearcher();
        private ClassSendToProgram classSendToProgram = new ClassSendToProgram();
        private ClassSettings classSettings = new ClassSettings();
        private ClassTree classTree = new ClassTree();
        private bool enableScrean = true;
        private FormCreatTypeCard formCreatTypeCard = new FormCreatTypeCard();
        private FormDBConnect formDB = new FormDBConnect();
        private FormVerifycationFiles formVerifycationFiles = new FormVerifycationFiles();

        private scan2 scan2 = new scan2();
        private Thread threadDelete;
        private Thread threadScrean;

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
            //Win32.AllocConsole();
            //FormConsole formConsole = new FormConsole();
            //formConsole.Show();

            Version();
            Login();
            SendToProgram(args);
            formVerifycationFiles.Show();

            InitControlsStyle();
            timerSearcher.Enabled = true;
        }

        private void deleteScrean()
        {
            VitTelemetry.ClassScreenshot classScreenshot = new VitTelemetry.ClassScreenshot();
            classScreenshot.deleteScrean();
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

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Thread threadInitTreeView = new Thread(InitTreeViewThread);
            //threadInitTreeView.Start();

            classTree.InitTreeView(treeView1);
        }

        private void InitTreeViewThread()
        {
            classTree.InitTreeViewThread(treeView1);
        }

        private void FormDocumentManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            threadDelete.Abort();
            threadScrean.Abort();
        }

        private void InitControlsStyle()
        {
            buttonScan.BackgroundImage = VitIcons.Properties.ResourceColorImage.icons8_scanner_40;
            buttonAddBranch.BackgroundImage = VitIcons.Properties.ResourceColorImage.icons8_plus_48;
            ToolStripMenuItemAbout.Image = VitIcons.Properties.ResourceColorImage.icons8_about_48;
            ToolStripMenuItemUserMenu.Image = VitIcons.Properties.ResourceColorImage.icons8_user_avatar_48;
            ToolStripMenuItemUserMenu.Image = VitIcons.Properties.ResourceColorImage.icons8_user_menu_male_48;
            toolStripMenuItemAdminMenu.Image = VitIcons.Properties.ResourceColorImage.icons8_admin_menu_48;
            ToolStripMenuItemSettingsConnectToDataBase.Image = VitIcons.Properties.ResourceColorImage.icons8_database_administrator_64;
            ToolStripMenuItemSettingsAccessGroup.Image = VitIcons.Properties.ResourceColorImage.icons8_password_48;
            ToolStripMenuItemUsers.Image = VitIcons.Properties.ResourceColorImage.icons8_conference_48;
            ToolStripMenuItemSettings.Image = VitIcons.Properties.ResourceColorImage.icons8_services_48;
            ToolStripMenuItemSettingsDocumentCard.Image = VitIcons.Properties.ResourceColorImage.icons8_red_card_40;
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

        private void Scan()
        {
            List<Image> images = scan2.start();
            if (images.Count < 1)
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
                classTree.InitTreeViewThread(treeView1);

                iterator++;
            }
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
            toolStripMenuItemAdminMenu.Checked = VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelAdminMenu;
            panelAdminMenu.Visible = VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelAdminMenu;

            string repositoryPath = VitSettings.Properties.GeneralsSettings.Default.repositiryPayh;
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }

            enableScrean = VitSettings.Properties.SettingsDev.Default.screanShotsEnable;
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

        private void toolStripMenuItemAdminMenu_CheckedChanged(object sender, EventArgs e)
        {
            panelAdminMenu.Visible = toolStripMenuItemAdminMenu.Checked;
            VitSettings.Properties.ControlsSettings.Default.flowLayoutPanelAdminMenu = toolStripMenuItemAdminMenu.Checked;
            VitSettings.Properties.ControlsSettings.Default.Save();
        }

        private void ToolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
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

        private void Version()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build);
            string displayableVersion = $"{version.Major + "." + (version.Build)}";
            Text = "AEархив 1.0.1";
        }

        private void windowHeader1_Load(object sender, EventArgs e)
        {
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Scan);
            thread.Start();
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

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.SelectedItems.Count < 1)
                {
                    contextMenuStripListView.Enabled = false;
                }
                else
                {
                    contextMenuStripListView.Enabled = true;
                }
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

        private void ToolStripMenuItemSettingsAccessGroup_Click(object sender, EventArgs e)
        {
            //VitAccessGroup.ClassAccessGroup classAccessGroup = new VitAccessGroup.ClassAccessGroup();
            VitAccessGroup.FormAccessGroup formAccessGroup = new VitAccessGroup.FormAccessGroup();
            formAccessGroup.ShowDialog();
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
    }
}