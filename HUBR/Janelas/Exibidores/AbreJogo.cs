using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Properties = UGNITE.Properties;

namespace UGNITE
{
    public partial class AbreJogo : Form
    {
        /// <summary>
        /// Código do jogo que vai ser aberto
        /// </summary>
        public static int GameCode;

        /// <summary>
        /// Código de execução do jogo
        /// </summary>
        public static int GameRunCode;
        /// <summary>
        /// Código do executável do jogo
        /// </summary>
        public static int GameEXE = 0;
        ChromiumWebBrowser chrome;
        System.Timers.Timer nTimer;

        /// <summary>
        /// Verifica se o usuário é HUBR Plus
        /// </summary>
        bool Plus = false;
        /// <summary>
        /// Nome do jogo adquirido direto do arquivo de shortcuts
        /// </summary>
        List<string> GameShortcutName = new List<string>();

        public AbreJogo()
        {
            InitializeComponent();


            System.Net.WebClient web = new System.Net.WebClient();
            Stream stream = web.OpenRead("https://s3-sa-east-1.amazonaws.com/ugnitedata/GameData/" + OpenGames.AvailableGames[GameCode].ToUpper() + "/GameShortcuts.txt");
            StreamReader sr = new StreamReader(stream);
            string l;
            while ((l = sr.ReadLine()) != null)
                GameShortcutName.Add(l.ToUpper());

        }
        // Specify what you want to happen when the Elapsed event is raised.
        void AbrirJogo(object source, ElapsedEventArgs e)
        {
            // Desabilita o timer
            nTimer.Dispose();

            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Atualiza o status
                chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='ADQUIRINDO INFORMACOES DA CONTA..'");
            else
                // Atualiza o status
                chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='ACQUIRING ACCOUNT INFO'");

            // Verifica primeiro se não é HUBR Plus
            MySQL.GetInformation(7);
            Plus = MySQL.GetData == "1" ? true : false;

            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Atualiza o status
                chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='ADQUIRINDO INFORMACOES DO JOGO..'");
            else
                // Atualiza o status
                chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='ACQUIRING GAME INFO'");

            // Verifica se o usuário possui o jogo
            MySQL.GetActivatedGame(GameRunCode.ToString());


            // Valida a informação recebida
            if (MySQL.ActivatedByUser || Plus)
            {
                // Adquire o nome do jogo de dentro do arquivo de atalhos
                //GameShortcutName = File.ReadAllLines(Application.StartupPath + @"\GameData\\" + OpenGames.AvailableGames[GameCode] + "\\GameShortcuts.txt");


                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Altera o status do cliente
                    chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='ABRINDO O JOGO..'");
                else
                    // Altera o status do cliente
                    chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='OPENING GAME'");

                // Abre o jogo
                OpenGames.OpenGame(GameCode, GameEXE);

                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Atualiza o status
                    chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='ATUALIZANDO INFORMACOES DE ATIVIDADE..'");
                else
                    chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='UPDATING USER ACTIVITY LOG'");

                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                MySQL.UpdateYourActivity($"0;GAMEPLAY;{GameShortcutName[GameEXE].ToUpper().Replace(" VIA UGNITE", "")};-;{DateTime.Today.ToString()}");

                // Sai do HUBR
                Application.Exit();
                 Cef.Shutdown();
            }
            else
            {  

                // Atualiza o status
                chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='='ERRO :('");

                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("O usuário " + Properties.Settings.Default["username"].ToString() + " não possui acesso a este jogo.");
                else
                    ProgramData.MensagemErro("The User ID " + Properties.Settings.Default["username"].ToString() + $" doesn't have the game {OpenGames.AvailableGames[GameCode]} / [{GameRunCode.ToString()}] linked.");

                Application.Exit();
            }
        }

        private void chromeBrowser_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                nTimer = new System.Timers.Timer();
                nTimer.Elapsed += new ElapsedEventHandler(AbrirJogo);
                nTimer.Interval = 10000;
                nTimer.Enabled = true;

                // Carrega o tema atual
                string Theme = Properties.Settings.Default["Theme"].ToString();

                // Cria a instância do navegador no site da HUBR
                chrome = new ChromiumWebBrowser("file:///" + Application.StartupPath + @"\Temas\" + Theme + @"\u.html\loadGame.html");


                // Verifica se a propriedade de salvar login está ativa
                if (Properties.Settings.Default["saveUser"].ToString() == "1")
                {
                    // Adquire o nome do jogo de dentro do arquivo de atalhos
                    //GameShortcutName = File.ReadAllLines(Application.StartupPath + @"\GameData\\" + OpenGames.AvailableGames[GameCode] + "\\GameShortcuts.txt"); 


                    // Seta o ProgramData.Username
                    ProgramData.Username = Properties.Settings.Default["username"].ToString();

                    // NÃO permitir scrollbars
                    chrome.ExecuteScriptAsyncWhenPageLoaded("document.body.style.overflow = 'hidden'");
                    
                    // Seta as informações necessárias
                    chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_userName').innerText='" + ProgramData.Username.ToUpper() + "'"); // Nome do usuário
                    chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('HUBRGAMENAME').innerText='" + GameShortcutName[GameEXE].ToUpper().Replace(" VIA UGNITE", "") + "'"); // Nome do jogo

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Atualiza o status
                        chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='CARREGANDO MÓDULOS'");
                    else
                        // Atualiza o status
                        chrome.ExecuteScriptAsyncWhenPageLoaded("document.getElementById('hbr_clientStatus').innerText='LOADING MODULE SYSTEM'");


                    // Seta a instância para o host de browser
                    chromeBrowser.Controls.Add(chrome);
                }

                else
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        ProgramData.MensagemErro("PARA EXECUTAR ESSA FUNÇÃO, VÁ EM OPÇÕES > SALVAR LOGIN E TENTE NOVAMENTE.");
                    else
                        ProgramData.MensagemErro("Save your login information before opening games using Desktop shortcuts.\nGo to > OPTIONS > SAVE LOGIN");

                    Application.Exit();
                }
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO AO EXECUTAR JOGO.\nPROVÁVELMENTE ELE NÃO EXISTE NA BASE DE DADOS UGNITE!");
                else
                    ProgramData.MensagemErro("ERROR WHILE OPENING THE GAME.\nMAYBE THE GAME HAS BEEN DEACTIVATED OR REMOVED FROM UGNITE DATABASE.");

                Application.Exit();
            }
        }

        private void AbreJogo_Load(object sender, EventArgs e)
        {
        }
    }
}
