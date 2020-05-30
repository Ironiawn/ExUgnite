namespace UGNITE___Update_Utility
{
    partial class Home
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbStatusUpdate = new System.Windows.Forms.Label();
            this.lbUpdaterVersion = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.imagemUg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imagemUg)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Agency FB", 18F);
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbTitle.Location = new System.Drawing.Point(12, 9);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(191, 28);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "UGNITE - UPDATE UTILITY";
            // 
            // lbStatusUpdate
            // 
            this.lbStatusUpdate.AutoSize = true;
            this.lbStatusUpdate.Font = new System.Drawing.Font("Agency FB", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatusUpdate.Location = new System.Drawing.Point(13, 88);
            this.lbStatusUpdate.Name = "lbStatusUpdate";
            this.lbStatusUpdate.Size = new System.Drawing.Size(157, 24);
            this.lbStatusUpdate.TabIndex = 1;
            this.lbStatusUpdate.Text = "Checking Ugnite version...";
            // 
            // lbUpdaterVersion
            // 
            this.lbUpdaterVersion.AutoSize = true;
            this.lbUpdaterVersion.Font = new System.Drawing.Font("Agency FB", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUpdaterVersion.ForeColor = System.Drawing.Color.White;
            this.lbUpdaterVersion.Location = new System.Drawing.Point(534, 156);
            this.lbUpdaterVersion.Name = "lbUpdaterVersion";
            this.lbUpdaterVersion.Size = new System.Drawing.Size(65, 24);
            this.lbUpdaterVersion.TabIndex = 2;
            this.lbUpdaterVersion.Text = "UGUU v0.1";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(17, 115);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(621, 38);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressBar.TabIndex = 1;
            this.ProgressBar.Value = 15;
            // 
            // imagemUg
            // 
            this.imagemUg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imagemUg.BackgroundImage")));
            this.imagemUg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imagemUg.Location = new System.Drawing.Point(538, 9);
            this.imagemUg.Name = "imagemUg";
            this.imagemUg.Size = new System.Drawing.Size(100, 100);
            this.imagemUg.TabIndex = 3;
            this.imagemUg.TabStop = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(650, 186);
            this.Controls.Add(this.imagemUg);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.lbUpdaterVersion);
            this.Controls.Add(this.lbStatusUpdate);
            this.Controls.Add(this.lbTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Home";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ugnite - Update Utility";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imagemUg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbStatusUpdate;
        private System.Windows.Forms.Label lbUpdaterVersion;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.PictureBox imagemUg;
    }
}

