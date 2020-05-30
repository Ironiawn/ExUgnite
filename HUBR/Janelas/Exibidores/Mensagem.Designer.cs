namespace UGNITE
{
    partial class U_Mensagem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(U_Mensagem));
            this.rtMessage = new System.Windows.Forms.RichTextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.appLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // rtMessage
            // 
            this.rtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtMessage.BackColor = System.Drawing.SystemColors.WindowText;
            this.rtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtMessage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rtMessage.Font = new System.Drawing.Font("Agency FB", 15F);
            this.rtMessage.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.rtMessage.Location = new System.Drawing.Point(12, 63);
            this.rtMessage.Name = "rtMessage";
            this.rtMessage.ReadOnly = true;
            this.rtMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtMessage.Size = new System.Drawing.Size(830, 175);
            this.rtMessage.TabIndex = 19;
            this.rtMessage.Text = "";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Font = new System.Drawing.Font("Agency FB", 23F);
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbTitle.Location = new System.Drawing.Point(65, 20);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(190, 37);
            this.lbTitle.TabIndex = 20;
            this.lbTitle.Text = "DE BOA NA LAGOA!!";
            this.lbTitle.UseMnemonic = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::UGNITE.Properties.Resources.Programming_Delete_Sign_icon;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(792, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 52);
            this.btnClose.TabIndex = 17;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // appLogo
            // 
            this.appLogo.BackColor = System.Drawing.Color.Transparent;
            this.appLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("appLogo.BackgroundImage")));
            this.appLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.appLogo.Location = new System.Drawing.Point(12, 12);
            this.appLogo.Name = "appLogo";
            this.appLogo.Size = new System.Drawing.Size(47, 45);
            this.appLogo.TabIndex = 22;
            this.appLogo.TabStop = false;
            // 
            // U_Mensagem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(854, 250);
            this.ControlBox = false;
            this.Controls.Add(this.appLogo);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.rtMessage);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "U_Mensagem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UGNITE - Suceesso!";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.Mensagem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtMessage;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox appLogo;
    }
}