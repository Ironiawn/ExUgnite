namespace UGNITE
{
    partial class PlusPlans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlusPlans));
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.lbPlusPlans = new System.Windows.Forms.Label();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.btnPay14Days = new System.Windows.Forms.Button();
            this.lbPlanPrice = new System.Windows.Forms.Label();
            this.lb14Days = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPay30Days = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb30Days = new System.Windows.Forms.Label();
            this.lbCurrentUserPlusDetails = new System.Windows.Forms.Label();
            this.lbPlusInfos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::UGNITE.Properties.Resources.Programming_Delete_Sign_icon;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(764, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 25);
            this.btnClose.TabIndex = 18;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbPlusPlans
            // 
            this.lbPlusPlans.AutoSize = true;
            this.lbPlusPlans.BackColor = System.Drawing.Color.Transparent;
            this.lbPlusPlans.Font = new System.Drawing.Font("Agency FB", 27F);
            this.lbPlusPlans.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbPlusPlans.Location = new System.Drawing.Point(12, 12);
            this.lbPlusPlans.Name = "lbPlusPlans";
            this.lbPlusPlans.Size = new System.Drawing.Size(190, 44);
            this.lbPlusPlans.TabIndex = 41;
            this.lbPlusPlans.Text = "PLANOS UGNITE+";
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.Wheat;
            this.panelContainer.Controls.Add(this.btnPay14Days);
            this.panelContainer.Controls.Add(this.lbPlanPrice);
            this.panelContainer.Controls.Add(this.lb14Days);
            this.panelContainer.Location = new System.Drawing.Point(50, 110);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(321, 137);
            this.panelContainer.TabIndex = 42;
            // 
            // btnPay14Days
            // 
            this.btnPay14Days.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPay14Days.BackColor = System.Drawing.Color.Wheat;
            this.btnPay14Days.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPay14Days.BackgroundImage")));
            this.btnPay14Days.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPay14Days.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPay14Days.Font = new System.Drawing.Font("Agency FB", 13.75F);
            this.btnPay14Days.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPay14Days.Image = global::UGNITE.Properties.Resources.icons8_bank_48;
            this.btnPay14Days.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPay14Days.Location = new System.Drawing.Point(135, 71);
            this.btnPay14Days.Name = "btnPay14Days";
            this.btnPay14Days.Size = new System.Drawing.Size(144, 50);
            this.btnPay14Days.TabIndex = 43;
            this.btnPay14Days.Text = "COMPRAR COM\r\nA UPAY\r\n";
            this.btnPay14Days.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPay14Days.UseCompatibleTextRendering = true;
            this.btnPay14Days.UseVisualStyleBackColor = false;
            this.btnPay14Days.Click += new System.EventHandler(this.btnBuyWithWallet_Click);
            // 
            // lbPlanPrice
            // 
            this.lbPlanPrice.AutoSize = true;
            this.lbPlanPrice.BackColor = System.Drawing.Color.Transparent;
            this.lbPlanPrice.Font = new System.Drawing.Font("Agency FB", 27F);
            this.lbPlanPrice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbPlanPrice.Location = new System.Drawing.Point(17, 71);
            this.lbPlanPrice.Name = "lbPlanPrice";
            this.lbPlanPrice.Size = new System.Drawing.Size(104, 44);
            this.lbPlanPrice.TabIndex = 44;
            this.lbPlanPrice.Text = "R$19,90";
            // 
            // lb14Days
            // 
            this.lb14Days.AutoSize = true;
            this.lb14Days.BackColor = System.Drawing.Color.Transparent;
            this.lb14Days.Font = new System.Drawing.Font("Agency FB", 27F);
            this.lb14Days.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lb14Days.Location = new System.Drawing.Point(17, 13);
            this.lb14Days.Name = "lb14Days";
            this.lb14Days.Size = new System.Drawing.Size(199, 44);
            this.lb14Days.TabIndex = 43;
            this.lb14Days.Text = "UGNITE+ | 14 DIAS";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Wheat;
            this.panel1.Controls.Add(this.btnPay30Days);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lb30Days);
            this.panel1.Location = new System.Drawing.Point(435, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 137);
            this.panel1.TabIndex = 54;
            // 
            // btnPay30Days
            // 
            this.btnPay30Days.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPay30Days.BackColor = System.Drawing.Color.Wheat;
            this.btnPay30Days.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPay30Days.BackgroundImage")));
            this.btnPay30Days.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPay30Days.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPay30Days.Font = new System.Drawing.Font("Agency FB", 13.75F);
            this.btnPay30Days.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPay30Days.Image = global::UGNITE.Properties.Resources.icons8_bank_48;
            this.btnPay30Days.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPay30Days.Location = new System.Drawing.Point(151, 71);
            this.btnPay30Days.Name = "btnPay30Days";
            this.btnPay30Days.Size = new System.Drawing.Size(144, 50);
            this.btnPay30Days.TabIndex = 43;
            this.btnPay30Days.Text = "COMPRAR COM\r\nA UPAY";
            this.btnPay30Days.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPay30Days.UseCompatibleTextRendering = true;
            this.btnPay30Days.UseVisualStyleBackColor = false;
            this.btnPay30Days.Click += new System.EventHandler(this.btnPay30Days_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Agency FB", 27F);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(17, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 44);
            this.label1.TabIndex = 44;
            this.label1.Text = "R$29,90";
            // 
            // lb30Days
            // 
            this.lb30Days.AutoSize = true;
            this.lb30Days.BackColor = System.Drawing.Color.Transparent;
            this.lb30Days.Font = new System.Drawing.Font("Agency FB", 27F);
            this.lb30Days.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lb30Days.Location = new System.Drawing.Point(17, 13);
            this.lb30Days.Name = "lb30Days";
            this.lb30Days.Size = new System.Drawing.Size(208, 44);
            this.lb30Days.TabIndex = 43;
            this.lb30Days.Text = "UGNITE+ | 30 DIAS";
            // 
            // lbCurrentUserPlusDetails
            // 
            this.lbCurrentUserPlusDetails.AutoSize = true;
            this.lbCurrentUserPlusDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbCurrentUserPlusDetails.Font = new System.Drawing.Font("Agency FB", 24F);
            this.lbCurrentUserPlusDetails.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbCurrentUserPlusDetails.Location = new System.Drawing.Point(52, 280);
            this.lbCurrentUserPlusDetails.Name = "lbCurrentUserPlusDetails";
            this.lbCurrentUserPlusDetails.Size = new System.Drawing.Size(199, 40);
            this.lbCurrentUserPlusDetails.TabIndex = 55;
            this.lbCurrentUserPlusDetails.Text = "%DETALHESPLUS%";
            // 
            // lbPlusInfos
            // 
            this.lbPlusInfos.AutoSize = true;
            this.lbPlusInfos.BackColor = System.Drawing.Color.Transparent;
            this.lbPlusInfos.Font = new System.Drawing.Font("Agency FB", 27F);
            this.lbPlusInfos.ForeColor = System.Drawing.Color.HotPink;
            this.lbPlusInfos.Location = new System.Drawing.Point(67, 63);
            this.lbPlusInfos.Name = "lbPlusInfos";
            this.lbPlusInfos.Size = new System.Drawing.Size(673, 44);
            this.lbPlusInfos.TabIndex = 56;
            this.lbPlusInfos.Text = "OS DIAS SERÃO SOMADOS CASO VOCÊ JÁ POSSUA O PLANO ATIVO";
            // 
            // PlusPlans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 329);
            this.Controls.Add(this.lbPlusInfos);
            this.Controls.Add(this.lbCurrentUserPlusDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.lbPlusPlans);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlusPlans";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLANOS UGNITE+";
            this.Load += new System.EventHandler(this.PlusPlans_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.Label lbPlusPlans;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lbPlanPrice;
        private System.Windows.Forms.Label lb14Days;
        public System.Windows.Forms.Button btnPay14Days;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnPay30Days;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb30Days;
        private System.Windows.Forms.Label lbCurrentUserPlusDetails;
        private System.Windows.Forms.Label lbPlusInfos;
    }
}