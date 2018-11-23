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
            this.labelUserName = new System.Windows.Forms.Label();
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
            this.panel1.Size = new System.Drawing.Size(536, 363);
            this.panel1.TabIndex = 19;
            // 
            // buttonDeletePhoto
            // 
            this.buttonDeletePhoto.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonDeletePhoto.BackColor = System.Drawing.Color.Transparent;
            this.buttonDeletePhoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDeletePhoto.BackgroundImage")));
            this.buttonDeletePhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDeletePhoto.Location = new System.Drawing.Point(153, 219);
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
            this.panel2.Controls.Add(this.labelLogin);
            this.panel2.Controls.Add(this.labelSubdivision);
            this.panel2.Controls.Add(this.labelPosition);
            this.panel2.Controls.Add(this.labelUserName);
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
            this.labelLogin.Padding = new System.Windows.Forms.Padding(5);
            this.labelLogin.Size = new System.Drawing.Size(48, 23);
            this.labelLogin.TabIndex = 40;
            this.labelLogin.Text = "Логин";
            // 
            // labelSubdivision
            // 
            this.labelSubdivision.AutoSize = true;
            this.labelSubdivision.Location = new System.Drawing.Point(115, 69);
            this.labelSubdivision.Name = "labelSubdivision";
            this.labelSubdivision.Padding = new System.Windows.Forms.Padding(5);
            this.labelSubdivision.Size = new System.Drawing.Size(97, 23);
            this.labelSubdivision.TabIndex = 39;
            this.labelSubdivision.Text = "Подразделение";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(115, 46);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Padding = new System.Windows.Forms.Padding(5);
            this.labelPosition.Size = new System.Drawing.Size(75, 23);
            this.labelPosition.TabIndex = 38;
            this.labelPosition.Text = "Должность";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(115, 23);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Padding = new System.Windows.Forms.Padding(5);
            this.labelUserName.Size = new System.Drawing.Size(53, 23);
            this.labelUserName.TabIndex = 37;
            this.labelUserName.Text = "Ф.И.О.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 92);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5);
            this.label5.Size = new System.Drawing.Size(48, 23);
            this.label5.TabIndex = 36;
            this.label5.Text = "Логин";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 69);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5);
            this.label3.Size = new System.Drawing.Size(97, 23);
            this.label3.TabIndex = 35;
            this.label3.Text = "Подразделение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(75, 23);
            this.label2.TabIndex = 34;
            this.label2.Text = "Должность";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(53, 23);
            this.label1.TabIndex = 33;
            this.label1.Text = "Ф.И.О.";
            // 
            // buttonEditUserPthoto
            // 
            this.buttonEditUserPthoto.AccessibleRole = System.Windows.Forms.AccessibleRole.OutlineButton;
            this.buttonEditUserPthoto.BackColor = System.Drawing.Color.Transparent;
            this.buttonEditUserPthoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonEditUserPthoto.BackgroundImage")));
            this.buttonEditUserPthoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonEditUserPthoto.Location = new System.Drawing.Point(183, 219);
            this.buttonEditUserPthoto.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEditUserPthoto.Name = "buttonEditUserPthoto";
            this.buttonEditUserPthoto.Size = new System.Drawing.Size(26, 27);
            this.buttonEditUserPthoto.TabIndex = 38;
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
            this.buttonEditPassword.Click += new System.EventHandler(this.buttonEditPassword_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 304);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(10);
            this.label6.Size = new System.Drawing.Size(155, 33);
            this.label6.TabIndex = 35;
            this.label6.Text = "Повторить новый пароль";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(214, 271);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(10);
            this.label7.Size = new System.Drawing.Size(100, 33);
            this.label7.TabIndex = 34;
            this.label7.Text = "Новый пароль";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(214, 238);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(10);
            this.label8.Size = new System.Drawing.Size(104, 33);
            this.label8.TabIndex = 33;
            this.label8.Text = "Старый пароль";
            // 
            // textBoxRetryPassword
            // 
            this.textBoxRetryPassword.Location = new System.Drawing.Point(371, 316);
            this.textBoxRetryPassword.Name = "textBoxRetryPassword";
            this.textBoxRetryPassword.ReadOnly = true;
            this.textBoxRetryPassword.Size = new System.Drawing.Size(150, 20);
            this.textBoxRetryPassword.TabIndex = 28;
            this.textBoxRetryPassword.UseSystemPasswordChar = true;
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.Location = new System.Drawing.Point(371, 283);
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.ReadOnly = true;
            this.textBoxNewPassword.Size = new System.Drawing.Size(150, 20);
            this.textBoxNewPassword.TabIndex = 27;
            this.textBoxNewPassword.UseSystemPasswordChar = true;
            // 
            // textBoxOldPassword
            // 
            this.textBoxOldPassword.Location = new System.Drawing.Point(371, 250);
            this.textBoxOldPassword.Name = "textBoxOldPassword";
            this.textBoxOldPassword.ReadOnly = true;
            this.textBoxOldPassword.Size = new System.Drawing.Size(150, 20);
            this.textBoxOldPassword.TabIndex = 26;
            this.textBoxOldPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 210);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(10);
            this.label4.Size = new System.Drawing.Size(65, 33);
            this.label4.TabIndex = 25;
            this.label4.Text = "Пароль";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(11, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // windowHeader1
            // 
            this.windowHeader1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.windowHeader1.AutoSize = true;
            this.windowHeader1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(207)))), ((int)(((byte)(251)))));
            this.windowHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowHeader1.Location = new System.Drawing.Point(0, 0);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.maximize = false;
            this.windowHeader1.minimize = false;
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.Size = new System.Drawing.Size(536, 34);
            this.windowHeader1.TabIndex = 40;
            // 
            // FormUserProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 365);
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
        private System.Windows.Forms.Label labelUserName;
        private VitControls.VitButton buttonDeletePhoto;
    }
}