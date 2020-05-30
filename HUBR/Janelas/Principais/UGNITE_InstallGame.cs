using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UGNITE
{
    public partial class UGNITE_InstallGame : Form
    {
        public string GameName;
        public HUBR_GameLibraryNew GLN;
        #region VÁRIAVEIS DE INSTALAÇÃO
        // DETALHES DA INSTALAÇÃO DOS JOGOS [OBRIGATÓRIAS]
        public List<string> Files = new List<string>(); // Lista de arquivos principais que deverão ser verificados
        public List<string> Shortcuts = new List<string>(); // Lista de atalhos que vão ser criados na area de trabalho
        public List<string> ShortcutsIcon = new List<string>(); // Lista dos arquivos de icone dos atalhos na area de trabalho
        public List<string> WorkingDirectories = new List<string>(); // Lista dos diretórios onde os executáveis serão rodados
        public List<string> CommandLine = new List<string>(); // Lista da lista de argumentos para iniciar o HUBR via Desktop
        /// <summary>
        /// Cria uma lista temporária para armazenar os dados de instalação dos arquivos
        /// </summary>
        List<string> GL = new List<string>();
        #endregion

        public UGNITE_InstallGame()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Lista de arquivos que vão ficar escondidos no exibidor e nos arquivos
        /// </summary>
        List<string> HiddenFiles = new List<string>();

        /// <summary>
        /// Arquivo para aplicar ações
        /// </summary>
        string FileToHide = "";

        #region COMPONENTES E FUNÇÕES DE DOWNLOAD
        Stopwatch sw = new Stopwatch();    // The stopwatch which we will be using to calculate the download speed
        int IndexCount; // Arquivos baixados
        List<string> fileCounting = new List<string>(); // Arquivos totais

        public List<string> GetWebList(string filename)
        {
            List<string> l = new List<string>();

            //Stream data = new WebClient().OpenRead("https://" + $"ironiawn.com.br/HUBRX/GameData/{GameName}/{filename}.txt");
            Stream data = new WebClient().OpenRead("https://" + $"ugnitedata.s3.amazonaws.com/GameData/{GameName}/{filename}.txt");
            StreamReader files = new StreamReader(data);
            string Line;
            while ((Line = files.ReadLine()) != null)
                l.Add(Line);

            return l;

        }
        // The event that will trigger when the WebClient is completed
        void Completed(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled == true)
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        ProgramData.MensagemErro("DOWNLOAD DO JOGO " + GameName + " CANCELADO PROPOSITALMENTE OU POR UM ERRO.");
                    else
                        ProgramData.MensagemErro("THE DOWNLOAD OF " + GameName + " WAS STOPPED BY AN ERROR OR SOMETHING UNKNOWN HAPPENED.");

                }
                else
                {
                    // Verifica se não é o último arquivo
                    if (IndexCount <= fileCounting.Count - 1 && fileCounting[IndexCount] != "*")
                    {

                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            lbCurServer.Text += $"\nINSTALANDO ARQUIVO {IndexCount}";
                        else
                            lbCurServer.Text += $"\nINSTALLING FILE {IndexCount}";


                        IndexCount++;
                        // Verifica se não há arquivos para aplicação de restrição
                        if (FileToHide != "")
                            HideFile(FileToHide);

                        // Reinicia a operação de download
                        InstallFragmented();
                    }
                    else
                        InstallFragmented();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Close();
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
                pbDownloadProgress.Value = (int)porcentagem;

                // Ativa o texto de progresso
                lbProgressDetails.Enabled = true;

                if (Properties.Settings.Default["lang"].ToString() != "en")
                {

                    //pbDownloadProgress.Style = ProgressBarStyle.Marquee;
                    if ((e.TotalBytesToReceive / 1024d / 1024d) >= 1)
                        lbProgressDetails.Text = "V/D: " + MB((e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds)).ToString("0.00") + $"MB/s\n{porcentagem}% CONCLUÍDO | RESTANTE : {restante.ToString("0")}MB";
                    else
                        lbProgressDetails.Text = "V/D: " + MB((e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds)).ToString("0.00") + $"MB/s\n{porcentagem}% CONCLUÍDO";
                }
                else
                {
                    //pbDownloadProgress.Style = ProgressBarStyle.Marquee;
                    if ((e.TotalBytesToReceive / 1024d / 1024d) >= 1)
                        lbProgressDetails.Text = "D/S: " + MB((e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds)).ToString("0.00") + $"MB/s\n{porcentagem}% COMPLETED | REMAINING : {restante.ToString("0")}MB";
                    else
                        lbProgressDetails.Text = "D/S: " + MB((e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds)).ToString("0.00") + $"MB/s\n{porcentagem}% COMPLETED";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        /// <summary>
        /// "Esconde" um arquivo
        /// </summary>
        /// <param name="File"></param>
        void HideFile(string File)
        {
            for (int i = 0; i < HiddenFiles.Count; i++)
            {
                if (new FileInfo(File).FullName.ToLower().Contains(HiddenFiles[i].ToLower()))
                    new FileInfo(File).Attributes = FileAttributes.Hidden;
            }
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
                    writer.WriteLine("IconFile=" + Application.StartupPath + @"\Library\Games\" + GameName + "\\" + GameIconShortcut);
                    writer.Flush();
                }
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO AO CRIAR ATALHOS PARA O JOGO.\nCÓDIGO 162");
                else
                    ProgramData.MensagemErro("ERROR WHILE CREATING DESKTOP SHORTCUTS FOR THE GAME.\nCODE 214");

            }
        }

        void InstallFragmented()
        {
            try
            {
                // Adiciona os arquivos em uma lista
                //string[] fileList = System.IO.File.ReadAllLines(Application.StartupPath + @"\GameData\\" + GameName + "\\gameFilesServer.txt");
                List<string> fileList = new List<string>();

                Stream data = new WebClient().OpenRead("https://ugnitedata.s3.amazonaws.com/GameData/" + GameName + "/gameFilesServer.txt");
                StreamReader files = new StreamReader(data);
                string Line;
                while ((Line = files.ReadLine()) != null)
                    fileList.Add(Line);

                /*
                try
                {
                    new WebClient().DownloadString("https://" + $"ironiawn.com.br/HUBRX/GameData/{GameName}/HUBR.txt");
                    Ironiawn = true;
                }
                catch
                {
                    Ironiawn = false;
                }
                */

                // Faz o download de arquivos, um por um.
                fileCounting = fileList;

                if (IndexCount <= fileCounting.Count - 1 && fileCounting[IndexCount] != "*")
                {
                    // Verifica qual o arquivo que está a ser baixado
                    var arquivo = Application.StartupPath + @"\Library\Games\" + GameName + "\\" + fileList[IndexCount];


                    // Verifica se o diretório em que o arquivo vai existe
                    if (!new FileInfo(arquivo).Directory.Exists)
                        new FileInfo(arquivo).Directory.Create();

                    // Verifica se é para "esconder" o arquivo
                    for (int i = 0; i < HiddenFiles.Count; i++)
                    {
                        if (new FileInfo(arquivo).Name.ToLower().Contains(HiddenFiles[i]))
                            FileToHide = arquivo;
                    }

                    // Se o arquivo já existir, pular
                    if (File.Exists(arquivo))
                    {
                        IndexCount++;
                        InstallFragmented();
                    }

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Exibe o arquivo atual / contagem de arquivos totais
                        lbCurServer.Text = $"ARQUIVO ATUAL : {IndexCount}\nTOTAL: {fileList.Count - 1} ARQUIVOS";
                    else
                        // Exibe o arquivo atual / contagem de arquivos totais
                        lbCurServer.Text = $"ACTUAL FILE : {IndexCount}\nTOTAL : {fileList.Count - 1} FILES";


                    // Start the stopwatch which we will be using to calculate the download speed
                    sw.Restart();

                    using (WebClient wb = new WebClient())
                    {
                        // Ativa texto de detalhes e barra de progresso
                        lbProgressDetails.Visible = true;
                        pbDownloadProgress.Visible = true;


                        // Ativa o texto de servidor
                        lbCurServer.Visible = true;

                        wb.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                        // Cria uma instância URI de download
                        Uri URL;

                        // Verifica se o país do usuário é Brasil, antes de continuar.
                        // Se não for Brasil, mudar para o S3 TA
                        if (ProgramData.GetMachineCurrentLocation().ToLower() == "brasil")
                            // The variable that will be holding the url address (making sure it starts with http://)
                            URL = new Uri("https://ugnitegames.s3.amazonaws.com/" + GameName + "/" + fileList[IndexCount]);
                        else
                            // The variable that will be holding the url address (making sure it starts with http://)
                            URL = new Uri("https://ugnitegames.s3-accelerate.amazonaws.com/" + GameName + "/" + fileList[IndexCount]);



                        // Start downloading the file
                        wb.DownloadFileAsync(URL, Application.StartupPath + @"\Library\Games\" + GameName + "\\" + fileList[IndexCount]);

                    }
                }
                else
                {
                    // Desativa a barra de progresso
                    pbDownloadProgress.Visible = false;

                    // Coloca o principal executável do arquivo no arquivo de configuração
                    if (WorkingDirectories[0] != "")
                    {
                        for (int d = 0; d < WorkingDirectories.Count; d++)
                        {
                            System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + GameName + "\\cfg_EXE[" + d + "].UCFG", Files[d]);
                            System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + GameName + "\\cfg_WD[" + d + "].UCFG", WorkingDirectories[d]);
                        }
                    }
                    else
                    {
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + GameName + "\\cfg_EXE[0].UCFG", Files[0]);
                        System.IO.File.WriteAllText(Application.StartupPath + @"\Library\Games\" + GameName + "\\cfg_WD[0].UCFG", WorkingDirectories[0]);
                    }

                    // Cria os atalhos de acordo com a quantidade
                    for (int x = 0; x < Shortcuts.Count; x++)
                    {
                        CreateShortcut(Shortcuts[x], CommandLine[x], ShortcutsIcon[x]);
                    }

                    // Desativa o texto de servidor
                    lbCurServer.Visible = false;

                    // Reseta as funções de download
                    IndexCount = 0;
                    fileCounting = null;

                    // Verifica se contém mais de um executável
                    if (Files.Count > 1)
                        GLN.MoreThanOneEXE = true;



                    if (Properties.Settings.Default["lang"].ToString() != "en")
                    {
                        // Muda o texto de download
                        lbProgressDetails.Text = "CONFIGURAÇÕES PÓS-DOWNLOAD..";

                        // Reseta configurações
                        GLN.Verificando = false;
                        GLN.btnPlayGame.Text = "JOGAR";
                        GLN.btnPlayGame.Enabled = true;
                        GLN.BadInstall = false;
                        GLN.gameListBox.Enabled = true;
                    }
                    else
                    {
                        // Muda o texto de download
                        lbProgressDetails.Text = "POS-DOWNLOAD SETUPS..";

                        // Reseta configurações
                        GLN.Verificando = false;
                        GLN.btnPlayGame.Text = "PLAY GAME";
                        GLN.btnPlayGame.Enabled = true;
                        GLN.BadInstall = false;
                        GLN.gameListBox.Enabled = true;
                    }



                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Notifica começo de instalação
                        ProgramData.NotificaUsuario("CONFIG", $"Jogo {GameName} instalado!", "Ugnite - Instalação de Jogos", 3000);
                    else
                        // Notifica começo de instalação
                        ProgramData.NotificaUsuario("CONFIG", $"The game {GameName} was installed!", "Ugnite - Game Installer", 3000);


                    // Fecha o diálogo
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO AO INSTALAR O JOGO!\nENTRE EM CONTATO COM O SUPORTE UGNITE EM WWW.SUPPORT.IRONIAWN.COM.BR");
                else
                    ProgramData.MensagemErro("ERROR WHILE INSTALLING THE GAME!\nCONTACT UGNITE SUPPORT AT WWW.SUPPORT.IRONIAWN.COM.BR");

                // Envia mensagem para nós sobre o erro, detalhando o jogo
                Rede.ClassesMail.EnviaReport(" GAME: " + GameName + $"\n {ex}");
            }
        }

        /// <summary>
        /// Cria o diretório de instalação do jogo, se não existir.
        /// </summary>
        /// <param name="Name"></param>
        void VerifyGameDirectory(string Name)
        {
            // A pasta a ser verificada
            string dir = Directory.GetCurrentDirectory() + "\\Library\\Games\\" + Name;

            // Verifica a pasta
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        void ApplyTranslation()
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                lbWarning.Text = Program.GetStringRM("en", "lbWarning"); // Texto de atenção
                btnLibrary.Text= Program.GetStringRM("en", "btnLibrary0"); // Texto de atenção
            }
        }

        private void UGNITE_InstallGame_Load(object sender, EventArgs e)
        {
            string msg = "";
            try
            {
                // Seta o nome do jogo na label
                lbGameName.Text = GameName;

                // Seta a imagem do jogo selecionado
                //GameImage.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\GameData\\" + gameListBox.SelectedItem.ToString().ToUpper() + "\\GameImage.hubr");
                //GameImage.Load("https://" + $"ironiawn.com.br/HUBRX/GameData/{GameName.ToUpper()}/GameImage.hubr");
                string GI = MySQL.RequestGameInfoByName(11, GameName.ToUpper());
                GameImage.Load(GI);

                ApplyTranslation();


                #region BLOQUEIO DE ARQUIVO
                HiddenFiles.Add("cdx");
                HiddenFiles.Add("steam");
                HiddenFiles.Add("3dm");
                HiddenFiles.Add("codex");
                HiddenFiles.Add("elamigos");
                HiddenFiles.Add("cpy");
                #endregion

                // Verifica se é para instalar jogo (padrão sim
                if (!Uninstall)
                {
                    
                    // Atualiza informações sobre o jogo na base de dados
                    MySQL.UpdateGameDetailsDB(GameName);

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Muda o texto de download
                        lbProgressDetails.Text = "CONFIGURAÇÕES DE INSTALAÇÃO";
                    else
                        // Muda o texto de download
                        lbProgressDetails.Text = "INSTALLATION SETTINGS";


                    // Configura as váriaveis requeridas
                    CommandLine = GetWebList("GameCommandLine");
                    msg += "gamecommandline\n";

                    // Seta as váriaveis de onde o jogo vai rodar
                    for (int wd = 0; wd < GetWebList("GameWorkingDirectories").Count; wd++)
                    {
                        if (GetWebList("GameWorkingDirectories")[wd] == "*")
                            GetWebList("GameWorkingDirectories")[wd] = "";

                        WorkingDirectories.Add("\\" + GetWebList("GameWorkingDirectories")[wd]);
                    }
                    msg += "workingdirectories\n";

                    // Seta os atalhos do jogo
                    Shortcuts = GetWebList("GameShortcuts");
                    msg += "shortcuts\n";

                    // Seta o icone dos atalhos do jogo
                    ShortcutsIcon = GetWebList("GameIcons");
                    msg += "icons\n";

                    // Seta os arquivos do jogo
                    Files = GetWebList("GameFiles");
                    msg += "files\n";

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Muda o texto de download
                        lbProgressDetails.Text = "CONFIGURAÇÕES DE SERVIDOR";
                    else
                        // Muda o texto de download
                        lbProgressDetails.Text = "SERVER SETUPS";


                    // Verifica a existência da pasta de um jogo
                    VerifyGameDirectory(GameName);

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Notifica começo de instalação
                        ProgramData.NotificaUsuario("CONFIG", $"Iniciando instalação do jogo {GameName}.", "Ugnite - Instalação de Jogos", 3000);
                    else
                        // Notifica começo de instalação
                        ProgramData.NotificaUsuario("CONFIG", $"Starting game installation {GameName}.", "Ugnite - Game Installer", 3000);

                    // Instala o jogo
                    InstallFragmented();
                }
                else
                {
                    // Altera propriedades
                    pbDownloadProgress.Visible = false;
                    lbCurServer.Visible = false;
                    this.Size = new Size(866, 238);

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Altera textos
                        lbWarning.Text = "AGUARDE ENQUANTO A UGNITE DESINSTALA O JOGO";
                    else
                        // Altera textos
                        lbWarning.Text = "PLEASE WAIT WHILE UGNITE UNINSTALLS THE GAME";


                    // Executa a função de desinstalar
                    UninstallGame();

                }
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO AO ADQUIRIR INFORMAÇÕES WEB DO JOGO!");
                else
                    ProgramData.MensagemErro("ERROR WHILE GETTING WEB GAME INFORMATION!");

                // Envia um e-mail detalhado para nós sobre o problema
                Rede.ClassesMail.EnviaReport(" GAME: " + GameName.ToUpper() + " " + ex + " | " + msg);


                // Fecha a Ugnite
                Application.Exit();
            }
        }

        #region OPÇÕES PARA DESINSTALAÇÃO
        public bool Uninstall = false;
        /// <summary>
        /// Desinstala o jogo selecionado
        /// </summary>
        void UninstallGame()
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o progresso
                lbProgressDetails.Text = "DELETANDO ARQUIVOS DO JOGO";
            else
                // Muda o progresso
                lbProgressDetails.Text = "DELETING GAME FILES";


            // Deleta a pasta do jogo
            Directory.Delete(Environment.CurrentDirectory + @"\Library\Games\" + GameName, true);

            // Adquire os atalhos                    
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Adquire o Desktop do usuário

            // Deletar os arquivos de atalho
            for (int i = 0; i < GetWebList("GameShortcuts").Count; i++)
            {
                if (System.IO.File.Exists(Path.Combine(desktopPath, GetWebList("GameShortcuts")[i] + ".lnk")))
                    System.IO.File.Delete(Path.Combine(desktopPath, GetWebList("GameShortcuts")[i] + ".lnk"));
            }

            // Reseta configurações
            Uninstall = false;

            if (Properties.Settings.Default["lang"].ToString() != "en")
            {
                // Exibir mensagem de confirmação
                MessageBox.Show($"O jogo {GameName} foi desinstalado.", "Ugnite - Desinstalação de Jogos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GLN.btnPlayGame.Text = "INSTALAR";
            }
            else
            {
                // Exibir mensagem de confirmação
                MessageBox.Show($"The game {GameName} was uninstalled.", "Ugnite - Game Uninstaller", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GLN.btnPlayGame.Text = "INSTALL";
            }



            // Fecha a janela
            Close();
        }

        #endregion
    }
}
