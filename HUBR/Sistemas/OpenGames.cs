using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace UGNITE
{
    public static class OpenGames
    {
        public static List<string> AvailableGames = new List<string>();
        #region REGIÃO PARA COMPRA/ABERTURA DE JOGO
        public static string GameBuyURL;
        public static int GameRunCode;
        public static bool HasGame;
        public static string LastSeenGame = null;
        #endregion

        /// <summary>
        /// string de conexão MySQL
        /// </summary>
        public static string conString;

        /// <summary>
        /// Abre um jogo por CÓDIGO
        /// </summary>
        /// <param name="GameCode">CÓDIGO DO JOGO DISPONÍVEL NA GameLibrary [AVAILABLEGAMES]</param>
        public static void OpenGame(int GameCode, int ExeNum)
        {
            // Verifica se o arqivo de configuração existe
            if (File.Exists(Application.StartupPath + @"\Library\Games\" + AvailableGames[GameCode] + "\\cfg_EXE[" + ExeNum + "].UCFG")
                && File.Exists(Application.StartupPath + @"\Library\Games\" + AvailableGames[GameCode] + "\\cfg_WD[" + ExeNum + "].UCFG"))
            {
                // Lê o arquivo de configuração do jogo
                string readEXE = File.ReadAllText(Application.StartupPath + @"\Library\Games\" + AvailableGames[GameCode] + "\\cfg_EXE[" + ExeNum + "].UCFG");
                string readWD = File.ReadAllText(Application.StartupPath + @"\Library\Games\" + AvailableGames[GameCode] + "\\cfg_WD[" + ExeNum + "].UCFG");

                // Verifica se o jogo possui utilização da API para adicionar argumentos de inicialização
                if (MySQL.RequestGameInfoByName(15, AvailableGames[GameCode].ToUpper()) == "1")
                {
                    // Define as váriaveis para inicialização do executável
                    var psi = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + @"\Library\\Games\\" + AvailableGames[GameCode] + readEXE)
                    {
                        Arguments = Encryptor.Encrypt(conString + "$" + ProgramData.Username, "VAYNE_HUBER_UGNITE_IRONIAWNSA"),
                        WorkingDirectory = Application.StartupPath + @"\Library\Games\" + AvailableGames[GameCode] + readWD.Replace("*", "") // Define o diretório que o executável irá trabalhar

                    }; // Define o executável

                    System.Diagnostics.Process.Start(psi); // Abre o jogo
                }
                else
                {
                    // Define as váriaveis para inicialização do executável
                    var psi = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + @"\Library\\Games\\" + AvailableGames[GameCode] + readEXE)
                    {                        
                        WorkingDirectory = Application.StartupPath + @"\Library\Games\" + AvailableGames[GameCode] + readWD.Replace("*", "") // Define o diretório que o executável irá trabalhar

                    }; // Define o executável

                    System.Diagnostics.Process.Start(psi); // Abre o jogo
                }

            }
            else
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Mostra uma mensagem ao usuário, de que o jogo não está instalado corretamente.
                    ProgramData.MensagemErro("ERRO AO EXECUTAR " + AvailableGames[GameCode].ToUpper() + ".\n\nREINSTALE O JOGO\n\nERRO : [BADINSTALL_CONFIGFILE]");
                else
                    // Mostra uma mensagem ao usuário, de que o jogo não está instalado corretamente.
                    ProgramData.MensagemErro("ERROR WHILE OPENING " + AvailableGames[GameCode].ToUpper() + ".\n\nREINSTALL THE GAME\n\nERROR : [BADINSTALL_CONFIGFILE]");


            }
        }
        /// <summary>
        /// Abre um jogo por NOME
        /// </summary>
        /// <param name="GameCode">CÓDIGO DO JOGO DISPONÍVEL NA GameLibrary [AVAILABLEGAMES]</param>
        public static void OpenGame(string GameName, int ExeNum)
        {
            // Verifica se o arqivo de configuração existe
            if (File.Exists(Application.StartupPath + @"\Library\Games\" + GameName + "\\cfg_EXE[" + ExeNum + "].UCFG")
                && File.Exists(Application.StartupPath + @"\Library\Games\" + GameName + "\\cfg_WD[" + ExeNum + "].UCFG"))
            {
                // Lê o arquivo de configuração do jogo
                string readEXE = File.ReadAllText(Application.StartupPath + @"\Library\Games\" + GameName + "\\cfg_EXE[" + ExeNum + "].UCFG");
                string readWD = File.ReadAllText(Application.StartupPath + @"\Library\\Games\" + GameName + "\\cfg_WD[" + ExeNum + "].UCFG");

                // Verifica se o jogo possui utilização da API para adicionar argumentos de inicialização
                if (MySQL.RequestGameInfoByName(15, GameName.ToUpper()) == "1")
                {
                    // Define as váriaveis para inicialização do executável
                    var psi = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + @"\Library\Games\\" + GameName + readEXE)
                    {
                        Arguments = Encryptor.Encrypt(conString + "$" + ProgramData.Username, "VAYNE_HUBER_UGNITE_IRONIAWNSA"),
                        WorkingDirectory = Application.StartupPath + @"\Library\Games\" + GameName + readWD.Replace("*", "") // Define o diretório que o executável irá trabalhar
                    };
                    System.Diagnostics.Process.Start(psi); // Abre o jogo
                }
                else
                {
                    // Define as váriaveis para inicialização do executável
                    var psi = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + @"\Library\Games\\" + GameName + readEXE)
                    {                        
                        WorkingDirectory = Application.StartupPath + @"\Library\Games\" + GameName + readWD.Replace("*", "") // Define o diretório que o executável irá trabalhar
                    };
                    System.Diagnostics.Process.Start(psi); // Abre o jogo

                }
            }
            else
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Mostra uma mensagem ao usuário, de que o jogo não está instalado corretamente.
                    ProgramData.MensagemErro("ERRO AO EXECUTAR " + GameName.ToUpper() + ".\n\nREINSTALE O JOGO\n\nERRO : [BADINSTALL_CONFIGFILE]");
                else
                    // Mostra uma mensagem ao usuário, de que o jogo não está instalado corretamente.
                    ProgramData.MensagemErro("ERROR WHILE OPENING " + GameName.ToUpper() + ".\n\nREINSTALL THE GAME\n\nERROR : [BADINSTALL_CONFIGFILE]");


            }
        }
    }
}
