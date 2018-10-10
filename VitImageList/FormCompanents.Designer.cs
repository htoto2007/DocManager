namespace VitIcons
{
    partial class FormCompanents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCompanents));
            this.imageListColor = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageListColor
            // 
            this.imageListColor.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListColor.ImageStream")));
            this.imageListColor.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListColor.Images.SetKeyName(0, "icons8-add-new-16.png");
            this.imageListColor.Images.SetKeyName(1, "icons8-close-window-48.png");
            this.imageListColor.Images.SetKeyName(2, "icons8-crown-50.png");
            this.imageListColor.Images.SetKeyName(3, "icons8-cursor-32.png");
            this.imageListColor.Images.SetKeyName(4, "icons8-document-48.png");
            this.imageListColor.Images.SetKeyName(5, "icons8-exit-48.png");
            this.imageListColor.Images.SetKeyName(6, "icons8-folder-48.png");
            this.imageListColor.Images.SetKeyName(7, "icons8-minimize-window-48.png");
            this.imageListColor.Images.SetKeyName(8, "icons8-plus-48.png");
            this.imageListColor.Images.SetKeyName(9, "icons8-question-mark-48.png");
            this.imageListColor.Images.SetKeyName(10, "icons8-scanner-40.png");
            this.imageListColor.Images.SetKeyName(11, "icons8-user-avatar-48.png");
            this.imageListColor.Images.SetKeyName(12, "icons8-tree-structure-40.png");
            this.imageListColor.Images.SetKeyName(13, "icons8-about-48.png");
            // 
            // FormCompanents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(140, 67);
            this.ControlBox = false;
            this.Name = "FormCompanents";
            this.Text = "FormCompanents";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ImageList imageListColor;
    }
}