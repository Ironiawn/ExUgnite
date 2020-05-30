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
    public partial class PainelDesenvolvedor : Form
    {
        public PainelDesenvolvedor()
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
                dataGames.Columns[1].HeaderCell.Value = "APP NAME";
                dataGames.Columns[2].HeaderCell.Value = "APP CODE";
                dataGames.Columns[3].HeaderCell.Value = "PUBLICATION DATE";
                dataGames.Columns[4].HeaderCell.Value = "PRICE (MERCADO PAGO/PAYPAL)";
                dataGames.Columns[5].HeaderCell.Value = "PRICE (UPAY) | INTERNATIONAL";
                dataGames.Columns[6].HeaderCell.Value = "PRICE (UPAY) | BRAZIL";
                dataGames.Columns[7].HeaderCell.Value = "APP SIZE";
                dataGames.Columns["GOM"].HeaderCell.Value = "G.O.M";
                dataGames.Columns["PLUS"].HeaderCell.Value = "UGNITE+";
                dataGames.Columns["ALLOWDOWNLOADS"].HeaderCell.Value = "ALLOW DOWNLOADS";
                dataGames.Columns["GAMERATING"].HeaderCell.Value = "AVG. RATING";
                dataGames.Columns["EDITBTN"].HeaderCell.Value = "APP INFO";

                lbDeveloperPanel.Text = Program.GetStringRM("en", "lbDeveloperPanel");
                lbStatusOperacoes.Text= Program.GetStringRM("en", "lbStatusOperacoes");
                lbWaitingApproval.Text = "WAITING UGNITE'S APPROVAL";
            }
        }

        private void PainelDesenvolvedor_Load(object sender, EventArgs e)
        {
            try
            {
                // Aplica tradução (se houver)
                ApplyTranslation();

                // Atualiza informações da table
                AtualizaTable();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Atualiza as informações da tabela
        /// </summary>
        void AtualizaTable()
        {
            // Seta a label de loading como visivel
            lbLoadingInfo.Visible = true;
            dataGames.Visible = false;

            // Seta a chave de desenvolvedor
            if (Properties.Settings.Default["lang"].ToString() != "en")
                lbDeveloperKey.Text = "CHAVE DE DESENVOLVEDOR : " + MySQL.VerifyDevKey(0);
            else
            {
                lbDeveloperKey.Text = "DEVELOPER KEY : " + MySQL.VerifyDevKey(0);
                lbLoadingInfo.Text = "LOADING...";
            }

            // Adquire informações sobre a aprovação do desenvolvedor
            if (MySQL.VerifyDevKey(2) == "1")
            {
                // Clear na table
                dataGames.Rows.Clear();
                cbListItems.Items.Clear();

                // Tira o texto de aguardando aprovação
                lbWaitingApproval.Dispose();

                // Verifica se o desenvolvedor é aprovado em operar informações financeiras
                bool Finance = MySQL.VerifyDevKey(3) == "1" ? true : false;

                // Verifica o idioma do usuário e cria itens de acordo
                if (Properties.Settings.Default["lang"].ToString() != "en")
                {
                    cbListItems.Items.Clear();

                    // Cria lista de itens
                    List<string> Itens = new List<string>() { "IDIOMA DO JOGADOR", "SALVAR/CARREGAR VIA UDB", "ID E NICKNAME DO JOGADOR" };

                    // Se o usuário tem permissão para financeiro, adicionar
                    if (Finance)
                        Itens.Add("FINANCEIRO");

                    // Adiciona a lista aos cbList
                    for (int i = 0; i < Itens.Count; i++)
                    {
                        cbListItems.Items.Add(Itens[i]);
                        cbListItems.SetItemChecked(i, true);
                    }
                }
                else
                {
                    cbListItems.Items.Clear();

                    // Cria lista de itens
                    List<string> Itens = new List<string>() { "PLAYER LANGUAGE", "SAVE/LOAD VIA UDB", "PLAYER NICKNAME/ID" };

                    // Se o usuário tem permissão para financeiro, adicionar
                    if (Finance)
                        Itens.Add("FINANCIAL ACTIONS");

                    // Adiciona a lista aos cbList
                    for (int i = 0; i < Itens.Count; i++)
                    {
                        cbListItems.Items.Add(Itens[i]);
                        cbListItems.SetItemChecked(i, true);
                    }

                }


                    // Verifica se o desenvolvedor tem jogos publicados na plataforma
                    if (MySQL.RequestDevGamesCode.Count > 0)
                {
                    // Adiciona informações na planilha
                    for (int i = 0; i < MySQL.RequestDevGamesCode.Count; i++)
                    {
                        // Adiciona uma linha
                        dataGames.Rows.Add();

                        // Preenche as informações das colunas da linha
                        dataGames.Rows[i].Cells["STATUS"].Value = Image.FromFile($"TEMAS\\{MySQL.RequestGameInfo(8, MySQL.RequestDevGamesCode[i])}.png");
                        dataGames.Rows[i].Cells["GAMENAME"].Value = MySQL.RequestGameInfo(1, MySQL.RequestDevGamesCode[i]);
                        dataGames.Rows[i].Cells["GAMENUM"].Value = MySQL.RequestGameInfo(2, MySQL.RequestDevGamesCode[i]);
                        dataGames.Rows[i].Cells["DATEPUBLISH"].Value = MySQL.RequestGameInfo(9, MySQL.RequestDevGamesCode[i]);
                        dataGames.Rows[i].Cells["PRICEMP"].Value = MySQL.RequestGameInfo(4, MySQL.RequestDevGamesCode[i]);
                        dataGames.Rows[i].Cells["PRICEINT"].Value = MySQL.RequestGameInfo(13, MySQL.RequestDevGamesCode[i]);
                        dataGames.Rows[i].Cells["PRICEUPAY"].Value = MySQL.RequestGameInfo(5, MySQL.RequestDevGamesCode[i]);
                        dataGames.Rows[i].Cells["GAMESIZE"].Value = MySQL.RequestGameInfo(10, MySQL.RequestDevGamesCode[i]);
                        dataGames.Rows[i].Cells["GOM"].Value = MySQL.RequestGameInfo(7, MySQL.RequestDevGamesCode[i]) == "1" ? true : false;
                        dataGames.Rows[i].Cells["DOWNLOADS"].Value = MySQL.RequestDownloadInfo(MySQL.RequestGameInfo(1, MySQL.RequestDevGamesCode[i]));
                        dataGames.Rows[i].Cells["ALLOWDOWNLOADS"].Value = MySQL.RequestGameInfo(12, MySQL.RequestDevGamesCode[i]) == "1" ? true : false;
                        dataGames.Rows[i].Cells["USEAPI"].Value = MySQL.RequestGameInfo(15, MySQL.RequestDevGamesCode[i]) == "1" ? true : false;

                        // Adquire a nota do jogo
                        int GameRating = MySQL.GetGameRating(MySQL.RequestDevGamesCode[i]);

                        // Verifica se a nota do jogo é boa para mudar a cor da célula
                        if(GameRating <= 4)
                        {
                            dataGames.Rows[i].Cells["GAMERATING"].Value = GameRating;
                            dataGames.Rows[i].Cells["GAMERATING"].Style.BackColor = Color.Red;
                        }else
                        if(GameRating >= 5 && GameRating < 7)
                        {
                            dataGames.Rows[i].Cells["GAMERATING"].Value = GameRating;
                            dataGames.Rows[i].Cells["GAMERATING"].Style.BackColor = Color.GreenYellow;
                        }
                        else
                        if (GameRating == 7 || GameRating == 8)
                        {
                            dataGames.Rows[i].Cells["GAMERATING"].Value = GameRating;
                            dataGames.Rows[i].Cells["GAMERATING"].Style.BackColor = Color.DarkSalmon;
                        }
                        else
                        if (GameRating == 9)
                        {
                            dataGames.Rows[i].Cells["GAMERATING"].Value = GameRating;
                            dataGames.Rows[i].Cells["GAMERATING"].Style.BackColor = Color.DarkTurquoise;
                        }
                        else
                        if (GameRating == 10)
                        {
                            dataGames.Rows[i].Cells["GAMERATING"].Value = GameRating;
                            dataGames.Rows[i].Cells["GAMERATING"].Style.BackColor = Color.Green;
                        }



                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            dataGames.Rows[i].Cells["EDITBTN"].Value = "SALVAR";
                        else
                            dataGames.Rows[i].Cells["EDITBTN"].Value = "UPDATE";

                        dataGames.Rows[i].Cells["PLUS"].Value = MySQL.RequestGameInfo(6, MySQL.RequestDevGamesCode[i]) == "1" ? true : false;
                    }
                }

                // Desativa itens desnecessários
                lbLoadingInfo.Visible = false;
                dataGames.Visible = true;
            }
            else
            {
                // Tira os itens desnecessários da página
                PainelAprovacoes.Dispose();
                dataGames.Dispose();
                lbLoadingInfo.Dispose();
                cbListItems.Dispose();
            }

        }

        private void dataGames_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica qual local da tabela mandou o comando
            var senderGrid = (DataGridView)sender;

            // Verifica se é um botão
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Adquire a coluna que o usuário selecionou
                int i = e.RowIndex;

                string PLUS = (bool)dataGames.Rows[i].Cells["PLUS"].Value == true ? "1" : "0"; // Verifica se está disponível na PLUS
                string DOWNLOADABLE = (bool)dataGames.Rows[i].Cells["ALLOWDOWNLOADS"].Value == true ? "1" : "0"; // Verifica se está disponível para download
                string GAMEPRICE_INT = (string)dataGames.Rows[i].Cells["PRICEINT"].Value; // O preço do jogo para valores internacionais
                string GAMEPRICE_UPAY = (string)dataGames.Rows[i].Cells["PRICEUPAY"].Value; // O preço do jogo para valores brasileiros
                string USEAPI = (bool)dataGames.Rows[i].Cells["USEAPI"].Value == true? "1" : "0"; // Se o jogo utiliza a uAPI


                // Atualiza as informações do jogo na base
                MySQL.UpdateGameInfo(dataGames.Rows[i].Cells["GAMENUM"].Value.ToString(), 0, PLUS);
                MySQL.UpdateGameInfo(dataGames.Rows[i].Cells["GAMENUM"].Value.ToString(), 1, DOWNLOADABLE);
                MySQL.UpdateGameInfo(dataGames.Rows[i].Cells["GAMENUM"].Value.ToString(), 2, GAMEPRICE_INT);
                MySQL.UpdateGameInfo(dataGames.Rows[i].Cells["GAMENUM"].Value.ToString(), 3, GAMEPRICE_UPAY);
                MySQL.UpdateGameInfo(dataGames.Rows[i].Cells["GAMENUM"].Value.ToString(), 4, USEAPI);

                // Atualiza valores da table
                AtualizaTable();
            }
        }

        /// <summary>
        /// Fecha a janela
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGames_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGames.CurrentCell.ColumnIndex == 4 || dataGames.CurrentCell.ColumnIndex == 5 || dataGames.CurrentCell.ColumnIndex == 6) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Globalization.NumberFormatInfo nfi = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat;
            char decSeperator;

            decSeperator = nfi.CurrencyDecimalSeparator[0];

            if (!char.IsControl(e.KeyChar) && !(char.IsDigit(e.KeyChar)
| e.KeyChar == decSeperator))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == decSeperator
                && (sender as TextBox).Text.IndexOf(decSeperator) > -1)
            {
                e.Handled = true;
            }
        }
    }
}
