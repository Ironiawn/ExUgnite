using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UGNITE
{
    static class Program
    {
        /// <summary>
        /// Adquire um texto dentro do arquivo de linguas
        /// </summary>
        /// <param name="Type">pt ou en</param>
        /// <param name="word">parte que quer ler</param>
        /// <returns></returns>
        public static string GetStringRM(string Type, string word)
        {
            string desired;
            ResourceManager rm = new ResourceManager("UGNITE." + Type, Assembly.GetExecutingAssembly());
            desired = rm.GetString(word);
            return desired;

        }
        /// <summary>
        /// Se o usuário é HUBR Plus ou não
        /// </summary>
        public static bool HUBRPlus = false;

        static void OnProcessExit(object sender, EventArgs e)
        {
            try
            {
                // Verifica se o usuário está ativo no ProgramData
                if (ProgramData.Username != "")
                    // Atualiza informações de último login
                    MySQL.UpdateInformation(12, DateTime.Parse(DateTime.Now.ToString(), new System.Globalization.CultureInfo("pt-BR", true)).ToString());
            }
            catch
            {
                // Nada, o programa está saindo.
            }
        }

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            #region LISTA DE JOGOS
            // Atualiza a lista de jogos(de acordo com a GameLibrary)
            //Sistemas.OpenGames.AvailableGames.Add("Penumbra Collection"); // 000 - 001 - 002
            //Sistemas.OpenGames.AvailableGames.Add("Minecraft JAVA Edition"); // 100
            //Sistemas.OpenGames.AvailableGames.Add("CrossCode"); // 200
            //Sistemas.OpenGames.AvailableGames.Add("Iris Fall"); // 300
            //Sistemas.OpenGames.AvailableGames.Add("Forest Siege"); // 400
            //Sistemas.OpenGames.AvailableGames.Add("GTA San Andreas"); // 500 e 501
            //Sistemas.OpenGames.AvailableGames.Add("SOMA"); // 600
            //Sistemas.OpenGames.AvailableGames.Add("OUTLAST 2"); // 700
            //Sistemas.OpenGames.AvailableGames.Add("SUPER MEAT BOY"); // 800
            //Sistemas.OpenGames.AvailableGames.Add("REMOTHERED: TORMENTED FATHERS"); // 900
            //Sistemas.OpenGames.AvailableGames.Add("GTA IV"); // 1000
            //Sistemas.OpenGames.AvailableGames.Add("CATHERINE CLASSIC"); // 1001
            #endregion

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory); //or set executing Assembly location path in param

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            #region SETA O REGISTRO PARA INICIALIZAÇÃO DO UPDATER
            File.WriteAllText("u.sys\\dir.cfg", AppDomain.CurrentDomain.BaseDirectory);
            #endregion

            /*
            // Lê o arquivo que contém os jogos                
            string[] Games = File.ReadAllLines(Application.StartupPath + @"\games.txt");
            for (int i = 0; i < Games.Length; i++)
                Sistemas.OpenGames.AvailableGames.Add(Games[i].ToUpper()); // Adiciona os jogos a lista de disponíveis

            */

            // Adquire as informações de inicialização
            if (Environment.CommandLine.ToLower().Contains("ugnite://rungame"))
            {

                // Verifica se a propriedade de salvar login está ativa
                if (Properties.Settings.Default["saveUser"].ToString() == "1")
                {
                    Application.Run(new Janelas.Principais.UGNITE_InitializeGame());
                    
                    /*
                    for (int i = 0; i < MySQL.AvailableGames("2").Count; i++)
                    {

                    System.Net.WebClient wba = new System.Net.WebClient();
                        //Stream srt = wba.OpenRead("https://" + $"ironiawn.com.br/HUBRX/GameData/{OpenGames.AvailableGames[i]}/GameCommandLine.txt");
                        Stream srt = wba.OpenRead("https://" + $"ugnitedata.s3.amazonaws.com/GameData/{MySQL.AvailableGames("2")[i]}/GameCommandLine.txt");
                        StreamReader srx = new StreamReader(srt);
                        string lx;
                        List<string> cmdLines = new List<string>();

                        while ((lx = srx.ReadLine()) != null)
                            cmdLines.Add(lx);

                        srx.Close();
                        srt.Close();


                        // Se conter APENAS uma linha, abrir o primeiro EXE
                        if (cmdLines.Count == 1)
                        {
                            // Se o código fornecido bate com o código de commandLine
                            if (Environment.CommandLine.ToLower().EndsWith("ugnite://rungameid/" + cmdLines[0]))
                            {
                                AbreJogo.GameCode = int.Parse(MySQL.RequestGameInfoByName(2, MySQL.AvailableGames("2")[i]));
                                AbreJogo.GameEXE = 0;
                                Application.Run(new AbreJogo());
                            }
                        }
                        else
                        {
                            // Percorre todos as linhas encontradas
                            for (int x = 0; x < cmdLines.Count; x++)
                            {
                                // Se o código fornecido bate com o código de commandLine
                                if (Environment.CommandLine.ToLower().EndsWith("ugnite://rungameid/" + cmdLines[x]))
                                {
                                    // Verifica qual executável será inicializado
                                    AbreJogo.GameCode = int.Parse(MySQL.RequestGameInfoByName(2, MySQL.AvailableGames("2")[i]));
                                    AbreJogo.GameEXE = x;
                                    Application.Run(new AbreJogo());
                                }
                            }
                        }

                    }
                    */
                }
                else
                {
                    if (Properties.Settings.Default["lang"].ToString() == "en")
                        ProgramData.MensagemErro("TO USE THIS FUNCTION GO TO OPTIONS > SAVE LOGIN INFO. AND TRY AGAIN.");
                    else
                        ProgramData.MensagemErro("PARA EXECUTAR ESSA FUNÇÃO, VÁ EM OPÇÕES > SALVAR LOGIN E TENTE NOVAMENTE.");

                    Application.Exit();
                }
            }
            else
            {
                // Inicia o processo do updater
                System.Diagnostics.Process.Start(Environment.CurrentDirectory + @"\u.sys\updater.exe");

                OpenGames.AvailableGames = MySQL.AvailableGames("2");

                // Abre o aplicativo
                Application.Run(new HomeUsuario());
            }

        }
       
    }
}
