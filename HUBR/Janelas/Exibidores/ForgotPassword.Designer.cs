namespace UGNITE
{
    partial class ForgotPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPassword));
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.appLogo = new System.Windows.Forms.PictureBox();
            this.lbUserID = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbSecurityCode = new System.Windows.Forms.TextBox();
            this.lbSecurityCode = new System.Windows.Forms.Label();
            this.tbNewPass = new System.Windows.Forms.TextBox();
            this.lbNewPass = new System.Windows.Forms.Label();
            this.btnUpdatePassword = new System.Windows.Forms.Button();
            this.lbPoweredBy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::UGNITE.Properties.Resources.Programming_Delete_Sign_icon;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Location = new System.Drawing.Point(624, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 25);
            this.btnClose.TabIndex = 18;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // appLogo
            // 
            this.appLogo.BackColor = System.Drawing.Color.Transparent;
            this.appLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("appLogo.BackgroundImage")));
            this.appLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.appLogo.Location = new System.Drawing.Point(12, 3);
            this.appLogo.Name = "appLogo";
            this.appLogo.Size = new System.Drawing.Size(133, 133);
            this.appLogo.TabIndex = 17;
            this.appLogo.TabStop = false;
            // 
            // lbUserID
            // 
            this.lbUserID.AutoSize = true;
            this.lbUserID.BackColor = System.Drawing.Color.Transparent;
            this.lbUserID.Font = new System.Drawing.Font("Agency FB", 16.75F, System.Drawing.FontStyle.Bold);
            this.lbUserID.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbUserID.Location = new System.Drawing.Point(217, 108);
            this.lbUserID.Name = "lbUserID";
            this.lbUserID.Size = new System.Drawing.Size(119, 28);
            this.lbUserID.TabIndex = 20;
            this.lbUserID.Text = "IDENTIFICAÇÃO";
            // 
            // tbUsername
            // 
            this.tbUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tbUsername.Location = new System.Drawing.Point(222, 141);
            this.tbUsername.MaxLength = 10;
            this.tbUsername.Multiline = true;
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(220, 28);
            this.tbUsername.TabIndex = 21;
            // 
            // tbSecurityCode
            // 
            this.tbSecurityCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSecurityCode.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tbSecurityCode.Location = new System.Drawing.Point(222, 213);
            this.tbSecurityCode.MaxLength = 10;
            this.tbSecurityCode.Multiline = true;
            this.tbSecurityCode.Name = "tbSecurityCode";
            this.tbSecurityCode.Size = new System.Drawing.Size(220, 28);
            this.tbSecurityCode.TabIndex = 23;
            // 
            // lbSecurityCode
            // 
            this.lbSecurityCode.AutoSize = true;
            this.lbSecurityCode.BackColor = System.Drawing.Color.Transparent;
            this.lbSecurityCode.Font = new System.Drawing.Font("Agency FB", 16.75F, System.Drawing.FontStyle.Bold);
            this.lbSecurityCode.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbSecurityCode.Location = new System.Drawing.Point(217, 180);
            this.lbSecurityCode.Name = "lbSecurityCode";
            this.lbSecurityCode.Size = new System.Drawing.Size(166, 28);
            this.lbSecurityCode.TabIndex = 22;
            this.lbSecurityCode.Text = "CÓDIGO VERIFICADOR";
            // 
            // tbNewPass
            // 
            this.tbNewPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNewPass.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tbNewPass.Location = new System.Drawing.Point(222, 289);
            this.tbNewPass.MaxLength = 30;
            this.tbNewPass.Multiline = true;
            this.tbNewPass.Name = "tbNewPass";
            this.tbNewPass.PasswordChar = '#';
            this.tbNewPass.Size = new System.Drawing.Size(220, 28);
            this.tbNewPass.TabIndex = 24;
            // 
            // lbNewPass
            // 
            this.lbNewPass.AutoSize = true;
            this.lbNewPass.BackColor = System.Drawing.Color.Transparent;
            this.lbNewPass.Font = new System.Drawing.Font("Agency FB", 16.75F, System.Drawing.FontStyle.Bold);
            this.lbNewPass.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbNewPass.Location = new System.Drawing.Point(217, 256);
            this.lbNewPass.Name = "lbNewPass";
            this.lbNewPass.Size = new System.Drawing.Size(103, 28);
            this.lbNewPass.TabIndex = 25;
            this.lbNewPass.Text = "NOVA SENHA";
            // 
            // btnUpdatePassword
            // 
            this.btnUpdatePassword.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUpdatePassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdatePassword.BackgroundImage")));
            this.btnUpdatePassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdatePassword.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUpdatePassword.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdatePassword.Font = new System.Drawing.Font("Agency FB", 17.75F);
            this.btnUpdatePassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUpdatePassword.Image = global::UGNITE.Properties.Resources.icons8_protect_64;
            this.btnUpdatePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdatePassword.Location = new System.Drawing.Point(240, 330);
            this.btnUpdatePassword.Name = "btnUpdatePassword";
            this.btnUpdatePassword.Size = new System.Drawing.Size(181, 62);
            this.btnUpdatePassword.TabIndex = 26;
            this.btnUpdatePassword.Text = "ALTERAR";
            this.btnUpdatePassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdatePassword.UseVisualStyleBackColor = true;
            this.btnUpdatePassword.Click += new System.EventHandler(this.btnUpdatePassword_Click);
            // 
            // lbPoweredBy
            // 
            this.lbPoweredBy.AutoSize = true;
            this.lbPoweredBy.BackColor = System.Drawing.Color.Transparent;
            this.lbPoweredBy.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoweredBy.ForeColor = System.Drawing.Color.Silver;
            this.lbPoweredBy.Location = new System.Drawing.Point(235, 418);
            this.lbPoweredBy.Name = "lbPoweredBy";
            this.lbPoweredBy.Size = new System.Drawing.Size(186, 30);
            this.lbPoweredBy.TabIndex = 27;
            this.lbPoweredBy.Text = " © IRONIAWN S.A";
            // 
            // ForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(658, 457);
            this.Controls.Add(this.lbPoweredBy);
            this.Controls.Add(this.btnUpdatePassword);
            this.Controls.Add(this.tbNewPass);
            this.Controls.Add(this.lbNewPass);
            this.Controls.Add(this.tbSecurityCode);
            this.Controls.Add(this.lbSecurityCode);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.lbUserID);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.appLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UGNITE - Esqueci a Senha";
            this.Load += new System.EventHandler(this.ForgotPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox appLogo;
        private System.Windows.Forms.Label lbUserID;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbSecurityCode;
        private System.Windows.Forms.Label lbSecurityCode;
        private System.Windows.Forms.TextBox tbNewPass;
        private System.Windows.Forms.Label lbNewPass;
        private System.Windows.Forms.Button btnUpdatePassword;
        private System.Windows.Forms.Label lbPoweredBy;
    }
}