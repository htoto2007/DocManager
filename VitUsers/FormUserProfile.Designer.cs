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
            this.buttonDeletePhoto = new VitControls.VitButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelSubdivision = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEditUserPthoto = new VitControls.VitButton();
            this.buttonOkPassword = new VitControls.VitButton();
            this.buttonEditPassword = new VitControls.VitButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxRetryPassword = new System.Windows.Forms.TextBox();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.textBoxOldPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonDeletePhoto);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.buttonEditUserPthoto);
            this.panel1.Controls.Add(this.buttonOkPassword);
            this.panel1.Controls.Add(this.buttonEditPassword);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBoxRetryPassword);
            this.panel1.Controls.Add(this.textBoxNewPassword);
            this.panel1.Controls.Add(this.textBoxOldPassword);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.windowHeader1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 242);
            this.panel1.TabIndex = 19;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonDeletePhoto
            // 
            this.buttonDeletePhoto.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonDeletePhoto.BackColor = System.Drawing.Color.Transparent;
            this.buttonDeletePhoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDeletePhoto.BackgroundImage")));
            this.buttonDeletePhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDeletePhoto.Location = new System.Drawing.Point(183, 209);
            this.buttonDeletePhoto.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeletePhoto.Name = "buttonDeletePhoto";
            this.buttonDeletePhoto.Size = new System.Drawing.Size(26, 27);
            this.buttonDeletePhoto.TabIndex = 41;
            this.toolTip1.SetToolTip(this.buttonDeletePhoto, "Удалить аватар");
            this.buttonDeletePhoto.Click += new System.EventHandler(this.buttonDeletePhoto_Click);
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
            this.panel2.Location = new System.Drawing.Point(217, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(304, 158);
            this.panel2.TabIndex = 39;
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
            // buttonEditUserPthoto
            // 
            this.buttonEditUserPthoto.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonEditUserPthoto.BackColor = System.Drawing.Color.Transparent;
            this.buttonEditUserPthoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonEditUserPthoto.BackgroundImage")));
            this.buttonEditUserPthoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonEditUserPthoto.Location = new System.Drawing.Point(11, 209);
            this.buttonEditUserPthoto.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEditUserPthoto.Name = "buttonEditUserPthoto";
            this.buttonEditUserPthoto.Size = new System.Drawing.Size(26, 27);
            this.buttonEditUserPthoto.TabIndex = 38;
            this.toolTip1.SetToolTip(this.buttonEditUserPthoto, "Сменить фото");
            this.buttonEditUserPthoto.Click += new System.EventHandler(this.buttonEditUserPthoto_Click);
            // 
            // buttonOkPassword
            // 
            this.buttonOkPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonOkPassword.BackColor = System.Drawing.Color.Transparent;
            this.buttonOkPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonOkPassword.BackgroundImage")));
            this.buttonOkPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonOkPassword.Location = new System.Drawing.Point(495, 209);
            this.buttonOkPassword.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOkPassword.Name = "buttonOkPassword";
            this.buttonOkPassword.Size = new System.Drawing.Size(26, 27);
            this.buttonOkPassword.TabIndex = 37;
            this.toolTip1.SetToolTip(this.buttonOkPassword, "Подтвердить изменение пароля");
            this.buttonOkPassword.Visible = false;
            this.buttonOkPassword.Click += new System.EventHandler(this.buttonOkPassword_Click);
            // 
            // buttonEditPassword
            // 
            this.buttonEditPassword.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonEditPassword.BackColor = System.Drawing.Color.Transparent;
            this.buttonEditPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonEditPassword.BackgroundImage")));
            this.buttonEditPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonEditPassword.Location = new System.Drawing.Point(371, 209);
            this.buttonEditPassword.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEditPassword.Name = "buttonEditPassword";
            this.buttonEditPassword.Size = new System.Drawing.Size(26, 27);
            this.buttonEditPassword.TabIndex = 36;
            this.buttonEditPassword.Visible = false;
            this.buttonEditPassword.Click += new System.EventHandler(this.buttonEditPassword_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(214, 296);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(3);
            this.label6.Size = new System.Drawing.Size(163, 19);
            this.label6.TabIndex = 35;
            this.label6.Text = "Повторить новый пароль";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(214, 270);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(3);
            this.label7.Size = new System.Drawing.Size(98, 19);
            this.label7.TabIndex = 34;
            this.label7.Text = "Новый пароль";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(214, 244);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(3);
            this.label8.Size = new System.Drawing.Size(103, 19);
            this.label8.TabIndex = 33;
            this.label8.Text = "Старый пароль";
            this.label8.Visible = false;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBoxRetryPassword
            // 
            this.textBoxRetryPassword.Location = new System.Drawing.Point(381, 293);
            this.textBoxRetryPassword.Name = "textBoxRetryPassword";
            this.textBoxRetryPassword.ReadOnly = true;
            this.textBoxRetryPassword.Size = new System.Drawing.Size(140, 20);
            this.textBoxRetryPassword.TabIndex = 28;
            this.textBoxRetryPassword.UseSystemPasswordChar = true;
            this.textBoxRetryPassword.Visible = false;
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.Location = new System.Drawing.Point(381, 267);
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.ReadOnly = true;
            this.textBoxNewPassword.Size = new System.Drawing.Size(140, 20);
            this.textBoxNewPassword.TabIndex = 27;
            this.textBoxNewPassword.UseSystemPasswordChar = true;
            this.textBoxNewPassword.Visible = false;
            // 
            // textBoxOldPassword
            // 
            this.textBoxOldPassword.Location = new System.Drawing.Point(381, 241);
            this.textBoxOldPassword.Name = "textBoxOldPassword";
            this.textBoxOldPassword.ReadOnly = true;
            this.textBoxOldPassword.Size = new System.Drawing.Size(140, 20);
            this.textBoxOldPassword.TabIndex = 26;
            this.textBoxOldPassword.UseSystemPasswordChar = true;
            this.textBoxOldPassword.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(214, 217);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(57, 19);
            this.label4.TabIndex = 25;
            this.label4.Text = "Пароль";
            this.label4.Visible = false;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(11, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 158);
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
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // FormUserProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 244);
            this.Controls.Add(this.panel1);
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
        private VitControls.VitButton buttonEditUserPthoto;
        private VitControls.VitButton buttonOkPassword;
        private System.Windows.Forms.ToolTip toolTip1;
        private VitControls.VitButton buttonEditPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxRetryPassword;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.TextBox textBoxOldPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private VitControls.WindowHeader windowHeader1;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelSubdivision;
        private System.Windows.Forms.Label labelPosition;
        private VitControls.VitButton buttonDeletePhoto;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}