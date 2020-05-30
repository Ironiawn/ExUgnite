using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UGNITE.Janelas.Exibidores
{
    public partial class InserirConquistas : Form
    {
        public InserirConquistas()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Aplica tradução ao aplicativo
        /// </summary>
        void ApplyTranslation()
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                lbAchievementsPanel.Text = Program.GetStringRM("en", "lbAchievementsPanel");
                lbInsercoes.Text = Program.GetStringRM("en", "lbInsercoes");
                lbSelectGameTarget.Text = Program.GetStringRM("en", "lbSelectGameTarget");
                lbAchievementName.Text = Program.GetStringRM("en", "lbAchievementName");
                lbGeneratedID.Text = Program.GetStringRM("en", "lbGeneratedID");
                lbAchievementDescription.Text = Program.GetStringRM("en", "lbAchievementDescription");
                lbAchievementIMG.Text = Program.GetStringRM("en", "lbAchievementIMG");
                btnInsertAchievement.Text = Program.GetStringRM("en", "btnInsertAchievement");
                tbAlert.Text = "WARNING!\nACHIEVEMENT UPDATES NEEDS TO BE DONE VIA E-MAIL!\nCONTACT US AT shop@ironiawn.com.br";
                lbGameAchievements.Text = Program.GetStringRM("en", "lbGameAchievements");
                lbDeveloperKey.Text = "DEVELOPER KEY : " + MySQL.VerifyDevKey(0);

                dataConquistas.Columns[0].HeaderCell.Value = "IMAGE";
                dataConquistas.Columns[1].HeaderCell.Value = "CONQUER NAME";
                dataConquistas.Columns[2].HeaderCell.Value = "CONQUER DESC";
                dataConquistas.Columns[3].HeaderCell.Value = "GAME";
                dataConquistas.Columns[4].HeaderCell.Value = "CONQUER ID";

                // Verifica se o usuário tem uma chave aprovada
                if (MySQL.VerifyDevKey(2) != "1")
                {
                    ProgramData.MensagemErro("DEVELOPER KEY WASN'T APPROVED YET.\nWAIT FOR IT AND THIS FUNCTION WILL BE AVAILABLE!");
                    this.Close();
                }
            }
            else
            {
                lbDeveloperKey.Text = "CHAVE DE DESENVOLVEDOR : " + MySQL.VerifyDevKey(0);

                // Verifica se o usuário tem uma chave aprovada
                if (MySQL.VerifyDevKey(2) != "1")
                {
                    ProgramData.MensagemErro("CHAVE DE DESENVOLVEDOR AINDA NÃO APROVADA.\nPARA UTILIZAR ESTA FUNÇÃO É NECESSÁRIO UMA CHAVE APROVADA!");
                    this.Close();
                }

            }
        }

        private void InserirConquistas_Load(object sender, EventArgs e)
        {

            // Aplica a tradução (se houver)
            ApplyTranslation();

            // Adiciona os jogos do desenvolvedor na lista
            foreach (string s in MySQL.RequestDevGamesName)
                comboGameTarget.Items.Add(s);

            // Preenche a lista
            UpdateList();
            
        }

        /// <summary>
        /// Atualiza a lista com as informações sobre conquistas
        /// </summary>
        void UpdateList()
        {
            #region ADQUIRE INFORMAÇÕES
            List<string> Imagem = MySQL.RequestDevGamesConquers(MySQL.VerifyDevKey(0), 3);
            List<string> Nome = MySQL.RequestDevGamesConquers(MySQL.VerifyDevKey(0), 1);
            List<string> Desc = MySQL.RequestDevGamesConquers(MySQL.VerifyDevKey(0), 2);
            List<string> Jogo = MySQL.RequestDevGamesConquers(MySQL.VerifyDevKey(0), 5);
            List<string> ID = MySQL.RequestDevGamesConquers(MySQL.VerifyDevKey(0), 4);
            #endregion
            // Limpa lista
            dataConquistas.Rows.Clear();

            // Preenche a lista
            for (int i = 0; i< Imagem.Count; i++)
            {
                    dataConquistas.Rows.Add();

                dataConquistas.Rows[i].Cells["IMAGEM"].Value = ProgramData.GetImagemURL(Imagem[i]);
                dataConquistas.Rows[i].Cells["NOME"].Value = Nome[i];
                dataConquistas.Rows[i].Cells["DESCRICAO"].Value = Desc[i];
                dataConquistas.Rows[i].Cells["JOGO"].Value = Jogo[i];
                dataConquistas.Rows[i].Cells["ID"].Value = ID[i];

            }

        }
        /// <summary>
        /// Fecha a janela
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnInsertAchievement_Click(object sender, EventArgs e)
        {
            // Verifica se todos os campos foram preenchidos corretamente
            if(comboGameTarget.SelectedItem != null)
            {
                if (tbAchievementDescription.Text.Length > 5 &&
                    tbAchievementName.Text.Length > 5 &&
                    tbAchievementURL.Text.Length > 5)
                {
                    // Auto-generate a random conquer id
                    string ConquerID = "UG_" + MySQL.RandomString(2) + "#" + MySQL.RandomString(3);

                    // Adquire informações das conquistas
                    List<string> IDs = MySQL.RequestDevGamesConquers(MySQL.VerifyDevKey(0), 4);
                    List<string> Nomes = MySQL.RequestDevGamesConquers(MySQL.VerifyDevKey(0), 1);
                    List<string> Desc = MySQL.RequestDevGamesConquers(MySQL.VerifyDevKey(0), 3);

                    // Compara com todas
                    for (int i = 0; i < IDs.Count; i++)
                    {
                        if(IDs[i].ToUpper() != ConquerID.ToUpper() && tbAchievementName.Text.ToUpper() != Nomes[i].ToUpper() && tbAchievementDescription.Text.ToUpper() != Desc[i].ToUpper())
                        {
                            // Insert the conquer at the database
                            MySQL.InsertConquer(tbAchievementURL.Text, tbAchievementName.Text, tbAchievementDescription.Text, comboGameTarget.Text, ConquerID.ToUpper(), MySQL.VerifyDevKey(0));

                            // Exibe a ID autogerada da conquista
                            tbGeneratedID.Text = ConquerID.ToUpper();

                            // Atualiza a lista
                            UpdateList();
                        }
                        else
                        {
                            if (Properties.Settings.Default["lang"].ToString() == "en")
                                ProgramData.MensagemErro("ERROR WHILE INSERTING THE ACHIEVEMENT\nDUPLICATE ID, NAME OR DESCRIPTION");
                            else
                                ProgramData.MensagemErro("ERRO AO INSERIR A CONQUISTA\nID, NOME OU DESCRIÇÃO DUPLICADO");
                        }
                    }
                }
                else
                {
                    if (Properties.Settings.Default["lang"].ToString() == "en")
                        ProgramData.MensagemErro("VERIFY IF ALL INFORMATION WAS INSERTED");
                    else
                        ProgramData.MensagemErro("VERIFIQUE SE TODAS AS INFORMAÇÕES FORAM INSERIDAS");
                }
            }
            else
            {
                if (Properties.Settings.Default["lang"].ToString() == "en")
                    ProgramData.MensagemErro("PLEASE, SELECT A GAME");
                else
                    ProgramData.MensagemErro("POR FAVOR, SELECIONE UM JOGO");
            }
        }
    }
}
