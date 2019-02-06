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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            ""}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Transparent, null);
            this.contextMenuStripTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRequestOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAddDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAddDocumentWithCard = new System.Windows.Forms.ToolStripMenuItem();
            this.безКарточкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSend = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSendToDesctop = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSendToPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSendToEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSendToFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMove = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemScanToThisFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ToolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemUserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAdministration = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSettingsConnectToDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSettingsDocumentCard = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSettingsAccessGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerSearcher = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.button1 = new VitControls.VitButton();
            this.buttonExit = new VitControls.VitButton();
            this.buttonAdminSettingsDocumentCard = new VitControls.VitButton();
            this.buttonAdminUsers = new VitControls.VitButton();
            this.buttonAdminSettings = new VitControls.VitButton();
            this.buttonScan = new VitControls.VitButton();
            this.buttonAddBranch = new VitControls.VitButton();
            this.vitButtonUpdateInfo = new VitControls.VitButton();
            this.panelUserMenu = new System.Windows.Forms.Panel();
            this.flowLayoutPanelUserMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateCreate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelExplorer = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelStatusProgress = new System.Windows.Forms.Panel();
            this.windowResizer1 = new VitControls.WindowResizer();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemShowUserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.администратированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подключениеКБазеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользователиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиКарточекToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.contextMenuStripTreeView.SuspendLayout();
            this.panelUserMenu.SuspendLayout();
            this.flowLayoutPanelUserMenu.SuspendLayout();
            this.panelExplorer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelStatusProgress.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripTreeView
            // 
            this.contextMenuStripTreeView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contextMenuStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddFolder,
            this.ToolStripMenuItemRequestOriginal,
            this.ToolStripMenuItemAddDocument,
            this.ToolStripMenuItemSend,
            this.ToolStripMenuItemDelete,
            this.ToolStripMenuItemCopy,
            this.ToolStripMenuItemMove,
            this.ToolStripMenuItemRename,
            this.toolStripMenuItemScanToThisFolder,
            this.ToolStripMenuItemSelectAll});
            this.contextMenuStripTreeView.Name = "contextMenuStrip1";
            this.contextMenuStripTreeView.Size = new System.Drawing.Size(262, 224);
            this.contextMenuStripTreeView.Opened += new System.EventHandler(this.contextMenuStripTreeView_Opened);
            // 
            // ToolStripMenuItemAddFolder
            // 
            this.ToolStripMenuItemAddFolder.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemAddFolder.Image")));
            this.ToolStripMenuItemAddFolder.Name = "ToolStripMenuItemAddFolder";
            this.ToolStripMenuItemAddFolder.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.ToolStripMenuItemAddFolder.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemAddFolder.Text = "Добавить папку";
            this.ToolStripMenuItemAddFolder.Click += new System.EventHandler(this.ToolStripMenuItemAddFolder_Click);
            // 
            // ToolStripMenuItemRequestOriginal
            // 
            this.ToolStripMenuItemRequestOriginal.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemRequestOriginal.Image")));
            this.ToolStripMenuItemRequestOriginal.Name = "ToolStripMenuItemRequestOriginal";
            this.ToolStripMenuItemRequestOriginal.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
            this.ToolStripMenuItemRequestOriginal.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemRequestOriginal.Text = "Запросить оригинал";
            // 
            // ToolStripMenuItemAddDocument
            // 
            this.ToolStripMenuItemAddDocument.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddDocumentWithCard,
            this.безКарточкиToolStripMenuItem});
            this.ToolStripMenuItemAddDocument.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemAddDocument.Image")));
            this.ToolStripMenuItemAddDocument.Name = "ToolStripMenuItemAddDocument";
            this.ToolStripMenuItemAddDocument.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemAddDocument.Text = "Добавить документ";
            // 
            // ToolStripMenuItemAddDocumentWithCard
            // 
            this.ToolStripMenuItemAddDocumentWithCard.Name = "ToolStripMenuItemAddDocumentWithCard";
            this.ToolStripMenuItemAddDocumentWithCard.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.NumPad1)));
            this.ToolStripMenuItemAddDocumentWithCard.Size = new System.Drawing.Size(272, 22);
            this.ToolStripMenuItemAddDocumentWithCard.Text = "С карточкой";
            this.ToolStripMenuItemAddDocumentWithCard.Click += new System.EventHandler(this.ToolStripMenuItemAddDocumentWithCard_Click);
            // 
            // безКарточкиToolStripMenuItem
            // 
            this.безКарточкиToolStripMenuItem.Name = "безКарточкиToolStripMenuItem";
            this.безКарточкиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.NumPad0)));
            this.безКарточкиToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.безКарточкиToolStripMenuItem.Text = "Без карточки";
            this.безКарточкиToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemWithoutCard_ClickAsync);
            // 
            // ToolStripMenuItemSend
            // 
            this.ToolStripMenuItemSend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSendToDesctop,
            this.ToolStripMenuItemSendToPrint,
            this.ToolStripMenuItemSendToEmail,
            this.ToolStripMenuItemSendToFolder});
            this.ToolStripMenuItemSend.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemSend.Image")));
            this.ToolStripMenuItemSend.Name = "ToolStripMenuItemSend";
            this.ToolStripMenuItemSend.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemSend.Text = "Отправить";
            // 
            // ToolStripMenuItemSendToDesctop
            // 
            this.ToolStripMenuItemSendToDesctop.Name = "ToolStripMenuItemSendToDesctop";
            this.ToolStripMenuItemSendToDesctop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.ToolStripMenuItemSendToDesctop.Size = new System.Drawing.Size(219, 22);
            this.ToolStripMenuItemSendToDesctop.Text = "На рабочий стол";
            this.ToolStripMenuItemSendToDesctop.Click += new System.EventHandler(this.ToolStripMenuItemSendToDesctop_Click);
            // 
            // ToolStripMenuItemSendToPrint
            // 
            this.ToolStripMenuItemSendToPrint.Name = "ToolStripMenuItemSendToPrint";
            this.ToolStripMenuItemSendToPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.ToolStripMenuItemSendToPrint.Size = new System.Drawing.Size(219, 22);
            this.ToolStripMenuItemSendToPrint.Text = "На печать";
            this.ToolStripMenuItemSendToPrint.Click += new System.EventHandler(this.ToolStripMenuItemSendToPrint_Click);
            // 
            // ToolStripMenuItemSendToEmail
            // 
            this.ToolStripMenuItemSendToEmail.Name = "ToolStripMenuItemSendToEmail";
            this.ToolStripMenuItemSendToEmail.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.ToolStripMenuItemSendToEmail.Size = new System.Drawing.Size(219, 22);
            this.ToolStripMenuItemSendToEmail.Text = "На почту";
            this.ToolStripMenuItemSendToEmail.Click += new System.EventHandler(this.ToolStripMenuItemSendToEmail_Click);
            // 
            // ToolStripMenuItemSendToFolder
            // 
            this.ToolStripMenuItemSendToFolder.Name = "ToolStripMenuItemSendToFolder";
            this.ToolStripMenuItemSendToFolder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.ToolStripMenuItemSendToFolder.Size = new System.Drawing.Size(219, 22);
            this.ToolStripMenuItemSendToFolder.Text = "В папку";
            this.ToolStripMenuItemSendToFolder.Click += new System.EventHandler(this.ToolStripMenuItemSendToFolder_Click);
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemDelete.Image")));
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ToolStripMenuItemDelete.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemDelete.Text = "Удалить";
            this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
            // 
            // ToolStripMenuItemCopy
            // 
            this.ToolStripMenuItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemCopy.Image")));
            this.ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy";
            this.ToolStripMenuItemCopy.ShortcutKeyDisplayString = "";
            this.ToolStripMenuItemCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.ToolStripMenuItemCopy.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemCopy.Text = "Копировать";
            this.ToolStripMenuItemCopy.Click += new System.EventHandler(this.ToolStripMenuItemCopy_Click);
            // 
            // ToolStripMenuItemMove
            // 
            this.ToolStripMenuItemMove.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemMove.Image")));
            this.ToolStripMenuItemMove.Name = "ToolStripMenuItemMove";
            this.ToolStripMenuItemMove.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ToolStripMenuItemMove.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemMove.Text = "Переместить";
            this.ToolStripMenuItemMove.Click += new System.EventHandler(this.ToolStripMenuItemMove_Click);
            // 
            // ToolStripMenuItemRename
            // 
            this.ToolStripMenuItemRename.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemRename.Image")));
            this.ToolStripMenuItemRename.Name = "ToolStripMenuItemRename";
            this.ToolStripMenuItemRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.ToolStripMenuItemRename.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemRename.Text = "Переименовать";
            this.ToolStripMenuItemRename.Click += new System.EventHandler(this.ToolStripMenuItemRename_Click);
            // 
            // toolStripMenuItemScanToThisFolder
            // 
            this.toolStripMenuItemScanToThisFolder.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemScanToThisFolder.Image")));
            this.toolStripMenuItemScanToThisFolder.Name = "toolStripMenuItemScanToThisFolder";
            this.toolStripMenuItemScanToThisFolder.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItemScanToThisFolder.Size = new System.Drawing.Size(261, 22);
            this.toolStripMenuItemScanToThisFolder.Text = "Сканировать в эту папку";
            // 
            // ToolStripMenuItemSelectAll
            // 
            this.ToolStripMenuItemSelectAll.Name = "ToolStripMenuItemSelectAll";
            this.ToolStripMenuItemSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.ToolStripMenuItemSelectAll.Size = new System.Drawing.Size(261, 22);
            this.ToolStripMenuItemSelectAll.Text = "Выделить все";
            this.ToolStripMenuItemSelectAll.Click += new System.EventHandler(this.ToolStripMenuItemSelectAll_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-plus-480.png");
            this.imageList1.Images.SetKeyName(1, "icons8-database-administrator-96.png");
            this.imageList1.Images.SetKeyName(2, "icons8-edit-480.png");
            this.imageList1.Images.SetKeyName(3, "icons8-plus-480.png");
            this.imageList1.Images.SetKeyName(4, "icons8-scanner-96.png");
            this.imageList1.Images.SetKeyName(5, "icons8-tags-96.png");
            this.imageList1.Images.SetKeyName(6, "icons8-trash-can-480 (1).png");
            this.imageList1.Images.SetKeyName(7, "icons8-exit-button-100.png");
            // 
            // ToolStripMenuItemView
            // 
            this.ToolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemUserMenu});
            this.ToolStripMenuItemView.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripMenuItemView.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItemView.Name = "ToolStripMenuItemView";
            this.ToolStripMenuItemView.Size = new System.Drawing.Size(45, 26);
            this.ToolStripMenuItemView.Text = "Вид";
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
            this.ToolStripMenuItemAbout,
            this.помощьToolStripMenuItem});
            this.ToolStripMenuItemHelp.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
            this.ToolStripMenuItemHelp.Size = new System.Drawing.Size(70, 26);
            this.ToolStripMenuItemHelp.Text = "Справка";
            // 
            // ToolStripMenuItemAbout
            // 
            this.ToolStripMenuItemAbout.Name = "ToolStripMenuItemAbout";
            this.ToolStripMenuItemAbout.Size = new System.Drawing.Size(149, 22);
            this.ToolStripMenuItemAbout.Text = "О программе";
            this.ToolStripMenuItemAbout.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // timerSearcher
            // 
            this.timerSearcher.Interval = 1000;
            this.timerSearcher.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.ReshowDelay = 20;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.BackColor = System.Drawing.Color.White;
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(423, 8);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(0, 4, 3, 0);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(357, 22);
            this.textBoxSearch.TabIndex = 7;
            this.toolTip1.SetToolTip(this.textBoxSearch, "Просто начните вводить текст и все найдется...");
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(472, 35);
            this.button1.Margin = new System.Windows.Forms.Padding(7, 1, 7, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 31);
            this.button1.TabIndex = 20;
            this.toolTip1.SetToolTip(this.button1, "Профиль пользователя");
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonExit.AllowDrop = true;
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackColor = System.Drawing.Color.White;
            this.buttonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonExit.BackgroundImage")));
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonExit.Location = new System.Drawing.Point(439, 35);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(32, 31);
            this.buttonExit.TabIndex = 18;
            this.toolTip1.SetToolTip(this.buttonExit, "Выход");
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonAdminSettingsDocumentCard
            // 
            this.buttonAdminSettingsDocumentCard.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonAdminSettingsDocumentCard.AllowDrop = true;
            this.buttonAdminSettingsDocumentCard.BackColor = System.Drawing.Color.White;
            this.buttonAdminSettingsDocumentCard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonAdminSettingsDocumentCard.BackgroundImage")));
            this.buttonAdminSettingsDocumentCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdminSettingsDocumentCard.Location = new System.Drawing.Point(3, 1);
            this.buttonAdminSettingsDocumentCard.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonAdminSettingsDocumentCard.Name = "buttonAdminSettingsDocumentCard";
            this.buttonAdminSettingsDocumentCard.Size = new System.Drawing.Size(30, 30);
            this.buttonAdminSettingsDocumentCard.TabIndex = 17;
            this.toolTip1.SetToolTip(this.buttonAdminSettingsDocumentCard, "Настройка карточек документов");
            this.buttonAdminSettingsDocumentCard.Click += new System.EventHandler(this.buttonSettingsDocumentCard_Click);
            // 
            // buttonAdminUsers
            // 
            this.buttonAdminUsers.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonAdminUsers.AllowDrop = true;
            this.buttonAdminUsers.BackColor = System.Drawing.Color.White;
            this.buttonAdminUsers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonAdminUsers.BackgroundImage")));
            this.buttonAdminUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdminUsers.Location = new System.Drawing.Point(40, 1);
            this.buttonAdminUsers.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.buttonAdminUsers.Name = "buttonAdminUsers";
            this.buttonAdminUsers.Size = new System.Drawing.Size(30, 30);
            this.buttonAdminUsers.TabIndex = 19;
            this.toolTip1.SetToolTip(this.buttonAdminUsers, "Пользователи");
            this.buttonAdminUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // buttonAdminSettings
            // 
            this.buttonAdminSettings.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonAdminSettings.AllowDrop = true;
            this.buttonAdminSettings.BackColor = System.Drawing.Color.White;
            this.buttonAdminSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonAdminSettings.BackgroundImage")));
            this.buttonAdminSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAdminSettings.Location = new System.Drawing.Point(78, 1);
            this.buttonAdminSettings.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.buttonAdminSettings.Name = "buttonAdminSettings";
            this.buttonAdminSettings.Size = new System.Drawing.Size(30, 30);
            this.buttonAdminSettings.TabIndex = 20;
            this.toolTip1.SetToolTip(this.buttonAdminSettings, "Настройки");
            this.buttonAdminSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonScan
            // 
            this.buttonScan.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonScan.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.buttonScan.BackColor = System.Drawing.Color.White;
            this.buttonScan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonScan.BackgroundImage")));
            this.buttonScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonScan.CausesValidation = false;
            this.buttonScan.Location = new System.Drawing.Point(115, 1);
            this.buttonScan.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(30, 30);
            this.buttonScan.TabIndex = 21;
            this.toolTip1.SetToolTip(this.buttonScan, "Сканировать");
            this.buttonScan.Click += new System.EventHandler(this.ButtonScan_Click);
            // 
            // buttonAddBranch
            // 
            this.buttonAddBranch.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonAddBranch.AllowDrop = true;
            this.buttonAddBranch.BackColor = System.Drawing.Color.White;
            this.buttonAddBranch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonAddBranch.BackgroundImage")));
            this.buttonAddBranch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonAddBranch.Location = new System.Drawing.Point(151, 1);
            this.buttonAddBranch.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonAddBranch.Name = "buttonAddBranch";
            this.buttonAddBranch.Size = new System.Drawing.Size(30, 30);
            this.buttonAddBranch.TabIndex = 22;
            this.toolTip1.SetToolTip(this.buttonAddBranch, "Дабавить филиал");
            this.buttonAddBranch.Click += new System.EventHandler(this.buttonAddBranch_Click);
            // 
            // vitButtonUpdateInfo
            // 
            this.vitButtonUpdateInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.vitButtonUpdateInfo.AllowDrop = true;
            this.vitButtonUpdateInfo.BackColor = System.Drawing.Color.White;
            this.vitButtonUpdateInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("vitButtonUpdateInfo.BackgroundImage")));
            this.vitButtonUpdateInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.vitButtonUpdateInfo.Location = new System.Drawing.Point(188, 1);
            this.vitButtonUpdateInfo.Margin = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.vitButtonUpdateInfo.Name = "vitButtonUpdateInfo";
            this.vitButtonUpdateInfo.Size = new System.Drawing.Size(30, 30);
            this.vitButtonUpdateInfo.TabIndex = 23;
            this.toolTip1.SetToolTip(this.vitButtonUpdateInfo, "Обновить данные в окне программы");
            this.vitButtonUpdateInfo.Click += new System.EventHandler(this.vitButtonUpdateInfo_Click);
            // 
            // panelUserMenu
            // 
            this.panelUserMenu.BackColor = System.Drawing.Color.White;
            this.panelUserMenu.Controls.Add(this.flowLayoutPanelUserMenu);
            this.panelUserMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUserMenu.Location = new System.Drawing.Point(1, 67);
            this.panelUserMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelUserMenu.Name = "panelUserMenu";
            this.panelUserMenu.Size = new System.Drawing.Size(798, 35);
            this.panelUserMenu.TabIndex = 5;
            // 
            // flowLayoutPanelUserMenu
            // 
            this.flowLayoutPanelUserMenu.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanelUserMenu.Controls.Add(this.buttonAdminSettingsDocumentCard);
            this.flowLayoutPanelUserMenu.Controls.Add(this.buttonAdminUsers);
            this.flowLayoutPanelUserMenu.Controls.Add(this.buttonAdminSettings);
            this.flowLayoutPanelUserMenu.Controls.Add(this.buttonScan);
            this.flowLayoutPanelUserMenu.Controls.Add(this.buttonAddBranch);
            this.flowLayoutPanelUserMenu.Controls.Add(this.vitButtonUpdateInfo);
            this.flowLayoutPanelUserMenu.Location = new System.Drawing.Point(3, 2);
            this.flowLayoutPanelUserMenu.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.flowLayoutPanelUserMenu.Name = "flowLayoutPanelUserMenu";
            this.flowLayoutPanelUserMenu.Size = new System.Drawing.Size(267, 33);
            this.flowLayoutPanelUserMenu.TabIndex = 10;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.ContextMenuStrip = this.contextMenuStripTreeView;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(396, 469);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpandAsync);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick_1);
            this.treeView1.MouseHover += new System.EventHandler(this.treeView1_MouseHover);
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
            this.listView1.ContextMenuStrip = this.contextMenuStripTreeView;
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.Location = new System.Drawing.Point(401, 43);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(379, 422);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.MouseHover += new System.EventHandler(this.listView1_MouseHover);
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
            // panelExplorer
            // 
            this.panelExplorer.BackColor = System.Drawing.Color.White;
            this.panelExplorer.Controls.Add(this.pictureBox1);
            this.panelExplorer.Controls.Add(this.textBoxSearch);
            this.panelExplorer.Controls.Add(this.listView1);
            this.panelExplorer.Controls.Add(this.panel1);
            this.panelExplorer.Controls.Add(this.treeView1);
            this.panelExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExplorer.Location = new System.Drawing.Point(1, 102);
            this.panelExplorer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelExplorer.Name = "panelExplorer";
            this.panelExplorer.Size = new System.Drawing.Size(798, 469);
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
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(402, 22);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 10);
            this.panel1.TabIndex = 9;
            // 
            // panelStatusProgress
            // 
            this.panelStatusProgress.BackColor = System.Drawing.Color.White;
            this.panelStatusProgress.Controls.Add(this.windowResizer1);
            this.panelStatusProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusProgress.Location = new System.Drawing.Point(1, 571);
            this.panelStatusProgress.Name = "panelStatusProgress";
            this.panelStatusProgress.Size = new System.Drawing.Size(798, 28);
            this.panelStatusProgress.TabIndex = 9;
            // 
            // windowResizer1
            // 
            this.windowResizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.windowResizer1.BackColor = System.Drawing.Color.Transparent;
            this.windowResizer1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("windowResizer1.BackgroundImage")));
            this.windowResizer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.windowResizer1.Location = new System.Drawing.Point(770, 0);
            this.windowResizer1.Margin = new System.Windows.Forms.Padding(0);
            this.windowResizer1.Name = "windowResizer1";
            this.windowResizer1.Size = new System.Drawing.Size(28, 28);
            this.windowResizer1.TabIndex = 3;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserName.BackColor = System.Drawing.Color.White;
            this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUserName.Location = new System.Drawing.Point(515, 40);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.ReadOnly = true;
            this.textBoxUserName.Size = new System.Drawing.Size(268, 18);
            this.textBoxUserName.TabIndex = 21;
            this.textBoxUserName.Text = "User Name";
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.видToolStripMenuItem,
            this.администратированиеToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(1, 35);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(798, 32);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemShowUserMenu});
            this.видToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(41, 26);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // ToolStripMenuItemShowUserMenu
            // 
            this.ToolStripMenuItemShowUserMenu.Checked = true;
            this.ToolStripMenuItemShowUserMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItemShowUserMenu.Name = "ToolStripMenuItemShowUserMenu";
            this.ToolStripMenuItemShowUserMenu.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemShowUserMenu.Text = "Вывод меню";
            this.ToolStripMenuItemShowUserMenu.CheckedChanged += new System.EventHandler(this.ToolStripMenuItemUserMenu_CheckedChanged);
            // 
            // администратированиеToolStripMenuItem
            // 
            this.администратированиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подключениеКБазеToolStripMenuItem,
            this.пользователиToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.настройкиКарточекToolStripMenuItem});
            this.администратированиеToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.администратированиеToolStripMenuItem.Name = "администратированиеToolStripMenuItem";
            this.администратированиеToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.администратированиеToolStripMenuItem.Text = "Администратирование";
            // 
            // подключениеКБазеToolStripMenuItem
            // 
            this.подключениеКБазеToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("подключениеКБазеToolStripMenuItem.Image")));
            this.подключениеКБазеToolStripMenuItem.Name = "подключениеКБазеToolStripMenuItem";
            this.подключениеКБазеToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.подключениеКБазеToolStripMenuItem.Text = "Подключение к базе";
            // 
            // пользователиToolStripMenuItem
            // 
            this.пользователиToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("пользователиToolStripMenuItem.Image")));
            this.пользователиToolStripMenuItem.Name = "пользователиToolStripMenuItem";
            this.пользователиToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.пользователиToolStripMenuItem.Text = "Пользователи";
            this.пользователиToolStripMenuItem.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("настройкиToolStripMenuItem.Image")));
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // настройкиКарточекToolStripMenuItem
            // 
            this.настройкиКарточекToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("настройкиКарточекToolStripMenuItem.Image")));
            this.настройкиКарточекToolStripMenuItem.Name = "настройкиКарточекToolStripMenuItem";
            this.настройкиКарточекToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.настройкиКарточекToolStripMenuItem.Text = "Настройки карточек";
            this.настройкиКарточекToolStripMenuItem.Click += new System.EventHandler(this.buttonSettingsDocumentCard_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.помощьToolStripMenuItem1});
            this.справкаToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(70, 26);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("оПрограммеToolStripMenuItem.Image")));
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(166, 30);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemAbout_Click);
            // 
            // помощьToolStripMenuItem1
            // 
            this.помощьToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("помощьToolStripMenuItem1.Image")));
            this.помощьToolStripMenuItem1.Name = "помощьToolStripMenuItem1";
            this.помощьToolStripMenuItem1.Size = new System.Drawing.Size(166, 30);
            this.помощьToolStripMenuItem1.Text = "Помощь";
            this.помощьToolStripMenuItem1.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // windowHeader1
            // 
            this.windowHeader1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.windowHeader1.AutoSize = true;
            this.windowHeader1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(207)))), ((int)(((byte)(251)))));
            this.windowHeader1.close = true;
            this.windowHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowHeader1.Location = new System.Drawing.Point(1, 1);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.maximize = true;
            this.windowHeader1.minimize = true;
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.showInTaskbar = true;
            this.windowHeader1.Size = new System.Drawing.Size(798, 34);
            this.windowHeader1.TabIndex = 10;
            // 
            // FormDocumentManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(12)))), ((int)(((byte)(255)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.panelExplorer);
            this.Controls.Add(this.panelUserMenu);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.windowHeader1);
            this.Controls.Add(this.panelStatusProgress);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "FormDocumentManager";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "z";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDocumentManager_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStripTreeView.ResumeLayout(false);
            this.panelUserMenu.ResumeLayout(false);
            this.flowLayoutPanelUserMenu.ResumeLayout(false);
            this.panelExplorer.ResumeLayout(false);
            this.panelExplorer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelStatusProgress.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddFolder;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
        private System.Windows.Forms.Timer timerSearcher;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAdministration;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettingsConnectToDataBase;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettingsDocumentCard;
        /// <summary>
        /// содержит картинки для основного окна программы
        /// </summary>
        public System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettingsAccessGroup;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemUsers;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemUserMenu;
        private System.Windows.Forms.Panel panelUserMenu;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader dateCreate;
        private System.Windows.Forms.ColumnHeader dateChange;
        private System.Windows.Forms.ColumnHeader path;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelExplorer;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAbout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelUserMenu;
        private VitControls.VitButton buttonAdminSettingsDocumentCard;
        private VitControls.VitButton buttonAdminUsers;
        private VitControls.VitButton buttonAdminSettings;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddDocument;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSend;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSendToDesctop;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSendToPrint;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSendToEmail;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSendToFolder;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRequestOriginal;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddDocumentWithCard;
        private System.Windows.Forms.ToolStripMenuItem безКарточкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMove;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRename;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScanToThisFolder;
        private VitControls.WindowHeader windowHeader1;
        private VitControls.VitButton button1;
        private VitControls.VitButton buttonExit;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.Panel panelStatusProgress;
        private VitControls.WindowResizer windowResizer1;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemShowUserMenu;
        private System.Windows.Forms.ToolStripMenuItem администратированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подключениеКБазеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пользователиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиКарточекToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSelectAll;
        private VitControls.VitButton buttonScan;
        private VitControls.VitButton buttonAddBranch;
        private VitControls.VitButton vitButtonUpdateInfo;
    }
}

