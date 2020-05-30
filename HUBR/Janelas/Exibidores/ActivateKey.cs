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
    public partial class ActivateKey : Form
    {
        public ActivateKey()
        {
            InitializeComponent();
            ApplyTranslate();
        }

        /// <summary>
        /// Fecha o diálogo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        /// <summary>
        /// Verifica a chave de ativação
        /// </summary>
        private void btnVerifyKey_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o texto do botão
                btnVerifyKey.Text = "AGUARDE";
            else
                // Muda o texto do botão
                btnVerifyKey.Text = "WAIT";

            // Primeiro, verifica se cumpre com os requisitos minimos
            if (tbKeyInput.TextLength == 29)
            {
                // Começa a verificar no banco de dados se é válida
                MySQL.ActivateGameKey(tbKeyInput.Text);
            }
            else
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Invalida a chave
                    ProgramData.MensagemErro("CHAVE UGNITE INVÁLIDA.\n\nFORNEÇA UMA CHAVE DE 25 DIGITOS, COMO NO EXEMPLO ABAIXO:\nUGNI1-FGH1J-KLMN0-PQ82X-R5TU9");
                else
                    // Invalida a chave
                    ProgramData.MensagemErro("INVALID UGNITE KEY.\n\nUSE A 25 CHARS KEY CODE, LIKE BELOW EXAMPLE:\nUGNI1-FGH1J-KLMN0-PQ82X-R5TU9");

            }
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o texto do botão
                btnVerifyKey.Text = "ATIVAR CHAVE UGNITE";
            else
                // Muda o texto do botão
                btnVerifyKey.Text = "ACTIVATE UGNITE KEY";
        }

        private void btnActivateSteamKey_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o texto do botão
                btnActivateSteamKey.Text = "AGUARDE";
            else
                // Muda o texto do botão
                btnActivateSteamKey.Text = "WAIT";

            // Primeiro, verifica se cumpre com os requisitos minimos
            if (tbKeyInput.TextLength == 29)
            {
                // Começa a verificar no banco de dados se é válida
                MySQL.RetrievePlatformKey(tbKeyInput.Text);
            }
            else
            {

                if (Properties.Settings.Default["lang"].ToString() != "en")
                {
                    // Invalida a chave
                    ProgramData.MensagemErro("CHAVE EXTERNA INVÁLIDA.\n\nFORNEÇA UMA CHAVE DE 25 DIGITOS, COMO NO EXEMPLO ABAIXO:\nSTEAM-FGH1J-KLMN0-PQ82X-R5TU9");
                }
                else
                {
                    // Invalida a chave
                    ProgramData.MensagemErro("INVALID EXTERNAL KEY.\n\nUSE A 25 CHARS KEY CODE, LIKE BELOW EXAMPLE:\nSTEAM-FGH1J-KLMN0-PQ82X-R5TU9");

                }
            }
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o texto do botão
                btnActivateSteamKey.Text = "ATIVAR CHAVE EXTERNA";
            else
                // Muda o texto do botão
                btnActivateSteamKey.Text = "ACTIVATE EXTERNAL KEY";

        }

        private void ActivateKey_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
            {
                lbWarning.Text = "A CHAVE SERÁ VINCULADA\n" +
            $@"A CONTA ""{ProgramData.Username.ToUpper()}"" NA UGNITE E NÃO PODERÁ MAIS SER UTILIZADA." +
            "\nCERTIFIQUE-SE DE ESTAR UTILIZANDO A CONTA CORRETA!";
            }else
            {
                lbWarning.Text = "THE KEY WILL BE ACTIVATED AT\n" +
            $@"USER ID: ""{ProgramData.Username.ToUpper()}"" AT UGNITE. THE KEY CANNOT BE USED AFTER THAT." +
            "\nBE SURE THAT THE KEY ARE BEIGN ACTIVATED AT THE CORRECT USER ID!";
            }


            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o texto do botão
                btnVerifyKey.Text = "ATIVAR CHAVE UGNITE";
            else
                // Muda o texto do botão
                btnVerifyKey.Text = "ACTIVATE UGNITE KEY";


            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o texto do botão
                btnActivateSteamKey.Text = "ATIVAR CHAVE EXTERNA";
            else
                // Muda o texto do botão
                btnActivateSteamKey.Text = "ACTIVATE EXTERNAL KEY";
        }

        void ApplyTranslate()
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                lbTitle.Text = Program.GetStringRM("en", "resgatarchave"); // Titulo da janela
                lbKeyCode.Text = Program.GetStringRM("en", "lbKeyCode"); // Texto de digite a chave
            }
        }
    }
}
