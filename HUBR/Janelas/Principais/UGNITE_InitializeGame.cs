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
namespace UGNITE.Janelas.Principais
{
    public partial class UGNITE_InitializeGame : Form
    {
        public UGNITE_InitializeGame()
        {
            InitializeComponent();
            OpenGames.AvailableGames = MySQL.AvailableGames("2");
        }



        private void UGNITE_InitializeGame_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default["lang"].ToString() == "en")
                lbWait.Text = "PLEASE WAIT WHILE WE SET UP THE\nUGNITE FOR YOUR GAME!";


            for (int i = 0; i < OpenGames.AvailableGames.Count; i++)
            {
                System.Net.WebClient wba = new System.Net.WebClient();
                //Stream srt = wba.OpenRead("https://" + $"ironiawn.com.br/HUBRX/GameData/{OpenGames.AvailableGames[i]}/GameCommandLine.txt");
                //wba.DownloadFile("https://s3-sa-east-1.amazonaws.com/ugnitedata/GameData/GTA SAN ANDREAS/GameCommandLine.txt", Path.GetTempPath() + "\\gcl.ug");

                Stream srt = wba.OpenRead("https://s3-sa-east-1.amazonaws.com/ugnitedata/GameData/" + OpenGames.AvailableGames[i].ToUpper() + "/GameCommandLine.txt");
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
                        AbreJogo.GameCode = i;
                        AbreJogo.GameEXE = 0;
                        AbreJogo.GameRunCode = GRC(cmdLines[0]);
                        // Cria a form principal de exibição para outra
                        this.Hide();
                        var form2 = new AbreJogo();
                        form2.Closed += (s, args) => this.Close();
                        form2.Show();                        
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
                            AbreJogo.GameCode = i;
                            AbreJogo.GameEXE = x;
                            AbreJogo.GameRunCode = GRC(cmdLines[0]);
                            // Cria a form principal de exibição para outra
                            this.Hide();
                            // Cria a form principal de exibição para outra
                            var form2 = new AbreJogo();
                            form2.Closed += (s, args) => this.Close();
                            form2.Show();
                        }
                    }
                }

                //Application.Exit();
            }
        }

        /// <summary>
        /// Volta o GameRunCode
        /// </summary>
        /// <param name="cmdLine">Código do jogo pela linha de comando</param>
        /// <returns></returns>
        int GRC(string cmdLine)
        {
            if (cmdLine.Length == 3)
                return int.Parse(cmdLine.Substring(0, 1));
            else
                return int.Parse(cmdLine.Substring(0, cmdLine.Length - 2));
        }
    }
}
