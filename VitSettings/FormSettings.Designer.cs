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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.tabControlFTP = new System.Windows.Forms.TabControl();
            this.tabPageGeneralsSettings = new System.Windows.Forms.TabPage();
            this.tabPageConnectToData = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDataBaseName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxRootFolderName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new VitControls.Button();
            this.linkLabelRepositoryToDocuments = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new VitControls.Button();
            this.button3 = new VitControls.Button();
            this.tabControlFTP.SuspendLayout();
            this.tabPageGeneralsSettings.SuspendLayout();
            this.tabPageConnectToData.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlFTP
            // 
            this.tabControlFTP.Controls.Add(this.tabPageConnectToData);
            this.tabControlFTP.Controls.Add(this.tabPageGeneralsSettings);
            this.tabControlFTP.Controls.Add(this.tabPageSettings);
            this.tabControlFTP.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlFTP.HotTrack = true;
            this.tabControlFTP.Location = new System.Drawing.Point(0, 33);
            this.tabControlFTP.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlFTP.Name = "tabControlFTP";
            this.tabControlFTP.SelectedIndex = 0;
            this.tabControlFTP.Size = new System.Drawing.Size(375, 307);
            this.tabControlFTP.TabIndex = 0;
            // 
            // tabPageGeneralsSettings
            // 
            this.tabPageGeneralsSettings.Controls.Add(this.textBoxRootFolderName);
            this.tabPageGeneralsSettings.Controls.Add(this.label2);
            this.tabPageGeneralsSettings.Controls.Add(this.button1);
            this.tabPageGeneralsSettings.Controls.Add(this.linkLabelRepositoryToDocuments);
            this.tabPageGeneralsSettings.Controls.Add(this.label1);
            this.tabPageGeneralsSettings.Location = new System.Drawing.Point(4, 26);
            this.tabPageGeneralsSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageGeneralsSettings.Name = "tabPageGeneralsSettings";
            this.tabPageGeneralsSettings.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageGeneralsSettings.Size = new System.Drawing.Size(367, 314);
            this.tabPageGeneralsSettings.TabIndex = 0;
            this.tabPageGeneralsSettings.Text = "Общие";
            this.tabPageGeneralsSettings.UseVisualStyleBackColor = true;
            // 
            // tabPageConnectToData
            // 
            this.tabPageConnectToData.Controls.Add(this.label7);
            this.tabPageConnectToData.Controls.Add(this.textBoxPort);
            this.tabPageConnectToData.Controls.Add(this.label4);
            this.tabPageConnectToData.Controls.Add(this.textBoxPassword);
            this.tabPageConnectToData.Controls.Add(this.label3);
            this.tabPageConnectToData.Controls.Add(this.textBoxLogin);
            this.tabPageConnectToData.Controls.Add(this.label5);
            this.tabPageConnectToData.Controls.Add(this.textBoxDataBaseName);
            this.tabPageConnectToData.Controls.Add(this.label6);
            this.tabPageConnectToData.Controls.Add(this.textBoxServer);
            this.tabPageConnectToData.Location = new System.Drawing.Point(4, 26);
            this.tabPageConnectToData.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageConnectToData.Name = "tabPageConnectToData";
            this.tabPageConnectToData.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageConnectToData.Size = new System.Drawing.Size(367, 277);
            this.tabPageConnectToData.TabIndex = 1;
            this.tabPageConnectToData.Text = "Подключение к базе";
            this.tabPageConnectToData.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Location = new System.Drawing.Point(4, 26);
            this.tabPageSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageSettings.Size = new System.Drawing.Size(367, 314);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Подключение к FTP";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Пароль";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(181, 144);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(178, 25);
            this.textBoxPassword.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Имя пользователя";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(181, 108);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(178, 25);
            this.textBoxLogin.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 78);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "База данных";
            // 
            // textBoxDataBaseName
            // 
            this.textBoxDataBaseName.Location = new System.Drawing.Point(181, 75);
            this.textBoxDataBaseName.Name = "textBoxDataBaseName";
            this.textBoxDataBaseName.Size = new System.Drawing.Size(178, 25);
            this.textBoxDataBaseName.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Сервер";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(181, 9);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(178, 25);
            this.textBoxServer.TabIndex = 10;
            // 
            // windowHeader1
            // 
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(207)))), ((int)(((byte)(251)))));
            this.windowHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowHeader1.Location = new System.Drawing.Point(0, 0);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.maximize = false;
            this.windowHeader1.minimize = false;
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.Size = new System.Drawing.Size(375, 33);
            this.windowHeader1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 45);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "Порт";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(181, 42);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(178, 25);
            this.textBoxPort.TabIndex = 18;
            // 
            // textBoxRootFolderName
            // 
            this.textBoxRootFolderName.Location = new System.Drawing.Point(189, 42);
            this.textBoxRootFolderName.Name = "textBoxRootFolderName";
            this.textBoxRootFolderName.Size = new System.Drawing.Size(162, 25);
            this.textBoxRootFolderName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Имя корневой папки";
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(189, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 7;
            // 
            // linkLabelRepositoryToDocuments
            // 
            this.linkLabelRepositoryToDocuments.AutoSize = true;
            this.linkLabelRepositoryToDocuments.Location = new System.Drawing.Point(228, 12);
            this.linkLabelRepositoryToDocuments.Name = "linkLabelRepositoryToDocuments";
            this.linkLabelRepositoryToDocuments.Size = new System.Drawing.Size(67, 17);
            this.linkLabelRepositoryToDocuments.TabIndex = 6;
            this.linkLabelRepositoryToDocuments.TabStop = true;
            this.linkLabelRepositoryToDocuments.Text = "linkLabel1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Репозиторий документов";
            // 
            // button2
            // 
            this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(330, 347);
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
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 32);
            this.button3.TabIndex = 3;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 391);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tabControlFTP);
            this.Controls.Add(this.windowHeader1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSettings";
            this.Text = "Настроки";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tabControlFTP.ResumeLayout(false);
            this.tabPageGeneralsSettings.ResumeLayout(false);
            this.tabPageGeneralsSettings.PerformLayout();
            this.tabPageConnectToData.ResumeLayout(false);
            this.tabPageConnectToData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlFTP;
        private System.Windows.Forms.TabPage tabPageGeneralsSettings;
        private System.Windows.Forms.TabPage tabPageConnectToData;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDataBaseName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.TextBox textBoxRootFolderName;
        private System.Windows.Forms.Label label2;
        private VitControls.Button button1;
        private System.Windows.Forms.LinkLabel linkLabelRepositoryToDocuments;
        private System.Windows.Forms.Label label1;
        private VitControls.WindowHeader windowHeader1;
        private VitControls.Button button2;
        private VitControls.Button button3;
    }
}