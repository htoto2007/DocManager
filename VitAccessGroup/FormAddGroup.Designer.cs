namespace VitAccessGroup
{
    partial class FormAddGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddGroup));
            this.windowHeader1 = new VitControls.WindowHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxNameGroup = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewAccessToFolders = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxReadFolder = new System.Windows.Forms.CheckBox();
            this.checkBoxAddFolder = new System.Windows.Forms.CheckBox();
            this.checkBoxDeleteFolder = new System.Windows.Forms.CheckBox();
            this.listViewAccessToControls = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
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
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.listViewAccessToControls);
            this.panel1.Controls.Add(this.checkBoxDeleteFolder);
            this.panel1.Controls.Add(this.checkBoxAddFolder);
            this.panel1.Controls.Add(this.checkBoxReadFolder);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.listViewAccessToFolders);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxNameGroup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 444);
            this.panel1.TabIndex = 1;
            // 
            // textBoxNameGroup
            // 
            this.textBoxNameGroup.Location = new System.Drawing.Point(6, 30);
            this.textBoxNameGroup.Name = "textBoxNameGroup";
            this.textBoxNameGroup.Size = new System.Drawing.Size(629, 20);
            this.textBoxNameGroup.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Название группы ";
            // 
            // listViewAccessToFolders
            // 
            this.listViewAccessToFolders.Location = new System.Drawing.Point(6, 73);
            this.listViewAccessToFolders.Name = "listViewAccessToFolders";
            this.listViewAccessToFolders.Size = new System.Drawing.Size(270, 333);
            this.listViewAccessToFolders.TabIndex = 3;
            this.listViewAccessToFolders.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Доступ к папкам";
            // 
            // checkBoxReadFolder
            // 
            this.checkBoxReadFolder.AutoSize = true;
            this.checkBoxReadFolder.Location = new System.Drawing.Point(282, 73);
            this.checkBoxReadFolder.Name = "checkBoxReadFolder";
            this.checkBoxReadFolder.Size = new System.Drawing.Size(63, 17);
            this.checkBoxReadFolder.TabIndex = 5;
            this.checkBoxReadFolder.Text = "Чтение";
            this.checkBoxReadFolder.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddFolder
            // 
            this.checkBoxAddFolder.AutoSize = true;
            this.checkBoxAddFolder.Location = new System.Drawing.Point(282, 96);
            this.checkBoxAddFolder.Name = "checkBoxAddFolder";
            this.checkBoxAddFolder.Size = new System.Drawing.Size(75, 17);
            this.checkBoxAddFolder.TabIndex = 6;
            this.checkBoxAddFolder.Text = "Создание";
            this.checkBoxAddFolder.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeleteFolder
            // 
            this.checkBoxDeleteFolder.AutoSize = true;
            this.checkBoxDeleteFolder.Location = new System.Drawing.Point(282, 119);
            this.checkBoxDeleteFolder.Name = "checkBoxDeleteFolder";
            this.checkBoxDeleteFolder.Size = new System.Drawing.Size(76, 17);
            this.checkBoxDeleteFolder.TabIndex = 7;
            this.checkBoxDeleteFolder.Text = "Удаление";
            this.checkBoxDeleteFolder.UseVisualStyleBackColor = true;
            // 
            // listViewAccessToControls
            // 
            this.listViewAccessToControls.Location = new System.Drawing.Point(363, 73);
            this.listViewAccessToControls.Name = "listViewAccessToControls";
            this.listViewAccessToControls.Size = new System.Drawing.Size(270, 333);
            this.listViewAccessToControls.TabIndex = 8;
            this.listViewAccessToControls.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(360, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Доступ к элементам управления";
            // 
            // buttonAdd
            // 
            this.buttonAdd.AutoSize = true;
            this.buttonAdd.ImageKey = "icons8-ok-480.png";
            this.buttonAdd.ImageList = this.imageList1;
            this.buttonAdd.Location = new System.Drawing.Point(6, 412);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(111, 23);
            this.buttonAdd.TabIndex = 10;
            this.buttonAdd.Text = "Создать группу";
            this.buttonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-ok-480.png");
            this.imageList1.Images.SetKeyName(1, "icons8-unavailable-480.png");
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.ImageKey = "icons8-unavailable-480.png";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(561, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Отмена";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FormAddGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddGroup";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Создание группы доступа";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VitControls.WindowHeader windowHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNameGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewAccessToControls;
        private System.Windows.Forms.CheckBox checkBoxDeleteFolder;
        private System.Windows.Forms.CheckBox checkBoxAddFolder;
        private System.Windows.Forms.CheckBox checkBoxReadFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewAccessToFolders;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonAdd;
    }
}