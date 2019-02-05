namespace VitSettings
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.windowHeader1 = new VitControls.WindowHeader();
            this.button2 = new VitControls.VitButton();
            this.button3 = new VitControls.VitButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageSettingsConnectToData = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxConnectDataBasePort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxConnectDataBasePassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxConnectDataBaseLogin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxConnectDataBaseDataBaseName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxConnectDataBaseHost = new System.Windows.Forms.TextBox();
            this.tabPageSettingsGenerals = new System.Windows.Forms.TabPage();
            this.tabPageSettingsFTP = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxFTPPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxFTPHost = new System.Windows.Forms.TextBox();
            this.tabPageEmailEmail = new System.Windows.Forms.TabPage();
            this.textBoxFTPPathForTmp = new System.Windows.Forms.TextBox();
            this.textBoxFTPPathForOpenFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxMailServerInAdres = new System.Windows.Forms.TextBox();
            this.textBoxMailServerInPort = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxMailServerOutAdres = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxMailServerOutPort = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPageSettingsConnectToData.SuspendLayout();
            this.tabPageSettingsFTP.SuspendLayout();
            this.tabPageEmailEmail.SuspendLayout();
            this.SuspendLayout();
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
            this.windowHeader1.maximize = false;
            this.windowHeader1.minimize = false;
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.showInTaskbar = false;
            this.windowHeader1.Size = new System.Drawing.Size(638, 33);
            this.windowHeader1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(330, 347);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 32);
            this.button2.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Location = new System.Drawing.Point(13, 346);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 32);
            this.button3.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.tabControlSettings);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 445);
            this.panel1.TabIndex = 4;
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabPageSettingsGenerals);
            this.tabControlSettings.Controls.Add(this.tabPageSettingsConnectToData);
            this.tabControlSettings.Controls.Add(this.tabPageSettingsFTP);
            this.tabControlSettings.Controls.Add(this.tabPageEmailEmail);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlSettings.HotTrack = true;
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(638, 412);
            this.tabControlSettings.TabIndex = 1;
            // 
            // tabPageSettingsConnectToData
            // 
            this.tabPageSettingsConnectToData.Controls.Add(this.label7);
            this.tabPageSettingsConnectToData.Controls.Add(this.textBoxConnectDataBasePort);
            this.tabPageSettingsConnectToData.Controls.Add(this.label4);
            this.tabPageSettingsConnectToData.Controls.Add(this.textBoxConnectDataBasePassword);
            this.tabPageSettingsConnectToData.Controls.Add(this.label3);
            this.tabPageSettingsConnectToData.Controls.Add(this.textBoxConnectDataBaseLogin);
            this.tabPageSettingsConnectToData.Controls.Add(this.label5);
            this.tabPageSettingsConnectToData.Controls.Add(this.textBoxConnectDataBaseDataBaseName);
            this.tabPageSettingsConnectToData.Controls.Add(this.label6);
            this.tabPageSettingsConnectToData.Controls.Add(this.textBoxConnectDataBaseHost);
            this.tabPageSettingsConnectToData.Location = new System.Drawing.Point(4, 26);
            this.tabPageSettingsConnectToData.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageSettingsConnectToData.Name = "tabPageSettingsConnectToData";
            this.tabPageSettingsConnectToData.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageSettingsConnectToData.Size = new System.Drawing.Size(630, 382);
            this.tabPageSettingsConnectToData.TabIndex = 1;
            this.tabPageSettingsConnectToData.Text = "Подключение к базе";
            this.tabPageSettingsConnectToData.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "Порт";
            // 
            // textBoxConnectDataBasePort
            // 
            this.textBoxConnectDataBasePort.Location = new System.Drawing.Point(312, 35);
            this.textBoxConnectDataBasePort.Name = "textBoxConnectDataBasePort";
            this.textBoxConnectDataBasePort.Size = new System.Drawing.Size(311, 25);
            this.textBoxConnectDataBasePort.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 134);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Пароль";
            // 
            // textBoxConnectDataBasePassword
            // 
            this.textBoxConnectDataBasePassword.Location = new System.Drawing.Point(312, 128);
            this.textBoxConnectDataBasePassword.Name = "textBoxConnectDataBasePassword";
            this.textBoxConnectDataBasePassword.Size = new System.Drawing.Size(311, 25);
            this.textBoxConnectDataBasePassword.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Имя пользователя";
            // 
            // textBoxConnectDataBaseLogin
            // 
            this.textBoxConnectDataBaseLogin.Location = new System.Drawing.Point(312, 97);
            this.textBoxConnectDataBaseLogin.Name = "textBoxConnectDataBaseLogin";
            this.textBoxConnectDataBaseLogin.Size = new System.Drawing.Size(311, 25);
            this.textBoxConnectDataBaseLogin.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "База данных";
            // 
            // textBoxConnectDataBaseDataBaseName
            // 
            this.textBoxConnectDataBaseDataBaseName.Location = new System.Drawing.Point(312, 66);
            this.textBoxConnectDataBaseDataBaseName.Name = "textBoxConnectDataBaseDataBaseName";
            this.textBoxConnectDataBaseDataBaseName.Size = new System.Drawing.Size(311, 25);
            this.textBoxConnectDataBaseDataBaseName.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Сервер";
            // 
            // textBoxConnectDataBaseHost
            // 
            this.textBoxConnectDataBaseHost.Location = new System.Drawing.Point(312, 7);
            this.textBoxConnectDataBaseHost.Name = "textBoxConnectDataBaseHost";
            this.textBoxConnectDataBaseHost.Size = new System.Drawing.Size(311, 25);
            this.textBoxConnectDataBaseHost.TabIndex = 10;
            // 
            // tabPageSettingsGenerals
            // 
            this.tabPageSettingsGenerals.Location = new System.Drawing.Point(4, 26);
            this.tabPageSettingsGenerals.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSettingsGenerals.Name = "tabPageSettingsGenerals";
            this.tabPageSettingsGenerals.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageSettingsGenerals.Size = new System.Drawing.Size(630, 382);
            this.tabPageSettingsGenerals.TabIndex = 0;
            this.tabPageSettingsGenerals.Text = "Общие";
            this.tabPageSettingsGenerals.UseVisualStyleBackColor = true;
            // 
            // tabPageSettingsFTP
            // 
            this.tabPageSettingsFTP.Controls.Add(this.button4);
            this.tabPageSettingsFTP.Controls.Add(this.button1);
            this.tabPageSettingsFTP.Controls.Add(this.textBoxFTPPathForTmp);
            this.tabPageSettingsFTP.Controls.Add(this.textBoxFTPPathForOpenFile);
            this.tabPageSettingsFTP.Controls.Add(this.label11);
            this.tabPageSettingsFTP.Controls.Add(this.label10);
            this.tabPageSettingsFTP.Controls.Add(this.label9);
            this.tabPageSettingsFTP.Controls.Add(this.textBoxFTPPort);
            this.tabPageSettingsFTP.Controls.Add(this.label8);
            this.tabPageSettingsFTP.Controls.Add(this.textBoxFTPHost);
            this.tabPageSettingsFTP.Location = new System.Drawing.Point(4, 26);
            this.tabPageSettingsFTP.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageSettingsFTP.Name = "tabPageSettingsFTP";
            this.tabPageSettingsFTP.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageSettingsFTP.Size = new System.Drawing.Size(630, 382);
            this.tabPageSettingsFTP.TabIndex = 2;
            this.tabPageSettingsFTP.Text = "Подключение к FTP";
            this.tabPageSettingsFTP.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(157, 17);
            this.label11.TabIndex = 7;
            this.label11.Text = "Для временных файлов";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(168, 17);
            this.label10.TabIndex = 5;
            this.label10.Text = "Для открываемыхфайлов";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "Порт";
            // 
            // textBoxFTPPort
            // 
            this.textBoxFTPPort.Location = new System.Drawing.Point(312, 38);
            this.textBoxFTPPort.Name = "textBoxFTPPort";
            this.textBoxFTPPort.Size = new System.Drawing.Size(311, 25);
            this.textBoxFTPPort.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Хост (IP или URL)";
            // 
            // textBoxFTPHost
            // 
            this.textBoxFTPHost.Location = new System.Drawing.Point(312, 7);
            this.textBoxFTPHost.Name = "textBoxFTPHost";
            this.textBoxFTPHost.Size = new System.Drawing.Size(311, 25);
            this.textBoxFTPHost.TabIndex = 0;
            // 
            // tabPageEmailEmail
            // 
            this.tabPageEmailEmail.Controls.Add(this.textBoxMailServerOutPort);
            this.tabPageEmailEmail.Controls.Add(this.label15);
            this.tabPageEmailEmail.Controls.Add(this.textBoxMailServerOutAdres);
            this.tabPageEmailEmail.Controls.Add(this.label14);
            this.tabPageEmailEmail.Controls.Add(this.textBoxMailServerInPort);
            this.tabPageEmailEmail.Controls.Add(this.label13);
            this.tabPageEmailEmail.Controls.Add(this.textBoxMailServerInAdres);
            this.tabPageEmailEmail.Controls.Add(this.label12);
            this.tabPageEmailEmail.Location = new System.Drawing.Point(4, 26);
            this.tabPageEmailEmail.Name = "tabPageEmailEmail";
            this.tabPageEmailEmail.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEmailEmail.Size = new System.Drawing.Size(630, 382);
            this.tabPageEmailEmail.TabIndex = 3;
            this.tabPageEmailEmail.Text = "Почта";
            this.tabPageEmailEmail.UseVisualStyleBackColor = true;
            // 
            // textBoxFTPPathForTmp
            // 
            this.textBoxFTPPathForTmp.Location = new System.Drawing.Point(312, 100);
            this.textBoxFTPPathForTmp.Name = "textBoxFTPPathForTmp";
            this.textBoxFTPPathForTmp.Size = new System.Drawing.Size(278, 25);
            this.textBoxFTPPathForTmp.TabIndex = 9;
            // 
            // textBoxFTPPathForOpenFile
            // 
            this.textBoxFTPPathForOpenFile.Location = new System.Drawing.Point(312, 69);
            this.textBoxFTPPathForOpenFile.Name = "textBoxFTPPathForOpenFile";
            this.textBoxFTPPathForOpenFile.Size = new System.Drawing.Size(278, 25);
            this.textBoxFTPPathForOpenFile.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.ImageKey = "icons8-opened-folder-96.png";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(596, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 22);
            this.button1.TabIndex = 10;
            this.toolTip1.SetToolTip(this.button1, "Выбрать папку");
            this.button1.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-opened-folder-96.png");
            this.imageList1.Images.SetKeyName(1, "icons8-ok-480.png");
            this.imageList1.Images.SetKeyName(2, "icons8-unavailable-480.png");
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button4.ImageKey = "icons8-opened-folder-96.png";
            this.button4.ImageList = this.imageList1;
            this.button4.Location = new System.Drawing.Point(596, 107);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(22, 22);
            this.button4.TabIndex = 11;
            this.toolTip1.SetToolTip(this.button4, "Выбрать папку");
            this.button4.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // buttonOk
            // 
            this.buttonOk.AutoSize = true;
            this.buttonOk.ImageKey = "icons8-ok-480.png";
            this.buttonOk.ImageList = this.imageList1;
            this.buttonOk.Location = new System.Drawing.Point(4, 414);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(106, 27);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Приментьб";
            this.buttonOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClose.ImageKey = "icons8-unavailable-480.png";
            this.buttonClose.ImageList = this.imageList1;
            this.buttonClose.Location = new System.Drawing.Point(548, 414);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(86, 27);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(209, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Сервенр входящей почты IMAP";
            // 
            // textBoxMailServerInAdres
            // 
            this.textBoxMailServerInAdres.Location = new System.Drawing.Point(313, 6);
            this.textBoxMailServerInAdres.Name = "textBoxMailServerInAdres";
            this.textBoxMailServerInAdres.Size = new System.Drawing.Size(311, 25);
            this.textBoxMailServerInAdres.TabIndex = 1;
            // 
            // textBoxMailServerInPort
            // 
            this.textBoxMailServerInPort.Location = new System.Drawing.Point(313, 37);
            this.textBoxMailServerInPort.Name = "textBoxMailServerInPort";
            this.textBoxMailServerInPort.Size = new System.Drawing.Size(311, 25);
            this.textBoxMailServerInPort.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(242, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Порт сервера входящей почты IMAP";
            // 
            // textBoxMailServerOutAdres
            // 
            this.textBoxMailServerOutAdres.Location = new System.Drawing.Point(313, 68);
            this.textBoxMailServerOutAdres.Name = "textBoxMailServerOutAdres";
            this.textBoxMailServerOutAdres.Size = new System.Drawing.Size(311, 25);
            this.textBoxMailServerOutAdres.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(217, 17);
            this.label14.TabIndex = 4;
            this.label14.Text = "Сервенр исходящей почты SMTP";
            // 
            // textBoxMailServerOutPort
            // 
            this.textBoxMailServerOutPort.Location = new System.Drawing.Point(313, 99);
            this.textBoxMailServerOutPort.Name = "textBoxMailServerOutPort";
            this.textBoxMailServerOutPort.Size = new System.Drawing.Size(311, 25);
            this.textBoxMailServerOutPort.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 102);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(250, 17);
            this.label15.TabIndex = 6;
            this.label15.Text = "Порт сервера исходящей почты SMTP";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSettings";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = false;
            this.Text = "Настроки";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageSettingsConnectToData.ResumeLayout(false);
            this.tabPageSettingsConnectToData.PerformLayout();
            this.tabPageSettingsFTP.ResumeLayout(false);
            this.tabPageSettingsFTP.PerformLayout();
            this.tabPageEmailEmail.ResumeLayout(false);
            this.tabPageEmailEmail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private VitControls.WindowHeader windowHeader1;
        private VitControls.VitButton button2;
        private VitControls.VitButton button3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxConnectDataBasePort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxConnectDataBasePassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxConnectDataBaseLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxConnectDataBaseDataBaseName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPageSettingsGenerals;
        private System.Windows.Forms.TabPage tabPageSettingsFTP;
        private System.Windows.Forms.TabPage tabPageEmailEmail;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TabControl tabControlSettings;
        public System.Windows.Forms.TabPage tabPageSettingsConnectToData;
        public System.Windows.Forms.TextBox textBoxConnectDataBaseHost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxFTPPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxFTPHost;
        private System.Windows.Forms.TextBox textBoxFTPPathForTmp;
        private System.Windows.Forms.TextBox textBoxFTPPathForOpenFile;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxMailServerOutPort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxMailServerOutAdres;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxMailServerInPort;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxMailServerInAdres;
        private System.Windows.Forms.Label label12;
    }
}