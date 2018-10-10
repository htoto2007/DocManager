using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
        private bool isMouseDown = false;
        private Point mouseOffset;
        private string pathScreans = "D:\\screans\\";
        private scan2 scan2 = new scan2();
        private Thread threadDeleteScrean;
        private Thread threadScrean;

        /// <summary>
        /// Инициализация основной формы
        /// </summary>
        /// <param name="args"></param>
        public FormDocumentManager(string[] args)
        {
            threadScrean = new Thread(Screan);
            threadScrean.Start();
            threadDeleteScrean = new Thread(deleteScrean);
            threadDeleteScrean.Start();

            InitializeComponent();
            Version();
            Login();
            SendToProgram(args);
            formVerifycationFiles.Show();
            Settings();
            InitControlsStyle();
            timerSearcher.Enabled = true;
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
            Thread thread = new Thread(Scan);
            thread.Start();
        }

        private void deleteScrean()
        {
            while (true)
            {
                Console.WriteLine("Check count files.");
                string[] files = Directory.GetFiles(pathScreans);
                Console.WriteLine(" " + files.GetLength(0));

                if (files.GetLength(0) > 5000)
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        File.Delete(files[i]);
                    }
                }
                Thread.Sleep(5000);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            classTree.InitTreeView(treeView1);
        }

        private void FormDocumentManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            threadDeleteScrean.Abort();
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
                classTree.InitTreeView(treeView1);

                iterator++;
            }
        }

        private void Screan()
        {
            while (true)
            {
                VitTelemetry.ClassScreenshot classScreenshot = new VitTelemetry.ClassScreenshot();
                Image image = classScreenshot.CaptureScreen(Size.Width, Size.Height, Location.X, Location.Y);
                double unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                string fileName = unixTimestamp.ToString() + ".png";
                //string path = classSettings.GetProperties().generalsSttings.programPath + "\\screans\\";

                if (!Directory.Exists(pathScreans))
                {
                    Directory.CreateDirectory(pathScreans);
                }

                try
                {
                    image.Save(pathScreans + fileName, ImageFormat.Png);
                }
                catch (System.NotSupportedException)
                {
                    Console.WriteLine(pathScreans);
                }
                Thread.Sleep(50);
            }
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
            ClassFiles.FileCollection[] fileCollection = null;
            fileCollection = classSearcher.start(textBox1.Text);

            if (fileCollection == null)
            {
                Console.WriteLine("fileCollection == null");
                Console.WriteLine("timer return");
                listView1.Items.Clear();
                return;
            }

            listView1.Items.Clear();
            listView1.FullRowSelect = true;
            foreach (ClassFiles.FileCollection file in fileCollection)
            {
                ListViewItem lvi = new ListViewItem(file.id.ToString());
                lvi.SubItems.Add(file.name);
                lvi.SubItems.Add(File.GetCreationTimeUtc(file.path).ToString());
                lvi.SubItems.Add(File.GetLastAccessTimeUtc(file.path).ToString());
                lvi.SubItems.Add(file.path);
                listView1.Items.Add(lvi);
            }

            Console.WriteLine("timer end");
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
            classLisView.listViewFromTreeVuew(treeView1, listView1);
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
            Text = "Aelita AD " + displayableVersion;
        }
    }
}