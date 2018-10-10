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
            this.textBoxNameFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTreePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonChangeLocation = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonCreateFile = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // textBoxNameFile
            // 
            this.textBoxNameFile.Location = new System.Drawing.Point(12, 25);
            this.textBoxNameFile.MaxLength = 60;
            this.textBoxNameFile.Name = "textBoxNameFile";
            this.textBoxNameFile.Size = new System.Drawing.Size(535, 20);
            this.textBoxNameFile.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Введите название файла";
            // 
            // textBoxTreePath
            // 
            this.textBoxTreePath.Location = new System.Drawing.Point(12, 64);
            this.textBoxTreePath.MaxLength = 30000;
            this.textBoxTreePath.Name = "textBoxTreePath";
            this.textBoxTreePath.ReadOnly = true;
            this.textBoxTreePath.Size = new System.Drawing.Size(494, 20);
            this.textBoxTreePath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Выберите путь до папки, в которую следует поместить фаил";
            // 
            // buttonChangeLocation
            // 
            this.buttonChangeLocation.BackColor = System.Drawing.Color.White;
            this.buttonChangeLocation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonChangeLocation.ImageIndex = 0;
            this.buttonChangeLocation.ImageList = this.imageList1;
            this.buttonChangeLocation.Location = new System.Drawing.Point(513, 60);
            this.buttonChangeLocation.Name = "buttonChangeLocation";
            this.buttonChangeLocation.Size = new System.Drawing.Size(34, 24);
            this.buttonChangeLocation.TabIndex = 4;
            this.buttonChangeLocation.UseVisualStyleBackColor = false;
            this.buttonChangeLocation.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Flat-Folder-icon.png");
            // 
            // buttonCreateFile
            // 
            this.buttonCreateFile.Location = new System.Drawing.Point(391, 432);
            this.buttonCreateFile.Name = "buttonCreateFile";
            this.buttonCreateFile.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateFile.TabIndex = 5;
            this.buttonCreateFile.Text = "Создать";
            this.buttonCreateFile.UseVisualStyleBackColor = true;
            this.buttonCreateFile.Click += new System.EventHandler(this.buttonCreateFile_Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(472, 432);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "отмена";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 111);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(535, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            this.comboBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBox1_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Выберите тип карточки";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 288);
            this.panel1.TabIndex = 8;
            // 
            // FormFiles
            // 
            this.AcceptButton = this.buttonCreateFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button3;
            this.ClientSize = new System.Drawing.Size(563, 467);
            this.ControlBox = false;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonCreateFile);
            this.Controls.Add(this.buttonChangeLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTreePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNameFile);
            this.Name = "FormFiles";
            this.Text = "Создание документа";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.TextBox textBoxNameFile;
        public System.Windows.Forms.Button buttonCreateFile;
        public System.Windows.Forms.TextBox textBoxTreePath;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonChangeLocation;
    }
}