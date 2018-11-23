namespace VitControls
{
    partial class WindowHeader
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowHeader));
            this.labelText = new System.Windows.Forms.Label();
            this.buttonMinimize = new VitControls.VitButton();
            this.buttonClose = new VitControls.VitButton();
            this.buttonMaximize = new VitControls.VitButton();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelText.AutoSize = true;
            this.labelText.BackColor = System.Drawing.Color.Transparent;
            this.labelText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelText.Location = new System.Drawing.Point(3, 8);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(43, 17);
            this.labelText.TabIndex = 12;
            this.labelText.Text = "label1";
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.buttonMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMinimize.BackColor = System.Drawing.Color.Transparent;
            this.buttonMinimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMinimize.BackgroundImage")));
            this.buttonMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMinimize.Location = new System.Drawing.Point(267, 1);
            this.buttonMinimize.Margin = new System.Windows.Forms.Padding(1);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(32, 32);
            this.buttonMinimize.TabIndex = 14;
            this.buttonMinimize.Click += new System.EventHandler(this.ButtonMinimize_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClose.BackgroundImage")));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonClose.Location = new System.Drawing.Point(335, 1);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(1);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(32, 32);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonMaximize
            // 
            this.buttonMaximize.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.buttonMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMaximize.BackColor = System.Drawing.Color.Transparent;
            this.buttonMaximize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMaximize.BackgroundImage")));
            this.buttonMaximize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMaximize.Location = new System.Drawing.Point(301, 1);
            this.buttonMaximize.Margin = new System.Windows.Forms.Padding(1);
            this.buttonMaximize.Name = "buttonMaximize";
            this.buttonMaximize.Size = new System.Drawing.Size(32, 32);
            this.buttonMaximize.TabIndex = 15;
            this.buttonMaximize.Click += new System.EventHandler(this.buttonMaximize_Click);
            // 
            // WindowHeader
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.buttonMinimize);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonMaximize);
            this.Controls.Add(this.labelText);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WindowHeader";
            this.Size = new System.Drawing.Size(368, 34);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WindowHeader_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowHeader_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowHeader_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WindowHeader_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelText;
        private VitButton buttonClose;
        private VitButton buttonMinimize;
        private VitButton buttonMaximize;
    }
}
