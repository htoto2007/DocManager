namespace VitSettings
{
    partial class FormSettings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneralsSettings = new System.Windows.Forms.TabPage();
            this.tabPageConnectToData = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.windowHeader1 = new VitControls.WindowHeader();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneralsSettings);
            this.tabControl1.Controls.Add(this.tabPageConnectToData);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 41);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 547);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageGeneralsSettings
            // 
            this.tabPageGeneralsSettings.Location = new System.Drawing.Point(4, 26);
            this.tabPageGeneralsSettings.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tabPageGeneralsSettings.Name = "tabPageGeneralsSettings";
            this.tabPageGeneralsSettings.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageGeneralsSettings.Size = new System.Drawing.Size(516, 517);
            this.tabPageGeneralsSettings.TabIndex = 0;
            this.tabPageGeneralsSettings.Text = "Общие";
            this.tabPageGeneralsSettings.UseVisualStyleBackColor = true;
            // 
            // tabPageConnectToData
            // 
            this.tabPageConnectToData.Location = new System.Drawing.Point(4, 26);
            this.tabPageConnectToData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageConnectToData.Name = "tabPageConnectToData";
            this.tabPageConnectToData.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageConnectToData.Size = new System.Drawing.Size(516, 517);
            this.tabPageConnectToData.TabIndex = 1;
            this.tabPageConnectToData.Text = "Подключение к базе";
            this.tabPageConnectToData.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Size = new System.Drawing.Size(516, 517);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // windowHeader1
            // 
            this.windowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.windowHeader1.Location = new System.Drawing.Point(0, -3);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.Size = new System.Drawing.Size(524, 33);
            this.windowHeader1.TabIndex = 1;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 588);
            this.Controls.Add(this.windowHeader1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormSettings";
            this.Text = "Настроки";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneralsSettings;
        private System.Windows.Forms.TabPage tabPageConnectToData;
        private System.Windows.Forms.TabPage tabPage3;
        private VitControls.WindowHeader windowHeader1;
    }
}