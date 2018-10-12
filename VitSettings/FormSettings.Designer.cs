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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneralsSettings = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new VitControls.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new VitControls.Button();
            this.linkLabelRepositoryToDocuments = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageConnectToData = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneralsSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneralsSettings);
            this.tabControl1.Controls.Add(this.tabPageConnectToData);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 41);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 547);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageGeneralsSettings
            // 
            this.tabPageGeneralsSettings.Controls.Add(this.panel1);
            this.tabPageGeneralsSettings.Location = new System.Drawing.Point(4, 26);
            this.tabPageGeneralsSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageGeneralsSettings.Name = "tabPageGeneralsSettings";
            this.tabPageGeneralsSettings.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageGeneralsSettings.Size = new System.Drawing.Size(516, 517);
            this.tabPageGeneralsSettings.TabIndex = 0;
            this.tabPageGeneralsSettings.Text = "Общие";
            this.tabPageGeneralsSettings.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.linkLabel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.linkLabelRepositoryToDocuments);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 194);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(192, 44);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 32);
            this.button2.TabIndex = 5;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(279, 51);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(67, 17);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "linkLabel2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "ggg";
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(191, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 2;
            this.button1.Click += new System.EventHandler(this.buttonRepositoryToDocuments_Click);
            // 
            // linkLabelRepositoryToDocuments
            // 
            this.linkLabelRepositoryToDocuments.AutoSize = true;
            this.linkLabelRepositoryToDocuments.Location = new System.Drawing.Point(279, 12);
            this.linkLabelRepositoryToDocuments.Name = "linkLabelRepositoryToDocuments";
            this.linkLabelRepositoryToDocuments.Size = new System.Drawing.Size(67, 17);
            this.linkLabelRepositoryToDocuments.TabIndex = 1;
            this.linkLabelRepositoryToDocuments.TabStop = true;
            this.linkLabelRepositoryToDocuments.Text = "linkLabel1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Репозиторий документов";
            // 
            // tabPageConnectToData
            // 
            this.tabPageConnectToData.Location = new System.Drawing.Point(4, 26);
            this.tabPageConnectToData.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageConnectToData.Name = "tabPageConnectToData";
            this.tabPageConnectToData.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageConnectToData.Size = new System.Drawing.Size(516, 517);
            this.tabPageConnectToData.TabIndex = 1;
            this.tabPageConnectToData.Text = "Подключение к базе";
            this.tabPageConnectToData.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(516, 517);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // windowHeader1
            // 
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(207)))), ((int)(((byte)(251)))));
            this.windowHeader1.Location = new System.Drawing.Point(0, -3);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.Size = new System.Drawing.Size(524, 33);
            this.windowHeader1.TabIndex = 1;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 588);
            this.Controls.Add(this.windowHeader1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSettings";
            this.Text = "Настроки";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneralsSettings.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneralsSettings;
        private System.Windows.Forms.TabPage tabPageConnectToData;
        private System.Windows.Forms.TabPage tabPage3;
        private VitControls.WindowHeader windowHeader1;
        private System.Windows.Forms.Panel panel1;
        private VitControls.Button button2;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label2;
        private VitControls.Button button1;
        private System.Windows.Forms.LinkLabel linkLabelRepositoryToDocuments;
        private System.Windows.Forms.Label label1;
    }
}