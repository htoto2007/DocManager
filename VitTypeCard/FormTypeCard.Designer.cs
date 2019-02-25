namespace VitTypeCard
{
    partial class FormTypeCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypeCard));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDeleteType = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonChangeType = new System.Windows.Forms.Button();
            this.buttonAddType = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewCardProps = new System.Windows.Forms.ListView();
            this.listViewTypeCards = new System.Windows.Forms.ListView();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonDeleteType);
            this.panel1.Controls.Add(this.buttonChangeType);
            this.panel1.Controls.Add(this.buttonAddType);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listViewCardProps);
            this.panel1.Controls.Add(this.listViewTypeCards);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 444);
            this.panel1.TabIndex = 1;
            // 
            // buttonDeleteType
            // 
            this.buttonDeleteType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteType.AutoSize = true;
            this.buttonDeleteType.ImageKey = "cancellationbutton_exitneartheaction_botondecancelacion_salida_6335.png";
            this.buttonDeleteType.ImageList = this.imageList1;
            this.buttonDeleteType.Location = new System.Drawing.Point(524, 111);
            this.buttonDeleteType.Name = "buttonDeleteType";
            this.buttonDeleteType.Size = new System.Drawing.Size(111, 30);
            this.buttonDeleteType.TabIndex = 10;
            this.buttonDeleteType.Text = "Удалить тип";
            this.buttonDeleteType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDeleteType.UseVisualStyleBackColor = true;
            this.buttonDeleteType.Click += new System.EventHandler(this.buttonDeleteType_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cancellationbutton_exitneartheaction_botondecancelacion_salida_6335.png");
            this.imageList1.Images.SetKeyName(1, "editionadded_theaction_6325.png");
            this.imageList1.Images.SetKeyName(2, "eliminatetheaction_eliminar_6322.png");
            this.imageList1.Images.SetKeyName(3, "solicit_accept_check_ok_theaction_6340.png");
            this.imageList1.Images.SetKeyName(4, "edit_pencil_6320.png");
            // 
            // buttonChangeType
            // 
            this.buttonChangeType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChangeType.AutoSize = true;
            this.buttonChangeType.ImageKey = "edit_pencil_6320.png";
            this.buttonChangeType.ImageList = this.imageList1;
            this.buttonChangeType.Location = new System.Drawing.Point(524, 75);
            this.buttonChangeType.Name = "buttonChangeType";
            this.buttonChangeType.Size = new System.Drawing.Size(112, 30);
            this.buttonChangeType.TabIndex = 9;
            this.buttonChangeType.Text = "Изменить тип";
            this.buttonChangeType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonChangeType.UseVisualStyleBackColor = true;
            this.buttonChangeType.Click += new System.EventHandler(this.buttonChangeType_Click);
            // 
            // buttonAddType
            // 
            this.buttonAddType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddType.AutoSize = true;
            this.buttonAddType.ImageIndex = 1;
            this.buttonAddType.ImageList = this.imageList1;
            this.buttonAddType.Location = new System.Drawing.Point(524, 39);
            this.buttonAddType.Name = "buttonAddType";
            this.buttonAddType.Size = new System.Drawing.Size(111, 30);
            this.buttonAddType.TabIndex = 8;
            this.buttonAddType.Text = "Добавить тип";
            this.buttonAddType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAddType.UseVisualStyleBackColor = true;
            this.buttonAddType.Click += new System.EventHandler(this.buttonAddType_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.AutoSize = true;
            this.buttonClose.ImageKey = "eliminatetheaction_eliminar_6322.png";
            this.buttonClose.ImageList = this.imageList1;
            this.buttonClose.Location = new System.Drawing.Point(524, 411);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(111, 30);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Свойства типов";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Типы карт";
            // 
            // listViewCardProps
            // 
            this.listViewCardProps.Location = new System.Drawing.Point(246, 39);
            this.listViewCardProps.Name = "listViewCardProps";
            this.listViewCardProps.Size = new System.Drawing.Size(272, 402);
            this.listViewCardProps.TabIndex = 4;
            this.listViewCardProps.UseCompatibleStateImageBehavior = false;
            // 
            // listViewTypeCards
            // 
            this.listViewTypeCards.Location = new System.Drawing.Point(3, 39);
            this.listViewTypeCards.Name = "listViewTypeCards";
            this.listViewTypeCards.Size = new System.Drawing.Size(237, 402);
            this.listViewTypeCards.TabIndex = 0;
            this.listViewTypeCards.UseCompatibleStateImageBehavior = false;
            this.listViewTypeCards.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewTypeCards_ItemSelectionChanged);
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
            this.windowHeader1.Size = new System.Drawing.Size(638, 34);
            this.windowHeader1.TabIndex = 2;
            // 
            // FormTypeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTypeCard";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Типы карточек документов";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listViewTypeCards;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewCardProps;
        private VitControls.WindowHeader windowHeader1;
        private System.Windows.Forms.Button buttonDeleteType;
        private System.Windows.Forms.Button buttonChangeType;
        private System.Windows.Forms.Button buttonAddType;
    }
}