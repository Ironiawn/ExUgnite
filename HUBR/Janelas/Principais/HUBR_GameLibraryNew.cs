using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace UGNITE
{
    public partial class HUBR_GameLibraryNew : Form
    {
        #region
        /// <summary>
        /// O jogo selecionado
        /// </summary>
        string selectedGame;

        Timer checkGames = new Timer();
        #endregion

        /// <summary>
        /// Lista de arquivos que vão ficar escondidos no exibidor e nos arquivos
        /// </summary>
        List<string> HiddenFiles = new List<string>();

        #region COMPONENTES E FUNÇÕES DE DOWNLOAD
        Stopwatch sw = new Stopwatch();    // The stopwatch which we will be using to calculate the download speed
        int IndexCount; // Arquivos baixados
        List<string> fileCounting = new List<string>(); // Arquivos totais

        // The event that will trigger when the WebClient is completed
        void Completed(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled == true)
                    ProgramData.MensagemErro("DOWNLOAD DO JOGO " + gameListBox.SelectedItem.ToString() + " CANCELADO PROPOSITALMENTE OU POR UM ERRO.");
                else
                {
                    #region [DESATIVADA] REGIÃO DE DOWNLOAD POR PARTES
                    /*
                    // Verifica se o download não é arquivo-por-arquivo do jogo
                    if (!System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\GameData\\" + selectedGame + "\\gameFilesServer.txt"))
                    {
                        // Muda o texto de instalação
                        btnPlayGame.Text = "EXTRAINDO ARQUIVOS";

                        if (currentPart <= getParts && getParts > 0)
                        {
                            currentPart++;
                            DownloadInParts();
                        }
                        else
                            OpenGame();
                    }
                    else
                    {
                    }
                    */
                    #endregion

                    // Verifica se não é o último arquivo
                    if (IndexCount <= fileCounting.Count - 1 && fileCounting[IndexCount] != "*")
                    {
                        IndexCount++;
                        InstallFragmented();

                        // Verifica se não há arquivos para aplicação de restrição
                        if (FileToHide != "")
                        {
                            new FileInfo(FileToHide).Attributes = FileAttributes.Hidden;
                            FileToHide = "";
                        }
                    }
                    else
                        InstallFragmented();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // The event that will fire whenever the progress of the WebClient is changed
        void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {

                // Calculate download speed and output it to labelSpeed.
                /*lbProgressDetails.Text = MB((e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds)).ToString("0.00") + " MB/s | " + e.ProgressPercentage.ToString() + "% | RECEBIDOS " + string.Format("{0} MB de {1} MB",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));*/

                double restante = (e.TotalBytesToReceive / 1024d / 1024d) - (e.BytesReceived / 1024d / 1024d);

                #region CALCULA A PORCENTAGEM BASEADA NOS ARQUIVOS RESTANTES
                float porcentagem = (100 * IndexCount) / fileCounting.Count - 1;

                // Impedir que fique -1%
                if (porcentagem <= -0.5)
                    porcentagem = 0;
                #endregion


                // Update the progressbar percentage only when the value is not the same.
                //pbDownloadProgress.Value = (int)porcentagem;

                // Ativa o texto de progresso
                lbProgressDetails.Enabled = true;

                //pbDownloadProgress.Style = ProgressBarStyle.Marquee;
                if ((e.TotalBytesToReceive / 1024d / 1024d) >= 5)
                    lbProgressDetails.Text = MB((e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds)).ToString("0.00") + $"MB/s\n{porcentagem}% CONCLUÍDO | RESTANTE : {restante.ToString("0")}MB";
                else
                    lbProgressDetails.Text = MB((e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds)).ToString("0.00") + $"MB/s\n{porcentagem}% CONCLUÍDO";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
        #region VÁRIAVEIS DE INSTALAÇÃO
        // DETALHES DA INSTALAÇÃO DOS JOGOS [OBRIGATÓRIAS]
        private List<string> Files = new List<string>(); // Lista de arquivos principais que deverão ser verificados
        private List<string> Shortcuts = new List<string>(); // Lista de atalhos que vão ser criados na area de trabalho
        private List<string> ShortcutsIcon = new List<string>(); // Lista dos arquivos de icone dos atalhos na area de trabalho
        private List<string> WorkingDirectories = new List<string>(); // Lista dos diretórios onde os executáveis serão rodados
        private List<string> CommandLine = new List<string>(); // Lista da lista de argumentos para iniciar o HUBR via Desktop
        public bool MoreThanOneEXE = false; // Se o jogo possui mais que um executável [OBRIGATÓRIO]
        public bool Verificando = false; // Se está verificando um jogo ou não
        /// <summary>
        /// Verifica se a instalação do jogo possui algum erro.
        /// </summary>
        public bool BadInstall = false;
        /// <summary>
        /// Cria uma lista temporária para armazenar os dados de instalação dos arquivos
        /// </summary>
        List<string> GL = new List<string>();
        #endregion
        #region FUNÇÕES DE INSTALAÇÃO

        
        /// <summary>
        /// Verifica se a pasta de um jogo existe
        /// </summary>
        /// <param name="Name">NOME DO JOGO</param>
        /// <summary>
        /// Verifica a instalação do jogo
        /// </summary>
        /// <param name="Code">CÓDIGO DO JOGO</param>
        /// <param name="GameFileList">LISTA DE ARQUIVOS PRINCIPAIS DO JOGO A SEREM VERIFICADOS</param>
        void VerifyGameInstallation(List<string> GameFiles)
        {
            // A pasta a ser verificada
            string dir = Directory.GetCurrentDirectory() + "\\Library\\Games\\" + selectedGame;

            // Verifica a pasta
            if (Directory.Exists(dir))
            {
                // Muda o texto de download
                btnPlayGame.Text = "VERIFICANDO";

                // Verifica se os arquivos do jogo existem
                for (int i = 0; i < GameFiles.Count; i++)
                {
                    // Por lista, verifica arquivos principais, um a um.
                    if (!System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\Library\\Games\\" + selectedGame + GameFiles[i]))
                        BadInstall = true;
                }
            }
            else
                // Não há instalação do jogo
                BadInstall = true;
        }

        /// <summary>
        /// Criação de atalho na área de trabalho
        /// </summary>
        /// <param name="Code">CÓDIGO DO JOGO</param>
        /// <param name="Name">NOME DO ATALHO</param>
        /// <param name="EXEName">NOME DO EXECUTÁVEL DO JOGO</param>
        /// <param name="IconLocal">LOCAL DO ICONE(ICO) DO JOGO</param>
        void CreateShortcut(string GameShortcutName, string GameCommandLine, string GameIconShortcut)
        {
            try
            {
                string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                using (StreamWriter writer = new StreamWriter(deskDir + "\\" + GameShortcutName + ".url"))
                {
                    writer.WriteLine("[InternetShortcut]");
                    writer.WriteLine("URL=" + "UGNITE://RunGameID/" + GameCommandLine);
                    writer.WriteLine("IconIndex=0");
                    writer.WriteLine("IconFile=" + Application.StartupPath + @"\Library\Games\" + selectedGame + "\\" + GameIconShortcut);
                    writer.Flush();
                }
            }
            catch
            {
                ProgramData.MensagemErro("ERRO AO CRIAR ATALHOS PARA O JOGO.\nCÓDIGO 218");
            }
        }

        /// <summary>
        /// Arquivo para aplicar ações
        /// </summary>
        string FileToHide = "";


        void InstallFragmented()
        {
            // Adiciona os arquivos em uma lista
            //string[] fileList = System.IO.File.ReadAllLines(Application.StartupPath + @"\GameData\\" + selectedGame + "\\gameFilesServer.txt");
            List<string> fileList = new List<string>();

            Stream data = new WebClient().OpenRead("https://" + $"ugnitedata.s3.amazonaws.com/GameData/{selectedGame}/gameFilesServer.txt");
            StreamReader files = new StreamReader(data);
            string Line;
            while ((Line = files.ReadLine()) != null)
                fileList.Add(Line);

            bool Ironiawn = false; // Verifica se o servidor é Ironiawn

            try
            {
                new WebClient().DownloadString("https://" + $"ugnitedata.s3.amazonaws.com/GameData/{selectedGame}/HUBR.txt");
                Ironiawn = true;
            }
            catch
            {
                Ironiawn = false;
            }

            // Faz o download de arquivos, um por um.
            if (Ironiawn)
            {
                // Adiciona os arquivos em uma lista
                fileCounting = fileList;

                if (IndexCount <= fileCounting.Count-1 && fileCounting[IndexCount] != "*")
                {
                    // Verifica se o diretório em que o arquivo vai existe
                    var arquivo = Application.StartupPath + @"\Library\\Games\\" + selectedGame + "\\" + fileList[IndexCount];
                    if (!new FileInfo(arquivo).Directory.Exists)
                        new FileInfo(arquivo).Directory.Create();

                    // Se o arquivo já existir, pular
                    if(System.IO.File.Exists(arquivo))
                    {
                        IndexCount++;
                        InstallFragmented();
                    }

                    // Exibe o arquivo atual / contagem de arquivos totais
                    lbCurServer.Text = $"ARQUIVO ATUAL : {IndexCount}\nTOTAL: {fileList.Count - 1} ARQUIVOS";

                    // Start the stopwatch which we will be using to calculate the download speed
                    sw.Restart();

                    using (WebClient wb = new WebClient())
                    {
                        // Ativa texto de detalhes e barra de progresso
                        lbProgressDetails.Visible = true;
                        //pbDownloadProgress.Visible = true;


                        // Ativa o texto de servidor
                        lbCurServer.Visible = true;

                        // Muda o texto de download
                        btnPlayGame.Text = "BAIXANDO";

                        wb.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                        // Cria uma instância URI de download
                        Uri URL;

                        // The variable that will be holding the url address (making sure it starts with http://)
                        URL = new Uri("http://ugnitedata.s3.amazonaws.com/Downloads/" + selectedGame + "/" + fileList[IndexCount]);


                        // Start downloading the file
                        wb.DownloadFileAsync(URL, Application.StartupPath + @"\\Library\\Games\\" + selectedGame + "\\" + fileList[IndexCount]);

                    }
                }
                else
                { 
                    // Muda o texto de download
                    btnPlayGame.Text = "INSTALANDO 3/3";

                    // Desativa a barra de progresso
                    //pbDownloadProgress.Visible = false;

                    // Coloca o principal executável do arquivo no arquivo de configuração
                    if (WorkingDirectories[0] != "")
                    {
                        for (int d = 0; d < WorkingDirectories.Count; d++)
                        {
                            System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame + "\\cfg_EXE[" + d + "].UCFG", Files[d]);
                            System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame + "\\cfg_WD[" + d + "].UCFG", WorkingDirectories[d]);
                        }
                    }
                    else
                    {
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame + "\\cfg_EXE[0].UCFG", Files[0]);
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame + "\\cfg_WD[0].UCFG", WorkingDirectories[0]);
                    }

                    // Cria os atalhos de acordo com a quantidade
                    for (int x = 0; x < Shortcuts.Count; x++)
                    {
                        CreateShortcut(Shortcuts[x], CommandLine[x], ShortcutsIcon[x]);
                    }

                    // Muda o texto de download
                    btnPlayGame.Text = "JOGAR";

                    // Ativa o botão de jogar
                    btnPlayGame.Enabled = true;

                    // Desativa o texto de servidor
                    lbCurServer.Visible = false;

                    // Desativa o texto de progresso
                    lbProgressDetails.Visible = false;

                    // NÃO está verificando um jogo
                    Verificando = false;

                }
            }
            else // Download via S3
            {
                fileCounting = fileList;

                if (IndexCount <= fileCounting.Count - 1 && fileCounting[IndexCount] != "*")
                {
                    // Verifica qual o arquivo que está a ser baixado
                    var arquivo = Application.StartupPath + @"\Library\Games\" + selectedGame + "\\" + fileList[IndexCount];


                    // Verifica se o diretório em que o arquivo vai existe
                    if (!new FileInfo(arquivo).Directory.Exists)
                        new FileInfo(arquivo).Directory.Create();

                    // Se o arquivo já existir, pular
                    if (File.Exists(arquivo))
                    {
                        IndexCount++;
                        InstallFragmented();
                    }
                    // Exibe o arquivo atual / contagem de arquivos totais
                    lbCurServer.Text = $"ARQUIVO ATUAL : {IndexCount}\nTOTAL: {fileList.Count - 1} ARQUIVOS";

                    // Start the stopwatch which we will be using to calculate the download speed
                    sw.Restart();

                    using (WebClient wb = new WebClient())
                    {
                        // Ativa texto de detalhes e barra de progresso
                        lbProgressDetails.Visible = true;
                        //pbDownloadProgress.Visible = true;


                        // Ativa o texto de servidor
                        lbCurServer.Visible = true;

                        // Muda o texto de download
                        btnPlayGame.Text = "BAIXANDO";

                        wb.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                        // Cria uma instância URI de download
                        Uri URL;

                        // The variable that will be holding the url address (making sure it starts with http://)
                        URL = new Uri("http://rest.s3for.me/hubr/" + selectedGame + "/" + fileList[IndexCount].Replace(" ", "+"));



                        // Start downloading the file
                        wb.DownloadFileAsync(URL, Application.StartupPath + @"\Library\Games\" + selectedGame + "\\" + fileList[IndexCount]);

                    }
                }
                else
                {
                    // Muda o texto de download
                    btnPlayGame.Text = "BAIXANDO";

                    // Desativa a barra de progresso
                    //pbDownloadProgress.Visible = false;

                    // Coloca o principal executável do arquivo no arquivo de configuração
                    if (WorkingDirectories[0] != "")
                    {
                        for (int d = 0; d < WorkingDirectories.Count; d++)
                        {
                            System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame + "\\cfg_EXE[" + d + "].UCFG", Files[d]);
                            System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame + "\\cfg_WD[" + d + "].UCFG", WorkingDirectories[d]);
                        }
                    }
                    else
                    {
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame + "\\cfg_EXE[0].UCFG", Files[0]);
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame + "\\cfg_WD[0].UCFG", WorkingDirectories[0]);
                    }

                    // Cria os atalhos de acordo com a quantidade
                    for (int x = 0; x < Shortcuts.Count; x++)
                    {
                        CreateShortcut(Shortcuts[x], CommandLine[x], ShortcutsIcon[x]);
                    }

                    // Muda o texto de download
                    btnPlayGame.Text = "JOGAR";

                    // Ativa o botão de jogar
                    btnPlayGame.Enabled = true;

                    // Desativa o texto de servidor
                    lbCurServer.Visible = false;

                    // Desativa o texto de progresso
                    lbProgressDetails.Visible = false;

                    // NÃO está verificando um jogo
                    Verificando = false;


                    // Reseta as funções de download
                    IndexCount = 0;
                    fileCounting = null;

                    // Ativa o botão de desinstalar
                    btnDeleteGame.Visible = true;
                    btnDeleteGame.Enabled = true;
                }

            }
        }


        /// <summary>
        /// Verifica a instância de um jogo 
        /// </summary>
        /// <param name="Code">CÓDIGO DO JOGO</param>
        /// <param name="FileList">LISTA DE ARQUIVOS PRINCIPAIS(E SEUS DIRETÓRIOS)</param>
        /// <param name="_pBar">BARRA DE PROGRESSO</param>
        /// <param name="EXEName">DIRETÓRIO DO EXECUTÁVEL DO JOGO</param>
        public void OpenGame()
        {
            
            try
            {
                #region VERIFICAÇÃO INICIAL
                // Reseta as listas
                Files.Clear();
                Shortcuts.Clear();
                ShortcutsIcon.Clear();
                CommandLine.Clear();
                WorkingDirectories.Clear();

                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Altera o texto do botão
                    btnPlayGame.Text = "ZONEANDO 1/2";
                else
                    // Muda o texto de download
                    btnPlayGame.Text = "ZONE CHECKING 1/2";


                // Está verificando um jogo
                Verificando = true;

                // Verifica se há mais que um executável no jogo
                Files = GetWebList("GameFiles");
                if(Files.Count > 1)
                    MoreThanOneEXE = true;

                // Verifica se o jogo está instalado
                VerifyGameInstallation(Files);
                #endregion

                // Instala o jogo se a instalação não for válida
                if (BadInstall)
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Muda o texto de download
                        btnPlayGame.Text = "ZONEANDO 2/2";
                    else
                        // Muda o texto de download
                        btnPlayGame.Text = "ZONE CHECKING 2/2";



                    // Desativa o botão de jogar
                    btnPlayGame.Enabled = false;


                    // Desativa texto de detalhes e barra de progresso
                    lbProgressDetails.Visible = false;
                    //pbDownloadProgress.Visible = false;

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Muda o texto de download
                        btnPlayGame.Text = "BAIXANDO";
                    else
                        // Muda o texto de download
                        btnPlayGame.Text = "DOWNLOADING";



                    // Instala o jogo
                    // InstallFragmented();

                    // Configura o novo instalador de jogos
                    UGNITE_InstallGame UIG = new UGNITE_InstallGame();
                    UIG.GameName = selectedGame.ToUpper();
                    UIG.GLN = this;

                    // Abre o diálogo
                    UIG.ShowDialog();
                }
                else
                {
                    // Desativa a barra de progresso e texto de detalhes
                    //pbDownloadProgress.Visible = false;
                    lbProgressDetails.Visible = false;

                    // Termino da verificação
                    Verificando = false;


                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // JOGAR
                        btnPlayGame.Text = "JOGAR";
                    else
                        // JOGAR
                        btnPlayGame.Text = "PLAY GAME";



                    // Verifica se o jogo possui mais de um executável
                    if (!MoreThanOneEXE)
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"0;JOGANDO;{OpenGames.LastSeenGame};-;{DateTime.Today.ToString()}");
                        else
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"0;PLAYING;{OpenGames.LastSeenGame};-;{DateTime.Today.ToString()}");


                        // Minimiza a HUBR
                        this.WindowState = FormWindowState.Minimized;


                        // Abre o jogo
                        OpenGames.OpenGame(selectedGame, 0); // Abrir via OpenGames
                    }
                    else
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            ProgramData.MensagemErro("Abra o jogo diretamente pelos atalhos criados na area de trabalho, beleza?");
                        else
                            ProgramData.MensagemErro("Open the game via shortcut created on your Desktop, okay?");
                    }
                }
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO AO VERIFICAR/INSTALAR JOGO.\n\nCÓDIGO OG584 [GRAVE]");
                else
                    ProgramData.MensagemErro($"ERROR WHILE VERIFYING THE GAME FILES.\nCODE OG586");

            }
        }

        /// <summary>
        /// Converte KB em MB
        /// </summary>
        /// <param name="kilobytes"></param>
        /// <returns></returns>
        static double MB(double kilobytes)
        {
            return kilobytes / 1024f;
        }
        #endregion

        public HUBR_GameLibraryNew()
        {
            InitializeComponent();
            this.Opacity = 0.0; // Muda a opacidade da janela para dar FadeIn depois ;)   

            // Inicia um intervalo que checa os jogos
            checkGames.Enabled = true;
            checkGames.Interval = 1000;
            checkGames.Tick += new EventHandler(BlockGameList);

            #region BLOQUEIO DE ARQUIVO
            HiddenFiles.Add("cdx");
            HiddenFiles.Add("steam");
            HiddenFiles.Add("3dm");
            HiddenFiles.Add("codex");
            HiddenFiles.Add("elamigos");
            HiddenFiles.Add("cpy");
            #endregion
        }

        void BlockGameList(object sender, EventArgs e)
        {
            if (Verificando)
                gameListBox.Enabled = false;
            else
                gameListBox.Enabled = true;
        }

        void ApplyTranslation()
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                btnStore.Text = Program.GetStringRM("en", "btnStore");
                btnLibrary.Text = Program.GetStringRM("en", "btnLibrary");
                btnYourActivity.Text = Program.GetStringRM("en", "btnYourActivity");
                btnYourActivity.Location = new Point(289, 3);
                btnDeleteGame.Text = Program.GetStringRM("en", "btnDeleteGame");
                btnStartupRepair.Text = "STARTUP\nREPAIR";
                btnAchievements.Location = new Point(420, 4);
                btnAchievements.Text = Program.GetStringRM("en", "btnAchievements");
                lbWichNote.Text = "RATE THE GAME!";

            }
        }

        #region [DESATIVADA] REGIÃO DE DOWNLOAD POR PARTES
        /*
        /// <summary>
        /// PARTE ATUAL DO DOWNLOAD
        /// </summary>
        int currentPart = 1;
        /// <summary>
        /// ARQUIVO ATUAL DO DOWNLOAD
        /// </summary>
        string curFile;
        /// <summary>
        /// AS PARTES DO ARQUIVO
        /// </summary>
        int getParts = 0;
        /// <summary>
        /// FAZ O DOWNLOAD DO JOGO EM PARTES
        /// </summary>
        void DownloadInParts()
        {
            try
            {
                // DESCRIPTOGRAFA O ARQUIVO COM AS INFORMAÇÕES 
                //FileEncrypt.Decrypt(Directory.GetCurrentDirectory() + "\\GameData\\" + selectedGame.ToUpper() + "\\installSettings2.cfg", "hubr.tmp", true);


                // LÊ O ARQUIVO COM O LINK DE DOWNLOAD DAS PARTES DO JOGO
                getParts = int.Parse(System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\GameData\\" + selectedGame.ToUpper() + "\\fileCount.txt"));


                if (currentPart != getParts)
                    btnPlayGame.Text = "BAIXANDO 2/3";
                else
                if (currentPart == getParts)
                    btnPlayGame.Text = "EXTRAINDO";

                if (currentPart <= getParts)
                {
                    // Verifica se não é mais de 100 arquivos para baixar
                    if (getParts < 100)
                    {
                        // Se for arquivo RAR, continuar com a formatação [.PART01.RAR]
                        if (currentPart < 10)
                            curFile = Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part0" + (currentPart).ToString() + ".rar";
                        else
                            curFile = Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part" + (currentPart).ToString() + ".rar";
                    }
                    else
                    {
                        // Se for arquivo RAR, continuar com a formatação [.PART01.RAR]
                        if (currentPart < 10)
                            curFile = Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part00" + (currentPart).ToString() + ".rar";
                        else
                        if (currentPart >= 10 && currentPart <= 99)
                            curFile = Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part0" + (currentPart).ToString() + ".rar";
                        else
                            curFile = Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part" + (currentPart).ToString() + ".rar";
                    }


                    if (!System.IO.File.Exists(curFile))
                    {
                        using (webClient = new WebClient())
                        {
                            try
                            {
                                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                                // Reinicia o stopwatch
                                sw.Restart();

                                // Verifica se é para baixar direto do servidor Ironiawn
                                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\GameData\\" + selectedGame.ToUpper() + "\\HUBR.txt"))
                                {
                                    // Seta o servidor atual
                                    lbCurServer.Text = "SERVIDOR: HUBR IRONIAWN";

                                    // Se for arquivo RAR, continuar com a formatação [.PART01.RAR]
                                    if (currentPart < 10)
                                        // Start downloading the file
                                        webClient.DownloadFileAsync(new Uri("http://ugnitedata.s3.amazonaws.com/Downloads/" + selectedGame.ToUpper() + "/" + selectedGame.ToUpper() + ".part0" + currentPart + ".rar"), curFile);
                                    else
                                    if (currentPart >= 10)
                                        // Start downloading the file
                                        webClient.DownloadFileAsync(new Uri("http://ugnitedata.s3.amazonaws.com/Downloads/" + selectedGame.ToUpper() + "/" + selectedGame.ToUpper() + ".part" + currentPart + ".rar"), curFile);
                                }
                                else
                                {
                                    // Seta o servidor atual
                                    lbCurServer.Text = "SERVIDOR: HUBR IRONIAWN BRA";

                                    // Verifica se não há mais de 100 partes para serem baixadas no servidor; Utilizada para mudar a contagem e baixar os arquivos corretos.
                                    if (getParts >= 100)
                                    {
                                        #region ARQUIVO EM MAIS DE 100 PARTES, MUDAR CONTAGEM NO SERVIDOR
                                        // Se for arquivo RAR, continuar com a formatação [.PART01.RAR]
                                        if (currentPart < 10)
                                            // Start downloading the file
                                            webClient.DownloadFileAsync(new Uri("http://rest.s3for.me/hubr/" + selectedGame.ToUpper().Replace(" ", "+") + "/" + selectedGame.ToUpper().Replace(" ", "+") + ".part00" + currentPart + ".rar"), curFile);
                                        else
                                        if (currentPart >= 10 && currentPart <= 99)
                                            // Start downloading the file
                                            webClient.DownloadFileAsync(new Uri("http://rest.s3for.me/hubr/" + selectedGame.ToUpper().Replace(" ", "+") + "/" + selectedGame.ToUpper().Replace(" ", "+") + ".part0" + currentPart + ".rar"), curFile);
                                        else
                                            // Start downloading the file
                                            webClient.DownloadFileAsync(new Uri("http://rest.s3for.me/hubr/" + selectedGame.ToUpper().Replace(" ", "+") + "/" + selectedGame.ToUpper().Replace(" ", "+") + ".part" + currentPart + ".rar"), curFile);
                                        #endregion
                                    }
                                    else
                                    {
                                        // Se for arquivo RAR, continuar com a formatação [.PART01.RAR]
                                        if (currentPart < 10)
                                            // Start downloading the file
                                            webClient.DownloadFileAsync(new Uri("http://rest.s3for.me/hubr/" + selectedGame.ToUpper().Replace(" ", "+") + "/" + selectedGame.ToUpper().Replace(" ", "+") + ".part0" + currentPart + ".rar"), curFile);
                                        else
                                        if (currentPart >= 10)
                                            // Start downloading the file
                                            webClient.DownloadFileAsync(new Uri("http://rest.s3for.me/hubr/" + selectedGame.ToUpper().Replace(" ", "+") + "/" + selectedGame.ToUpper().Replace(" ", "+") + ".part" + currentPart + ".rar"), curFile);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ProgramData.MensagemErro(ex.ToString() + " || " + currentPart.ToString());
                            }
                        }
                    }
                    else
                    {
                        currentPart++;
                        DownloadInParts();
                    }

                }
                else
                    VerifyDownloadedPart();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // O tamanho do primeiro arquivo baixado
        long firstPartSize = 0;

        // Parte atual que está sendo verificada
        int curPart;


        /// <summary>
        /// Verifica o tamanho dos arquivos para ver se estão corretos
        /// </summary>
        void VerifyDownloadedPart()
        {
            // Partes que existem no jogo
            int maxParts = getParts;

            // Percorre loops de checagem
            for (int i = 1; i <= maxParts; i++)
            {
                // Seta a parte atual que está sendo verificada
                curPart = i;

                // Altera o botão de jogar
                btnPlayGame.Text = $"CHECANDO..{i}/{maxParts}";
                btnPlayGame.Enabled = false;

                if (i != maxParts)
                {
                    // Se a parte atual é menor que 10, fazer um método diferente.
                    if (i < 10)
                    {
                        // Se o arquivo da parte existir, comparar o tamanho com o da primeira parte
                        if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part0" + i + ".rar"))
                        {
                            if (firstPartSize == 0)
                                firstPartSize = new FileInfo(Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part0" + i + ".rar").Length;

                            long FileSize = new FileInfo(Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part0" + i + ".rar").Length;
                            if (FileSize != firstPartSize)
                            {
                                currentPart = i;
                                DownloadInParts();
                            }
                        }
                        else // O arquivo da parte atual que está sendo checada está faltando, refazer o download
                        {
                            currentPart = i;
                            DownloadInParts();
                        }
                    }
                    else
                    {
                        // Se o arquivo da parte existir, comparar o tamanho com o da primeira parte
                        if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part" + i + ".rar"))
                        {
                            // Adquire o tamanho da parte a ser verificada
                            long FileSize = new FileInfo(Directory.GetCurrentDirectory() + "\\Downloads\\" + selectedGame.ToUpper() + ".part" + i + ".rar").Length;

                            // Verifica se o tamanho é igual ao do primeiro arquivo
                            if (FileSize != firstPartSize)
                            {
                                currentPart = i;
                                DownloadInParts();
                            }
                        }
                        else // O arquivo da parte atual que está sendo checada está faltando, refazer o download
                        {
                            currentPart = i;
                            DownloadInParts();
                        }
                    }
                }
                else
                {

                    // Altera o botão de jogar
                    btnPlayGame.Text = "JOGAR";
                    btnPlayGame.Enabled = true;
                    OpenGame();
                }
            }
        }
        */
        #endregion
        private void HUBR_GameLibraryNew_Load(object sender, EventArgs e)
        {
            try
            {
                // Trazer para frente
                this.BringToFront();

                // Exibe animação de FadeIn
                ProgramData.FadeIn(this, 65);

                // Carrega o tema
                LoadTheme();

                // Verifica se é HUBR Plus
                MySQL.VerifyPlus();

                // Verifica se os dias restantes são maior que 0
                imgHUBRPlus.Visible = MySQL.RemainingDays == 0 ? false : true;

                // Deleta a lista
                gameListBox.Items.Clear();

                // Desabilita itens de detalhes
                DisableVisualItems();

                // Adiciona a imagem do usuário
                pbUserImage.Load(ProgramData.ImagemURL);

                // Se for HUBR Plus, adicionar todos os jogos da HUBR na lista
                if (!Program.HUBRPlus)
                {
                    // Adquire todos os jogos na conta do usuário
                    MySQL.GetGames();

                    // Adiciona os jogos disponíveis na conta do usuário
                    if (MySQL.GetDataList.Count > 0)
                    {
                        for (int i = 0; i < MySQL.GetDataList.Count; i++)
                            gameListBox.Items.Add(MySQL.GetDataList[i].ToUpper());
                    }

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                    {
                        // Verifica se a lista possui jogos
                        if (gameListBox.Items.Count <= 0)
                            lbGameName.Text = "VOCÊ NÃO POSSUI JOGOS :(\nASSINE UGNITE+ E\nTENHA TODOS*!";
                        else
                            lbGameName.Text = "SELECIONE UM JOGO NA LISTA";
                    }
                    else
                    {
                        // Verifica se a lista possui jogos
                        if (gameListBox.Items.Count <= 0)
                            lbGameName.Text = "YOU DON'T HAVE ANY GAME :(\nGET UGNITE+ AND\nHAVE ALL OF THEM*!";
                        else
                            lbGameName.Text = "SELECT A GAME ON THE LIST";

                    }
                }
                else
                {
                    // Lista de jogos da Ugnite
                    List<string> AllAvailableGames = MySQL.AvailableGames("2");

                    // Verifica se o jogo que o usuário possui está na Plus
                    for(int i = 0; i < AllAvailableGames.Count; i++)
                    {
                        // Verifica se o usuário possui o jogo atual do loop
                        MySQL.GetActivatedGame(MySQL.RequestGameInfoByName(2, AllAvailableGames[i]));

                        // Se o usuário não possuir o jogo, verificar se o jogo está na Plus.
                        if (!MySQL.ActivatedByUser)
                        {
                            // Se o jogo está disponível na Plus, adicionar, se não, ignorar.
                            if(MySQL.RequestGameInfoByName(6, AllAvailableGames[i]) == "1")
                            {
                                gameListBox.Items.Add(AllAvailableGames[i].ToUpper());
                            }
                        }
                        else
                        {
                            // O usuário possui o jogo, adicionar
                            gameListBox.Items.Add(AllAvailableGames[i].ToUpper());
                        }
                    }
                    /*
                    
                    WebClient web = new WebClient();
                    Stream stream = web.OpenRead("https://ugnitedata.s3.amazonaws.com/games.txt");
                    StreamReader sr = new StreamReader(stream);
                    while ((l = sr.ReadLine()) != null)
                    {
                        // Verifica se o jogo é aceito na Ugnite Plus
                        string allowed = new WebClient().DownloadString("https://ugnitedata.s3.amazonaws.com/GameData/" + l.ToUpper() + "/Plus.txt");

                        // Se for, adicionar
                        if (allowed == "1")
                                    gameListBox.Items.Add(l.ToUpper());
                        else
                        {
                            for(int i = 0; i < MySQL.GetDataList.Count;i++)
                            {
                                if (l.ToLower() == MySQL.GetDataList[i].ToLower())
                                    gameListBox.Items.Add(l.ToUpper());
                            }
                        }
                    }
                    */
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Altera o texto de jogos
                        lbGameName.Text = "SELECIONE UM JOGO NA LISTA";
                    else
                        // Altera o texto de jogos
                        lbGameName.Text = "SELECT A GAME ON THE LIST";

                }
                // Seta o nome do usuário
                lbDetails.Text = ProgramData.Username.ToUpper();

                // Atualiza o idioma selecionado pelo usuário
                MySQL.UpdateInformation(13, Properties.Settings.Default["lang"].ToString());

                // Aplica a tradução (se houver)
                ApplyTranslation();

            }
            catch(Exception EX)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO DE CONEXÃO!\nTALVEZ A UGNITE ESTEJA EM MANUTENÇÃO OU ACONTECEU ALGO QUE OS DEUSES DA INTERNET NÃO PODEM EXPLICAR!\nERRO: GLN_LOAD\nA UGNITE SERÁ ENCERRADA.\n" + EX.Message);
                else
                    ProgramData.MensagemErro("CONNECTION ERROR!\nMAYBE THE UGNITE IS AT MAINTENANCE OR SOMETHING THAT GODS OF INTERNET CAN'T EXPLAIN!\nERROR: GLN_LOAD\nUGNITE MUST RECOVER ITSELF AND WILL BE CLOSED, SORRY.\n" + EX.Message);
                Application.Exit();
            }
        }
        
        /// <summary>
        /// Quando o usuário selecionar um jogo na listBox, adicionar detalhes dele
        /// </summary>
        private void gameListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                // Verifica se não há uma verificação em andamento(hehe)
                if (!Verificando)
                {
                    // Seta o jogo selecionado
                    selectedGame = gameListBox.SelectedItem.ToString().ToUpper();

                    // Seta o nome do jogo selecionado
                    lbGameName.Text = gameListBox.SelectedItem.ToString();

                    // Adquire o código do jogo
                    string gCode = MySQL.RequestGameInfoByName(2, gameListBox.SelectedItem.ToString().ToUpper());//new WebClient().DownloadString("https://" + $"ugnitedata.s3.amazonaws.com/Pags/{gameListBox.SelectedItem.ToString().ToUpper()}/Code.HUBR@LOOPS");


                    // Reseta as configurações de download
                    /* currentPart = 1;
                    curPart = 0;
                    getParts = 0;
                    firstPartSize = 0;*/
                    IndexCount = 0;
                    fileCounting = null;

                    // URL de descrição
                    string descURL;

                    // Verifica o idioma da pessoa
                    if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                        descURL = "https://" + $"ugnitedata.s3.amazonaws.com/GameData/{gameListBox.SelectedItem.ToString().ToUpper()}/GameDetails.hubr";
                    else
                        descURL = "https://" + $"ugnitedata.s3.amazonaws.com/GameData/{gameListBox.SelectedItem.ToString().ToUpper()}/GameDetailsEN.hubr";

                        // Seta o texto de detalhes do jogo selecionado
                        //string FileX = Directory.GetCurrentDirectory() + "\\GameData\\" + gameListBox.SelectedItem.ToString().ToUpper() + "\\GameDetails.hubr";
                        string FileX = new WebClient().DownloadString(descURL);
                    lbGameDetails.Text = FileX;

                    // Seta a imagem do jogo selecionado
                    //GameImage.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\GameData\\" + gameListBox.SelectedItem.ToString().ToUpper() + "\\GameImage.hubr");
                    string GI = MySQL.RequestGameInfoByName(11, gameListBox.SelectedItem.ToString().ToUpper());
                    GameImage.Load(GI);//"https://" + $"ugnitedata.s3.amazonaws.com/GameData/{gameListBox.SelectedItem.ToString().ToUpper()}/GameImage.hubr");

                    // Habilita os controles
                    GameImage.Visible = true;
                    lbGameDetails.Visible = true;
                    btnPlayGame.Visible = true;
                    GameImage.Enabled = true;
                    lbGameDetails.Enabled = true;
                    btnPlayGame.Enabled = true;
                    btnStartupRepair.Enabled = true;
                    btnStartupRepair.Visible = true;


                    // Verifica se a pasta do jogo existe (para habilitar a opção de desinstalação)
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Library\\Games\\" + selectedGame.ToUpper()))
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            btnPlayGame.Text = "JOGAR";
                        else
                            btnPlayGame.Text = "PLAY";


                        RatingPanel.Visible = true;
                        RatingPanel.Enabled = true;
                        btnDeleteGame.Visible = true;
                        btnDeleteGame.Enabled = true;
                        btnStartupRepair.Enabled = true;
                        btnStartupRepair.Visible = true;

                        if (Properties.Settings.Default["lang"].ToString() != "en")
                        {
                            // Adquire a nota do jogo
                            lbGeneralRating.Text = "MÉDIA DO JOGO : " + MySQL.GetGameRating(gCode);

                            // Altera o texto do botão de votação
                            btnSendRating.Text = "AVALIE O JOGO!";
                        }
                        else
                        {
                            // Adquire a nota do jogo
                            lbGeneralRating.Text = "AVG. GAME RATING : " + MySQL.GetGameRating(gCode);

                            // Altera o texto do botão de votação
                            btnSendRating.Text = "RATE THE GAME!";
                        }

                        // Verifica se o usuário atual já votou no jogo
                        if(MySQL.UserRated(gCode))
                        {
                            RatingPanel.Size = new Size(273, 80);
                            btnSendRating.Enabled = false;
                            trackRating.Enabled = false;
                            lbWichNote.Enabled = false;
                        }
                        else
                        {
                            RatingPanel.Size = new Size(273, 218);
                            btnSendRating.Enabled = true;
                            trackRating.Enabled = true;
                            lbWichNote.Enabled = true;
                        }

                    }
                    else
                    {
                        // Verifica se os usuários podem baixar ou não o jogo
                        string down = MySQL.RequestGameInfoByName(12, gameListBox.SelectedItem.ToString().ToUpper());//new WebClient().DownloadString("https://" + $"ugnitedata.s3.amazonaws.com/GameData/{gameListBox.SelectedItem.ToString().ToUpper()}/Downloadable.txt");

                        if (Properties.Settings.Default["lang"].ToString() != "en")
                        {
                            if (down == "0")
                            {
                                btnPlayGame.Text = "EM BREVE";
                                btnPlayGame.Enabled = false;
                            }
                            else
                            if (down == "1")
                                btnPlayGame.Text = "INSTALAR";
                            else
                            if (down == "2")
                            {
                                btnPlayGame.Text = "EM ATUALIZAÇÃO";
                                btnPlayGame.Enabled = false;
                            }
                            else
                            if (down == "3")
                            {
                                btnPlayGame.Text = "DESATIVADO :(";
                                btnPlayGame.Enabled = false;
                            }
                        }
                        else
                        {
                            if (down == "0")
                            {
                                btnPlayGame.Text = "COMING SOON";
                                btnPlayGame.Enabled = false;
                            }
                            else
                            if (down == "1")
                                btnPlayGame.Text = "INSTALL";
                            else
                            if (down == "2")
                            {
                                btnPlayGame.Text = "MAINTENANCE MODE";
                                btnPlayGame.Enabled = false;
                            }
                            else
                            if (down == "3")
                            {
                                btnPlayGame.Text = "DEACTIVATED :(";
                                btnPlayGame.Enabled = false;
                            }

                        }

                        btnDeleteGame.Visible = false;
                        btnDeleteGame.Enabled = false;
                        btnStartupRepair.Enabled = false;
                        btnStartupRepair.Visible = false;
                        RatingPanel.Visible = false;
                        RatingPanel.Enabled = false;
                    }
                }
                else
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        ProgramData.MensagemErro("HÁ UMA VERIFICAÇÃO/INSTALAÇÃO EM ANDAMENTO!\nPOR FAVOR, TENHA PACIÊNCIA!! A UGNITE AINDA É UMA BEBEZINHA E ESTÁ DANDO SEUS PRIMEIROS PASSOS,\nE POR ISSO HÁ ALGUMAS LIMITAÇÕES :(");
                    else
                        ProgramData.MensagemErro("AN VERIFICATION/INSTALLATION PROCESS IS CURRENTLY ACTIVE!\nPLEASE, BE PATIENT!! UGNITE IS STILL A BABY AND IS GIVING ITS FIRST STEPS, AND THEREFORE THERE ARE SOME LIMITATIONS :(");

                }
            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"DEU RUIM!\nSUA CONTA ESTÁ COM ALGUM PROBLEMA NO BANCO DE DADOS DA UGNITE.\nENTRE EM CONTATO PARA RESOLVER ESSE PEQUENO PROBLEMA.\nE-MAIL: shop@ironiawn.com.br\n{ex}");
                else
                    ProgramData.MensagemErro($"OH SH*T!\nSOMETHING BAD HAPPENED TO YOUR ACCOUNT AT UGNITE'S DATABASE.\nCONTACT US TO SOLVE THIS PROBLEM.\nE-MAIL: shop@ironiawn.com.br\n\n{ex}");

                Application.Exit();
            }
        }
            #region REGIÃO VISUAL
            /// <summary>
            /// Desabilita itens que exibem detalhes
            /// </summary>
            void DisableVisualItems()
        {
            lbProgressDetails.Visible = false;
            btnPlayGame.Visible = false;
            //pbDownloadProgress.Visible = false;
            lbGameDetails.Visible = false;
            GameImage.Visible = false;
            lbCurServer.Visible = false;

            lbProgressDetails.Enabled = false;
            btnPlayGame.Enabled = false;
            //pbDownloadProgress.Enabled = false;
            lbGameDetails.Enabled = false;
            GameImage.Enabled = false;
        }
        #endregion

        #region REGIÃO SUPERIOR ESQUERDA
        /// <summary>
        /// Botão de exibir opções do HUBR
        /// </summary>
        private void btnOptions_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            Options opt = new Options();
            opt.Show();
        }
        /// <summary>
        /// Botão de ativação de jogos via chave
        /// </summary>
        private void btnActivateKey_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            ActivateKey keyEnable = new ActivateKey();
            keyEnable.Show();
        }
        /// <summary>
        /// Minimiza o HUBR
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

        #endregion

        /// <summary>
        /// Adquire lista de arquivos na IwWeb
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<string> GetWebList(string filename)
        {
            try
            {
                List<string> l = new List<string>();

                Stream data = new WebClient().OpenRead("https://" + $"ugnitedata.s3.amazonaws.com/GameData/{selectedGame.ToUpper()}/{filename}.txt");
                StreamReader files = new StreamReader(data);
                string Line;
                while ((Line = files.ReadLine()) != null)
                    l.Add(Line);

                return l;
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO AO ADQUIRIR INFORMAÇÕES DO JOGO!");
                else
                    ProgramData.MensagemErro("ERROR WHILE GETTING GAME INFORMATION!");

                // Envia um e-mail detalhado para nós sobre o problema
                Rede.ClassesMail.EnviaReport(" GAME: " + selectedGame.ToUpper() + " " + ex);

                return null;
            }
        }
        private void btnPlayGame_Click(object sender, EventArgs e)
        {
            // Abre o jogo
            OpenGame();

        }

        private void btnStore_Click(object sender, EventArgs e)
        {

            if (!Verificando)
            {
                // Cria a form principal de exibição para outra
                this.Hide();
                var form2 = new HUBR_SHOP();
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }
            else
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("HÁ UMA VERIFICAÇÃO/INSTALAÇÃO EM ANDAMENTO!\nPOR FAVOR, TENHA PACIÊNCIA!! A UGNITE AINDA É UMA BEBEZINHA E ESTÁ DANDO SEUS PRIMEIROS PASSOS,\nE POR ISSO HÁ ALGUMAS LIMITAÇÕES :(");
                else
                    ProgramData.MensagemErro("AN VERIFICATION/INSTALLTION PROCESS IS CURRENTLY ACTIVE!\nPLEASE, BE PATIENT!! UGNITE IS STILL A BABY AND IS GIVING ITS FIRST STEPS, AND THEREFORE THERE ARE SOME LIMITATIONS :(");
            }
        }
        private void btnActivateGame_Click(object sender, EventArgs e)
        {
            // Cria uma instância da janela de opções e exibe
            ActivateKey keyEnable = new ActivateKey();
            keyEnable.Show();
        }

        private void btnOptions_Click_1(object sender, EventArgs e)
        {

            // Cria uma instância da janela de opções e exibe
            Options opt = new Options();
            opt.ShowDialog();
            // Atualiza a imagem do usuário
            pbUserImage.Load(ProgramData.ImagemURL);
        }

        private void btnYourActivity_Click(object sender, EventArgs e)
        {
            if (!Verificando)
            {
                // Cria a form principal de exibição para outra
                this.Hide();
            var form2 = new HUBR_YourActivity();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
            }
            else
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("HÁ UMA VERIFICAÇÃO/INSTALAÇÃO EM ANDAMENTO!\nPOR FAVOR, TENHA PACIÊNCIA!! A UGNITE AINDA É UMA BEBEZINHA E ESTÁ DANDO SEUS PRIMEIROS PASSOS,\nE POR ISSO HÁ ALGUMAS LIMITAÇÕES :(");
                else
                    ProgramData.MensagemErro("AN VERIFICATION/INSTALLTION PROCESS IS CURRENTLY ACTIVE!\nPLEASE, BE PATIENT!! UGNITE IS STILL A BABY AND IS GIVING ITS FIRST STEPS, AND THEREFORE THERE ARE SOME LIMITATIONS :(");
            }
        }


        /// <summary>
        /// Desinstala o jogo
        /// </summary>
        private void btnDeleteGame_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se não está instalando/baixando o jogo
                if (!Verificando)
                {
                    // Verifica se a pasta do jogo existe
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Library\\Games\\" + selectedGame.ToUpper()))
                    {

                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            // Altera o texto do botão
                            btnDeleteGame.Text = "AGUARDE";
                        else
                            // Altera o texto do botão
                            btnDeleteGame.Text = "PLEASE\nWAIT";


                        // Mostra a tela do UIG
                        UGNITE_InstallGame UIG = new UGNITE_InstallGame
                        {
                            GameName = selectedGame,
                            Uninstall = true,
                            GLN = this
                        };

                        UIG.Show();

                        // Desativa esse botão lindo
                        btnDeleteGame.Visible = false;
                        btnDeleteGame.Enabled = false;
                        btnStartupRepair.Enabled = false;
                        btnStartupRepair.Visible = false;

                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            // Altera o texto do botão
                            btnDeleteGame.Text = "DESINSTALAR";
                        else
                            // Altera o texto do botão
                            btnDeleteGame.Text = "UNISTALL\nGAME";

                    }
                    else
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            // Exibir mensagem de erro
                            MessageBox.Show($"O jogo {selectedGame} não está instalado.", "Ugnite - Desinstalação de Jogos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        // Exibir mensagem de erro
                        MessageBox.Show($"The game {selectedGame} isn't installed.", "Ugnite - Game Uninstaller", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Exibir mensagem de erro
                        MessageBox.Show($"O jogo {selectedGame} está sendo configurado. Aguarde a finalização do processo.", "Ugnite - Desinstalação de Jogos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        // Exibir mensagem de erro
                        MessageBox.Show($"The game {selectedGame} is being configured. Wait until this process ends.", "Ugnite - Game Uninstaller", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                }
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO AO DESINSTALAR O JOGO!\nVERIFIQUE SE ELE NÃO ESTÁ SENDO EXECUTADO.");
                else
                    ProgramData.MensagemErro("ERROR WHILE UNINSTALLING THE GAME!\nVERIFY IF THE GAME IS OPEN.");


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
                lbGameName.ForeColor = Color.FromName(data["Textos"]["lbGameName"]);
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

        private void btnPlusPlans_Click(object sender, EventArgs e)
        {
            PlusPlans pp = new PlusPlans();
            pp.ShowDialog();
        }

        private void btnStartupRepair_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
            {
                // Altera o status do botão
                btnStartupRepair.Text = "ADQUIRINDO\nBASES";
                btnStartupRepair.Enabled = false;

                // Adquire os arquivos de execução do jogo
                Files = GetWebList("GameFiles");

                // Seta as váriaveis de onde o jogo vai rodar
                for (int wd = 0; wd < GetWebList("GameWorkingDirectories").Count; wd++)
                {
                    if (GetWebList("GameWorkingDirectories")[wd] == "*")
                        GetWebList("GameWorkingDirectories")[wd] = "";

                    WorkingDirectories.Add("\\" + GetWebList("GameWorkingDirectories")[wd]);
                }

                // Altera o status do botão
                btnStartupRepair.Text = "APLICANDO\nBASES";
                btnStartupRepair.Enabled = false;

                // Coloca o principal executável do arquivo no arquivo de configuração
                if (WorkingDirectories[0] != "")
                {
                    for (int d = 0; d < WorkingDirectories.Count; d++)
                    {
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame.ToUpper() + "\\cfg_EXE[" + d + "].UCFG", Files[d]);
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame.ToUpper() + "\\cfg_WD[" + d + "].UCFG", WorkingDirectories[d]);
                    }
                }
                else
                {
                    System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame.ToUpper() + "\\cfg_EXE[0].UCFG", Files[0]);
                    System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame.ToUpper() + "\\cfg_WD[0].UCFG", WorkingDirectories[0]);
                }

                // Mostra mensagem de sucesso
                MessageBox.Show("Reparo concluído.\nVerifique se o jogo inicializa normalmente pelo atalho na área de trabalho ou diretamente na Ugnite.\nSe persistir o erro contate o suporte: shop@ironiawn.com.br", "Ugnite - Reparos");

                // Altera o status do botão
                btnStartupRepair.Text = "REPARAR\nINICIALIZAÇÃO";
                btnStartupRepair.Enabled = true;
            }
            else
            {
                // Altera o status do botão
                btnStartupRepair.Text = "UPDATING\nBASES";
                btnStartupRepair.Enabled = false;

                // Adquire os arquivos de execução do jogo
                Files = GetWebList("GameFiles");

                // Seta as váriaveis de onde o jogo vai rodar
                for (int wd = 0; wd < GetWebList("GameWorkingDirectories").Count; wd++)
                {
                    if (GetWebList("GameWorkingDirectories")[wd] == "*")
                        GetWebList("GameWorkingDirectories")[wd] = "";

                    WorkingDirectories.Add("\\" + GetWebList("GameWorkingDirectories")[wd]);
                }

                // Altera o status do botão
                btnStartupRepair.Text = "APPLYING\nBASES";
                btnStartupRepair.Enabled = false;

                // Coloca o principal executável do arquivo no arquivo de configuração
                if (WorkingDirectories[0] != "")
                {
                    for (int d = 0; d < WorkingDirectories.Count; d++)
                    {
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame.ToUpper() + "\\cfg_EXE[" + d + "].UCFG", Files[d]);
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame.ToUpper() + "\\cfg_WD[" + d + "].UCFG", WorkingDirectories[d]);
                    }
                }
                else
                {
                    System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame.ToUpper() + "\\cfg_EXE[0].UCFG", Files[0]);
                    System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + selectedGame.ToUpper() + "\\cfg_WD[0].UCFG", WorkingDirectories[0]);
                }

                // Mostra mensagem de sucesso
                MessageBox.Show("Repair finished.\nVerify if the game initializes via desktop shortcut or directly via Ugnite.\nIf the error still persists, contact us: shop@ironiawn.com.br", "Ugnite - Startup Repair");

                // Altera o status do botão
                btnStartupRepair.Text = "STARTUP\nREPAIR";
                btnStartupRepair.Enabled = true;

            }
            // Reseta os status
            Files.Clear();
            WorkingDirectories.Clear();
        }

        private void btnAchievements_Click(object sender, EventArgs e)
        {
            // Cria a form principal de exibição para outra
            this.Hide();
            var form2 = new Janelas.Principais.UGNITE_Conquistas();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void TrackRating_Scroll(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
            {
                btnSendRating.Text = "VOTAR COM " + trackRating.Value + " DE 10";
            }
            else
            {
                btnSendRating.Text = "SEND WITH " + trackRating.Value + " OF 10";
            }
        }

        /// <summary>
        /// Vota no jogo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSendRating_Click(object sender, EventArgs e)
        {

            // Adquire o código do jogo
            string gCode = MySQL.RequestGameInfoByName(2, gameListBox.SelectedItem.ToString().ToUpper());//new WebClient().DownloadString("https://" + $"ugnitedata.s3.amazonaws.com/Pags/{gameListBox.SelectedItem.ToString().ToUpper()}/Code.HUBR@LOOPS");

            // Envia a nota que o usuário setou
            MySQL.SetGameRating(gCode, trackRating.Value.ToString());

            // Desativa o painel de votação e permite apenas a visualização da nota média
            RatingPanel.Size = new Size(273, 80);
            btnSendRating.Enabled = false;
            trackRating.Enabled = false;
            lbWichNote.Enabled = false;

            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Adquire a nota do jogo
                lbGeneralRating.Text = "NOTA DO JOGO : " + MySQL.GetGameRating(gCode);
            else
                // Adquire a nota do jogo
                lbGeneralRating.Text = "GAME RATING : " + MySQL.GetGameRating(gCode);
        }
    }
}
