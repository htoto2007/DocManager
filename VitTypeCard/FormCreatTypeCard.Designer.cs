namespace VitTypeCard
{
    partial class FormCreatTypeCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreatTypeCard));
            this.windowHeader1 = new VitControls.WindowHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonAddProp = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelProps = new System.Windows.Forms.Panel();
            this.buttonDeleteProp_0 = new System.Windows.Forms.Button();
            this.comboBoxType_0 = new System.Windows.Forms.ComboBox();
            this.textBoxName_0 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelProps.SuspendLayout();
            this.SuspendLayout();
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
            this.windowHeader1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.buttonAddProp);
            this.panel1.Controls.Add(this.panelProps);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 444);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Название типа карточки докусента";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(621, 20);
            this.textBox1.TabIndex = 11;
            // 
            // buttonAddProp
            // 
            this.buttonAddProp.AutoSize = true;
            this.buttonAddProp.ImageKey = "editionadded_theaction_6325.png";
            this.buttonAddProp.ImageList = this.imageList1;
            this.buttonAddProp.Location = new System.Drawing.Point(3, 53);
            this.buttonAddProp.Name = "buttonAddProp";
            this.buttonAddProp.Size = new System.Drawing.Size(118, 30);
            this.buttonAddProp.TabIndex = 15;
            this.buttonAddProp.Text = "Добавить поле";
            this.buttonAddProp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAddProp.UseVisualStyleBackColor = true;
            this.buttonAddProp.Click += new System.EventHandler(this.buttonAddProp_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "editionadded_theaction_6325.png");
            this.imageList1.Images.SetKeyName(1, "cancellationbutton_exitneartheaction_botondecancelacion_salida_6335.png");
            this.imageList1.Images.SetKeyName(2, "eliminatetheaction_eliminar_6322.png");
            this.imageList1.Images.SetKeyName(3, "solicit_accept_check_ok_theaction_6340.png");
            // 
            // panelProps
            // 
            this.panelProps.AutoScroll = true;
            this.panelProps.Controls.Add(this.buttonDeleteProp_0);
            this.panelProps.Controls.Add(this.comboBoxType_0);
            this.panelProps.Controls.Add(this.textBoxName_0);
            this.panelProps.Location = new System.Drawing.Point(3, 108);
            this.panelProps.Name = "panelProps";
            this.panelProps.Size = new System.Drawing.Size(632, 295);
            this.panelProps.TabIndex = 17;
            // 
            // buttonDeleteProp_0
            // 
            this.buttonDeleteProp_0.AutoSize = true;
            this.buttonDeleteProp_0.Enabled = false;
            this.buttonDeleteProp_0.ImageKey = "cancellationbutton_exitneartheaction_botondecancelacion_salida_6335.png";
            this.buttonDeleteProp_0.ImageList = this.imageList1;
            this.buttonDeleteProp_0.Location = new System.Drawing.Point(564, 7);
            this.buttonDeleteProp_0.Margin = new System.Windows.Forms.Padding(1);
            this.buttonDeleteProp_0.Name = "buttonDeleteProp_0";
            this.buttonDeleteProp_0.Size = new System.Drawing.Size(30, 30);
            this.buttonDeleteProp_0.TabIndex = 7;
            this.buttonDeleteProp_0.UseVisualStyleBackColor = true;
            this.buttonDeleteProp_0.Click += new System.EventHandler(this.buttonDeleteProp_Click);
            // 
            // comboBoxType_0
            // 
            this.comboBoxType_0.FormattingEnabled = true;
            this.comboBoxType_0.Items.AddRange(new object[] {
            "Строковый",
            "Числовой",
            "Дата",
            "Дата и время",
            "Логический",
            "Текстовой"});
            this.comboBoxType_0.Location = new System.Drawing.Point(309, 12);
            this.comboBoxType_0.Name = "comboBoxType_0";
            this.comboBoxType_0.Size = new System.Drawing.Size(251, 21);
            this.comboBoxType_0.TabIndex = 6;
            // 
            // textBoxName_0
            // 
            this.textBoxName_0.Location = new System.Drawing.Point(3, 13);
            this.textBoxName_0.Name = "textBoxName_0";
            this.textBoxName_0.Size = new System.Drawing.Size(300, 20);
            this.textBoxName_0.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.ImageKey = "eliminatetheaction_eliminar_6322.png";
            this.button3.ImageList = this.imageList1;
            this.button3.Location = new System.Drawing.Point(552, 409);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 30);
            this.button3.TabIndex = 19;
            this.button3.Text = "Отмена";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.ImageKey = "solicit_accept_check_ok_theaction_6340.png";
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(3, 409);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 30);
            this.button2.TabIndex = 18;
            this.button2.Text = "Создать тип";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(309, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Тип поля";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Описание свойства";
            // 
            // FormCreatTypeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeader1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCreatTypeCard";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание типов карточек документа";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelProps.ResumeLayout(false);
            this.panelProps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VitControls.WindowHeader windowHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonAddProp;
        private System.Windows.Forms.Panel panelProps;
        private System.Windows.Forms.Button buttonDeleteProp_0;
        private System.Windows.Forms.ComboBox comboBoxType_0;
        private System.Windows.Forms.TextBox textBoxName_0;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ImageList imageList1;
    }
}