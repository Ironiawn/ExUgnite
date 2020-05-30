namespace UGNITE
{
    partial class ActivateKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivateKey));
            this.appLogo = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.lbKeyCode = new System.Windows.Forms.Label();
            this.tbKeyInput = new System.Windows.Forms.TextBox();
            this.btnVerifyKey = new System.Windows.Forms.Button();
            this.btnActivateSteamKey = new System.Windows.Forms.Button();
            this.lbWarning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // appLogo
            // 
            this.appLogo.BackColor = System.Drawing.Color.Transparent;
            this.appLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("appLogo.BackgroundImage")));
            this.appLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.appLogo.Location = new System.Drawing.Point(12, 3);
            this.appLogo.Name = "appLogo";
            this.appLogo.Size = new System.Drawing.Size(118, 110);
            this.appLogo.TabIndex = 21;
            this.appLogo.TabStop = false;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Font = new System.Drawing.Font("Agency FB", 21F);
            this.lbTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbTitle.Location = new System.Drawing.Point(136, 20);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(256, 33);
            this.lbTitle.TabIndex = 22;
            this.lbTitle.Text = "RESGATAR CHAVE DE ATIVAÇÃO";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::UGNITE.Properties.Resources.Programming_Delete_Sign_icon;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(523, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 25);
            this.btnClose.TabIndex = 23;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbKeyCode
            // 
            this.lbKeyCode.AutoSize = true;
            this.lbKeyCode.BackColor = System.Drawing.Color.Transparent;
            this.lbKeyCode.Font = new System.Drawing.Font("Agency FB", 15.75F, System.Drawing.FontStyle.Bold);
            this.lbKeyCode.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbKeyCode.Location = new System.Drawing.Point(137, 134);
            this.lbKeyCode.Name = "lbKeyCode";
            this.lbKeyCode.Size = new System.Drawing.Size(292, 25);
            this.lbKeyCode.TabIndex = 25;
            this.lbKeyCode.Text = "DIGITE A CHAVE DE 25 DIGITOS ABAIXO";
            // 
            // tbKeyInput
            // 
            this.tbKeyInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbKeyInput.Font = new System.Drawing.Font("Agency FB", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tbKeyInput.Location = new System.Drawing.Point(88, 162);
            this.tbKeyInput.MaxLength = 30;
            this.tbKeyInput.Multiline = true;
            this.tbKeyInput.Name = "tbKeyInput";
            this.tbKeyInput.Size = new System.Drawing.Size(394, 28);
            this.tbKeyInput.TabIndex = 1;
            // 
            // btnVerifyKey
            // 
            this.btnVerifyKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnVerifyKey.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVerifyKey.BackgroundImage")));
            this.btnVerifyKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnVerifyKey.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVerifyKey.Font = new System.Drawing.Font("Agency FB", 15.75F);
            this.btnVerifyKey.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnVerifyKey.Image = global::UGNITE.Properties.Resources.Keys_icon;
            this.btnVerifyKey.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerifyKey.Location = new System.Drawing.Point(12, 261);
            this.btnVerifyKey.Name = "btnVerifyKey";
            this.btnVerifyKey.Size = new System.Drawing.Size(239, 44);
            this.btnVerifyKey.TabIndex = 26;
            this.btnVerifyKey.Text = "ATIVAR CHAVE UGNITE";
            this.btnVerifyKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVerifyKey.UseVisualStyleBackColor = true;
            this.btnVerifyKey.Click += new System.EventHandler(this.btnVerifyKey_Click);
            // 
            // btnActivateSteamKey
            // 
            this.btnActivateSteamKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnActivateSteamKey.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnActivateSteamKey.BackgroundImage")));
            this.btnActivateSteamKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnActivateSteamKey.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnActivateSteamKey.Font = new System.Drawing.Font("Agency FB", 15.75F);
            this.btnActivateSteamKey.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActivateSteamKey.Image = global::UGNITE.Properties.Resources.icons8_steam_64;
            this.btnActivateSteamKey.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActivateSteamKey.Location = new System.Drawing.Point(308, 261);
            this.btnActivateSteamKey.Name = "btnActivateSteamKey";
            this.btnActivateSteamKey.Size = new System.Drawing.Size(239, 44);
            this.btnActivateSteamKey.TabIndex = 27;
            this.btnActivateSteamKey.Text = "ATIVAR CHAVE EXTERNA";
            this.btnActivateSteamKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActivateSteamKey.UseVisualStyleBackColor = true;
            this.btnActivateSteamKey.Click += new System.EventHandler(this.btnActivateSteamKey_Click);
            // 
            // lbWarning
            // 
            this.lbWarning.AutoSize = true;
            this.lbWarning.BackColor = System.Drawing.Color.Transparent;
            this.lbWarning.Font = new System.Drawing.Font("Agency FB", 10.75F, System.Drawing.FontStyle.Bold);
            this.lbWarning.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbWarning.Location = new System.Drawing.Point(85, 193);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(338, 36);
            this.lbWarning.TabIndex = 28;
            this.lbWarning.Text = "A CHAVE SERÁ VINCULADA\r\nA CONTA %ID% NA UGNITE E NÃO PODERÁ MAIS SER UTILIZADA";
            // 
            // ActivateKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(559, 317);
            this.ControlBox = false;
            this.Controls.Add(this.lbWarning);
            this.Controls.Add(this.btnActivateSteamKey);
            this.Controls.Add(this.btnVerifyKey);
            this.Controls.Add(this.lbKeyCode);
            this.Controls.Add(this.tbKeyInput);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.appLogo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActivateKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ugnite - Ativação de Chave";
            this.Load += new System.EventHandler(this.ActivateKey_Load);
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox appLogo;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Label lbKeyCode;
        private System.Windows.Forms.TextBox tbKeyInput;
        private System.Windows.Forms.Button btnVerifyKey;
        private System.Windows.Forms.Button btnActivateSteamKey;
        private System.Windows.Forms.Label lbWarning;
    }
}