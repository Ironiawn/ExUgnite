namespace UGNITE.Janelas.Exibidores
{
    partial class InserirConquistas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InserirConquistas));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbDeveloperKey = new System.Windows.Forms.Label();
            this.pbUserImage = new System.Windows.Forms.PictureBox();
            this.lbAchievementsPanel = new System.Windows.Forms.Label();
            this.dataConquistas = new System.Windows.Forms.DataGridView();
            this.IMAGEM = new System.Windows.Forms.DataGridViewImageColumn();
            this.NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JOGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PainelDetalhes = new System.Windows.Forms.Panel();
            this.btnInsertAchievement = new System.Windows.Forms.Button();
            this.tbAlert = new System.Windows.Forms.Label();
            this.tbAchievementURL = new System.Windows.Forms.TextBox();
            this.lbAchievementIMG = new System.Windows.Forms.Label();
            this.tbGeneratedID = new System.Windows.Forms.TextBox();
            this.lbGeneratedID = new System.Windows.Forms.Label();
            this.comboGameTarget = new System.Windows.Forms.ComboBox();
            this.tbAchievementDescription = new System.Windows.Forms.TextBox();
            this.lbAchievementDescription = new System.Windows.Forms.Label();
            this.tbAchievementName = new System.Windows.Forms.TextBox();
            this.lbAchievementName = new System.Windows.Forms.Label();
            this.lbSelectGameTarget = new System.Windows.Forms.Label();
            this.lbInsercoes = new System.Windows.Forms.Label();
            this.lbGameAchievements = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataConquistas)).BeginInit();
            this.PainelDetalhes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDeveloperKey
            // 
            this.lbDeveloperKey.AutoSize = true;
            this.lbDeveloperKey.BackColor = System.Drawing.Color.Transparent;
            this.lbDeveloperKey.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbDeveloperKey.Font = new System.Drawing.Font("Agency FB", 17F, System.Drawing.FontStyle.Bold);
            this.lbDeveloperKey.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbDeveloperKey.Location = new System.Drawing.Point(100, 67);
            this.lbDeveloperKey.Name = "lbDeveloperKey";
            this.lbDeveloperKey.Size = new System.Drawing.Size(152, 28);
            this.lbDeveloperKey.TabIndex = 110;
            this.lbDeveloperKey.Text = "%DEVELOPERKEY%";
            // 
            // pbUserImage
            // 
            this.pbUserImage.BackColor = System.Drawing.Color.Transparent;
            this.pbUserImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbUserImage.BackgroundImage")));
            this.pbUserImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbUserImage.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pbUserImage.ErrorImage")));
            this.pbUserImage.Location = new System.Drawing.Point(4, 9);
            this.pbUserImage.Name = "pbUserImage";
            this.pbUserImage.Size = new System.Drawing.Size(87, 86);
            this.pbUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbUserImage.TabIndex = 109;
            this.pbUserImage.TabStop = false;
            // 
            // lbAchievementsPanel
            // 
            this.lbAchievementsPanel.AutoSize = true;
            this.lbAchievementsPanel.BackColor = System.Drawing.Color.Transparent;
            this.lbAchievementsPanel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbAchievementsPanel.Font = new System.Drawing.Font("Agency FB", 28F);
            this.lbAchievementsPanel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbAchievementsPanel.Location = new System.Drawing.Point(97, 9);
            this.lbAchievementsPanel.Name = "lbAchievementsPanel";
            this.lbAchievementsPanel.Size = new System.Drawing.Size(300, 46);
            this.lbAchievementsPanel.TabIndex = 108;
            this.lbAchievementsPanel.Text = "PAINEL DE CONQUISTAS";
            // 
            // dataConquistas
            // 
            this.dataConquistas.AllowUserToAddRows = false;
            this.dataConquistas.AllowUserToDeleteRows = false;
            this.dataConquistas.AllowUserToResizeColumns = false;
            this.dataConquistas.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Agency FB", 15F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataConquistas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataConquistas.BackgroundColor = System.Drawing.Color.Black;
            this.dataConquistas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataConquistas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataConquistas.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataConquistas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Agency FB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataConquistas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataConquistas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataConquistas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IMAGEM,
            this.NOME,
            this.DESCRICAO,
            this.JOGO,
            this.ID});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Agency FB", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataConquistas.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataConquistas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataConquistas.Location = new System.Drawing.Point(4, 101);
            this.dataConquistas.Name = "dataConquistas";
            this.dataConquistas.ReadOnly = true;
            this.dataConquistas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Agency FB", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataConquistas.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataConquistas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Agency FB", 15F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            this.dataConquistas.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataConquistas.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Navy;
            this.dataConquistas.RowTemplate.Height = 65;
            this.dataConquistas.RowTemplate.ReadOnly = true;
            this.dataConquistas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataConquistas.ShowEditingIcon = false;
            this.dataConquistas.Size = new System.Drawing.Size(993, 236);
            this.dataConquistas.TabIndex = 0;
            // 
            // IMAGEM
            // 
            this.IMAGEM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IMAGEM.DefaultCellStyle = dataGridViewCellStyle3;
            this.IMAGEM.Frozen = true;
            this.IMAGEM.HeaderText = "REPRESENTAÇÃO";
            this.IMAGEM.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.IMAGEM.Name = "IMAGEM";
            this.IMAGEM.ReadOnly = true;
            this.IMAGEM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IMAGEM.Width = 120;
            // 
            // NOME
            // 
            this.NOME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Agency FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NOME.DefaultCellStyle = dataGridViewCellStyle4;
            this.NOME.HeaderText = "NOME CONQUISTA";
            this.NOME.Name = "NOME";
            this.NOME.ReadOnly = true;
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Agency FB", 15.75F);
            this.DESCRICAO.DefaultCellStyle = dataGridViewCellStyle5;
            this.DESCRICAO.HeaderText = "DESCRIÇÃO";
            this.DESCRICAO.Name = "DESCRICAO";
            this.DESCRICAO.ReadOnly = true;
            this.DESCRICAO.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // JOGO
            // 
            this.JOGO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Agency FB", 15.75F);
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.JOGO.DefaultCellStyle = dataGridViewCellStyle6;
            this.JOGO.HeaderText = "JOGO";
            this.JOGO.Name = "JOGO";
            this.JOGO.ReadOnly = true;
            this.JOGO.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Agency FB", 15.75F);
            this.ID.DefaultCellStyle = dataGridViewCellStyle7;
            this.ID.HeaderText = "ID CONQUISTA";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // PainelDetalhes
            // 
            this.PainelDetalhes.BackColor = System.Drawing.Color.Black;
            this.PainelDetalhes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PainelDetalhes.BackgroundImage")));
            this.PainelDetalhes.Controls.Add(this.btnInsertAchievement);
            this.PainelDetalhes.Controls.Add(this.tbAlert);
            this.PainelDetalhes.Controls.Add(this.tbAchievementURL);
            this.PainelDetalhes.Controls.Add(this.lbAchievementIMG);
            this.PainelDetalhes.Controls.Add(this.tbGeneratedID);
            this.PainelDetalhes.Controls.Add(this.lbGeneratedID);
            this.PainelDetalhes.Controls.Add(this.comboGameTarget);
            this.PainelDetalhes.Controls.Add(this.tbAchievementDescription);
            this.PainelDetalhes.Controls.Add(this.lbAchievementDescription);
            this.PainelDetalhes.Controls.Add(this.tbAchievementName);
            this.PainelDetalhes.Controls.Add(this.lbAchievementName);
            this.PainelDetalhes.Controls.Add(this.lbSelectGameTarget);
            this.PainelDetalhes.Controls.Add(this.lbInsercoes);
            this.PainelDetalhes.Location = new System.Drawing.Point(4, 343);
            this.PainelDetalhes.Name = "PainelDetalhes";
            this.PainelDetalhes.Size = new System.Drawing.Size(993, 176);
            this.PainelDetalhes.TabIndex = 112;
            // 
            // btnInsertAchievement
            // 
            this.btnInsertAchievement.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInsertAchievement.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsertAchievement.BackgroundImage")));
            this.btnInsertAchievement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsertAchievement.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInsertAchievement.Font = new System.Drawing.Font("Agency FB", 18.75F);
            this.btnInsertAchievement.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInsertAchievement.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertAchievement.Image")));
            this.btnInsertAchievement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInsertAchievement.Location = new System.Drawing.Point(833, 7);
            this.btnInsertAchievement.Name = "btnInsertAchievement";
            this.btnInsertAchievement.Size = new System.Drawing.Size(153, 65);
            this.btnInsertAchievement.TabIndex = 118;
            this.btnInsertAchievement.Text = "INSERIR";
            this.btnInsertAchievement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInsertAchievement.UseVisualStyleBackColor = true;
            this.btnInsertAchievement.Click += new System.EventHandler(this.BtnInsertAchievement_Click);
            // 
            // tbAlert
            // 
            this.tbAlert.AutoSize = true;
            this.tbAlert.BackColor = System.Drawing.Color.Transparent;
            this.tbAlert.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbAlert.Font = new System.Drawing.Font("Agency FB", 14F);
            this.tbAlert.ForeColor = System.Drawing.Color.Cornsilk;
            this.tbAlert.Location = new System.Drawing.Point(503, 22);
            this.tbAlert.Name = "tbAlert";
            this.tbAlert.Size = new System.Drawing.Size(375, 72);
            this.tbAlert.TabIndex = 117;
            this.tbAlert.Text = "ATENÇÃO:\r\nEdição de conquistas terão de ser feitas\r\nmanualmente por nossa equipe." +
    " E-mail: shop@ironiawn.com.br";
            // 
            // tbAchievementURL
            // 
            this.tbAchievementURL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAchievementURL.Font = new System.Drawing.Font("Agency FB", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAchievementURL.Location = new System.Drawing.Point(494, 130);
            this.tbAchievementURL.MaxLength = 100;
            this.tbAchievementURL.Multiline = true;
            this.tbAchievementURL.Name = "tbAchievementURL";
            this.tbAchievementURL.Size = new System.Drawing.Size(269, 28);
            this.tbAchievementURL.TabIndex = 115;
            // 
            // lbAchievementIMG
            // 
            this.lbAchievementIMG.AutoSize = true;
            this.lbAchievementIMG.BackColor = System.Drawing.Color.Transparent;
            this.lbAchievementIMG.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbAchievementIMG.Font = new System.Drawing.Font("Agency FB", 17F, System.Drawing.FontStyle.Bold);
            this.lbAchievementIMG.ForeColor = System.Drawing.Color.Olive;
            this.lbAchievementIMG.Location = new System.Drawing.Point(489, 99);
            this.lbAchievementIMG.Name = "lbAchievementIMG";
            this.lbAchievementIMG.Size = new System.Drawing.Size(364, 28);
            this.lbAchievementIMG.TabIndex = 114;
            this.lbAchievementIMG.Text = "REPRESENTAÇÃO CONQUISTA (128X128) VIA URL";
            // 
            // tbGeneratedID
            // 
            this.tbGeneratedID.BackColor = System.Drawing.Color.Red;
            this.tbGeneratedID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbGeneratedID.Enabled = false;
            this.tbGeneratedID.Font = new System.Drawing.Font("Agency FB", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGeneratedID.ForeColor = System.Drawing.Color.White;
            this.tbGeneratedID.Location = new System.Drawing.Point(225, 68);
            this.tbGeneratedID.MaxLength = 100;
            this.tbGeneratedID.Multiline = true;
            this.tbGeneratedID.Name = "tbGeneratedID";
            this.tbGeneratedID.ReadOnly = true;
            this.tbGeneratedID.Size = new System.Drawing.Size(269, 28);
            this.tbGeneratedID.TabIndex = 113;
            // 
            // lbGeneratedID
            // 
            this.lbGeneratedID.AutoSize = true;
            this.lbGeneratedID.BackColor = System.Drawing.Color.Transparent;
            this.lbGeneratedID.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbGeneratedID.Font = new System.Drawing.Font("Agency FB", 17F, System.Drawing.FontStyle.Bold);
            this.lbGeneratedID.ForeColor = System.Drawing.Color.Olive;
            this.lbGeneratedID.Location = new System.Drawing.Point(220, 37);
            this.lbGeneratedID.Name = "lbGeneratedID";
            this.lbGeneratedID.Size = new System.Drawing.Size(263, 28);
            this.lbGeneratedID.TabIndex = 112;
            this.lbGeneratedID.Text = "ID DA CONQUISTA (AUTO-GERADA)";
            // 
            // comboGameTarget
            // 
            this.comboGameTarget.DropDownHeight = 150;
            this.comboGameTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGameTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboGameTarget.FormattingEnabled = true;
            this.comboGameTarget.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.comboGameTarget.IntegralHeight = false;
            this.comboGameTarget.ItemHeight = 13;
            this.comboGameTarget.Location = new System.Drawing.Point(18, 73);
            this.comboGameTarget.Name = "comboGameTarget";
            this.comboGameTarget.Size = new System.Drawing.Size(184, 21);
            this.comboGameTarget.TabIndex = 107;
            // 
            // tbAchievementDescription
            // 
            this.tbAchievementDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAchievementDescription.Font = new System.Drawing.Font("Agency FB", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAchievementDescription.Location = new System.Drawing.Point(214, 130);
            this.tbAchievementDescription.MaxLength = 100;
            this.tbAchievementDescription.Multiline = true;
            this.tbAchievementDescription.Name = "tbAchievementDescription";
            this.tbAchievementDescription.Size = new System.Drawing.Size(269, 28);
            this.tbAchievementDescription.TabIndex = 111;
            // 
            // lbAchievementDescription
            // 
            this.lbAchievementDescription.AutoSize = true;
            this.lbAchievementDescription.BackColor = System.Drawing.Color.Transparent;
            this.lbAchievementDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbAchievementDescription.Font = new System.Drawing.Font("Agency FB", 17F, System.Drawing.FontStyle.Bold);
            this.lbAchievementDescription.ForeColor = System.Drawing.Color.Olive;
            this.lbAchievementDescription.Location = new System.Drawing.Point(209, 99);
            this.lbAchievementDescription.Name = "lbAchievementDescription";
            this.lbAchievementDescription.Size = new System.Drawing.Size(183, 28);
            this.lbAchievementDescription.TabIndex = 110;
            this.lbAchievementDescription.Text = "DESCRIÇÃO CONQUISTA";
            // 
            // tbAchievementName
            // 
            this.tbAchievementName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAchievementName.Font = new System.Drawing.Font("Agency FB", 17F);
            this.tbAchievementName.Location = new System.Drawing.Point(19, 130);
            this.tbAchievementName.MaxLength = 25;
            this.tbAchievementName.Multiline = true;
            this.tbAchievementName.Name = "tbAchievementName";
            this.tbAchievementName.Size = new System.Drawing.Size(184, 28);
            this.tbAchievementName.TabIndex = 109;
            // 
            // lbAchievementName
            // 
            this.lbAchievementName.AutoSize = true;
            this.lbAchievementName.BackColor = System.Drawing.Color.Transparent;
            this.lbAchievementName.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbAchievementName.Font = new System.Drawing.Font("Agency FB", 17F, System.Drawing.FontStyle.Bold);
            this.lbAchievementName.ForeColor = System.Drawing.Color.Olive;
            this.lbAchievementName.Location = new System.Drawing.Point(14, 99);
            this.lbAchievementName.Name = "lbAchievementName";
            this.lbAchievementName.Size = new System.Drawing.Size(166, 28);
            this.lbAchievementName.TabIndex = 108;
            this.lbAchievementName.Text = "NOME DA CONQUISTA";
            // 
            // lbSelectGameTarget
            // 
            this.lbSelectGameTarget.AutoSize = true;
            this.lbSelectGameTarget.BackColor = System.Drawing.Color.Transparent;
            this.lbSelectGameTarget.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbSelectGameTarget.Font = new System.Drawing.Font("Agency FB", 17F, System.Drawing.FontStyle.Bold);
            this.lbSelectGameTarget.ForeColor = System.Drawing.Color.Olive;
            this.lbSelectGameTarget.Location = new System.Drawing.Point(13, 44);
            this.lbSelectGameTarget.Name = "lbSelectGameTarget";
            this.lbSelectGameTarget.Size = new System.Drawing.Size(190, 28);
            this.lbSelectGameTarget.TabIndex = 106;
            this.lbSelectGameTarget.Text = "SELECIONE O JOGO ALVO";
            // 
            // lbInsercoes
            // 
            this.lbInsercoes.AutoSize = true;
            this.lbInsercoes.BackColor = System.Drawing.Color.Transparent;
            this.lbInsercoes.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbInsercoes.Font = new System.Drawing.Font("Agency FB", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInsercoes.ForeColor = System.Drawing.Color.Aqua;
            this.lbInsercoes.Location = new System.Drawing.Point(13, 11);
            this.lbInsercoes.Name = "lbInsercoes";
            this.lbInsercoes.Size = new System.Drawing.Size(198, 33);
            this.lbInsercoes.TabIndex = 105;
            this.lbInsercoes.Text = "INSERIR CONQUISTAS";
            // 
            // lbGameAchievements
            // 
            this.lbGameAchievements.AutoSize = true;
            this.lbGameAchievements.BackColor = System.Drawing.Color.Transparent;
            this.lbGameAchievements.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbGameAchievements.Font = new System.Drawing.Font("Agency FB", 17F, System.Drawing.FontStyle.Bold);
            this.lbGameAchievements.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbGameAchievements.Location = new System.Drawing.Point(662, 70);
            this.lbGameAchievements.Name = "lbGameAchievements";
            this.lbGameAchievements.Size = new System.Drawing.Size(328, 28);
            this.lbGameAchievements.TabIndex = 113;
            this.lbGameAchievements.Text = "CONQUISTAS DISPONÍVEIS EM SEUS JOGOS";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::UGNITE.Properties.Resources.Programming_Delete_Sign_icon;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(966, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 25);
            this.btnClose.TabIndex = 114;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // InserirConquistas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1002, 531);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbGameAchievements);
            this.Controls.Add(this.PainelDetalhes);
            this.Controls.Add(this.dataConquistas);
            this.Controls.Add(this.lbDeveloperKey);
            this.Controls.Add(this.pbUserImage);
            this.Controls.Add(this.lbAchievementsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InserirConquistas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ugnite - Inserir Conquistas";
            this.Load += new System.EventHandler(this.InserirConquistas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbUserImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataConquistas)).EndInit();
            this.PainelDetalhes.ResumeLayout(false);
            this.PainelDetalhes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDeveloperKey;
        private System.Windows.Forms.PictureBox pbUserImage;
        private System.Windows.Forms.Label lbAchievementsPanel;
        private System.Windows.Forms.DataGridView dataConquistas;
        private System.Windows.Forms.Panel PainelDetalhes;
        private System.Windows.Forms.ComboBox comboGameTarget;
        private System.Windows.Forms.Label lbSelectGameTarget;
        private System.Windows.Forms.Label lbInsercoes;
        private System.Windows.Forms.Label lbAchievementName;
        private System.Windows.Forms.TextBox tbAchievementName;
        private System.Windows.Forms.TextBox tbAchievementDescription;
        private System.Windows.Forms.Label lbAchievementDescription;
        private System.Windows.Forms.TextBox tbAchievementURL;
        private System.Windows.Forms.Label lbAchievementIMG;
        private System.Windows.Forms.Button btnInsertAchievement;
        private System.Windows.Forms.Label tbAlert;
        private System.Windows.Forms.Label lbGameAchievements;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.DataGridViewImageColumn IMAGEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn JOGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.TextBox tbGeneratedID;
        private System.Windows.Forms.Label lbGeneratedID;
    }
}