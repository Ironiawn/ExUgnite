namespace UGNITE
{
    partial class AbreJogo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbreJogo));
            this.chromeBrowser = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // chromeBrowser
            // 
            this.chromeBrowser.AutoSize = true;
            this.chromeBrowser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chromeBrowser.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chromeBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chromeBrowser.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.chromeBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chromeBrowser.Enabled = false;
            this.chromeBrowser.Location = new System.Drawing.Point(0, 0);
            this.chromeBrowser.Name = "chromeBrowser";
            this.chromeBrowser.Size = new System.Drawing.Size(750, 500);
            this.chromeBrowser.TabIndex = 32;
            this.chromeBrowser.Paint += new System.Windows.Forms.PaintEventHandler(this.chromeBrowser_Paint);
            // 
            // AbreJogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(750, 500);
            this.Controls.Add(this.chromeBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AbreJogo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UGNITE - AGUARDE...";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AbreJogo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel chromeBrowser;
    }
}