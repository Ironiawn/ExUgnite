using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace UGNITE
{
    public static class ProgramData
    {
        /// <summary>
        /// Usuário ativo na sessão atual
        /// </summary>
        public static string Username;

        /// <summary>
        /// Email do usuário ativo na sessão atual
        /// </summary>
        public static string Mail;

        /// <summary>
        /// Imagem do usuário
        /// </summary>
        public static string ImagemURL;

        /// <summary>
        /// NOTIFICA O USUÁRIO SOBRE ALGO
        /// </summary>
        /// <param name="Tipo">TIPO [DE ACORDO COM O ARQUIVO]</param>
        /// <param name="Mensagem">MENSAGEM DE EXIBIÇÃO</param>
        /// <param name="Titulo">TÍTULO MENSAGEM</param>
        /// <param name="Tempo">TEMPO DE EXIBIÇÃO</param>
        public static void NotificaUsuario(string Tipo, string Mensagem, string Titulo, int Tempo)
        {
            NotifyIcon ni = new NotifyIcon
            {
                BalloonTipText = Mensagem,
                Visible = true,
                Icon = new Icon(Environment.CurrentDirectory + "\\Temas\\" + Tipo + ".ico"),
                BalloonTipTitle = Titulo
            };
            ni.ShowBalloonTip(Tempo);

        }

        /// <summary>
        /// [APENAS PARA CASOS SEM ERROS]
        /// Exibe uma form em forma de dialogo para o usuário
        /// </summary>
        /// <param name="Mensagem">Mensagem para ser exibida</param>
        public static void MensagemSucesso(string Mensagem)
        {
            /*
            // Seta o texto da mensagem
            U_Mensagem.DialogoMensagem = Mensagem;

            // Cria uma form
            U_Mensagem msgForm = new U_Mensagem();


            // Exibe a form
            msgForm.ShowDialog();
            */

            MessageBox.Show(Mensagem, "UGNITE", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        /// <summary>
        /// [APENAS PARA CASOS SEM ERROS]
        /// Exibe uma form em forma de dialogo para o usuário
        /// </summary>
        /// <param name="Mensagem">Mensagem para ser exibida</param>
        public static void MensagemSucesso(string Mensagem, int Tipo = 1)
        {
            /*
            // Seta a mensagem a ser exibida
            U_Mensagem.DialogoMensagem = Mensagem;

            // Seta o tipo para exibir scroll
            U_Mensagem.TipoScroll = Tipo;

            // Cria uma form
            U_Mensagem msgForm = new U_Mensagem();

            // Exibe a form
            msgForm.ShowDialog();
            */

            MessageBox.Show(Mensagem, "UGNITE", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        /// <summary>
        /// [APENAS PARA CASOS DE ERRO]
        /// Exibe uma form em forma de diálogo para o usuário
        /// </summary>
        /// <param name="Mensagem"></param>
        public static void MensagemErro(string Mensagem)
        {
            /*
            // Seta a mensagem a ser exibida
            ExibeErro.DialogoMensagem = Mensagem;

            // Cria uma form
            ExibeErro msgForm = new ExibeErro();

            // Exibe a form
            msgForm.ShowDialog();
            */

            MessageBox.Show(Mensagem, "UGNITE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void EnviaMail(string Mensagem, string Destino, string Assunto)
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.hostinger.com.br";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("no-reply@ironiawn.com.br", "xjR=eBrzr9n6iU#$HU");

            MailMessage mm = new MailMessage("no-reply@ironiawn.com.br", Destino, Assunto, "<html>" + Mensagem + "</html>");
            mm.From = new MailAddress("no-reply@ironiawn.com.br", "Ugnite Support");
            mm.IsBodyHtml = true;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

        /// <summary>
        /// Retorna uma imagem a partir de uma URL
        /// </summary>
        /// <param name="url">URL para Stream</param>
        /// <returns></returns>
        public static Image GetImagemURL(string url)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] bytes = wc.DownloadData(url);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

        /// <summary>
        /// Cria um efetio de Fade para abrir outra form[ENTRADA]
        /// </summary>
        public static async void FadeIn(Form o, int interval = 80)
        {
            try
            {
                //Object is not fully invisible. Fade it in
                while (o.Opacity < 1.0)
                {
                    await Task.Delay(interval);
                    o.Opacity += 0.05;
                }
                o.Opacity = 1; //make fully visible     
            }
            catch
            {
                ProgramData.MensagemErro("ERRO DE EXECUÇÃO DO APLICATIVO!\nELE SERÁ ENCERRADO.\nCÓDIGO FIER100");
                Application.Exit();
            }
        }

        /// <summary>
        /// Cria um efeito de fade para esta form [SAÍDA]
        /// </summary>
        /// <param name="o"></param>
        /// <param name="interval"></param>
        public static async void FadeOut(Form o, int interval = 80)
        {
            //Object is fully visible. Fade it out
            while (o.Opacity > 0.0)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0; //make fully invisible       
        }

        /// <summary>
        /// Faz o download de uma imagem da web e converte ela em formato apropriado
        /// </summary>
        /// <param name="imageUrl">URL da imagem</param>
        /// <returns></returns>
        public static Image DownloadImageFromUrl(string imageUrl)
        {
            Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch
            {
                return null;
            }

            return image;
        }

        /// <summary>
        /// Imagem com pontas arredondadas 
        /// </summary>
        /// <param name="StartImage">Imagem que vai ser arredondada</param>
        /// <param name="CornerRadius">O tamanho da ponta que via ser arredondada</param>
        /// <returns></returns>
        public static Image RoundCorners(Image StartImage, int CornerRadius)
        {
            CornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
            using (Graphics g = Graphics.FromImage(RoundedImage))
            {
                //g.Clear(BackgroundColor);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Brush brush = new TextureBrush(StartImage);
                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
                gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
                gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
                g.FillPath(brush, gp);
                return RoundedImage;
            }
        }

        #region Constants

        private const int GEO_FRIENDLYNAME = 8;

        #endregion

        #region Private Enums

        private enum GeoClass : int
        {
            Nation = 16,
            Region = 14,
        };

        #endregion

        #region Win32 Declarations

        [DllImport("kernel32.dll", ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int GetUserGeoID(GeoClass geoClass);

        [DllImport("kernel32.dll")]
        private static extern int GetUserDefaultLCID();

        [DllImport("kernel32.dll")]
        private static extern int GetGeoInfo(int geoid, int geoType, StringBuilder lpGeoData, int cchData, int langid);

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns machine current location as specified in Region and Language settings.
        /// </summary>
        public static string GetMachineCurrentLocation()
        {
            int geoId = GetUserGeoID(GeoClass.Nation); ;
            int lcid = GetUserDefaultLCID();
            StringBuilder locationBuffer = new StringBuilder(100);
            GetGeoInfo(geoId, GEO_FRIENDLYNAME, locationBuffer, locationBuffer.Capacity, lcid);

            return locationBuffer.ToString().Trim();
        }

        #endregion
    }
}
