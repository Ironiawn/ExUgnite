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

namespace UGNITE.Janelas.Parceiros
{
    public partial class UGNITE_InsoniaGames : Form
    {
        public UGNITE_InsoniaGames()
        {
            InitializeComponent();
        }

        private void UGNITE_InsoniaGames_Load(object sender, EventArgs e)
        {
            // Exibe animação de FadeIn
            ProgramData.FadeIn(this, 65);

            // Trazer para frente
            this.BringToFront();

            // Verifica se é HUBR Plus
            MySQL.VerifyPlus();

            // Verifica se os dias restantes são maior que 0
            imgHUBRPlus.Visible = MySQL.RemainingDays == 0 ? false : true;


            // Seta a informação de detalhes na aba de cima
            lbDetails.Text = ProgramData.Username.ToUpper();

            // Adiciona a imagem do usuário
            pbUserImage.Load(ProgramData.ImagemURL);

            // Carrega o tema
            LoadTheme();
        }


        /// <summary>
        /// Carrega o tema aplicado em CONFIGURAÇÕES
        /// </summary>
        void LoadTheme()
        {
            // Inicializa o leitor de INI
            var parser = new IniParser.FileIniDataParser();

            // Carrega o tema atual
            string Theme = Properties.Settings.Default["Theme"].ToString();

            // Lê o arquivo de configurações do tema
            IniParser.Model.IniData data = parser.ReadFile(Application.StartupPath + @"\Temas\\" + Theme + "\\Config.IW@THEMES");

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

        private void btnYourActivity_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new HUBR_YourActivity();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void btnLibrary_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new HUBR_GameLibraryNew();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new HUBR_SHOP();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

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
    }
}
