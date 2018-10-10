namespace VitUsers
{
    partial class FormUsers
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
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxAccessGroup = new System.Windows.Forms.ListBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.buttonUSerEdit = new System.Windows.Forms.Button();
            this.textBoxIdUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.SuspendLayout();
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(148, 90);
            this.textBoxLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(247, 25);
            this.textBoxLogin.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Логин (имя)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Группы доступа";
            // 
            // listBoxAccessGroup
            // 
            this.listBoxAccessGroup.FormattingEnabled = true;
            this.listBoxAccessGroup.ItemHeight = 17;
            this.listBoxAccessGroup.Location = new System.Drawing.Point(148, 124);
            this.listBoxAccessGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBoxAccessGroup.Name = "listBoxAccessGroup";
            this.listBoxAccessGroup.Size = new System.Drawing.Size(510, 72);
            this.listBoxAccessGroup.TabIndex = 4;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(148, 239);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(146, 30);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Отправить в бвзу";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(148, 205);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.ReadOnly = true;
            this.textBoxPassword.Size = new System.Drawing.Size(510, 25);
            this.textBoxPassword.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Пароль";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(572, 647);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 30);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Закрыть";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(14, 277);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(644, 361);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // buttonUSerEdit
            // 
            this.buttonUSerEdit.Location = new System.Drawing.Point(14, 647);
            this.buttonUSerEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonUSerEdit.Name = "buttonUSerEdit";
            this.buttonUSerEdit.Size = new System.Drawing.Size(111, 30);
            this.buttonUSerEdit.TabIndex = 10;
            this.buttonUSerEdit.Text = "Редактировать";
            this.buttonUSerEdit.UseVisualStyleBackColor = true;
            this.buttonUSerEdit.Click += new System.EventHandler(this.buttonUSerEdit_Click);
            // 
            // textBoxIdUser
            // 
            this.textBoxIdUser.Location = new System.Drawing.Point(558, 90);
            this.textBoxIdUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxIdUser.Name = "textBoxIdUser";
            this.textBoxIdUser.ReadOnly = true;
            this.textBoxIdUser.Size = new System.Drawing.Size(101, 25);
            this.textBoxIdUser.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(472, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "номер";
            // 
            // windowHeader1
            // 
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(207)))), ((int)(((byte)(251)))));
            this.windowHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowHeader1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.windowHeader1.Location = new System.Drawing.Point(0, 0);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.Size = new System.Drawing.Size(673, 43);
            this.windowHeader1.TabIndex = 13;
            // 
            // FormUsers
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(673, 693);
            this.ControlBox = false;
            this.Controls.Add(this.windowHeader1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxIdUser);
            this.Controls.Add(this.buttonUSerEdit);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listBoxAccessGroup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLogin);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormUsers";
            this.Text = "Пользователи";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxLogin;
        public System.Windows.Forms.ListBox listBoxAccessGroup;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonUSerEdit;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.TextBox textBoxIdUser;
        private System.Windows.Forms.Label label4;
        private VitControls.WindowHeader windowHeader1;
    }
}