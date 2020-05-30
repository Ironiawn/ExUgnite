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
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Atualiza as informações do usuário(Senha)
        /// </summary>
        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {

            // Primeiro, checa as condições minimas de login
            if (tbUsername.TextLength > 5 && tbNewPass.TextLength > 6)
            {
                // Verifica se o usuário existe
                MySQL.GetUsuario(tbUsername.Text);

                // Se existir, prosseguir
                if (MySQL.GetUsuario(tbUsername.Text))
                {
                    // Seta o usuário temporário da sessão
                    ProgramData.Username = tbUsername.Text;

                    // Adquire a informação de SecurityCode
                    MySQL.GetInformation(5);

                    // Faz a comparação com o código fornecido
                    if (MySQL.GetData == tbSecurityCode.Text)
                    {
                        // É informação correta, prosseguir com alteração.
                        MySQL.UpdateInformation(3, tbNewPass.Text); // Atualiza a senha do usuário


                        // Envia e-mail de notificação
                        Rede.ClassesMail.EnviaMailSenha(tbUsername.Text);

                        if (Properties.Settings.Default["lang"].ToString() != "en")
                        {
                            // Verifica se atualizou
                            if (MySQL.InfoUpdated)
                                ProgramData.MensagemSucesso("SENHA ATUALIZADA.\nAGORA VOCÊ PODERÁ EFETUAR LOGIN SEM PROBLEMAS ;)");
                            else
                                ProgramData.MensagemErro("A SENHA NÃO FOI ATUALIZADA!\nTENTE NOVAMENTE E, SE O ERRO PERSISTIR, CONTATE A UGNITE VIA CHAT OU EMAIL.\n\nEMAIL HUBR.: shop@ironiawn.com.br");
                        }
                        else
                        {
                            // Verifica se atualizou
                            if (MySQL.InfoUpdated)
                                ProgramData.MensagemSucesso("PASSWORD UPDATED.\nNOW YOU CAN LOGON WITHOUT PROBLEMS ;)");
                            else
                                ProgramData.MensagemErro("PASSWORD ISN'T UPDATED!\nTRY AGAIN AND, IF PERSISTS, CONTACT-US BY E-MAIL: shop@ironiawn.com.br");

                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            // O código fornecido não procede
                            ProgramData.MensagemErro("O CÓDIGO DE SEGURANÇA FORNECIDO NÃO É VÁLIDO PARA A IDENTIFICAÇÃO FORNECIDA!\nVERIFIQUE E TENTE NOVAMENTE.\nCASO TENHA PERDIDO O CÓDIGO, ENTRE EM CONTATO COM A UGNITE VIA EMAIL OU CHAT.\n\nEMAIL HUBR.: shop@ironiawn.com.br\n");
                        else
                            // O código fornecido não procede
                            ProgramData.MensagemErro("INVALID UGNITE SECURE CODE.\nIF YOU LOST IT, CONTACT US WITH ALL INFORMATION THAT YOU HAVE OF THE ACCOUNT VIA E-MAIL: shop@ironiawn.com.br");

                    }

                    // Reseta o usuário temporário da sessão
                    ProgramData.Username = null;
                }
                else
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Usuário não existe
                        ProgramData.MensagemErro("O USUÁRIO INFORMADO NÃO EXISTE!");
                    else
                        // Usuário não existe
                        ProgramData.MensagemErro("USER ID NOT FOUND.");
                }
            }else
            {

                if (Properties.Settings.Default["lang"].ToString() != "en")
                {
                    // Verifica se é o nome de usuário pequeno demais
                    if (tbUsername.TextLength < 6)
                        // MessageBox.Show("Nome de usuário precisa ter 6 ou mais caracteres!", "HUBR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ProgramData.MensagemErro("Nome de usuário precisa ter 6 ou mais caracteres!");

                    // Verifica se a senha está muito curta
                    if (tbNewPass.TextLength < 7)
                        // MessageBox.Show("Senha precisa ter 7 ou mais caracteres!", "HUBR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ProgramData.MensagemErro("Senha precisa ter 7 ou mais caracteres!");
                }
                else
                {
                    // Verifica se é o nome de usuário pequeno demais
                    if (tbUsername.TextLength < 6)
                        // MessageBox.Show("Nome de usuário precisa ter 6 ou mais caracteres!", "HUBR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ProgramData.MensagemErro("User ID must have 6 or more chars.");

                    // Verifica se a senha está muito curta
                    if (tbNewPass.TextLength < 7)
                        // MessageBox.Show("Senha precisa ter 7 ou mais caracteres!", "HUBR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        ProgramData.MensagemErro("Password must have 7 or more chars.");

                }

            }
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

        private void ForgotPassword_Load(object sender, EventArgs e)
        {
            // Aplica a tradução (se houver)

            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                lbUserID.Text = Program.GetStringRM("en", "identificacao");
                lbSecurityCode.Text = Program.GetStringRM("en", "lbSecurityCode");
                lbNewPass.Text = Program.GetStringRM("en", "lbNewPass");
                btnUpdatePassword.Text = Program.GetStringRM("en", "btnUpdatePassword");
            }
        }
    }
}