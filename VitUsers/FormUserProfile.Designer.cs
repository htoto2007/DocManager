namespace VitUsers
{
    partial class FormUserProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserProfile));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonQueryPassword = new System.Windows.Forms.Button();
            this.buttonDeletePhoto = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelSubdivision = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonQueryPassword);
            this.panel1.Controls.Add(this.buttonDeletePhoto);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.windowHeader1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 254);
            this.panel1.TabIndex = 19;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImageKey = "icons8-unavailable-480.png";
            this.buttonCancel.ImageList = this.imageList1;
            this.buttonCancel.Location = new System.Drawing.Point(445, 209);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 23);
            this.buttonCancel.TabIndex = 45;
            this.buttonCancel.Text = "закрыть";
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-edit-480.png");
            this.imageList1.Images.SetKeyName(1, "icons8-trash-can-480 (1).png");
            this.imageList1.Images.SetKeyName(2, "icons8-unavailable-480.png");
            this.imageList1.Images.SetKeyName(3, "icons8-восстановить-пароль-96.png");
            // 
            // buttonQueryPassword
            // 
            this.buttonQueryPassword.AutoSize = true;
            this.buttonQueryPassword.ImageKey = "icons8-восстановить-пароль-96.png";
            this.buttonQueryPassword.ImageList = this.imageList1;
            this.buttonQueryPassword.Location = new System.Drawing.Point(232, 209);
            this.buttonQueryPassword.Name = "buttonQueryPassword";
            this.buttonQueryPassword.Size = new System.Drawing.Size(159, 23);
            this.buttonQueryPassword.TabIndex = 44;
            this.buttonQueryPassword.Text = "запросить смену пароля";
            this.buttonQueryPassword.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonQueryPassword.UseVisualStyleBackColor = true;
            this.buttonQueryPassword.Click += new System.EventHandler(this.buttonQueryPassword_Click);
            // 
            // buttonDeletePhoto
            // 
            this.buttonDeletePhoto.AutoSize = true;
            this.buttonDeletePhoto.ImageKey = "icons8-trash-can-480 (1).png";
            this.buttonDeletePhoto.ImageList = this.imageList1;
            this.buttonDeletePhoto.Location = new System.Drawing.Point(121, 209);
            this.buttonDeletePhoto.Name = "buttonDeletePhoto";
            this.buttonDeletePhoto.Size = new System.Drawing.Size(104, 23);
            this.buttonDeletePhoto.TabIndex = 43;
            this.buttonDeletePhoto.Text = "удалить фото";
            this.buttonDeletePhoto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDeletePhoto.UseVisualStyleBackColor = true;
            this.buttonDeletePhoto.Click += new System.EventHandler(this.buttonDeletePhoto_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.ImageKey = "icons8-edit-480.png";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(11, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 42;
            this.button1.Text = "сменить фото";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonEditUserPthoto_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Controls.Add(this.labelLogin);
            this.panel2.Controls.Add(this.labelSubdivision);
            this.panel2.Controls.Add(this.labelPosition);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(232, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(289, 158);
            this.panel2.TabIndex = 39;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(118, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(178, 20);
            this.richTextBox1.TabIndex = 41;
            this.richTextBox1.Text = "User Name";
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(115, 92);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Padding = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.labelLogin.Size = new System.Drawing.Size(45, 23);
            this.labelLogin.TabIndex = 40;
            this.labelLogin.Text = "Логин";
            // 
            // labelSubdivision
            // 
            this.labelSubdivision.AutoSize = true;
            this.labelSubdivision.Location = new System.Drawing.Point(115, 69);
            this.labelSubdivision.Name = "labelSubdivision";
            this.labelSubdivision.Padding = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.labelSubdivision.Size = new System.Drawing.Size(94, 23);
            this.labelSubdivision.TabIndex = 39;
            this.labelSubdivision.Text = "Подразделение";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(115, 46);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Padding = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.labelPosition.Size = new System.Drawing.Size(72, 23);
            this.labelPosition.TabIndex = 38;
            this.labelPosition.Text = "Должность";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(3, 92);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5);
            this.label5.Size = new System.Drawing.Size(53, 23);
            this.label5.TabIndex = 36;
            this.label5.Text = "Логин";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 69);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5);
            this.label3.Size = new System.Drawing.Size(110, 23);
            this.label3.TabIndex = 35;
            this.label3.Text = "Подразделение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(84, 23);
            this.label2.TabIndex = 34;
            this.label2.Text = "Должность";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(59, 23);
            this.label1.TabIndex = 33;
            this.label1.Text = "Ф.И.О.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(11, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(214, 158);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // windowHeader1
            // 
            this.windowHeader1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.windowHeader1.AutoSize = true;
            this.windowHeader1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(207)))), ((int)(((byte)(251)))));
            this.windowHeader1.close = true;
            this.windowHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowHeader1.Location = new System.Drawing.Point(0, 0);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.maximize = false;
            this.windowHeader1.minimize = false;
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.showInTaskbar = false;
            this.windowHeader1.Size = new System.Drawing.Size(536, 34);
            this.windowHeader1.TabIndex = 40;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // FormUserProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(538, 256);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormUserProfile";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Text = "Профиль пользователя";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private VitControls.WindowHeader windowHeader1;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelSubdivision;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button buttonQueryPassword;
        private System.Windows.Forms.Button buttonDeletePhoto;
        private System.Windows.Forms.Button button1;
    }
}