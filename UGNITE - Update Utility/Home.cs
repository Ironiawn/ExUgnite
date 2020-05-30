using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using SharpCompress.Readers;
using SharpCompress.Common;

namespace UGNITE___Update_Utility 
{
    public partial class Home : Form
    {
        /// <summary>
        /// Versão Ugnite
        /// </summary>
        string UgVers = "";

        /// <summary>
        /// Versão atual online da Ugnite
        /// </summary>
        string UgVersWeb = "";

        WebClient webClient;               // Our WebClient that will be doing the downloading for us
        readonly Stopwatch sw = new Stopwatch();    // The stopwatch which we will be using to calculate the download speed

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {

                // Lê a versão atual da Ugnite
                UgVers = File.ReadAllText("versao.txt");

                // Atualiza texto de status
                lbStatusUpdate.Text = "Checking actual Ugnite version...";

                // Seta a versão do Updater
                lbUpdaterVersion.Text = $"U.G.U.U@{ProductVersion}";

                // Lê a versão atual no servidor
                UgVersWeb = new WebClient().DownloadString("https://ironiawn.com.br/HUBRX/versao.txt");

                // Se a versão atual não for igual, atualizar status e baixar.
                if (UgVers != UgVersWeb)
                {
                    // Atualiza texto de status
                    lbStatusUpdate.Text = "Downloading Ugnite Client update...";

                    // Finaliza a Ugnite se existir o processo em andamento
                    try
                    {
                        Process[] proc = Process.GetProcessesByName("UGNITE");
                        proc[0].Kill();
                    }
                    catch
                    {

                    }

                    // Faz o download do arquivo zip
                    DownloadFile("https://ironiawn.com.br/HUBRX/update.rar", "UgUpdate_" + UgVersWeb + ".rar");
                }
                else
                {
                    // Sair do updater, já que não há atualização disponívelç
                    DestroiUpdate();
                    Application.Exit();
                }
            }
            catch(Exception ex)
            {
                File.WriteAllText("erro.log", ex.ToString());
                DestroiUpdate();
                Application.Exit();
            }
        }
        public void DownloadFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // The variable that will be holding the url address (making sure it starts with http://)
                Uri URL = urlAddress.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("https://" + urlAddress);

                // Start the stopwatch which we will be using to calculate the download speed
                sw.Start();

                try
                {
                    // Start downloading the file
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    DestroiUpdate();
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Destrói último arquivo de atualização para evitar erros
        /// </summary>
        void DestroiUpdate()
        {
            // Verifica se o arquivo de atualização existe
            if (File.Exists("UgUpdate_" + UgVersWeb + ".rar"))
                File.Delete("UgUpdate_" + UgVersWeb + ".rar");
        }

        // The event that will fire whenever the progress of the WebClient is changed
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            // Update the progressbar percentage only when the value is not the same.
            ProgressBar.Value = e.ProgressPercentage;

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading
            lbStatusUpdate.Text = string.Format("Downloading update.. {0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        /// <summary>
        /// Extrai a atualização
        /// </summary>
        void ExtrairAtualizacao()
        {
            try
            {
                using (Stream stream = File.OpenRead("UgUpdate_" + UgVersWeb + ".rar"))
                {
                    var reader = ReaderFactory.Open(stream);
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            string dir = File.ReadAllText("dir.cfg"); // Lê o diretório onde a Ugnite está
                            reader.WriteEntryToDirectory(dir, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("erro.log", ex.ToString());
                DestroiUpdate();
                Application.Exit();
            }
        }

        // The event that will trigger when the WebClient is completed
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Reset the stopwatch.
            sw.Reset();

            if (e.Cancelled == true)
            {
                MessageBox.Show("Download has been canceled.");
                DestroiUpdate();
                Application.Exit();
            }
            else
            {
                // Instala a atualização
                ExtrairAtualizacao();

                // Atualiza texto de ações
                lbStatusUpdate.Text = "Update download complete!\nPlease wait while we check the file..";

                // Inicia a Ugnite
                IniciaUgnite();
            }
        }

        void IniciaUgnite()
        {
            // Atualiza texto de status
            lbStatusUpdate.Text = "Initializing Ugnite..";

            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                try
                {
                        // Abre a Ugnite, após a atualização
                        string dir = File.ReadAllText("dir.cfg"); // Lê o diretório onde a Ugnite está
                        System.Diagnostics.Process.Start(dir + @"\UGNITE.exe");
                        // Fecha o updater
                        timer.Dispose();
                }
                catch (Exception ex)
                {
                    File.WriteAllText("erro.log", ex.ToString());
                    DestroiUpdate();
                    Application.Exit();

                }
                DestroiUpdate();
                Application.Exit();
            },
                        null, 15000, System.Threading.Timeout.Infinite);

        }

    }
}
