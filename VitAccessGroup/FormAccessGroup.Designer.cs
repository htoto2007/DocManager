namespace VitAccessGroup
{
    partial class FormAccessGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAccessGroup));
            this.windowHeader1 = new VitControls.WindowHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.windowResizer1 = new VitControls.WindowResizer();
            this.buttonClose = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listViewGroups = new System.Windows.Forms.ListView();
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
            this.windowHeader1.Size = new System.Drawing.Size(622, 34);
            this.windowHeader1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.windowResizer1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonEdit);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.listViewGroups);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 405);
            this.panel1.TabIndex = 1;
            // 
            // windowResizer1
            // 
            this.windowResizer1.BackColor = System.Drawing.Color.Transparent;
            this.windowResizer1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("windowResizer1.BackgroundImage")));
            this.windowResizer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.windowResizer1.Location = new System.Drawing.Point(598, 381);
            this.windowResizer1.Margin = new System.Windows.Forms.Padding(0);
            this.windowResizer1.Name = "windowResizer1";
            this.windowResizer1.Size = new System.Drawing.Size(24, 24);
            this.windowResizer1.TabIndex = 5;
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.ImageKey = "icons8-unavailable-480.png";
            this.buttonClose.ImageList = this.imageList1;
            this.buttonClose.Location = new System.Drawing.Point(512, 379);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(83, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-edit-480.png");
            this.imageList1.Images.SetKeyName(1, "icons8-plus-480.png");
            this.imageList1.Images.SetKeyName(2, "icons8-trash-can-480 (1).png");
            this.imageList1.Images.SetKeyName(3, "icons8-unavailable-480.png");
            // 
            // buttonDelete
            // 
            this.buttonDelete.AutoSize = true;
            this.buttonDelete.ImageKey = "icons8-trash-can-480 (1).png";
            this.buttonDelete.ImageList = this.imageList1;
            this.buttonDelete.Location = new System.Drawing.Point(512, 64);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(83, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // buttonEdit
            // 
            this.buttonEdit.AutoSize = true;
            this.buttonEdit.ImageKey = "icons8-edit-480.png";
            this.buttonEdit.ImageList = this.imageList1;
            this.buttonEdit.Location = new System.Drawing.Point(512, 35);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(84, 23);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Изменить";
            this.buttonEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonEdit.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.AutoSize = true;
            this.buttonAdd.ImageKey = "icons8-plus-480.png";
            this.buttonAdd.ImageList = this.imageList1;
            this.buttonAdd.Location = new System.Drawing.Point(512, 6);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(83, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // listViewGroups
            // 
            this.listViewGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewGroups.Location = new System.Drawing.Point(3, 3);
            this.listViewGroups.Name = "listViewGroups";
            this.listViewGroups.Size = new System.Drawing.Size(503, 399);
            this.listViewGroups.TabIndex = 0;
            this.listViewGroups.UseCompatibleStateImageBehavior = false;
            // 
            // FormAccessGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAccessGroup";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.Text = "Группы доступа";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAccessGroup_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VitControls.WindowHeader windowHeader1;
        private System.Windows.Forms.Panel panel1;
        private VitControls.WindowResizer windowResizer1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListView listViewGroups;
    }
}