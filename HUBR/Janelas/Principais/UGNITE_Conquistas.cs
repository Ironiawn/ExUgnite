using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UGNITE.Janelas.Principais
{
    public partial class UGNITE_Conquistas : Form
    {
        public UGNITE_Conquistas()
        {
            InitializeComponent();
        }

        void ApplyTranslation()
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                btnStore.Text = Program.GetStringRM("en", "btnStore");
                btnLibrary.Text = Program.GetStringRM("en", "btnLibrary");
                btnYourActivity.Text = Program.GetStringRM("en", "btnYourActivity");
                btnYourActivity.Location = new Point(289, 3);
                btnAchievements.Location = new Point(420, 4);
                btnAchievements.Text = Program.GetStringRM("en", "btnAchievements");
                lbYourAchievements.Text = Program.GetStringRM("en", "lbYourAchievements");

                dataConquistas.Columns[1].HeaderCell.Value = "CONQUER NAME";
                dataConquistas.Columns[2].HeaderCell.Value = "DESCRIPTION";
                dataConquistas.Columns[3].HeaderCell.Value = "GAME";
                dataConquistas.Columns[4].HeaderCell.Value = "ADDED ON";
            }
        }

        private void UGNITE_Conquistas_Load(object sender, EventArgs e)
        {
            try
            {
                #region ADICIONA AS CONQUISTAS
                 
                // Cria uma lista temporária para abrigar as conquistas
                List<string> ConquerID = MySQL.RetrieveConquers;

                // Se a lista não for nula, continuar
                if (ConquerID != null)
                {
                    // Na planilha, adiciona todos os dados necessários das conquistas
                    for (int i = 0; i < ConquerID.Count; i++)
                    {
                        // Adiciona uma linha
                        dataConquistas.Rows.Add();

                        // Preeche as colunas da linha
                        dataConquistas.Rows[i].Cells["IMAGEM"].Value = ProgramData.GetImagemURL(MySQL.ConquerInfo(ConquerID[i], 1));
                        dataConquistas.Rows[i].Cells["DESCRICAO"].Value = MySQL.ConquerInfo(ConquerID[i], 0);
                        dataConquistas.Rows[i].Cells["JOGO"].Value = MySQL.ConquerInfo(ConquerID[i], 3);
                        dataConquistas.Rows[i].Cells["NOME"].Value = MySQL.ConquerInfo(ConquerID[i], 4);
                        dataConquistas.Rows[i].Cells["ADICIONADAEM"].Value = DateTime.Parse(MySQL.UserConquerInfo(ConquerID[i], 1), new CultureInfo("pt-BR", true));
                    }
                }
                #endregion

                #region CARREGA OS DADOS DO USUÁRIO

                // Exibe animação de FadeIn
                ProgramData.FadeIn(this, 65);

                // Trazer para frente
                this.BringToFront();

                // Carrega o tema selecionado pelo usuário
                LoadTheme();

                // Aplica a tradução (se houver)
                ApplyTranslation();

                // Verifica se é HUBR Plus
                MySQL.VerifyPlus();

                // Verifica se os dias restantes são maior que 0
                imgHUBRPlus.Visible = MySQL.RemainingDays == 0 ? false : true;

                // Adiciona a imagem do usuário
                pbUserImage.Load(ProgramData.ImagemURL);

                // Seta o nome do usuário
                lbDetails.Text = ProgramData.Username.ToUpper();
                #endregion

                #region OPÇÕES DE DESENVOLVEDOR
                // Primeiro, verifica se o usuário é desenvolvedor
                MySQL.GetInformation(4);

                if (MySQL.GetData == "1")
                {
                    // Verifica se o usuário tem uma chave de desenvolvedor válida
                    if (MySQL.VerifyDevKey(3) != "NOK")
                    {
                        // Habilita e altera o botão de adicionar novas conquistas para os jogos
                        // de acordo com o idioma
                        btnInsertAchievements.Enabled = true;
                        if (Properties.Settings.Default["lang"].ToString() == "en")
                        {
                            btnInsertAchievements.Text = "INSERT ACHIEVEMENTS\n OF YOUR GAME";
                            btnYourDeveloperPanel.Text = "YOUR DEVELOPER\nPANEL";

                        }

                        // Muda o botão de painel de desenvolvedor de posição
                        btnYourDeveloperPanel.Location = new Point(815, 619);

                        // Desabilita o botão de requisitar chave de acesso
                        btnRequestDeveloperKey.Dispose();
                    }
                    else
                    {
                        // Altera o texto do botão de requisitar chave de desenvolvedor de acordo
                        // com o idioma
                        if (Properties.Settings.Default["lang"].ToString() == "en")
                            btnRequestDeveloperKey.Text = "REQUEST\nYOUR DEVELOPER KEY";

                        // Altera o posicionamento do botão de requisição de chaves
                        btnRequestDeveloperKey.Location = new Point(1044, 618);

                        // Destrói o botão de inserir conquistas 
                        // e do painel de desenvolvedor
                        btnInsertAchievements.Dispose();
                        btnYourDeveloperPanel.Dispose();
                    }
                }
                else
                {
                    // O usuário não é developer/publisher
                    // então, destruir os botões de significado
                    btnInsertAchievements.Dispose();
                    btnRequestDeveloperKey.Dispose();
                    btnYourDeveloperPanel.Dispose();

                }

                #endregion
            }
            catch
            {

            }
        }


        /// <summary>
        /// Carrega o tema aplicado em CONFIGURAÇÕES
        /// </summary>
        void LoadTheme()
        {
            try
            {
                // Inicializa o leitor de INI
                var parser = new FileIniDataParser();

                // Carrega o tema atual
                string Theme = Properties.Settings.Default["Theme"].ToString();

                // Lê o arquivo de configurações do tema
                IniData data = parser.ReadFile(Application.StartupPath + @"\Temas\\" + Theme + "\\Config.IW@THEMES");

                // Lê os dados do INI
                lbDetails.ForeColor = Color.FromName(data["Textos"]["lbDetails"]);
                btnLibrary.ForeColor = Color.FromName(data["Textos"]["btnLibrary"]);
                btnStore.ForeColor = Color.FromName(data["Textos"]["btnStore"]);
                btnYourActivity.ForeColor = Color.FromName(data["Textos"]["btnYourActivity"]);

                // Verifica se há background
                if (File.Exists(Application.StartupPath + "\\Temas\\" + Theme.ToUpper() + "\\Fundo.png"))
                {
                    // Se houver dados de background, aplicar também
                    Image bg = new Bitmap(Application.StartupPath + "\\Temas\\" + Theme.ToUpper() + "\\Fundo.png"); // Seta a imagem de fundo
                    this.BackgroundImage = bg;
                }
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO AO CARREGAR TEMA APLICADO A UGNITE.");
                else
                    ProgramData.MensagemErro("ERROR WHILE APPLYING THEME TO UGNITE.");

                Application.Exit();
            }
        }

        /// <summary>
        ///  Abre a biblioteca de jogos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLibrary_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new HUBR_GameLibraryNew();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        /// <summary>
        /// Vê as atividades do jogador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYourActivity_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new HUBR_YourActivity();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        /// <summary>
        /// Volta para a página da loja
        /// </summary>
        private void btnStore_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new HUBR_SHOP();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }
        /// <summary>
        /// Minimiza a HUBR
        /// </summary>
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Fecha o cliente HUBR por completo
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {

            Application.ExitThread();
        }

        private void btnPlusPlans_Click(object sender, EventArgs e)
        {
            PlusPlans pp = new PlusPlans();
            pp.ShowDialog();
        }

        private void btnActivateGame_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            ActivateKey keyEnable = new ActivateKey();
            keyEnable.Show();

        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            Options opt = new Options();
            opt.Show();

            // Atualiza a imagem do usuário
            pbUserImage.Load(ProgramData.ImagemURL);

        }

        /// <summary>
        /// Envia uma requisição de chave de desenvolvedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestDeveloperKey_Click(object sender, EventArgs e)
        {
            btnRequestDeveloperKey.Dispose();
            MySQL.RequestDevKey();
        }

        /// <summary>
        /// Exibe o painel de desenvolvedores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYourDeveloperPanel_Click(object sender, EventArgs e)
        {
            Exibidores.PainelDesenvolvedor PD = new Exibidores.PainelDesenvolvedor();
            PD.ShowDialog();

        }

        /// <summary>
        /// Exibe o painel de inserção de conquistas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInsertAchievements_Click(object sender, EventArgs e)
        {
            Exibidores.InserirConquistas ic = new Exibidores.InserirConquistas();
            ic.ShowDialog();
        }
    }
}
