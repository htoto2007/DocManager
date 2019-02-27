namespace VitFileCard
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonCansel = new System.Windows.Forms.Button();
            this.panelCardProps = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxTypeCard = new System.Windows.Forms.ComboBox();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonCansel);
            this.panel1.Controls.Add(this.panelCardProps);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBoxTypeCard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 592);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.ImageKey = "advice_accept_ok_theaction_10829-24.png";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(3, 549);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 39);
            this.button1.TabIndex = 4;
            this.button1.Text = "Сохранить";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "eliminatetheaction_eliminar_6322-24.png");
            this.imageList1.Images.SetKeyName(1, "advice_accept_ok_theaction_10829-24.png");
            // 
            // buttonCansel
            // 
            this.buttonCansel.AutoSize = true;
            this.buttonCansel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCansel.ImageKey = "eliminatetheaction_eliminar_6322-24.png";
            this.buttonCansel.ImageList = this.imageList1;
            this.buttonCansel.Location = new System.Drawing.Point(649, 549);
            this.buttonCansel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCansel.Name = "buttonCansel";
            this.buttonCansel.Size = new System.Drawing.Size(93, 39);
            this.buttonCansel.TabIndex = 3;
            this.buttonCansel.Text = "Отмена";
            this.buttonCansel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCansel.UseVisualStyleBackColor = true;
            // 
            // panelCardProps
            // 
            this.panelCardProps.AutoScroll = true;
            this.panelCardProps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCardProps.Location = new System.Drawing.Point(0, 69);
            this.panelCardProps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelCardProps.Name = "panelCardProps";
            this.panelCardProps.Size = new System.Drawing.Size(744, 472);
            this.panelCardProps.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип карточки:";
            // 
            // comboBoxTypeCard
            // 
            this.comboBoxTypeCard.FormattingEnabled = true;
            this.comboBoxTypeCard.Location = new System.Drawing.Point(101, 9);
            this.comboBoxTypeCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxTypeCard.Name = "comboBoxTypeCard";
            this.comboBoxTypeCard.Size = new System.Drawing.Size(642, 25);
            this.comboBoxTypeCard.TabIndex = 0;
            this.comboBoxTypeCard.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeCard_SelectedIndexChanged);
            // 
            // windowHeader1
            // 
            this.windowHeader1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
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
            this.windowHeader1.Size = new System.Drawing.Size(745, 34);
            this.windowHeader1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Поля карточки:";
            // 
            // FormFileCard
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCansel;
            this.ClientSize = new System.Drawing.Size(747, 628);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormFileCard";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Карточка документа";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonCansel;
        public System.Windows.Forms.Panel panelCardProps;
        public System.Windows.Forms.ComboBox comboBoxTypeCard;
        private System.Windows.Forms.Label label2;
        private VitControls.WindowHeader windowHeader1;
    }
}