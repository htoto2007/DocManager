namespace VitTree
{
    partial class FormFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFiles));
            this.windowHeader1 = new VitControls.WindowHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonChangeLocation = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTreePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNameFile = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonCreateFile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowHeader1
            // 
            this.windowHeader1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.windowHeader1.AutoSize = true;
            this.windowHeader1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(207)))), ((int)(((byte)(251)))));
            this.windowHeader1.close = false;
            this.windowHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowHeader1.Location = new System.Drawing.Point(1, 1);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.maximize = false;
            this.windowHeader1.minimize = false;
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.showInTaskbar = false;
            this.windowHeader1.Size = new System.Drawing.Size(561, 34);
            this.windowHeader1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonChangeLocation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxTreePath);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxNameFile);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.buttonCreateFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 513);
            this.panel1.TabIndex = 1;
            // 
            // buttonChangeLocation
            // 
            this.buttonChangeLocation.AutoSize = true;
            this.buttonChangeLocation.BackColor = System.Drawing.Color.White;
            this.buttonChangeLocation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonChangeLocation.ImageKey = "folder_yellow_10921.png";
            this.buttonChangeLocation.ImageList = this.imageList1;
            this.buttonChangeLocation.Location = new System.Drawing.Point(514, 63);
            this.buttonChangeLocation.Name = "buttonChangeLocation";
            this.buttonChangeLocation.Size = new System.Drawing.Size(34, 30);
            this.buttonChangeLocation.TabIndex = 14;
            this.buttonChangeLocation.UseVisualStyleBackColor = false;
            this.buttonChangeLocation.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder_yellow_10921.png");
            this.imageList1.Images.SetKeyName(1, "eliminatetheaction_eliminar_6322.png");
            this.imageList1.Images.SetKeyName(2, "solicit_accept_check_ok_theaction_6340.png");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Выберите путь до папки, в которую следует поместить фаил";
            // 
            // textBoxTreePath
            // 
            this.textBoxTreePath.Location = new System.Drawing.Point(10, 69);
            this.textBoxTreePath.MaxLength = 30000;
            this.textBoxTreePath.Name = "textBoxTreePath";
            this.textBoxTreePath.ReadOnly = true;
            this.textBoxTreePath.Size = new System.Drawing.Size(494, 20);
            this.textBoxTreePath.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Введите название файла";
            // 
            // textBoxNameFile
            // 
            this.textBoxNameFile.Location = new System.Drawing.Point(10, 30);
            this.textBoxNameFile.MaxLength = 60;
            this.textBoxNameFile.Name = "textBoxNameFile";
            this.textBoxNameFile.Size = new System.Drawing.Size(535, 20);
            this.textBoxNameFile.TabIndex = 10;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 111);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(535, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Выберите тип карточки";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(13, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(535, 312);
            this.panel2.TabIndex = 18;
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.ImageKey = "eliminatetheaction_eliminar_6322.png";
            this.button3.ImageList = this.imageList1;
            this.button3.Location = new System.Drawing.Point(470, 456);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(78, 30);
            this.button3.TabIndex = 16;
            this.button3.Text = "отмена";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonCreateFile
            // 
            this.buttonCreateFile.AutoSize = true;
            this.buttonCreateFile.ImageKey = "solicit_accept_check_ok_theaction_6340.png";
            this.buttonCreateFile.ImageList = this.imageList1;
            this.buttonCreateFile.Location = new System.Drawing.Point(13, 456);
            this.buttonCreateFile.Name = "buttonCreateFile";
            this.buttonCreateFile.Size = new System.Drawing.Size(83, 30);
            this.buttonCreateFile.TabIndex = 15;
            this.buttonCreateFile.Text = "Создать";
            this.buttonCreateFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCreateFile.UseVisualStyleBackColor = true;
            this.buttonCreateFile.Click += new System.EventHandler(this.buttonCreateFile_Click);
            // 
            // FormFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(12)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(563, 549);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFiles";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Создание документа";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VitControls.WindowHeader windowHeader1;
        public System.Windows.Forms.Button buttonChangeLocation;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxTreePath;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxNameFile;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button buttonCreateFile;
        public System.Windows.Forms.Panel panel1;
    }
}