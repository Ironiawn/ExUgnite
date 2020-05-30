using System;
using System.Windows.Forms;

namespace UGNITE
{
    public partial class ExibeErro : Form
    {  // Textos aleatórios
        string[] TextoAleatorio = new string[6];
        // Controlador de aleatoriedade
        Random rndText = new Random();
        // Atual texto selecionado via Random
        int rt;

        /// <summary>
        /// Mensagem a ser exibida ao usuário
        /// </summary>
        public static string DialogoMensagem;

        public ExibeErro()
        {
            // Cria todos os textos a serem exibidos no titulo do diálogo

            if (Properties.Settings.Default["lang"].ToString() != "en")
                TextoAleatorio[0] = "UGNITE - ERRO!";
            else
                TextoAleatorio[0] = "UGNITE - ERROR!";


            // Faz o sorteio dos textos
            rt = rndText.Next(TextoAleatorio.Length);

            // Inicializa a janela do diálogo
            InitializeComponent();

            // Exibe o texto aleatóriamente selecionado
            lbTitle.Text = TextoAleatorio[rt];

            // Trás a janela para frente
            this.BringToFront();
        }

        /// <summary>
        /// Fecha a mensagem de texto
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Ao carregar, exibe mensagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExibeErro_Load_1(object sender, EventArgs e)
        {
            rtMessage.Text = DialogoMensagem;
        }
    }
}
