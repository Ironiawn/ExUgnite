namespace UGNITE
{
    partial class UGNITE_InstallGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UGNITE_InstallGame));
            this.btnLibrary = new System.Windows.Forms.Label();
            this.lbCurServer = new System.Windows.Forms.Label();
            this.lbProgressDetails = new System.Windows.Forms.Label();
            this.pbDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.lbGameName = new System.Windows.Forms.Label();
            this.lbWarning = new System.Windows.Forms.Label();
            this.GameImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GameImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLibrary
            // 
            this.btnLibrary.AutoSize = true;
            this.btnLibrary.BackColor = System.Drawing.Color.Transparent;
            this.btnLibrary.Font = new System.Drawing.Font("Bahnschrift Light Condensed", 21F);
            this.btnLibrary.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLibrary.Location = new System.Drawing.Point(313, 12);
            this.btnLibrary.Name = "btnLibrary";
            this.btnLibrary.Size = new System.Drawing.Size(243, 34);
            this.btnLibrary.TabIndex = 43;
            this.btnLibrary.Text = "CONFIGURAÇÃO DE JOGOS";
            // 
            // lbCurServer
            // 
            this.lbCurServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurServer.BackColor = System.Drawing.Color.Transparent;
            this.lbCurServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbCurServer.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lbCurServer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbCurServer.Location = new System.Drawing.Point(12, 301);
            this.lbCurServer.Name = "lbCurServer";
            this.lbCurServer.Size = new System.Drawing.Size(220, 33);
            this.lbCurServer.TabIndex = 60;
            this.lbCurServer.Text = "SERVIDOR: IWBR";
            this.lbCurServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbCurServer.UseCompatibleTextRendering = true;
            // 
            // lbProgressDetails
            // 
            this.lbProgressDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProgressDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbProgressDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbProgressDetails.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lbProgressDetails.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbProgressDetails.Location = new System.Drawing.Point(12, 195);
            this.lbProgressDetails.Name = "lbProgressDetails";
            this.lbProgressDetails.Size = new System.Drawing.Size(416, 33);
            this.lbProgressDetails.TabIndex = 57;
            this.lbProgressDetails.Text = "CONECTANDO-SE AO SERVIDOR...";
            this.lbProgressDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbProgressDetails.UseCompatibleTextRendering = true;
            // 
            // pbDownloadProgress
            // 
            this.pbDownloadProgress.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbDownloadProgress.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pbDownloadProgress.ForeColor = System.Drawing.Color.DarkRed;
            this.pbDownloadProgress.Location = new System.Drawing.Point(12, 242);
            this.pbDownloadProgress.Name = "pbDownloadProgress";
            this.pbDownloadProgress.Size = new System.Drawing.Size(842, 47);
            this.pbDownloadProgress.TabIndex = 59;
            // 
            // lbGameName
            // 
            this.lbGameName.AutoSize = true;
            this.lbGameName.BackColor = System.Drawing.Color.Transparent;
            this.lbGameName.Font = new System.Drawing.Font("Bahnschrift Light Condensed", 45F);
            this.lbGameName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbGameName.Location = new System.Drawing.Point(307, 110);
            this.lbGameName.Name = "lbGameName";
            this.lbGameName.Size = new System.Drawing.Size(323, 72);
            this.lbGameName.TabIndex = 58;
            this.lbGameName.Text = "%GameName%";
            // 
            // lbWarning
            // 
            this.lbWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbWarning.AutoEllipsis = true;
            this.lbWarning.BackColor = System.Drawing.Color.Transparent;
            this.lbWarning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbWarning.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWarning.ForeColor = System.Drawing.Color.Gold;
            this.lbWarning.Location = new System.Drawing.Point(313, 56);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(454, 65);
            this.lbWarning.TabIndex = 62;
            this.lbWarning.Text = "AGUARDE ENQUANTO A UGNITE FAZ O DOWNLOAD E INSTALAÇÃO DO JOGO";
            this.lbWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbWarning.UseCompatibleTextRendering = true;
            // 
            // GameImage
            // 
            this.GameImage.BackColor = System.Drawing.Color.Transparent;
            this.GameImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GameImage.BackgroundImage")));
            this.GameImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GameImage.Location = new System.Drawing.Point(12, 12);
            this.GameImage.Name = "GameImage";
            this.GameImage.Size = new System.Drawing.Size(295, 170);
            this.GameImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GameImage.TabIndex = 63;
            this.GameImage.TabStop = false;
            // 
            // UGNITE_InstallGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(866, 370);
            this.Controls.Add(this.GameImage);
            this.Controls.Add(this.lbWarning);
            this.Controls.Add(this.lbCurServer);
            this.Controls.Add(this.lbProgressDetails);
            this.Controls.Add(this.pbDownloadProgress);
            this.Controls.Add(this.lbGameName);
            this.Controls.Add(this.btnLibrary);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UGNITE_InstallGame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UGNITE - CONFIGURAÇÃO DE JOGOS";
            this.Load += new System.EventHandler(this.UGNITE_InstallGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GameImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label btnLibrary;
        public System.Windows.Forms.Label lbCurServer;
        public System.Windows.Forms.Label lbProgressDetails;
        private System.Windows.Forms.ProgressBar pbDownloadProgress;
        public System.Windows.Forms.Label lbGameName;
        public System.Windows.Forms.Label lbWarning;
        private System.Windows.Forms.PictureBox GameImage;
    }
}