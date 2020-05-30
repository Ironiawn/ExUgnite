using System;
using CefSharp;
using CefSharp.WinForms;
using System.Windows.Forms;
using CefSharp.WinForms.Internals;
using System.Drawing;
using System.IO;
using System.Net;
using UGNITE.Rede;

namespace UGNITE
{

    public partial class HUBR_SHOP : Form
    {
        /// <summary>
        /// A instância do navegador
        /// </summary>
        ChromiumWebBrowser m_chromeBrowser = null;

        Button GameButton;
        string currentAddress;

        #region INFORMAÇÃO DE JOGOS
        string gameCode;
        string GameBuyURL;
        string GameRunCode;
        string GameValue;
        string GameOfMonth;
        #endregion

        public HUBR_SHOP()
        {
            InitializeComponent();

            // Deleta o arquivo de update, se existir
            if (File.Exists(Directory.GetCurrentDirectory() + "\\u.sys\\update.rar"))
                File.Delete(Directory.GetCurrentDirectory() + "\\u.sys\\update.rar");

            this.Opacity = 0.0; // Muda a opacidade da janela para dar FadeIn depois ;)

            // Botão para jogar 
            GameButton = btnPlayGame;
            GameButton.Visible = false;
        }


        /// <summary>
        /// Atualiza a URL do browser
        /// </summary>
        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => currentAddress = args.Address);
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            // Primeiro, atualiza o exibidor de titulo
            this.InvokeOnUiThreadIfRequired(() => lbURL.Text = args.Title);

            // Atualiza os nomes após o título ser alterado
            this.InvokeOnUiThreadIfRequired(() => VerifyHUBR(args, lbURL));
        }

        void VerifyHUBR(TitleChangedEventArgs args, Label URLText)
        {
            try
            {
                // IMPEDIR que o cliente veja a página de download da HUBR
                if (currentAddress.ToLower().Contains("download") || currentAddress.ToLower() == "https://ugnite.ironiawn.com.br/")
                {
                    // Verifica se o jogador é brasileiro
                    if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                        m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/games");
                    else
                        m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/en/games");
                }

                // Se as páginas não contém SUA PLATAFORMA ou MERCADOPAGO ou PAYPAL(TESTE), voltar a página principal do HUBR
                if (!URLText.Text.Contains("UGNITE") && !currentAddress.ToUpper().Contains("MERCADOPAGO") && !URLText.Text.ToUpper().Contains("PAYMENT OK") && !URLText.Text.ToUpper().Contains("OBRIGADO")
                     && !currentAddress.ToUpper().Contains("PAYPAL"))
                {
                    // Verifica se o jogador é brasileiro
                    if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                        m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/games");
                    else
                        m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/en/games");
                }

                // Verifica se o cliente não está na página principal da HUBR
                if (URLText.Text.Contains("LISTA DE JOGOS") || URLText.Text.Contains("LIST OF GAMES")
                     || URLText.Text.Contains("UPAY") || URLText.Text.Contains("PLUS") || URLText.Text.ToLower().Contains("pesquisa") || URLText.Text.ToLower().Contains("search"))
                {
                    OpenGames.LastSeenGame = null;
                    MySQL.ActivatedByUser = false;

                    // Desativa o botão de compra via MP
                    GameButton.Enabled = false;
                    GameButton.Visible = false;

                    // Desativa o botão HUBR Wallet
                    btnBuyWithWallet.Enabled = false;
                    btnBuyWithWallet.Visible = false;

                    // Reseta valores
                    gameCode = null;
                    GameRunCode = null;
                    GameBuyURL = null;

                    // Ativa o botão de pesquisa
                    btnSearchGame.Enabled = true;
                    btnSearchGame.Visible = true;
                    tbSearchGame.Visible = true;
                    tbSearchGame.Enabled = true;

                    GameButton.Text = "";
                    lbURL.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    // Verifica se está no MercadoPago
                    if (currentAddress.ToUpper().Contains("MERCADOPAGO") || lbURL.Text.ToUpper().Contains("PAYMENT") || lbURL.Text.ToUpper().Contains("PAYPAL"))
                    {
                        lbURL.ForeColor = System.Drawing.Color.Green;

                        // Desativa o botão de compra via MP
                        GameButton.Enabled = false;
                        GameButton.Visible = false;

                        // Desativa o botão de pesquisa
                        btnSearchGame.Enabled = false;
                        btnSearchGame.Visible = false;
                        tbSearchGame.Visible = false;
                        tbSearchGame.Enabled = false;

                        // Reseta valores
                        gameCode = null;
                        GameRunCode = null;
                        GameBuyURL = null;


                        // Desativa o botão HUBR Wallet
                        btnBuyWithWallet.Enabled = false;
                        btnBuyWithWallet.Visible = false;
                    }
                    else
                        lbURL.ForeColor = System.Drawing.Color.White;


                    try
                    {
                        #region VERIFICAÇÃO DE JOGOS EM TEMPO REAL
                        // Verifica se o usuário está na página de algum jogo disponível
                        for (int i = 0; i < OpenGames.AvailableGames.Count; i++)
                        {
                            if (!lbURL.Text.ToLower().Contains("search") || !lbURL.Text.ToLower().Contains("pesquisa"))
                            {
                                if (lbURL.Text.ToUpper().Contains(OpenGames.AvailableGames[i].ToUpper()) || currentAddress.ToUpper().Contains("MERCADO") && lbURL.Text.ToUpper() == OpenGames.AvailableGames[i].ToUpper() ||
                                    (lbURL.Text.ToUpper().Contains("PAYMENT") || lbURL.Text.ToUpper().Contains("OBRIGADO")) && OpenGames.LastSeenGame.ToUpper() == OpenGames.AvailableGames[i].ToUpper())
                                {
                                    // Verifica se está na página do jogo
                                    if (lbURL.Text.ToUpper().Contains(OpenGames.AvailableGames[i].ToUpper()))
                                    {

                                        // Adquire os campos, se não forem vazios
                                        if (string.IsNullOrEmpty(gameCode))
                                        {
                                            gameCode = MySQL.RequestGameInfoByName(2, OpenGames.AvailableGames[i].ToUpper());//new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.AvailableGames[i].ToUpper() + "/DataCode.HUBR@LOOPS");
                                            GameBuyURL = MySQL.RequestGameInfoByName(14, OpenGames.AvailableGames[i].ToUpper());//new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.AvailableGames[i].ToUpper() + "/Sec.HUBR@LOOPS");
                                            GameRunCode = i.ToString();//new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.AvailableGames[i].ToUpper() + "/Code.HUBR@LOOPS");
                                            GameValue = MySQL.RequestGameInfoByName(4, OpenGames.AvailableGames[i].ToUpper());//new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.AvailableGames[i].ToUpper() + "/Val.HUBR@LOOPS");
                                            GameOfMonth = MySQL.RequestGameInfoByName(7, OpenGames.AvailableGames[i].ToUpper());//new WebClient().DownloadString("http://ironiawn.com.br/HUBRX/GameData/" + OpenGames.AvailableGames[i].ToUpper() + "/GOM.txt");
                                        }

                                        // Verifica se o usuário tem o jogo na conta
                                        MySQL.GetActivatedGame(gameCode);


                                        // Altera as propriedades de compra do jogo
                                        OpenGames.GameBuyURL = GameBuyURL;
                                        OpenGames.GameRunCode = int.Parse(GameRunCode);
                                        OpenGames.LastSeenGame = OpenGames.AvailableGames[i].ToUpper();

                                        // Se sim, exibir o botão para jogar
                                        if (MySQL.ActivatedByUser)
                                        {
                                            // Alterar botão
                                            GameButton.Visible = true;
                                            GameButton.Enabled = true;

                                            OpenGames.HasGame = true;
                                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                                GameButton.Text = $"JOGAR {OpenGames.AvailableGames[i].ToUpper()}";
                                            else
                                                GameButton.Text = $"PLAY {OpenGames.AvailableGames[i].ToUpper()}";

                                            // Desativa o botão HUBR Wallet
                                            btnBuyWithWallet.Enabled = false;
                                            btnBuyWithWallet.Visible = false;
                                        }
                                        else
                                        {
                                            // Adquire os valores do jogo
                                            string GamePrice = MySQL.RequestGameInfoByName(4, OpenGames.LastSeenGame.ToUpper()); /*new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.AvailableGames[i].ToUpper() + "/Pri.HUBR@LOOPS")*/
                                            string GamePriceUpay = "";

                                            // Se o jogo for de 0 preço, desabilitar botão de compra via PP ou MP
                                            if (GamePrice == "0")
                                            {
                                                GameButton.Visible = false;
                                                GameButton.Enabled = false;
                                            }
                                            else
                                            {
                                                GameButton.Visible = true;
                                                GameButton.Enabled = true;
                                            }

                                            // Verifica se o jogador é brasileiro
                                            if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                                                GamePriceUpay = MySQL.RequestGameInfoByName(5, OpenGames.LastSeenGame.ToUpper());//float.Parse(new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.LastSeenGame + "/Val.HUBR@LOOPS")); // Preço Upay Brasil 
                                            else
                                                GamePriceUpay = MySQL.RequestGameInfoByName(13, OpenGames.LastSeenGame.ToUpper());

                                            OpenGames.HasGame = false;

                                            // Verifica se a URL de compra não é por PayPal
                                            if (GameBuyURL.ToLower().Contains("paypal"))
                                            {
                                                    if (Properties.Settings.Default["lang"].ToString() != "en")
                                                        GameButton.Text = $"ADQUIRIR POR R${OpenGames.AvailableGames[i].ToUpper()} \n {GamePrice} (APROX.) VIA PAYPAL";
                                                    else
                                                        GameButton.Text = $"GET {OpenGames.AvailableGames[i].ToUpper()} \n FOR $" + GamePrice + " (APPROX.) VIA PAYPAL";
                                            }
                                            else
                                            {

                                                if (Properties.Settings.Default["lang"].ToString() != "en")
                                                    GameButton.Text = $"ADQUIRIR POR R${OpenGames.AvailableGames[i].ToUpper()} \n {GamePrice} VIA MERCADOPAGO";
                                                else
                                                    GameButton.Text = $"GET {OpenGames.AvailableGames[i].ToUpper()} \n FOR " + GamePrice + " BRL VIA MERCADOPAGO";
                                            }

                                            float UC = MySQL.GetWalletValue; // Valor disponível na carteira
                                                                             // Verifica se é o Game Of Month
                                            string GOM = GameOfMonth;


                                            // Primeiro, verifica se o jogo não é o gratuito do mês
                                            if (GOM == "1")
                                            {

                                                // Verifica se o usuário é HUBR Plus antes de oferecer gratuitamente o jogo
                                                if (Program.HUBRPlus)
                                                {
                                                    // Ativa o botão HUBR Wallet
                                                    btnBuyWithWallet.Enabled = true;
                                                    btnBuyWithWallet.Visible = true;

                                                    if (Properties.Settings.Default["lang"].ToString() != "en")
                                                        // Atualiza o valor do jogo na carteira
                                                        btnBuyWithWallet.Text = $"ADQUIRIR GRÁTIS\nCOM A UGNITE+!";
                                                    else
                                                        // Atualiza o valor do jogo na carteira
                                                        btnBuyWithWallet.Text = $"GET FOR FREE\nONLY WITH UGNITE+!";

                                                }
                                                else // O usuário não é HUBR Plus :( Desativar o botão de HUBR PAY
                                                {
                                                    // Ativa o botão HUBR Wallet
                                                    btnBuyWithWallet.Enabled = false;
                                                    btnBuyWithWallet.Visible = false;
                                                }
                                            }
                                            else // Não é o jogo gratuito do mês ou houve algum erro ao obter a informação, cobrar!
                                            {
                                                if(float.Parse(GamePriceUpay) <= 0f)
                                                {
                                                    if (Properties.Settings.Default["lang"].ToString() != "en")
                                                        // Verifica se o valor do jogo é maior ou igual que zero
                                                        // se for, exibir mensagem de gratuito!                             
                                                        btnBuyWithWallet.Text = $"ADQUIRIR GRÁTIS\nCOM A UPAY";
                                                    else
                                                        // Verifica se o valor do jogo é maior ou igual que zero
                                                        // se for, exibir mensagem de gratuito!                             
                                                        btnBuyWithWallet.Text = $"GET FOR FREE\nUSING UPAY";


                                                    // Ativa o botão HUBR Wallet
                                                    btnBuyWithWallet.Enabled = true;
                                                    btnBuyWithWallet.Visible = true;
                                                }else
                                                // Verifica se o valor do jogo é igual ou maior que o valor disponível na carteira do usuário
                                                if (UC >= float.Parse(GamePriceUpay))
                                                {
                                                        if (Properties.Settings.Default["lang"].ToString() != "en")
                                                            // Verifica se o valor do jogo é maior ou igual que zero
                                                            // se for, exibir mensagem de gratuito!                             
                                                            btnBuyWithWallet.Text = $"COMPRAR POR UP${GamePriceUpay}\nCOM A UPAY";
                                                        else
                                                            // Verifica se o valor do jogo é maior ou igual que zero
                                                            // se for, exibir mensagem de gratuito!                             
                                                            btnBuyWithWallet.Text = $"GET IT FOR UP${GamePriceUpay}\nUSING UPAY";

                                                    // Ativa o botão HUBR Wallet
                                                    btnBuyWithWallet.Enabled = true;
                                                    btnBuyWithWallet.Visible = true;
                                                }                                                
                                                else
                                                {
                                                    // Desativa o botão HUBR Wallet
                                                    btnBuyWithWallet.Enabled = false;
                                                    btnBuyWithWallet.Visible = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        // Verifica se a compra ocorreu bem
                                        if (lbURL.Text.ToUpper().Contains("PAYMENT OK") || lbURL.Text.ToUpper().Contains("OBRIGADO"))
                                        {

                                            // Para a navegação
                                            m_chromeBrowser.Stop();

                                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                                // Muda o texto da URL
                                                lbURL.Text = "ADICIONANDO JOGO À BIBLIOTECA, AGUARDE...";
                                            else
                                                // Muda o texto da URL
                                                lbURL.Text = "LINKING GAME TO YOUR ACCOUNT, PLEASE WAIT...";



                                            // Desativa o botão HUBR Wallet
                                            btnBuyWithWallet.Enabled = false;
                                            btnBuyWithWallet.Visible = false;

                                            // Volta a página inicial
                                            m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/games");

                                            // Adiciona o jogo à conta do cliente
                                            MySQL.ActivateGameKey(true, MySQL.RequestGameInfoByName(2, OpenGames.AvailableGames[i].ToUpper())/*new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.AvailableGames[i].ToUpper() + "/DataCode.HUBR@LOOPS")*/, "MERCADOPAGO/PAYPAL");

                                            // Verifica se salvou o jogo
                                            MySQL.GetActivatedGame(MySQL.RequestGameInfoByName(2, OpenGames.AvailableGames[i].ToUpper())/*new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.AvailableGames[i].ToUpper() + "/DataCode.HUBR@LOOPS")*/);

                                            // Preço do jogo no MP
                                            string Price = MySQL.RequestGameInfoByName(4, OpenGames.AvailableGames[i].ToUpper());//new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.AvailableGames[i].ToUpper() + "/Pri.HUBR@LOOPS");

                                            // Se sim, exibir mensagem de sucesso
                                            if (MySQL.ActivatedByUser)
                                            {
                                                if (Properties.Settings.Default["lang"].ToString() != "en")
                                                {
                                                    // Atualiza registro do usuário
                                                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                                    MySQL.UpdateYourActivity($"0;COMPRA;VIA MERCADOPAGO;ADQUIRIU {OpenGames.AvailableGames[i].ToUpper()} POR {Price};{DateTime.Today.ToString()}");

                                                    // Exibe mensagem com o comprovante
                                                    ProgramData.MensagemSucesso($"COMPROVANTE Nº {MySQL.InvoiceID}\n\nVocê adicionou com sucesso o jogo {OpenGames.AvailableGames[i]} à conta Ugnite.\n\nAgora, acesse a aba BIBLIOTECA DE JOGOS para começar a baixar!\nObrigado pela compra\nComprovante disponível em Suas Atividades.!");
                                                }
                                                else
                                                {
                                                    // Atualiza registro do usuário
                                                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                                    MySQL.UpdateYourActivity($"0;BUY;VIA MERCADOPAGO;ACQUIRED {OpenGames.AvailableGames[i].ToUpper()} FOR {Price};{DateTime.Today.ToString()}");

                                                    // Exibe mensagem com o comprovante
                                                    ProgramData.MensagemSucesso($"INVOICE Nº {MySQL.InvoiceID}\n\nYou have acquired the game {OpenGames.AvailableGames[i]} with success.\nNow, access your library to start downloading it!\nThanks for buying it.\nInvoice Number available at your activity tab.");
                                                }
                                            }
                                            else
                                            {
                                                if (Properties.Settings.Default["lang"].ToString() != "en")
                                                {
                                                    // Exibe mensagem com o comprovante, mas com erro
                                                    ProgramData.MensagemErro($"COMPROVANTE Nº {MySQL.InvoiceID}\n\nHouve um erro ao ativar o jogo {OpenGames.AvailableGames[i]} em sua conta!\nContate o suporte Ugnite via e-mail: shop@ironiawn.com.br com o comprovante de compra.");

                                                    // Atualiza registro do usuário
                                                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                                    MySQL.UpdateYourActivity($"1;COMPRA;VIA MERCADOPAGO;ADQUIRIU {OpenGames.AvailableGames[i].ToUpper()} POR {Price};{DateTime.Today.ToString()}");
                                                }
                                                else
                                                {
                                                    // Exibe mensagem com o comprovante, mas com erro
                                                    ProgramData.MensagemErro($"INVOICE Nº {MySQL.InvoiceID}\n\nError while linking the game {OpenGames.AvailableGames[i]} to your account!\nSend a e-mail to: shop@ironiawn.com.br with your invoice number.");

                                                    // Atualiza registro do usuário
                                                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                                    MySQL.UpdateYourActivity($"1;BUY;VIA MERCADOPAGO;ACQUIRED {OpenGames.AvailableGames[i].ToUpper()} FOR {Price};{DateTime.Today.ToString()}");

                                                }
                                            }
                                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                                // Atualiza registro do usuário
                                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                                MySQL.UpdateYourActivity($"0;FATURA;{MySQL.InvoiceID};REFERÊNCIA: {OpenGames.AvailableGames[i].ToUpper()} POR {Price};{DateTime.Today.ToString()}");
                                            else
                                                // Atualiza registro do usuário
                                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                                MySQL.UpdateYourActivity($"0;INVOICE;{MySQL.InvoiceID};REFERENCE: {OpenGames.AvailableGames[i].ToUpper()} FOR {Price};{DateTime.Today.ToString()}");

                                            // Reseta o invoiceID
                                            MySQL.InvoiceID = null;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Desativa o botão HUBR Wallet
                                btnBuyWithWallet.Enabled = false;
                                btnBuyWithWallet.Visible = false;

                                // Desativa o botão de compra via MP
                                GameButton.Enabled = false;
                                GameButton.Visible = false;

                            }
                        }
                        #endregion
                    }
                    catch
                    {
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ApplyTranslation()
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                btnStore.Text = Program.GetStringRM("en", "btnStore");
                btnLibrary.Text = Program.GetStringRM("en", "btnLibrary");
                btnYourActivity.Text = Program.GetStringRM("en", "btnYourActivity");
                btnYourActivity.Location = new Point(289, 4);
                btnAchievements.Text = Program.GetStringRM("en", "btnAchievements");
                btnAchievements.Location = new Point(420, 4);
            }
        }

        /// <summary>
        /// Fecha o aplicativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Destrói o browser
            Cef.Shutdown();
            Application.ExitThread();
        }

        /// <summary>
        /// Minimiza o aplicativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        /// <summary>
        /// Carregamento da página
        /// </summary>
        private void HUBR_Home_Load(object sender, EventArgs e)
        {
            try
            {

                // Exibe animação de FadeIn
                ProgramData.FadeIn(this, 65);

                // Trazer para frente
                this.BringToFront();

                // Verifica se é HUBR Plus
                MySQL.VerifyPlus();

                // Verifica se os dias restantes são maior que 0
                imgHUBRPlus.Visible = MySQL.RemainingDays == 0 ? false : true;

                // Carrega o tema selecionado pelo usuário
                LoadTheme();

                // Seta a informação de detalhes na aba de cima
                lbDetails.Text = ProgramData.Username.ToUpper();

                // Adiciona a imagem do usuário
                pbUserImage.Load(ProgramData.ImagemURL);

                // Seta o valor disponível da carteira do usuário
                lbWalletValue.Text = MySQL.FormataValor(MySQL.GetWalletValue);

                // Atualiza o idioma selecionado pelo usuário
                MySQL.UpdateInformation(13, Properties.Settings.Default["lang"].ToString());

                // Seta a tradução (se houver)
                ApplyTranslation();

                // Cria a instância do navegador no site da HUBR
                m_chromeBrowser = new ChromiumWebBrowser("https://ugnite.ironiawn.com.br/games");

                // Verifica se o jogador é brasileiro
                if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                    m_chromeBrowser = new ChromiumWebBrowser("https://ugnite.ironiawn.com.br/games");
                else
                    m_chromeBrowser = new ChromiumWebBrowser("https://ugnite.ironiawn.com.br/en/games");

                // Seta a instância para o host de browser
                chromeBrowser.Controls.Add(m_chromeBrowser);

                m_chromeBrowser.TitleChanged += OnBrowserTitleChanged;
                m_chromeBrowser.AddressChanged += OnBrowserAddressChanged;

            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO DE CONEXÃO!\nTALVEZ A UGNITE ESTEJA EM MANUTENÇÃO OU ACONTECEU ALGO QUE OS DEUSES DA INTERNET NÃO PODEM EXPLICAR!\nERRO: HOME_LOAD\nA UGNITE SERÁ ENCERRADA.");
                else
                    ProgramData.MensagemErro("CONNECTION ERROR!\nMAYBE THE UGNITE IS AT MAINTENANCE OR SOMETHING THAT GODS OF INTERNET CAN'T EXPLAIN!\nERROR: HOME_LOAD\nUGNITE MUST RECOVER ITSELF AND WILL BE CLOSED, SORRY.");

                // Manda um bug report
                ClassesMail.EnviaReport(ex.ToString());

                Application.Exit();
            }
        }

        /// <summary>
        /// Abre o diálogo de opções
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOptions_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            Options opt = new Options();
            opt.Show();
        }

        /// <summary>
        /// Mostra o diálogo de ativação de chaves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateKey_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            ActivateKey keyEnable = new ActivateKey();
            keyEnable.Show();
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
        /// Avança a URL no browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browserForward_Click(object sender, EventArgs e)
        {
            m_chromeBrowser.Forward();
        }

        /// <summary>
        /// Volta uma URL no browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browserBack_Click(object sender, EventArgs e)
        {
            m_chromeBrowser.Back();
        }

        /// <summary>
        /// Abre um jogo via botão na navegação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayGame_Click_1(object sender, EventArgs e)
        {
            // Verifica se o jogador é brasileiro
            if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/games");
            else
                m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/en/games");

            // Abre o jogo ou a página de compra
            if (OpenGames.HasGame)
            {
                System.Collections.Generic.List<string> l = new System.Collections.Generic.List<string>();

                Stream data = new WebClient().OpenRead("https://" + $"ugnitedata.s3.amazonaws.com/GameData/{OpenGames.AvailableGames[int.Parse(GameRunCode)].ToUpper()}/GameFiles.txt");
                StreamReader files = new StreamReader(data);
                string Line;
                while ((Line = files.ReadLine()) != null)
                    l.Add(Line);


                // Verifica se o jogo possui mais de um executável
                if(l.Count == 1)
                // Reseta os dados de registro de compra
                OpenGames.OpenGame(OpenGames.GameRunCode, 0);
                else
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        ProgramData.MensagemErro("Por favor, execute o jogo diretamente da área de trabalho.");
                    else
                        ProgramData.MensagemErro("Please, run the game using the desktop shortcut.");

                }
            }
            else
                m_chromeBrowser.Load(OpenGames.GameBuyURL);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HUBR_GameLibraryNew hgc = new HUBR_GameLibraryNew();
            hgc.ShowDialog();
        }

        private void btnOptions_Click_1(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            Options opt = new Options();
            opt.Show();

            // Atualiza a imagem do usuário
            pbUserImage.Load(ProgramData.ImagemURL);
        }

        private void btnActivateGame_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            ActivateKey keyEnable = new ActivateKey();
            keyEnable.Show();
        }

        private void btnYourActivity_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new HUBR_YourActivity();
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
                if (File.Exists(Application.StartupPath + "\\Temas\\" + Theme + "\\Fundo.png"))
                {
                    // Se houver dados de background, aplicar também
                    Image bg = new Bitmap(Application.StartupPath + "\\Temas\\" + Theme + "\\Fundo.png"); // Seta a imagem de fundo
                    this.BackgroundImage = bg;
                }
            }catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO AO CARREGAR TEMA APLICADO A UGNITE.");
                else
                    ProgramData.MensagemErro("ERROR WHILE APPLYING THEME TO UGNITE.");

                Application.Exit();
            }
        }

        /// <summary>
        /// Compra o jogo com o dinheiro disponível na carteira
        /// </summary>
        private void btnBuyWithWallet_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Altera o texto do botão
                btnBuyWithWallet.Text = "AGUARDE...";
            else
                // Altera o texto do botão
                btnBuyWithWallet.Text = "PLEASE WAIT...";

            // Cria o valor do jogo
            float GP = 0;

            // Adquire os valores
            // Verifica se o jogador é brasileiro
            if(ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                GP = float.Parse(MySQL.RequestGameInfoByName(5, OpenGames.LastSeenGame.ToUpper()));//float.Parse(new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.LastSeenGame + "/Val.HUBR@LOOPS")); // Preço Upay Brasil 
            else
                GP = float.Parse(MySQL.RequestGameInfoByName(13, OpenGames.LastSeenGame.ToUpper()));//float.Parse(new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.LastSeenGame + "/Val.HUBR@LOOPS")); // Preço Upay International 

            float Corel = MySQL.GetWalletValue; // W
            string LSG = OpenGames.LastSeenGame; // Ultimo jogo visto
            // Verifica se é o Game Of Month
            string GOM = MySQL.RequestGameInfoByName(7, LSG.ToUpper());//new WebClient().DownloadString("https://ironiawn.com.br/HUBRX/GameData/" + OpenGames.LastSeenGame.ToUpper() + "/GOM.txt");

            // Verifica se o valor do jogo é igual ou maior que o valor disponível na carteira do usuário
            if (Corel >= GP || GOM == "1")
            {
                // Para a navegação
                m_chromeBrowser.Stop();


                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Muda o texto da URL
                    lbURL.Text = "ADICIONANDO JOGO À BIBLIOTECA, AGUARDE...";
                else
                    // Muda o texto da URL
                    lbURL.Text = "LINKING GAME TO YOUR ACCOUNT, PLEASE WAIT...";

                
                // Adiciona o jogo à conta do cliente
                MySQL.ActivateGameKey(true, MySQL.RequestGameInfoByName(2, LSG.ToUpper())/*new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.LastSeenGame + "/DataCode.HUBR@LOOPS")*/, "UPAY");

                // Verifica se salvou o jogo
                MySQL.GetActivatedGame(MySQL.RequestGameInfoByName(2, LSG.ToUpper())/*new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/Pags/" + OpenGames.LastSeenGame + "/DataCode.HUBR@LOOPS")*/);

                // Se sim, exibir mensagem de sucesso
                if (MySQL.ActivatedByUser)
                {
                    // Se o jogo não for o gratuito do mês, cobrar
                    if (GOM != "1")
                        // Atualiza a carteira do usuário
                        MySQL.WalletPayDebit(GP);


                    // Verifica se o jogador é brasileiro
                    if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                        m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/games");
                    else
                        m_chromeBrowser.Load("https://ugnite.ironiawn.com.br/en/games");

                    // Seta o valor disponível da carteira do usuário
                    lbWalletValue.Text = MySQL.FormataValor(MySQL.GetWalletValue);

                    // Se foi gratuito, atualizar de modo diferente
                    if (GOM != "1")
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                        {
                            // Exibe mensagem de sucesso
                            MessageBox.Show($"COMPROVANTE: {MySQL.InvoiceID}\n\nVocê adicionou com sucesso o jogo {OpenGames.LastSeenGame} à conta Ugnite.\n\nAgora, acesse a aba BIBLIOTECA DE JOGOS para começar a baixar!\nObrigado pela compra!\nDetalhes da compra:\nPAGAMENTO UTILIZADO: UPAY" +
                            $"\nPREÇO TOTAL: {MySQL.FormataValor(GP)}\nRESTANTE NA UPAY: {MySQL.FormataValor(MySQL.GetWalletValue)}\nComprovante disponível em Suas Atividades.", "Ugnite - Detalhes da Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Atualiza registro do usuário
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"0;COMPRA;{LSG};ADQUIRIU POR {MySQL.FormataValor(GP)} VIA UPAY;{DateTime.Today.ToString()}");
                        }
                        else
                        {
                            // Exibe mensagem de sucesso
                            MessageBox.Show($"INVOICE Nº {MySQL.InvoiceID}\n\nWe have added the game {OpenGames.LastSeenGame} tor your Ugnite's Account.\nNow, access your library and start downloading it!\nThanks for shopping with us!\nInvoice details:\nPAYMENT METHOD: UPAY" +
                            $"\nTOTAL PRICE: {MySQL.FormataValor(GP)}\nAVAILABLE AT UPAY WALLET: {MySQL.FormataValor(MySQL.GetWalletValue)}\nInvoice available at your activities tab.", "Ugnite - Invoice Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Atualiza registro do usuário
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"0;BUY;{LSG};ACQUIRED FOR {MySQL.FormataValor(GP)} VIA UPAY;{DateTime.Today.ToString()}");
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                        {
                            // Exibe mensagem de sucesso
                            MessageBox.Show($"COMPROVANTE: {MySQL.InvoiceID}\n\nVocê resgatou com sucesso o jogo {OpenGames.LastSeenGame}!\n\nAgora, acesse a aba BIBLIOTECA DE JOGOS para começar a baixá-lo!\nObrigado pela compra!\nDetalhes da compra:\nPAGAMENTO UTILIZADO: UPAY" +
                            $"\nPREÇO TOTAL: R$0,00\nRESTANTE NA UPAY: {MySQL.FormataValor(MySQL.GetWalletValue)}\nComprovante disponível em Suas Atividades.", "Ugnite - Detalhes Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Atualiza registro do usuário
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"0;RESGATE;{LSG};GAME OF THE MONTH UGNITE+;{DateTime.Today.ToString()}");
                        }
                        else
                        {
                            // Exibe mensagem de sucesso
                            MessageBox.Show($"INVOICE Nº {MySQL.InvoiceID}\n\nWe have added the game {OpenGames.LastSeenGame} tor your Ugnite's Account.\nNow, access your library and start downloading it!\nThanks for shopping with us!\nInvoice details:\nPAYMENT METHOD: UPAY" +
                            $"\nTOTAL PRICE: {MySQL.FormataValor(GP)}\nAVAILABLE AT UPAY WALLET: {MySQL.FormataValor(MySQL.GetWalletValue)}\nInvoice available at your activities tab.", "Ugnite - Invoice Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Atualiza registro do usuário
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"0;GET FOR FREE;{LSG};GAME OF THE MONTH UGNITE+;{DateTime.Today.ToString()}");

                        }
                    }

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                    {
                        // Atualiza registro do usuário
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"0;SALDO UPAY;SALDO ANTERIOR: {MySQL.FormataValor(Corel)};SALDO ATUAL: {MySQL.FormataValor(MySQL.GetWalletValue)};{DateTime.Today.ToString()}");

                        // Atualiza registro do usuário
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"0;FATURA;{MySQL.InvoiceID};REFERÊNCIA: {LSG.ToUpper()};{DateTime.Today.ToString()}");
                    }
                    else
                    {
                        // Atualiza registro do usuário
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"0;UPAY BALANCE;PREVIOUS BALANCE: {MySQL.FormataValor(Corel)};ACTUAL BALANCE: {MySQL.FormataValor(MySQL.GetWalletValue)};{DateTime.Today.ToString()}");

                        // Atualiza registro do usuário
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"0;INVOICE;{MySQL.InvoiceID};REFERENCE: {LSG.ToUpper()};{DateTime.Today.ToString()}");

                    }

                    // Envia e-mail para o user
                    ClassesMail.EnviaMailCompra(MySQL.InvoiceID, LSG.ToString(), DateTime.Now.ToString(), GP.ToString());


                    // Reseta os valores
                    Corel = 0;
                    GP = 0;
                }
                else
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                    {
                        ProgramData.MensagemErro($"Houve um erro ao ativar o jogo {LSG} em sua conta!\nContate o suporte Ugnite via e-mail: shop@ironiawn.com.br com o comprovante de compra.");

                        // Atualiza registro do usuário
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"1;COMPRA;{LSG};ADQUIRIU POR {MySQL.FormataValor(GP)};{DateTime.Today.ToString()}");
                    }
                    else
                    {
                        ProgramData.MensagemErro($"Error while linking {LSG} to your account!\nContact us via e-mail: shop@ironiawn.com.br with your invoice number.");

                        // Atualiza registro do usuário
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"1;BUY;{LSG};ACQUIRED FOR {MySQL.FormataValor(GP)};{DateTime.Today.ToString()}");
                    }

                }

                // Altera o texto do botão
                btnBuyWithWallet.Text = "UPAY";

                // Desativa o botão
                btnBuyWithWallet.Enabled = false;
                btnBuyWithWallet.Visible = false;

            }
            else
            {
                // Altera o texto do botão
                btnBuyWithWallet.Text = "UPAY";

                // Desativa o botão
                btnBuyWithWallet.Enabled = false;
                btnBuyWithWallet.Visible = false;
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {
            using (System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker())
            {
                bw.DoWork += (obj, ext) =>
                {
                    MySQL.ActivatePlus(30);
                };

                bw.RunWorkerAsync();
            }
        }

        private void lbDeactivatePlus_Click(object sender, EventArgs e)
        {
            MySQL.DeactivatePlus();
        }

        private void btnPlusPlans_Click(object sender, EventArgs e)
        {
            PlusPlans pp = new PlusPlans();
            pp.ShowDialog();
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new Janelas.Parceiros.UGNITE_InsoniaGames();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void btnSearchGame_Click(object sender, EventArgs e)
        {
            // Verifica se o jogador é brasileiro
            if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                m_chromeBrowser.Load($"https://ugnite.ironiawn.com.br/?s={tbSearchGame.Text}");
            else
                m_chromeBrowser.Load($"https://ugnite.ironiawn.com.br/en/?s={tbSearchGame.Text}");
        }

        private void btnAchievements_Click_1(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new Janelas.Principais.UGNITE_Conquistas();
            form2.Closed += (s, args) => this.Close();
            form2.Show();

        }
    }
}
