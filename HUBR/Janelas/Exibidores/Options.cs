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

namespace UGNITE
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            this.Opacity = 0.0; // Muda a opacidade da janela para dar FadeIn depois ;)
        }

        void ApplyTranslation()
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                lbImageURL.Text = Program.GetStringRM("en", "lbImageURL");
                checkSaveLogin.Text = Program.GetStringRM("en", "checkSaveLogin");
                btnUpdateInformation.Text = "UPDATE\nINFO";
                btnUploadImage.Text = Program.GetStringRM("en", "btnUploadImage");
                lbTheme.Text = Program.GetStringRM("en", "lbTheme");
                lbSecurityCode.Text = Program.GetStringRM("en", "lbSecurityCode");
                lbNewPass.Text = Program.GetStringRM("en", "lbNewPass");
                lbUsermail.Text = Program.GetStringRM("en", "lbUsermail");
                lbUpdateAlert.Text = Program.GetStringRM("en", "lbUpdateAlert");
                lbDialogAbout.Text = Program.GetStringRM("en", "lbDialogAbout");
            }
        }

        /// <summary>
        ///  Ao carregar, adquire informações sobre o ousuário
        /// </summary>
        private void Options_Load(object sender, EventArgs e)
        {
                    // Adiciona os temas, de acordo com as pastas disponíveis na pasta "Temas"
                    for (int i = 0; i < Directory.GetDirectories(Application.StartupPath + @"\Temas").Length; i++)
                    {
                        if (File.Exists(Directory.GetDirectories(Application.StartupPath + @"\Temas")[i] + "\\Config.IW@THEMES"))
                            ThemeSelector.Items.Add(new DirectoryInfo(Directory.GetDirectories(Application.StartupPath + @"\Temas")[i]).Name.ToUpper());
                    }

                    // Seta o tema definido
                    ThemeSelector.Text = UGNITE.Properties.Settings.Default["Theme"].ToString().ToUpper();

                    // Verifica se a propriedade de salvar login está ativa
                    if (UGNITE.Properties.Settings.Default["saveUser"].ToString() == "1")
                        checkSaveLogin.Checked = true;
                    else
                        checkSaveLogin.Checked = false;

                    // Exibe animação de FadeIn
                    ProgramData.FadeIn(this, 65);

            if (Properties.Settings.Default["lang"].ToString() != "en")
            {
                // Atualiza informações
                lbLoadingProgress.Text = "CARREGANDO INFORMAÇÕES...(2/5)";

                // String para montar dados no paragráfo de detalhes
                string dtlText;

                // Monta com o e-mail
                dtlText = "DADOS ATUAIS\n\nE-MAIL.: " + ProgramData.Mail;

                // Seta o servidor de download
                string server = ProgramData.GetMachineCurrentLocation().ToUpper() == "BRAZIL" ? "S3TA" : "SAO";

                // Adquire o nome de usuário
                dtlText += "\nIDENTIFICAÇÃO.: " + ProgramData.Username + "\nSERVIDOR.: " + server;


                // Desabilita os textos e inputs
                TextShow(false);
                
                // Atualiza informações
                lbLoadingProgress.Text = "CARREGANDO INFORMAÇÕES...(3/5)";

                // Verifica se é publisher
                MySQL.GetInformation(4);
                if (MySQL.GetData == "1")
                    dtlText += "\nDESENVOLVEDOR/PUBLISHER.: SIM";
                else
                    dtlText += "\nDESENVOLVEDOR/PUBLISHER.: NÃO";
                
                // Atualiza informações
                lbLoadingProgress.Text = "CARREGANDO INFORMAÇÕES...(4/5)";

                // Mosta se o usuário é HUBR Plus
                MySQL.VerifyPlus();
                dtlText += MySQL.RemainingDays.ToString() != "0" ? $"\nU+ ATIVO | {MySQL.RemainingDays} DIA(S) RESTANTE(S)" : "";

                // Seta a input de imagem
                tbImageURL.Text = ProgramData.ImagemURL;

                // Seta a label de detalhes com os dados adquiridos
                lbUserDetails.Text = dtlText;

                // Atualiza informações
                lbLoadingProgress.Text = "CARREGANDO INFORMAÇÕES...(5/5)";
            }
            else
            {
                // Atualiza informações
                lbLoadingProgress.Text = "LOADING INFORMATION...(2/5)";

                // String para montar dados no paragráfo de detalhes
                string dtlText;

                // Monta com o e-mail
                dtlText = "USER INFORMATION\n\nE-MAIL.: " + ProgramData.Mail;

                // Seta o servidor de download
                string server = ProgramData.GetMachineCurrentLocation().ToUpper() == "BRAZIL" ? "S3TA" : "SAO"; 

                // Adquire o nome de usuário
                dtlText += "\nID.: " + ProgramData.Username + "\nSERVER.: " + server;


                // Desabilita os textos e inputs
                TextShow(false);

                // Atualiza informações
                lbLoadingProgress.Text = "LOADING INFORMATION...(3/5)";

                // Verifica se é publisher
                MySQL.GetInformation(4);
                if (MySQL.GetData == "1")
                    dtlText += "\nDEVELOPER/PUBLISHER.: YES";
                else
                    dtlText += "\nDEVELOPER/PUBLISHER.: NO";

                // Atualiza informações
                lbLoadingProgress.Text = "LOADING INFORMATION...(4/5)";

                // Mosta se o usuário é HUBR Plus
                MySQL.VerifyPlus();
                dtlText += MySQL.RemainingDays.ToString() != "0" ? $"\nU+ ACTIVE | {MySQL.RemainingDays} DAY(S) REMAINING" : "";

                // Seta a input de imagem
                tbImageURL.Text = ProgramData.ImagemURL;

                // Seta a label de detalhes com os dados adquiridos
                lbUserDetails.Text = dtlText;

                // Atualiza informações
                lbLoadingProgress.Text = "LOADING INFORMATION...(5/5)";

            }
                    // Faz o exibidor de informações ser desativado
                    lbLoadingProgress.Visible = false;
                    lbLoadingProgress.Enabled = false;

                    // Exibe os controles
                    TextShow(true);

            // Aplica tradução (se houver)
            ApplyTranslation();
        }


        void TextShow(bool show)
        {
            tbImageURL.Visible = show;
            lbImageURL.Visible = show;
            btnUploadImage.Visible = show;
            lbUserDetails.Visible = show;
            btnUpdateInformation.Visible = show;
            tbSecurityCode.Visible = show;
            tbUsermail.Visible = show;
            tbNewPass.Visible = show;
            tbUsermail.Visible = show;
            btnClose.Visible = show;
            btnUploadImage.Visible = show;
            lbNewPass.Visible = show;
            lbSecurityCode.Visible = show;
            lbUpdateAlert.Visible = show;
            lbUsermail.Visible = show;
        }

        /// <summary>
        /// Atualiza as informações do usuário
        /// </summary>
        private void btnUpdateInformation_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o texto do botão
                btnUpdateInformation.Text = "AGUARDE..";
            else
                // Muda o texto do botão
                btnUpdateInformation.Text = "WAIT..";


            // Primeiro, checa as condições minimas de login
            if (tbNewPass.Text != "" && tbNewPass.TextLength > 6 || (tbUsermail.Text != "" && (tbUsermail.TextLength > 5 && tbUsermail.Text.Contains("@") && tbUsermail.Text.Contains("."))))
            {
                // Verifica se o campo de senha não está vazio
                if (tbNewPass.TextLength > 6)
                {
                    // Adquire a informação de SecurityCode
                    MySQL.GetInformation(5);

                    // Faz a comparação com o código fornecido
                    if (MySQL.GetData == tbSecurityCode.Text)
                    {
                        // É informação correta, prosseguir com alteração.
                        MySQL.UpdateInformation(3, tbNewPass.Text); // Atualiza a senha do usuário

                        // Verifica se atualizou
                        if (MySQL.InfoUpdated)
                        {
                            // Envia e-mail de notificação
                            Rede.ClassesMail.EnviaMailSenha(ProgramData.Username);

                            if (Properties.Settings.Default["lang"].ToString() != "en")
                            {
                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"0;ALTERAÇÃO;SENHA DE CONTA;EFETUADO;{DateTime.Today.ToString()}");

                                ProgramData.MensagemSucesso("SENHA ATUALIZADA.\nAGORA VOCÊ PODERÁ EFETUAR LOGIN SEM PROBLEMAS ;)");
                            }
                            else
                            {
                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"0;UPDATE;ACCOUNT PASSWORD;FINISHED;{DateTime.Today.ToString()}");

                                ProgramData.MensagemSucesso("PASSWORD UPDATED.\nNOW YOU CAN LOGON WITHOUT PROBLEMS ;)");

                            }
                        }
                        else
                        {
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                            {
                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"1;ALTERAÇÃO;SENHA DE CONTA;NÃO EFETUADO (ERRO INTERNO);{DateTime.Today.ToString()}");

                                ProgramData.MensagemErro("A SENHA NÃO FOI ATUALIZADA!\nTENTE NOVAMENTE E, SE O ERRO PERSISTIR, CONTATE A UGNITE VIA CHAT OU EMAIL.\n\nEMAIL UGNITE.: shop@ironiawn.com.br");
                            }
                            else
                            {
                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"1;UPDADTE;ACCOUNT PASSWORD;NOT FINISHED (INTERNAL ERROR);{DateTime.Today.ToString()}");

                                ProgramData.MensagemErro("PASSWORD WASN'T UPDATED.\nPLEASE TRY AGAIN.\nIF THIS ERROR OCCURS AGAIN, CONTACT US VIA E-MAIL: shop@ironiawn.com.br");

                            }
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                        {
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"1;ALTERAÇÃO;SENHA DE CONTA;CÓDIGO DE SEGURANÇA INVÁLIDO;{DateTime.Today.ToString()}");
                            // O código fornecido não procede
                            ProgramData.MensagemErro("O CÓDIGO DE SEGURANÇA FORNECIDO NÃO É VÁLIDO!");
                        }
                        else
                        {
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"1;UPDATE;ACCOUNT PASSWORD;INVALID UGNITE SECURE CODE;{DateTime.Today.ToString()}");
                            // O código fornecido não procede
                            ProgramData.MensagemErro("INVALID UGNITE SECURE CODE!");

                        }
                    }
                }

                // Verifica se o campo de e-mail não está vazio
                if (tbUsermail.TextLength > 6)
                {
                    // Adquire a informação de SecurityCode
                    MySQL.GetInformation(5);

                    // Faz a comparação com o código fornecido
                    if (MySQL.GetData == tbSecurityCode.Text)
                    {
                        // É informação correta, prosseguir com alteração.
                        MySQL.UpdateInformation(2, tbUsermail.Text);

                        // Verifica se atualizou
                        if (MySQL.InfoUpdated)
                        {

                            if (Properties.Settings.Default["lang"].ToString() != "en")
                            {
                                // Exibe mensagem de atualização
                                ProgramData.MensagemSucesso("E-MAIL ATUALIZADO.\nAGORA PODEMOS BATER UM PAPO SOBRE QUALQUER COISA COM VOCÊ QUANDO QUISERMOS, TÁOQUEI?!");

                                // Muda o e-mail na programData
                                ProgramData.Mail = MySQL.GetData;

                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"0;ALTERAÇÃO;E-MAIL DE CONTA;EFETUADO;{DateTime.Today.ToString()}");
                            }
                            else
                            {
                                // Exibe mensagem de atualização
                                ProgramData.MensagemSucesso("WE'VE UPDATED YOUR E-MAIL.\nIF YOU NEED ANY HELP, CONTACT US VIA E-MAIL: shop@ironiawn.com.br");

                                // Muda o e-mail na programData
                                ProgramData.Mail = MySQL.GetData;

                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"0;UPDATE;ACCOUNT EMAIL;FINISHED;{DateTime.Today.ToString()}");

                            }
                        }
                        else
                        {
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                            {
                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"1;ALTERAÇÃO;E-MAIL DE CONTA;NÃO EFETUADO (ERRO INTERNO);{DateTime.Today.ToString()}");
                                ProgramData.MensagemErro("O E-MAIL NÃO FOI ATUALIZADO!\nTENTE NOVAMENTE E, SE O ERRO PERSISTIR, CONTATE A UGNITE VIA CHAT OU EMAIL.\n\nEMAIL UGNITE.: shop@ironiawn.com.br");
                            }
                            else
                            {
                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"1;UPDATE;ACCOUNT EMAIL;NOT FINISHED (INTERNAL ERROR);{DateTime.Today.ToString()}");
                                ProgramData.MensagemErro("E-MAIL WASN'T UPDATED.\nPLEASE TRY AGAIN.\nIF THIS ERROR OCCURS AGAIN, CONTACT US VIA E-MAIL: shop@ironiawn.com.br");

                            }
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                        {
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"1;ALTERAÇÃO;E-MAIL DE CONTA;CÓDIGO DE SEGURANÇA INVÁLIDO;{DateTime.Today.ToString()}");
                            // O código fornecido não procede
                            ProgramData.MensagemErro("O CÓDIGO DE SEGURANÇA FORNECIDO NÃO É VÁLIDO!");
                        }
                        else
                        {
                            // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                            MySQL.UpdateYourActivity($"1;UPDATE;ACCOUNT E-MAIL;INVALID UGNITE SECURE CODE;{DateTime.Today.ToString()}");
                            // O código fornecido não procede
                            ProgramData.MensagemErro("INVALID UGNITE SECURE CODE!");

                        }
                    }
                }
            }
            else
            {

                // Verifica se é o e-mail que está curto
                if (tbUsermail.TextLength < 6)
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                    {
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"1;ALTERAÇÃO;E-MAIL DE CONTA;NÃO EFETUADO (MUITO CURTO);{DateTime.Today.ToString()}");

                        ProgramData.MensagemErro("E-Mail fornecido é curto demais ou não é válido!");
                    }
                    else
                    {
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"1;UPDATE;ACCOUNT E-MAIL;NOT FINISHED (TOO SHORT/INVALID);{DateTime.Today.ToString()}");

                        ProgramData.MensagemErro("The provided e-mail address is to shoor or isn't valid!");

                    }
                }

                // Verifica se a senha está muito curta
                if (tbNewPass.TextLength < 7)
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                    {
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"1;ALTERAÇÃO;SENHA DE CONTA;NÃO EFETUADO (MUITO CURTO);{DateTime.Today.ToString()}");

                        ProgramData.MensagemErro("Senha precisa ter 7 ou mais caracteres!");
                    }
                    else
                    {
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"1;UPDATE;ACCOUNT PASSWORD;NOT FINISHED (TOO SHORT);{DateTime.Today.ToString()}");

                        ProgramData.MensagemErro("The password must have 7 or more chars!");

                    }
                }

            }

            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda o texto do botão
                btnUpdateInformation.Text = "ALTERAR\nDADOS";
            else
                // Muda o texto do botão
                btnUpdateInformation.Text = "UPDATE\nINFO";

        }

        /// <summary>
        /// Fecha a janela de dialogo atual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Atualiza a imagem do usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadImage_ClickAsync(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
            {
                btnUploadImage.Text = "AGUARDE";
                if (tbImageURL.Text.Length > 4 && tbImageURL.Text.Contains(".com"))
                {
                    UploadImagem.UploadImage(tbImageURL.Text); // Atualiza a imagem do usuário online
                    ProgramData.ImagemURL = tbImageURL.Text; // Atualiza a imagem do usuário offline
                                                             // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"0;ALTERAÇÃO;IMAGEM DE PERFIL;-;{DateTime.Today.ToString()}");

                }
                else
                    ProgramData.MensagemErro("VERIFIQUE SE A URL ESTÁ CORRETA.\nNÃO É ACEITO IMAGENS DE SITES QUE NÃO CONTENHAM .COM");

                btnUploadImage.Text = "ALTERAR MINHA IMAGEM";
            }
            else
            {
                btnUploadImage.Text = "WAIT";
                if (tbImageURL.Text.Length > 4 && tbImageURL.Text.Contains(".com"))
                {
                    UploadImagem.UploadImage(tbImageURL.Text); // Atualiza a imagem do usuário online
                    ProgramData.ImagemURL = tbImageURL.Text; // Atualiza a imagem do usuário offline
                                                             // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"0;UPDATE;ACCOUNT IMAGE;-;{DateTime.Today.ToString()}");

                }
                else
                    ProgramData.MensagemErro("Please, verify if the URL is correct!\nWe don't accept URLs that doesn't have .com");

                btnUploadImage.Text = "UPDATE MY IMAGE";

            }
        }

                /// <summary>
        ///  Salva o usuário atual logado
        /// </summary>
        private void checkSaveLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSaveLogin.Checked)
            {
                Properties.Settings.Default["saveUser"] = "1";
                Properties.Settings.Default["username"] = ProgramData.Username;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default["saveUser"] = "0";
                Properties.Settings.Default["username"] = "";
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Seta o tema selecionado pelo usuário
        /// </summary>
        private void ThemeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["Theme"] = ThemeSelector.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
