using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UGNITE
{
    public partial class U_Mensagem : Form
    {
        // Textos aleatórios
        string[] TextoAleatorio = new string[5];
        // Controlador de aleatoriedade
        Random rndText = new Random();
        // Atual texto selecionado via Random
        int rt;

        /// <summary>
        /// Mensagem a ser exibida ao usuário
        /// </summary>
        public static string DialogoMensagem;
        public static int TipoScroll = 1;


        public U_Mensagem()
        {
            // Cria todos os textos a serem exibidos no titulo do diálogo
            TextoAleatorio[0] = "DE BOA NA LAGOA!";
            TextoAleatorio[1] = "TUDO CERTO POR AQUI, 06.";
            TextoAleatorio[2] = "AAAAAI QUE LINDO, PERFEITO E SEXY.";
            TextoAleatorio[3] = "O QUE É TUDO? VOCÊ!";
            TextoAleatorio[4] = "O ROBÔ TE OBEDECEU, OH MEU REI!";

            // Faz o sorteio dos textos
            rt = rndText.Next(TextoAleatorio.Length);

            // Inicializa a janela do diálogo
            InitializeComponent();

            // Exibe o texto aleatóriamente selecionado
            lbTitle.Text = TextoAleatorio[rt];
        }

        /// <summary>
        /// Fecha a mensagem de texto
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Ao carregar a janela, exibir a mensagem ao usuário
        /// </summary>
        private void Mensagem_Load(object sender, EventArgs e)
        {
            // Exibe a mensagem
            rtMessage.Text = DialogoMensagem;

            // Verifica se é para exibir scroll
            if (TipoScroll != 1)
                rtMessage.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            else
                // Força a não exibir o scroll
                rtMessage.ScrollBars = RichTextBoxScrollBars.None;

        }
    }
}
