namespace VitControls
{
    partial class WindowResizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowResizer));
            this.SuspendLayout();
            // 
            // WindowResizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WindowResizer";
            this.Size = new System.Drawing.Size(24, 24);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowResizer_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowResizer_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WindowResizer_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
