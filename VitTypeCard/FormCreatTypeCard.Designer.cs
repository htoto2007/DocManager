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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDeleteProp_0 = new System.Windows.Forms.Button();
            this.comboBoxType_0 = new System.Windows.Forms.ComboBox();
            this.textBoxName_0 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(291, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название типа карточки докусента";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "w512h5121380984207add[3].png");
            this.imageList1.Images.SetKeyName(1, "1s-udalenie[1].png");
            this.imageList1.Images.SetKeyName(2, "rodentia-icons_ok[1].png");
            // 
            // button1
            // 
            this.button1.ImageIndex = 0;
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(13, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 6;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Добавить поле в карточку";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.buttonDeleteProp_0);
            this.panel1.Controls.Add(this.comboBoxType_0);
            this.panel1.Controls.Add(this.textBoxName_0);
            this.panel1.Location = new System.Drawing.Point(13, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 302);
            this.panel1.TabIndex = 8;
            // 
            // buttonDeleteProp_0
            // 
            this.buttonDeleteProp_0.Enabled = false;
            this.buttonDeleteProp_0.ImageIndex = 1;
            this.buttonDeleteProp_0.ImageList = this.imageList1;
            this.buttonDeleteProp_0.Location = new System.Drawing.Point(339, 9);
            this.buttonDeleteProp_0.Name = "buttonDeleteProp_0";
            this.buttonDeleteProp_0.Size = new System.Drawing.Size(34, 23);
            this.buttonDeleteProp_0.TabIndex = 7;
            this.buttonDeleteProp_0.UseVisualStyleBackColor = true;
            // 
            // comboBoxType_0
            // 
            this.comboBoxType_0.FormattingEnabled = true;
            this.comboBoxType_0.Items.AddRange(new object[] {
            "Строковый",
            "Числовой",
            "Дата",
            "Дата и время",
            "Логический"});
            this.comboBoxType_0.Location = new System.Drawing.Point(176, 12);
            this.comboBoxType_0.Name = "comboBoxType_0";
            this.comboBoxType_0.Size = new System.Drawing.Size(156, 21);
            this.comboBoxType_0.TabIndex = 6;
            // 
            // textBoxName_0
            // 
            this.textBoxName_0.Location = new System.Drawing.Point(14, 13);
            this.textBoxName_0.Name = "textBoxName_0";
            this.textBoxName_0.Size = new System.Drawing.Size(156, 20);
            this.textBoxName_0.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Тип поля";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Описание свойства";
            // 
            // button2
            // 
            this.button2.ImageIndex = 2;
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(13, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 23);
            this.button2.TabIndex = 9;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(324, 418);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Отмена";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormCreatTypeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 450);
            this.ControlBox = false;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "FormCreatTypeCard";
            this.Text = "FormCreatTypeCard";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonDeleteProp_0;
        private System.Windows.Forms.ComboBox comboBoxType_0;
        private System.Windows.Forms.TextBox textBoxName_0;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.ImageList imageList1;
    }
}