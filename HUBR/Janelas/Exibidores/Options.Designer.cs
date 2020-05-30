namespace UGNITE
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.appLogo = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.lbDialogAbout = new System.Windows.Forms.Label();
            this.tbSecurityCode = new System.Windows.Forms.TextBox();
            this.lbSecurityCode = new System.Windows.Forms.Label();
            this.tbUsermail = new System.Windows.Forms.TextBox();
            this.lbUsermail = new System.Windows.Forms.Label();
            this.tbNewPass = new System.Windows.Forms.TextBox();
            this.lbNewPass = new System.Windows.Forms.Label();
            this.lbUserDetails = new System.Windows.Forms.Label();
            this.lbUpdateAlert = new System.Windows.Forms.Label();
            this.btnUpdateInformation = new System.Windows.Forms.Button();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.opfd_HUBR = new System.Windows.Forms.OpenFileDialog();
            this.tbImageURL = new System.Windows.Forms.TextBox();
            this.lbImageURL = new System.Windows.Forms.Label();
            this.lbLoadingProgress = new System.Windows.Forms.Label();
            this.checkSaveLogin = new System.Windows.Forms.CheckBox();
            this.ThemeSelector = new System.Windows.Forms.ComboBox();
            this.lbTheme = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // appLogo
            // 
            this.appLogo.BackColor = System.Drawing.Color.Transparent;
            this.appLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("appLogo.BackgroundImage")));
            this.appLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.appLogo.Location = new System.Drawing.Point(12, 12);
            this.appLogo.Name = "appLogo";
            this.appLogo.Size = new System.Drawing.Size(90, 84);
            this.appLogo.TabIndex = 1;
            this.appLogo.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::UGNITE.Properties.Resources.Programming_Delete_Sign_icon;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(800, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 25);
            this.btnClose.TabIndex = 17;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbDialogAbout
            // 
            this.lbDialogAbout.AutoSize = true;
            this.lbDialogAbout.BackColor = System.Drawing.Color.Transparent;
            this.lbDialogAbout.Font = new System.Drawing.Font("Agency FB", 21F);
            this.lbDialogAbout.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbDialogAbout.Location = new System.Drawing.Point(108, 12);
            this.lbDialogAbout.Name = "lbDialogAbout";
            this.lbDialogAbout.Size = new System.Drawing.Size(325, 33);
            this.lbDialogAbout.TabIndex = 22;
            this.lbDialogAbout.Text = "CONFIGURAÇÕES DE CONTA DE USUÁRIO";
            // 
            // tbSecurityCode
            // 
            this.tbSecurityCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSecurityCode.Font = new System.Drawing.Font("Agency FB", 17F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tbSecurityCode.Location = new System.Drawing.Point(594, 282);
            this.tbSecurityCode.MaxLength = 10;
            this.tbSecurityCode.Multiline = true;
            this.tbSecurityCode.Name = "tbSecurityCode";
            this.tbSecurityCode.Size = new System.Drawing.Size(220, 28);
            this.tbSecurityCode.TabIndex = 3;
            // 
            // lbSecurityCode
            // 
            this.lbSecurityCode.AutoSize = true;
            this.lbSecurityCode.BackColor = System.Drawing.Color.Transparent;
            this.lbSecurityCode.Font = new System.Drawing.Font("Agency FB", 18F);
            this.lbSecurityCode.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbSecurityCode.Location = new System.Drawing.Point(589, 249);
            this.lbSecurityCode.Name = "lbSecurityCode";
            this.lbSecurityCode.Size = new System.Drawing.Size(162, 28);
            this.lbSecurityCode.TabIndex = 24;
            this.lbSecurityCode.Text = "CÓDIGO VERIFICADOR";
            // 
            // tbUsermail
            // 
            this.tbUsermail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUsermail.Font = new System.Drawing.Font("Agency FB", 17F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tbUsermail.Location = new System.Drawing.Point(594, 128);
            this.tbUsermail.MaxLength = 25;
            this.tbUsermail.Multiline = true;
            this.tbUsermail.Name = "tbUsermail";
            this.tbUsermail.Size = new System.Drawing.Size(220, 28);
            this.tbUsermail.TabIndex = 1;
            // 
            // lbUsermail
            // 
            this.lbUsermail.AutoSize = true;
            this.lbUsermail.BackColor = System.Drawing.Color.Transparent;
            this.lbUsermail.Font = new System.Drawing.Font("Agency FB", 18F);
            this.lbUsermail.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbUsermail.Location = new System.Drawing.Point(589, 95);
            this.lbUsermail.Name = "lbUsermail";
            this.lbUsermail.Size = new System.Drawing.Size(153, 28);
            this.lbUsermail.TabIndex = 27;
            this.lbUsermail.Text = "E-MAIL DE USUÁRIO";
            // 
            // tbNewPass
            // 
            this.tbNewPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNewPass.Font = new System.Drawing.Font("Agency FB", 17F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tbNewPass.Location = new System.Drawing.Point(594, 201);
            this.tbNewPass.MaxLength = 30;
            this.tbNewPass.Multiline = true;
            this.tbNewPass.Name = "tbNewPass";
            this.tbNewPass.PasswordChar = '•';
            this.tbNewPass.Size = new System.Drawing.Size(220, 28);
            this.tbNewPass.TabIndex = 2;
            // 
            // lbNewPass
            // 
            this.lbNewPass.AutoSize = true;
            this.lbNewPass.BackColor = System.Drawing.Color.Transparent;
            this.lbNewPass.Font = new System.Drawing.Font("Agency FB", 18F);
            this.lbNewPass.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbNewPass.Location = new System.Drawing.Point(589, 168);
            this.lbNewPass.Name = "lbNewPass";
            this.lbNewPass.Size = new System.Drawing.Size(103, 28);
            this.lbNewPass.TabIndex = 30;
            this.lbNewPass.Text = "NOVA SENHA";
            // 
            // lbUserDetails
            // 
            this.lbUserDetails.AutoSize = true;
            this.lbUserDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbUserDetails.Font = new System.Drawing.Font("Agency FB", 18F);
            this.lbUserDetails.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbUserDetails.Location = new System.Drawing.Point(14, 99);
            this.lbUserDetails.Name = "lbUserDetails";
            this.lbUserDetails.Size = new System.Drawing.Size(266, 112);
            this.lbUserDetails.TabIndex = 31;
            this.lbUserDetails.Text = "DADOS ATUAIS\r\n\r\nEMAIL.: @USERMAIL\r\nIDENTIFICAÇÃO : @USERNAME #@ID\r\n";
            // 
            // lbUpdateAlert
            // 
            this.lbUpdateAlert.AutoSize = true;
            this.lbUpdateAlert.BackColor = System.Drawing.Color.Transparent;
            this.lbUpdateAlert.Font = new System.Drawing.Font("Agency FB", 12.5F);
            this.lbUpdateAlert.ForeColor = System.Drawing.Color.Red;
            this.lbUpdateAlert.Location = new System.Drawing.Point(19, 434);
            this.lbUpdateAlert.Name = "lbUpdateAlert";
            this.lbUpdateAlert.Size = new System.Drawing.Size(749, 66);
            this.lbUpdateAlert.TabIndex = 33;
            this.lbUpdateAlert.Text = resources.GetString("lbUpdateAlert.Text");
            // 
            // btnUpdateInformation
            // 
            this.btnUpdateInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUpdateInformation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdateInformation.BackgroundImage")));
            this.btnUpdateInformation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdateInformation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUpdateInformation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateInformation.Font = new System.Drawing.Font("Agency FB", 19F);
            this.btnUpdateInformation.ForeColor = System.Drawing.Color.Red;
            this.btnUpdateInformation.Image = global::UGNITE.Properties.Resources.icons8_available_updates_64;
            this.btnUpdateInformation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateInformation.Location = new System.Drawing.Point(639, 461);
            this.btnUpdateInformation.Name = "btnUpdateInformation";
            this.btnUpdateInformation.Size = new System.Drawing.Size(175, 71);
            this.btnUpdateInformation.TabIndex = 4;
            this.btnUpdateInformation.Text = "ALTERAR \r\nDADOS";
            this.btnUpdateInformation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateInformation.UseVisualStyleBackColor = true;
            this.btnUpdateInformation.Click += new System.EventHandler(this.btnUpdateInformation_Click);
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUploadImage.AutoSize = true;
            this.btnUploadImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUploadImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUploadImage.BackgroundImage")));
            this.btnUploadImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUploadImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUploadImage.Font = new System.Drawing.Font("Agency FB", 19F);
            this.btnUploadImage.ForeColor = System.Drawing.Color.Yellow;
            this.btnUploadImage.Location = new System.Drawing.Point(23, 388);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(202, 41);
            this.btnUploadImage.TabIndex = 34;
            this.btnUploadImage.Text = "ALTERAR MINHA IMAGEM";
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_ClickAsync);
            // 
            // opfd_HUBR
            // 
            this.opfd_HUBR.FileName = "Arquivo de Imagem";
            this.opfd_HUBR.Title = "Ugnite - Adicionar Imagem";
            // 
            // tbImageURL
            // 
            this.tbImageURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbImageURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbImageURL.Cursor = System.Windows.Forms.Cursors.Cross;
            this.tbImageURL.Font = new System.Drawing.Font("Agency FB", 17F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tbImageURL.Location = new System.Drawing.Point(23, 351);
            this.tbImageURL.MaxLength = 200;
            this.tbImageURL.Name = "tbImageURL";
            this.tbImageURL.Size = new System.Drawing.Size(323, 34);
            this.tbImageURL.TabIndex = 1;
            // 
            // lbImageURL
            // 
            this.lbImageURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbImageURL.AutoSize = true;
            this.lbImageURL.BackColor = System.Drawing.Color.Transparent;
            this.lbImageURL.Font = new System.Drawing.Font("Agency FB", 19F);
            this.lbImageURL.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbImageURL.Location = new System.Drawing.Point(18, 318);
            this.lbImageURL.Name = "lbImageURL";
            this.lbImageURL.Size = new System.Drawing.Size(129, 31);
            this.lbImageURL.TabIndex = 36;
            this.lbImageURL.Text = "URL DA IMAGEM";
            this.lbImageURL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbLoadingProgress
            // 
            this.lbLoadingProgress.AutoSize = true;
            this.lbLoadingProgress.BackColor = System.Drawing.Color.Transparent;
            this.lbLoadingProgress.Font = new System.Drawing.Font("Agency FB", 38F);
            this.lbLoadingProgress.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbLoadingProgress.Location = new System.Drawing.Point(132, 221);
            this.lbLoadingProgress.Name = "lbLoadingProgress";
            this.lbLoadingProgress.Size = new System.Drawing.Size(542, 61);
            this.lbLoadingProgress.TabIndex = 37;
            this.lbLoadingProgress.Text = "CARREGANDO INFORMAÇÕES (1/5)";
            // 
            // checkSaveLogin
            // 
            this.checkSaveLogin.AutoSize = true;
            this.checkSaveLogin.BackColor = System.Drawing.Color.Transparent;
            this.checkSaveLogin.Font = new System.Drawing.Font("Agency FB", 14F);
            this.checkSaveLogin.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.checkSaveLogin.Location = new System.Drawing.Point(24, 492);
            this.checkSaveLogin.Name = "checkSaveLogin";
            this.checkSaveLogin.Size = new System.Drawing.Size(107, 28);
            this.checkSaveLogin.TabIndex = 40;
            this.checkSaveLogin.Text = "SALVAR LOGIN";
            this.checkSaveLogin.UseVisualStyleBackColor = false;
            this.checkSaveLogin.CheckedChanged += new System.EventHandler(this.checkSaveLogin_CheckedChanged);
            // 
            // ThemeSelector
            // 
            this.ThemeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ThemeSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ThemeSelector.Font = new System.Drawing.Font("Agency FB", 16F, System.Drawing.FontStyle.Bold);
            this.ThemeSelector.FormattingEnabled = true;
            this.ThemeSelector.IntegralHeight = false;
            this.ThemeSelector.ItemHeight = 25;
            this.ThemeSelector.Location = new System.Drawing.Point(352, 352);
            this.ThemeSelector.Name = "ThemeSelector";
            this.ThemeSelector.Size = new System.Drawing.Size(146, 33);
            this.ThemeSelector.TabIndex = 0;
            this.ThemeSelector.SelectedIndexChanged += new System.EventHandler(this.ThemeSelector_SelectedIndexChanged);
            // 
            // lbTheme
            // 
            this.lbTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTheme.AutoSize = true;
            this.lbTheme.BackColor = System.Drawing.Color.Transparent;
            this.lbTheme.Font = new System.Drawing.Font("Agency FB", 19F);
            this.lbTheme.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbTheme.Location = new System.Drawing.Point(346, 318);
            this.lbTheme.Name = "lbTheme";
            this.lbTheme.Size = new System.Drawing.Size(53, 31);
            this.lbTheme.TabIndex = 41;
            this.lbTheme.Text = "TEMA";
            this.lbTheme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(835, 544);
            this.Controls.Add(this.lbTheme);
            this.Controls.Add(this.ThemeSelector);
            this.Controls.Add(this.checkSaveLogin);
            this.Controls.Add(this.lbLoadingProgress);
            this.Controls.Add(this.tbImageURL);
            this.Controls.Add(this.lbImageURL);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.btnUpdateInformation);
            this.Controls.Add(this.lbUpdateAlert);
            this.Controls.Add(this.lbUserDetails);
            this.Controls.Add(this.tbNewPass);
            this.Controls.Add(this.lbNewPass);
            this.Controls.Add(this.tbUsermail);
            this.Controls.Add(this.lbUsermail);
            this.Controls.Add(this.tbSecurityCode);
            this.Controls.Add(this.lbSecurityCode);
            this.Controls.Add(this.lbDialogAbout);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.appLogo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UGNITE - Opções";
            this.Load += new System.EventHandler(this.Options_Load);
            ((System.ComponentModel.ISupportInitialize)(this.appLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox appLogo;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Label lbDialogAbout;
        private System.Windows.Forms.TextBox tbSecurityCode;
        private System.Windows.Forms.Label lbSecurityCode;
        private System.Windows.Forms.TextBox tbUsermail;
        private System.Windows.Forms.Label lbUsermail;
        private System.Windows.Forms.TextBox tbNewPass;
        private System.Windows.Forms.Label lbNewPass;
        private System.Windows.Forms.Label lbUserDetails;
        private System.Windows.Forms.Label lbUpdateAlert;
        private System.Windows.Forms.Button btnUpdateInformation;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.OpenFileDialog opfd_HUBR;
        private System.Windows.Forms.TextBox tbImageURL;
        private System.Windows.Forms.Label lbImageURL;
        private System.Windows.Forms.Label lbLoadingProgress;
        private System.Windows.Forms.CheckBox checkSaveLogin;
        private System.Windows.Forms.ComboBox ThemeSelector;
        private System.Windows.Forms.Label lbTheme;
    }
}