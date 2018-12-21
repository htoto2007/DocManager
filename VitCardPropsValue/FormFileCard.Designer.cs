﻿namespace VitCardPropsValue
{
    partial class FormFileCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFileCard));
            this.windowHeader1 = new VitControls.WindowHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panelCardProps = new System.Windows.Forms.Panel();
            this.comboBoxTypeCard = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panelCardProps);
            this.panel1.Controls.Add(this.comboBoxTypeCard);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 444);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.ImageKey = "icons8-ok-480.png";
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(3, 411);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "Сохранить";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
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
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.ImageKey = "icons8-unavailable-480.png";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(555, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "Отмена";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panelCardProps
            // 
            this.panelCardProps.AutoScroll = true;
            this.panelCardProps.Location = new System.Drawing.Point(3, 78);
            this.panelCardProps.Name = "panelCardProps";
            this.panelCardProps.Size = new System.Drawing.Size(632, 327);
            this.panelCardProps.TabIndex = 2;
            // 
            // comboBoxTypeCard
            // 
            this.comboBoxTypeCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxTypeCard.FormattingEnabled = true;
            this.comboBoxTypeCard.Location = new System.Drawing.Point(0, 23);
            this.comboBoxTypeCard.Name = "comboBoxTypeCard";
            this.comboBoxTypeCard.Size = new System.Drawing.Size(638, 21);
            this.comboBoxTypeCard.TabIndex = 0;
            this.comboBoxTypeCard.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeCard_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(136, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите тип карточки";
            // 
            // FormFileCard
            // 
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFileCard";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Карточка документа";
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
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panelCardProps;
        public System.Windows.Forms.ComboBox comboBoxTypeCard;
    }
}