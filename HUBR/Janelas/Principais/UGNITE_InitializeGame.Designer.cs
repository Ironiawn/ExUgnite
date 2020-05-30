namespace UGNITE.Janelas.Principais
{
    partial class UGNITE_InitializeGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UGNITE_InitializeGame));
            this.lbWait = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbWait
            // 
            this.lbWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbWait.BackColor = System.Drawing.Color.Transparent;
            this.lbWait.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbWait.Font = new System.Drawing.Font("Bahnschrift", 14.75F, System.Drawing.FontStyle.Bold);
            this.lbWait.ForeColor = System.Drawing.Color.Gold;
            this.lbWait.Location = new System.Drawing.Point(15, 5);
            this.lbWait.Name = "lbWait";
            this.lbWait.Size = new System.Drawing.Size(454, 65);
            this.lbWait.TabIndex = 63;
            this.lbWait.Text = "AGUARDE ENQUANTO CONFIGURAMOS A UGNITE\r\nPARA INICIALIZAR SEU JOGO..";
            this.lbWait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbWait.UseMnemonic = false;
            this.lbWait.UseWaitCursor = true;
            // 
            // UGNITE_InitializeGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(481, 79);
            this.Controls.Add(this.lbWait);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UGNITE_InitializeGame";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ugnite Client";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.UGNITE_InitializeGame_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbWait;
    }
}