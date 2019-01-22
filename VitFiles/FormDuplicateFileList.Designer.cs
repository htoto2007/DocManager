namespace VitFiles
{
    partial class FormDuplicateFileList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDuplicateFileList));
            this.windowHeader1 = new VitControls.WindowHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCreateIndex = new System.Windows.Forms.Button();
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
            this.windowHeader1.Size = new System.Drawing.Size(638, 34);
            this.windowHeader1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonCreateIndex);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 204);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.ImageKey = "icons8-ok-480.png";
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(3, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "Заменить дубликаты";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-ok-480.png");
            this.imageList1.Images.SetKeyName(1, "icons8-unavailable-480.png");
            this.imageList1.Images.SetKeyName(2, "info.png");
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.ImageKey = "icons8-unavailable-480.png";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(491, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Отменить операцию";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(0, 32);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(638, 133);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(492, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Некоторые файлы уже существуют в выбраной вами директориии. Хотите их заменть?";
            // 
            // buttonCreateIndex
            // 
            this.buttonCreateIndex.AutoSize = true;
            this.buttonCreateIndex.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.buttonCreateIndex.ImageKey = "info.png";
            this.buttonCreateIndex.ImageList = this.imageList1;
            this.buttonCreateIndex.Location = new System.Drawing.Point(160, 171);
            this.buttonCreateIndex.Name = "buttonCreateIndex";
            this.buttonCreateIndex.Size = new System.Drawing.Size(198, 30);
            this.buttonCreateIndex.TabIndex = 4;
            this.buttonCreateIndex.Text = "Добавить индекс к названию";
            this.buttonCreateIndex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCreateIndex.UseVisualStyleBackColor = true;
            this.buttonCreateIndex.Click += new System.EventHandler(this.buttonCreateIndex_Click);
            // 
            // FormDuplicateFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 240);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDuplicateFileList";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дупликаты файлов";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VitControls.WindowHeader windowHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCreateIndex;
    }
}