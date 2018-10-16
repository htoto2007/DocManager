namespace DocManager
{
    partial class FormDocumentManager
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDocumentManager));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Узел8");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Узел10");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Узел6", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Узел7");
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            ""}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Transparent, null);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьПапкукToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьДокументToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAdminMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemUserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAdministration = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSettingsConnectToDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSettingsDocumentCard = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSettingsAccessGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.timerSearcher = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelUserMenu = new System.Windows.Forms.Panel();
            this.labelUserName = new System.Windows.Forms.Label();
            this.button1 = new VitControls.Button();
            this.flowLayoutPanelUserMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonScan = new VitControls.Button();
            this.buttonAddBranch = new VitControls.Button();
            this.buttonExit = new VitControls.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateCreate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMove = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelExplorer = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.panelAdminMenu = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new VitControls.Button();
            this.button3 = new VitControls.Button();
            this.button4 = new VitControls.Button();
            this.buttonUsers = new VitControls.Button();
            this.buttonSettings = new VitControls.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelUserMenu.SuspendLayout();
            this.flowLayoutPanelUserMenu.SuspendLayout();
            this.contextMenuStripListView.SuspendLayout();
            this.panelExplorer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelAdminMenu.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьПапкукToolStripMenuItem,
            this.добавитьДокументToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 48);
            // 
            // добавитьПапкукToolStripMenuItem
            // 
            this.добавитьПапкукToolStripMenuItem.Name = "добавитьПапкукToolStripMenuItem";
            this.добавитьПапкукToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.добавитьПапкукToolStripMenuItem.Text = "Добавить папкук";
            // 
            // добавитьДокументToolStripMenuItem
            // 
            this.добавитьДокументToolStripMenuItem.Name = "добавитьДокументToolStripMenuItem";
            this.добавитьДокументToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.добавитьДокументToolStripMenuItem.Text = "Удалить папку";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "copy.png");
            this.imageList1.Images.SetKeyName(1, "credit-card.png");
            this.imageList1.Images.SetKeyName(2, "document-scanner-icon.png");
            this.imageList1.Images.SetKeyName(3, "edit.png");
            this.imageList1.Images.SetKeyName(4, "file-alt.png");
            this.imageList1.Images.SetKeyName(5, "folder.png");
            this.imageList1.Images.SetKeyName(6, "folder-open.png");
            this.imageList1.Images.SetKeyName(7, "info.png");
            this.imageList1.Images.SetKeyName(8, "paste.png");
            this.imageList1.Images.SetKeyName(9, "plus-square.png");
            this.imageList1.Images.SetKeyName(10, "sign-out-alt.png");
            this.imageList1.Images.SetKeyName(11, "user.png");
            this.imageList1.Images.SetKeyName(12, "users-cog.png");
            this.imageList1.Images.SetKeyName(13, "plus-square2.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemView,
            this.ToolStripMenuItemAdministration,
            this.ToolStripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 33);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(825, 32);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemView
            // 
            this.ToolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAdminMenu,
            this.ToolStripMenuItemUserMenu});
            this.ToolStripMenuItemView.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripMenuItemView.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItemView.Name = "ToolStripMenuItemView";
            this.ToolStripMenuItemView.Size = new System.Drawing.Size(45, 26);
            this.ToolStripMenuItemView.Text = "Вид";
            // 
            // toolStripMenuItemAdminMenu
            // 
            this.toolStripMenuItemAdminMenu.CheckOnClick = true;
            this.toolStripMenuItemAdminMenu.Name = "toolStripMenuItemAdminMenu";
            this.toolStripMenuItemAdminMenu.Size = new System.Drawing.Size(233, 24);
            this.toolStripMenuItemAdminMenu.Text = "Меню администратора";
            this.toolStripMenuItemAdminMenu.CheckedChanged += new System.EventHandler(this.toolStripMenuItemAdminMenu_CheckedChanged);
            // 
            // ToolStripMenuItemUserMenu
            // 
            this.ToolStripMenuItemUserMenu.CheckOnClick = true;
            this.ToolStripMenuItemUserMenu.Name = "ToolStripMenuItemUserMenu";
            this.ToolStripMenuItemUserMenu.Size = new System.Drawing.Size(233, 24);
            this.ToolStripMenuItemUserMenu.Text = "Пользовательское меню";
            this.ToolStripMenuItemUserMenu.CheckedChanged += new System.EventHandler(this.ToolStripMenuItemUserMenu_CheckedChanged);
            // 
            // ToolStripMenuItemAdministration
            // 
            this.ToolStripMenuItemAdministration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSettingsConnectToDataBase,
            this.ToolStripMenuItemSettingsDocumentCard,
            this.ToolStripMenuItemSettingsAccessGroup,
            this.ToolStripMenuItemUsers,
            this.ToolStripMenuItemSettings});
            this.ToolStripMenuItemAdministration.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripMenuItemAdministration.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItemAdministration.Name = "ToolStripMenuItemAdministration";
            this.ToolStripMenuItemAdministration.Size = new System.Drawing.Size(166, 26);
            this.ToolStripMenuItemAdministration.Text = "Администратирование";
            // 
            // ToolStripMenuItemSettingsConnectToDataBase
            // 
            this.ToolStripMenuItemSettingsConnectToDataBase.Name = "ToolStripMenuItemSettingsConnectToDataBase";
            this.ToolStripMenuItemSettingsConnectToDataBase.Size = new System.Drawing.Size(278, 24);
            this.ToolStripMenuItemSettingsConnectToDataBase.Text = "настройки подключения к базе";
            this.ToolStripMenuItemSettingsConnectToDataBase.Click += new System.EventHandler(this.ToolStripMenuItemSettingsConnectToDataBase_Click);
            // 
            // ToolStripMenuItemSettingsDocumentCard
            // 
            this.ToolStripMenuItemSettingsDocumentCard.Name = "ToolStripMenuItemSettingsDocumentCard";
            this.ToolStripMenuItemSettingsDocumentCard.Size = new System.Drawing.Size(278, 24);
            this.ToolStripMenuItemSettingsDocumentCard.Text = "настройка карточек";
            this.ToolStripMenuItemSettingsDocumentCard.Click += new System.EventHandler(this.ToolStripMenuItemSettingsDocumentCard_Click);
            // 
            // ToolStripMenuItemSettingsAccessGroup
            // 
            this.ToolStripMenuItemSettingsAccessGroup.Name = "ToolStripMenuItemSettingsAccessGroup";
            this.ToolStripMenuItemSettingsAccessGroup.Size = new System.Drawing.Size(278, 24);
            this.ToolStripMenuItemSettingsAccessGroup.Text = "Настройка прав доступа";
            this.ToolStripMenuItemSettingsAccessGroup.Click += new System.EventHandler(this.ToolStripMenuItemSettingsAccessGroup_Click);
            // 
            // ToolStripMenuItemUsers
            // 
            this.ToolStripMenuItemUsers.Name = "ToolStripMenuItemUsers";
            this.ToolStripMenuItemUsers.Size = new System.Drawing.Size(278, 24);
            this.ToolStripMenuItemUsers.Text = "Пользователи";
            this.ToolStripMenuItemUsers.Click += new System.EventHandler(this.ToolStripMenuItemUsers_Click);
            // 
            // ToolStripMenuItemSettings
            // 
            this.ToolStripMenuItemSettings.Name = "ToolStripMenuItemSettings";
            this.ToolStripMenuItemSettings.Size = new System.Drawing.Size(278, 24);
            this.ToolStripMenuItemSettings.Text = "настроки";
            this.ToolStripMenuItemSettings.Click += new System.EventHandler(this.ToolStripMenuItemSettings_Click);
            // 
            // ToolStripMenuItemHelp
            // 
            this.ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAbout});
            this.ToolStripMenuItemHelp.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
            this.ToolStripMenuItemHelp.Size = new System.Drawing.Size(70, 26);
            this.ToolStripMenuItemHelp.Text = "Справкв";
            // 
            // ToolStripMenuItemAbout
            // 
            this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
            this.ToolStripMenuItemAbout.Size = new System.Drawing.Size(158, 22);
            this.ToolStripMenuItemAbout.Text = "О программе";
            this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
            // 
            // timerSearcher
            // 
            this.timerSearcher.Interval = 1000;
            this.timerSearcher.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelUserMenu
            // 
            this.panelUserMenu.Controls.Add(this.labelUserName);
            this.panelUserMenu.Controls.Add(this.button1);
            this.panelUserMenu.Controls.Add(this.flowLayoutPanelUserMenu);
            this.panelUserMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUserMenu.Location = new System.Drawing.Point(0, 65);
            this.panelUserMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelUserMenu.Name = "panelUserMenu";
            this.panelUserMenu.Size = new System.Drawing.Size(825, 35);
            this.panelUserMenu.TabIndex = 5;
            // 
            // labelUserName
            // 
            this.labelUserName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelUserName.AutoSize = true;
            this.labelUserName.ForeColor = System.Drawing.Color.Black;
            this.labelUserName.Location = new System.Drawing.Point(710, 6);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(112, 17);
            this.labelUserName.TabIndex = 15;
            this.labelUserName.Text = "User not connect";
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::DocManager.Properties.Resources.icons8_user_avatar_48;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(664, 1);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 14;
            // 
            // flowLayoutPanelUserMenu
            // 
            this.flowLayoutPanelUserMenu.AllowDrop = true;
            this.flowLayoutPanelUserMenu.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanelUserMenu.Controls.Add(this.buttonScan);
            this.flowLayoutPanelUserMenu.Controls.Add(this.buttonAddBranch);
            this.flowLayoutPanelUserMenu.Controls.Add(this.buttonExit);
            this.flowLayoutPanelUserMenu.Location = new System.Drawing.Point(9, 1);
            this.flowLayoutPanelUserMenu.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelUserMenu.Name = "flowLayoutPanelUserMenu";
            this.flowLayoutPanelUserMenu.Size = new System.Drawing.Size(653, 34);
            this.flowLayoutPanelUserMenu.TabIndex = 13;
            // 
            // buttonScan
            // 
            this.buttonScan.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonScan.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.buttonScan.BackColor = System.Drawing.Color.White;
            this.buttonScan.BackgroundImage = global::DocManager.Properties.Resources.icons8_scanner_40;
            this.buttonScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonScan.CausesValidation = false;
            this.buttonScan.Location = new System.Drawing.Point(2, 1);
            this.buttonScan.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(32, 32);
            this.buttonScan.TabIndex = 13;
            this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // buttonAddBranch
            // 
            this.buttonAddBranch.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonAddBranch.AllowDrop = true;
            this.buttonAddBranch.BackColor = System.Drawing.Color.White;
            this.buttonAddBranch.BackgroundImage = global::DocManager.Properties.Resources.icons8_plus_48;
            this.buttonAddBranch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAddBranch.Location = new System.Drawing.Point(38, 1);
            this.buttonAddBranch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonAddBranch.Name = "buttonAddBranch";
            this.buttonAddBranch.Size = new System.Drawing.Size(32, 32);
            this.buttonAddBranch.TabIndex = 14;
            this.buttonAddBranch.Click += new System.EventHandler(this.buttonAddBranch_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonExit.AllowDrop = true;
            this.buttonExit.BackColor = System.Drawing.Color.White;
            this.buttonExit.BackgroundImage = global::DocManager.Properties.Resources.icons8_exit_48;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonExit.Location = new System.Drawing.Point(74, 1);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(32, 32);
            this.buttonExit.TabIndex = 15;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(16, 8);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Узел8";
            treeNode1.Text = "Узел8";
            treeNode2.Name = "Узел10";
            treeNode2.Text = "Узел10";
            treeNode3.ImageKey = "folder.png";
            treeNode3.Name = "Узел6";
            treeNode3.SelectedImageKey = "folder-open.png";
            treeNode3.Text = "Узел6";
            treeNode4.ContextMenuStrip = this.contextMenuStrip1;
            treeNode4.Name = "Узел7";
            treeNode4.Text = "Узел7";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(377, 511);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick_1);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.name,
            this.dateCreate,
            this.dateChange,
            this.path});
            this.listView1.ContextMenuStrip = this.contextMenuStripListView;
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.Location = new System.Drawing.Point(401, 43);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(406, 476);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // id
            // 
            this.id.Text = "id";
            this.id.Width = 32;
            // 
            // name
            // 
            this.name.Text = "Имя файла";
            this.name.Width = 220;
            // 
            // dateCreate
            // 
            this.dateCreate.Text = "Дата создания";
            this.dateCreate.Width = 127;
            // 
            // dateChange
            // 
            this.dateChange.Text = "Дата изменеия";
            this.dateChange.Width = 102;
            // 
            // path
            // 
            this.path.Text = "Путь";
            // 
            // contextMenuStripListView
            // 
            this.contextMenuStripListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDelete,
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemMove});
            this.contextMenuStripListView.Name = "contextMenuStripListView";
            this.contextMenuStripListView.Size = new System.Drawing.Size(147, 70);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemDelete.Image")));
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItemDelete.Text = "Удалить";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemCopy.Image")));
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItemCopy.Text = "Копировать";
            // 
            // toolStripMenuItemMove
            // 
            this.toolStripMenuItemMove.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemMove.Image")));
            this.toolStripMenuItemMove.Name = "toolStripMenuItemMove";
            this.toolStripMenuItemMove.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItemMove.Text = "Переместить";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(423, 8);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0, 4, 3, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(384, 22);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panelExplorer
            // 
            this.panelExplorer.Controls.Add(this.pictureBox1);
            this.panelExplorer.Controls.Add(this.textBox1);
            this.panelExplorer.Controls.Add(this.listView1);
            this.panelExplorer.Controls.Add(this.treeView1);
            this.panelExplorer.Controls.Add(this.panel1);
            this.panelExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExplorer.Location = new System.Drawing.Point(0, 136);
            this.panelExplorer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelExplorer.Name = "panelExplorer";
            this.panelExplorer.Size = new System.Drawing.Size(825, 523);
            this.panelExplorer.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(401, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(402, 22);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 10);
            this.panel1.TabIndex = 9;
            // 
            // windowHeader1
            // 
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(207)))), ((int)(((byte)(251)))));
            this.windowHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowHeader1.ForeColor = System.Drawing.Color.Black;
            this.windowHeader1.Location = new System.Drawing.Point(0, 0);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.maximize = true;
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.Size = new System.Drawing.Size(825, 33);
            this.windowHeader1.TabIndex = 2;
            this.windowHeader1.Load += new System.EventHandler(this.windowHeader1_Load);
            // 
            // panelAdminMenu
            // 
            this.panelAdminMenu.Controls.Add(this.flowLayoutPanel1);
            this.panelAdminMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAdminMenu.Location = new System.Drawing.Point(0, 100);
            this.panelAdminMenu.Name = "panelAdminMenu";
            this.panelAdminMenu.Size = new System.Drawing.Size(825, 36);
            this.panelAdminMenu.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Controls.Add(this.button4);
            this.flowLayoutPanel1.Controls.Add(this.buttonUsers);
            this.flowLayoutPanel1.Controls.Add(this.buttonSettings);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 1);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(653, 34);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button2.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImage = global::DocManager.Properties.Resources.icons8_database_administrator_64;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.CausesValidation = false;
            this.button2.Location = new System.Drawing.Point(3, 1);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 32);
            this.button2.TabIndex = 16;
            // 
            // button3
            // 
            this.button3.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button3.AllowDrop = true;
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImage = global::DocManager.Properties.Resources.icons8_red_card_40;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Location = new System.Drawing.Point(41, 1);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 32);
            this.button3.TabIndex = 17;
            // 
            // button4
            // 
            this.button4.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button4.AllowDrop = true;
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.BackgroundImage = global::DocManager.Properties.Resources.icons8_password_48;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.Location = new System.Drawing.Point(79, 1);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 32);
            this.button4.TabIndex = 18;
            // 
            // buttonUsers
            // 
            this.buttonUsers.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonUsers.AllowDrop = true;
            this.buttonUsers.BackColor = System.Drawing.Color.White;
            this.buttonUsers.BackgroundImage = global::DocManager.Properties.Resources.icons8_conference_48;
            this.buttonUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonUsers.Location = new System.Drawing.Point(118, 1);
            this.buttonUsers.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.buttonUsers.Name = "buttonUsers";
            this.buttonUsers.Size = new System.Drawing.Size(32, 32);
            this.buttonUsers.TabIndex = 19;
            this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonSettings.AllowDrop = true;
            this.buttonSettings.BackColor = System.Drawing.Color.White;
            this.buttonSettings.BackgroundImage = global::DocManager.Properties.Resources.icons8_services_48;
            this.buttonSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSettings.Location = new System.Drawing.Point(158, 1);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(32, 32);
            this.buttonSettings.TabIndex = 20;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // FormDocumentManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(825, 659);
            this.ControlBox = false;
            this.Controls.Add(this.panelExplorer);
            this.Controls.Add(this.panelAdminMenu);
            this.Controls.Add(this.panelUserMenu);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.windowHeader1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormDocumentManager";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "z";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDocumentManager_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelUserMenu.ResumeLayout(false);
            this.panelUserMenu.PerformLayout();
            this.flowLayoutPanelUserMenu.ResumeLayout(false);
            this.contextMenuStripListView.ResumeLayout(false);
            this.panelExplorer.ResumeLayout(false);
            this.panelExplorer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelAdminMenu.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem добавитьПапкукToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьДокументToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Timer timerSearcher;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAdministration;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettingsConnectToDataBase;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettingsDocumentCard;
        public System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettingsAccessGroup;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemUsers;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdminMenu;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemUserMenu;
        private System.Windows.Forms.Panel panelUserMenu;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelUserMenu;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader dateCreate;
        private System.Windows.Forms.ColumnHeader dateChange;
        private System.Windows.Forms.ColumnHeader path;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelExplorer;
        private VitControls.Button buttonScan;
        private VitControls.Button buttonAddBranch;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbout;
        private VitControls.Button buttonExit;
        private VitControls.WindowHeader windowHeader1;
        private VitControls.Button button1;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelAdminMenu;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private VitControls.Button button2;
        private VitControls.Button button3;
        private VitControls.Button button4;
        private VitControls.Button buttonUsers;
        private VitControls.Button buttonSettings;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListView;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMove;
    }
}

