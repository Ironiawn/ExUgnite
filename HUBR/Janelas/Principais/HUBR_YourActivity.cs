using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UGNITE
{
    public partial class HUBR_YourActivity : Form
    {
        public HUBR_YourActivity()
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
                lbLastActivities.Text = Program.GetStringRM("en", "lbLastActivities");
                btnAchievements.Location = new Point(420, 4);
                btnAchievements.Text = Program.GetStringRM("en", "btnAchievements");

                dataTable.Columns[1].HeaderCell.Value = "TYPE";
                dataTable.Columns[2].HeaderCell.Value = "NAME";
                dataTable.Columns[3].HeaderCell.Value = "DETAILS";
                dataTable.Columns[4].HeaderCell.Value = "PERIOD/TIME";
            }
        }

        private void HUBR_YourActivity_Load(object sender, EventArgs e)
        {
            try
            {
                // Exibe animação de FadeIn
                ProgramData.FadeIn(this, 65);

                // Trazer para frente
                this.BringToFront();

                // Carrega o tema selecionado pelo usuário
                LoadTheme();

                // Verifica se é HUBR Plus
                MySQL.VerifyPlus();

                // Verifica se os dias restantes são maior que 0
                imgHUBRPlus.Visible = MySQL.RemainingDays == 0 ? false : true;

                // Adiciona a imagem do usuário
                pbUserImage.Load(ProgramData.ImagemURL);

                // Seta o nome do usuário
                lbDetails.Text = ProgramData.Username.ToUpper();

                
                // Adquire a atividade do usuário
                MySQL.GetYourActivity(ProgramData.Username);

                // Seta o mural
                for (int i = 0; i < MySQL.ActivitiesGet.Count; i++)
                {
                    // Separa as atividades por ;
                    // Sendo O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    string[] CurrentActivityGet = System.Text.RegularExpressions.Regex.Split(MySQL.ActivitiesGet[i], ";");

                    // Adiciona uma nova linha
                    dataTable.Rows.Add();

                    // Preenche as linhas
                    dataTable.Rows[i].Cells["STATUS"].Value = Image.FromFile($"TEMAS\\{CurrentActivityGet[0]}.png"); // STATUS. 0 = OK | 1 = ERRO
                    dataTable.Rows[i].Cells["TIPO"].Value = CurrentActivityGet[1]; // TIPO DE ATIVIDADE FEITA
                    dataTable.Rows[i].Cells["NOME"].Value = CurrentActivityGet[2]; // NOME DA ATIVIDADE FEITA
                    dataTable.Rows[i].Cells["DETALHES"].Value = CurrentActivityGet[3]; // DETALHES DA ATIVIDADE FEITA
                    dataTable.Rows[i].Cells["DATA"].Value = DateTime.Parse(CurrentActivityGet[4], new System.Globalization.CultureInfo("pt-BR", true)).ToShortDateString(); // DATA DA ATIVIDADE FEITA
                }
                


            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO DE CONEXÃO!\nTALVEZ A UGNITE ESTEJA EM MANUTENÇÃO OU ACONTECEU ALGO QUE OS DEUSES DA INTERNET NÃO PODEM EXPLICAR!\nERRO: YACT_LOAD\nA UGNITE SERÁ ENCERRADA.\n{ex}");
                else
                    ProgramData.MensagemErro($"CONNECTION ERROR!\nMAYBE THE UGNITE IS AT MAINTENANCE OR SOMETHING THAT GODS OF INTERNET CAN'T EXPLAIN!\nERROR: YACT_LOAD\nUGNITE MUST RECOVER ITSELF AND WILL BE CLOSED, SORRY.\n{ex}");

                Application.Exit();
            }

            // Aplica a tradução (se houver)
            ApplyTranslation();

            // Seta o valor disponível da carteira do usuário
            lbWalletValue.Text = MySQL.FormataValor(MySQL.GetWalletValue);
        }

        private void btnLibrary_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new HUBR_GameLibraryNew();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {

            // Cria uma instância da janela de opções e exibe
            Options opt = new Options();
            opt.ShowDialog();
            // Atualiza a imagem do usuário
            pbUserImage.Load(ProgramData.ImagemURL);
        }

        private void btnActivateGame_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            ActivateKey keyEnable = new ActivateKey();
            keyEnable.Show();
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
                lbLastActivities.ForeColor = Color.FromName(data["Textos"]["lbLastActivities"]);
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

        private void btnPlusPlans_Click(object sender, EventArgs e)
        {
            PlusPlans pp = new PlusPlans();
            pp.ShowDialog();

        }

        private void btnAchievements_Click(object sender, EventArgs e)
        {

            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new Janelas.Principais.UGNITE_Conquistas();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }
    }
}
