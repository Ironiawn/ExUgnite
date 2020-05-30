using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using IniParser;
using IniParser.Model;
using Microsoft.Win32;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace UGNITE
{
    public partial class HomeUsuario : Form
    {
        public HomeUsuario()
        {
            InitializeComponent();

            this.Opacity = 0.0; // Muda a opacidade da janela para dar FadeIn depois ;)
        }

        /// <summary>
        /// Aplica os dados de tradução
        /// </summary>
        void ApplyTranslation()
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                lbLoginInfo.Text = Program.GetStringRM("en", "login"); // Texto de login
                lbUsername.Text = Program.GetStringRM("en", "identificacao"); // Texto de ID;
                lbCreateUser.Text = Program.GetStringRM("en", "cadastro_de_usuario"); // Texto de Cadastro de Usuário;
                lbPassword.Text = Program.GetStringRM("en", "senha"); // Texto de Senha;
                lbNewUsername.Text = Program.GetStringRM("en", "identificacao"); // Texto de ID;
                lbNewPassword.Text = Program.GetStringRM("en", "senha"); // Texto de Senha;
                lbForgotPassword.Text = Program.GetStringRM("en", "esquecisenha"); // Texto de Esqueceu senha;
                lbDevORPub.Text = Program.GetStringRM("en", "devpublisher"); // Texto de Desenvolvedor/Publisher
                lbTermsGet.Text = Program.GetStringRM("en", "termos"); // Texto de Termos de Uso
                btnLogin.Text = Program.GetStringRM("en", "entrar"); // Botão de login
                btnCreateUser.Text = Program.GetStringRM("en", "cadastrar"); // Botão de cadastro
                cbDevPub.Text = Program.GetStringRM("en", "soudev"); // Checkbox de desenvolvedor/publisher
                btnSwitchLanguage.Text = "MUDAR PARA\nPT-BR";
            }
            else
            {
                btnSwitchLanguage.Text = "CHANGE TO\nEN-US";
            }
        }


        private void HomeUsuario_Load(object sender, EventArgs e)
        {
            #region SETA O REGISTRO PARA INICIALIZAÇÃO DE JOGOS EXTERNAMENTE
            using (RegistryKey reg = Registry.ClassesRoot.CreateSubKey("UGNITE"))
            {
                reg.SetValue("", "URL: Protocolo Ugnite");
                reg.SetValue("URL Protocol", "");

                RegistryKey df = reg.CreateSubKey("DefaultIcon");
                df.SetValue("", "UGNITE.exe");

                RegistryKey cm = reg.CreateSubKey("Shell\\Open\\Command");
                cm.SetValue("", Environment.CurrentDirectory + @"\UGNITE.exe %1");

                reg.Flush();
            }
            #endregion


            ApplyTranslation();

            // Verifica se a propriedade de salvar login está ativa
            if (Properties.Settings.Default["saveUser"].ToString() == "1")
                tbUsername.Text =
                Properties.Settings.Default["username"].ToString();

            // Verifica se o arquivo de versões existe
            if (File.Exists(Environment.CurrentDirectory + "\\u.sys\\versao.txt"))
                // Verifica a versão atual do programa
                lbAppVersion.Text = "UGNITE_" + File.ReadAllText(Environment.CurrentDirectory + "\\u.sys\\versao.txt") + "@" + Application.ProductVersion;

            // Exibe animação de FadeIn
            ProgramData.FadeIn(this, 65);

            loadingGIF.Image = Properties.Resources.ajax_loader;
            loadingGIF.Visible = false;

        }

        /// <summary>
        ///  Calcula o MD5 do arquivo
        /// </summary>
        string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        // Loga no servidor
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Muda a cor do texto de senha
                lbPassword.ForeColor = Color.White;

                //btnLogin.Text = "AGUARDE...";
                btnLogin.Visible = false;
                loadingGIF.Visible = true;

                // Primeiro, checa as condições minimas de login
                if (tbUsername.TextLength > 5 && tbPassword.TextLength > 6)
                {
                    // Verifica existencia usuário
                    MySQL.GetUsuario(tbUsername.Text);

                    // Não existe? Prosseguir com login.
                    if (MySQL.GetUsuario(tbUsername.Text))
                    {
                        // Seta o usuário ativo na ProgramData
                        ProgramData.Username = tbUsername.Text;

                        // Verifica a versão atual da HUBR
                        string versaoAtual = $"{File.ReadAllText(Environment.CurrentDirectory + @"\u.sys\versao.txt")}@{ProductVersion}";

                        // Verifica a versão do cliente HUBR que está sendo utilizada
                        MySQL.GetInformation(8);
                        if (MySQL.GetData != versaoAtual)
                            MySQL.UpdateInformation(8, versaoAtual);


                        // Verifica se o usuário está banido
                        if (MySQL.Banned != "-1")
                        {
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                ProgramData.MensagemErro("USUÁRIO BANIDO DA UGNITE!\nMOTIVO: " + MySQL.Banned);
                            else
                                ProgramData.MensagemErro("BANNED USER FROM UGNITE!\nREASON: " + MySQL.Banned);
                        }
                        else
                        {

                            // Verifica se a senha é válida
                            MySQL.VerifyPassword(tbPassword.Text);

                            // Se for válida, logar
                            if (MySQL.ValidPassword)
                            {
                                // Atualiza o IP do usuário
                                MySQL.UpdateInformation(10, MySQL.getExternalIp());

                                // Adquire o e-mail do usuário
                                MySQL.GetInformation(2);

                                // Cadastra, de modo temporário, os dados no ProgramData
                                ProgramData.Mail = MySQL.GetData; // Email do usuário

                                // Adquire a URL da imagem do usuário
                                MySQL.GetInformation(6);
                                ProgramData.ImagemURL = MySQL.GetData;

                                // Verifica se o usuário é HUBR Plus
                                MySQL.GetInformation(7);
                                Program.HUBRPlus = int.Parse(MySQL.GetData) == 1 ? true : false;

                                // Verifica se a propriedade de salvar login está ativa
                                if (Properties.Settings.Default["saveUser"].ToString() == "1")
                                {
                                    Properties.Settings.Default["username"] = ProgramData.Username;
                                    Properties.Settings.Default.Save();
                                }

                                // Atualiza informações de último login
                                MySQL.UpdateInformation(11, DateTime.Parse(DateTime.Now.ToString(), new System.Globalization.CultureInfo("pt-BR", true)).ToString());

                                // Cria a form principal de exibição após o login
                                this.Hide();
                                var form2 = new HUBR_SHOP();
                                form2.Closed += (a, args) => this.Close();
                                form2.Show();

                            }
                            else
                                lbPassword.ForeColor = Color.Red; // Muda a cor do texto de senha para sinalizar que está incorreta
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            ProgramData.MensagemErro("CADASTRO NÃO ENCONTRADO.\nPOR FAVOR, REALIZE O CADASTRO ANTES DE LOGAR COM A IDENTIFICAÇÃO FORNECIDA.");
                        else
                            ProgramData.MensagemErro("USER ID NOT FOUND.\nPLEASE, SIGN UP BEFORE TRY TO LOGON WITH THIS ID.");
                    }
                }
                else
                {
                    // Verifica se é o nome de usuário pequeno demais
                    if (tbUsername.TextLength < 6)
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            ProgramData.MensagemErro("Nome de usuário precisa ter 6 ou mais caracteres!");
                        else
                            ProgramData.MensagemErro("User ID must have 6 or more chars!");

                    }

                    // Verifica se a senha está muito curta
                    if (tbPassword.TextLength < 7)
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            ProgramData.MensagemErro("Senha precisa ter 7 ou mais caracteres!");
                        else
                            ProgramData.MensagemErro("Password must have 7 or more chars!");

                    }
                }
                btnLogin.Visible = true;
                loadingGIF.Visible = false;
                // Muda texto do botão
                btnLogin.Text = "ENTRAR";
            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO AO EXECUTAR FUNÇÃO DE LOGIN!\nCONTATE O SUPORTE UGNITE VIA: shop@ironiawn.com.br\n\nDetalhes do erro abaixo:\n\n{ex}");
                else
                    ProgramData.MensagemErro($"SOMETHING BAD HAPPENED!\nERROR WHILE TRYING TO LOGON.\nCONTACT UGNITE SUPPORT VIA E-MAIL: shop@ironiawn.com.br\n\nError details below:\n\n{ex}");

                Application.Exit();
            }

        }


        /// <summary>
        /// Cria um usuário no servidor
        /// </summary>
        private void btnCreateUser_ClickAsync(object sender, EventArgs e)
        {
            try
            {                
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Muda texto do botão
                    btnCreateUser.Text = "AGUARDE..";
                else
                    // Muda texto do botão
                    btnCreateUser.Text = "WAIT..";
                

                    // Primeiro, checa as condições minimas de cadastro
                    if (tbNewID.TextLength > 5 && tbNewPass.TextLength > 6 && (tbNewMail.TextLength > 5 && tbNewMail.Text.Contains("@") && tbNewMail.Text.Contains(".")))
                    {
                        // Verifica se existe um e-mail já sendo utilizado
                        MySQL.GetMail(tbNewMail.Text);

                        // Atualiza o ProgramDataUser
                        ProgramData.Username = tbNewID.Text;


                    // Verifica se o usuário e e-mail existe
                    if (!MySQL.GetUsuario(tbNewID.Text))
                    {
                        // Verifica se existe um e-mail 
                        if (!MySQL.MailExiste)
                        {
                            MySQL.CriaUsuario(tbNewID.Text, tbNewPass.Text, tbNewMail.Text, cbDevPub.Checked == true ? 1 : 0); // Cria o usuário, já que não existe ambos

                            // Muda para a tela Home
                            // Adquire o e-mail do usuário
                            MySQL.GetInformation(2);
                            ProgramData.Mail = MySQL.GetData; // Seta o e-mail do usuário

                            // Adquire o nome de usuário cadastrado
                            // Cadastra, de modo temporário, os dados no ProgramData
                            ProgramData.Username = tbNewID.Text; // Usuário ativo

                            // Adquire a imagem temporária do usuário
                            MySQL.GetInformation(6);
                            ProgramData.ImagemURL = MySQL.GetData;

                            // Verifica se o usuário é HUBR Plus
                            MySQL.GetInformation(7);
                            Program.HUBRPlus = int.Parse(MySQL.GetData) == 1 ? true : false;


                            // Mostra a Home
                            this.Hide();
                            var form2 = new HUBR_SHOP();
                            form2.Closed += (s, args) => this.Close();
                            form2.Show();
                        }
                        else
                        {
                            // Reseta as informações e exibe mensagem de erro
                            ProgramData.Username = "";
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                ProgramData.MensagemErro($"O E-MAIL {tbNewMail.Text.ToUpper()} JÁ ESTÁ SENDO UTILIZADO!\nPOR FAVOR, SELECIONE OUTRO.");
                            else
                                ProgramData.MensagemErro($"THE E-MAIL {tbNewMail.Text.ToUpper()} IS BEIGN USED!\nPLEASE, CHOOSE ANOTHER ONE.");
                        }
                    }
                    else
                    {
                        // Reseta as informações e exibe mensagem de erro
                        ProgramData.Username = "";
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            ProgramData.MensagemErro($"O USUÁRIO {tbNewID.Text.ToUpper()} JÁ EXISTE!\nPOR FAVOR, SELECIONE OUTRO.");
                        else
                            ProgramData.MensagemErro($"THE ID {tbNewID.Text.ToUpper()} IS BEIGN USED!\nPLEASE, CHOOSE ANOTHER ONE.");
                    }
                    }
                    else
                    {
                        // Verifica se é o nome de usuário pequeno demais
                        if (tbNewID.TextLength < 6)
                        {
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                ProgramData.MensagemErro("Nome de usuário precisa ter 6 ou mais caracteres!");
                            else
                                ProgramData.MensagemErro("User ID must have 6 or more chars!");
                        }

                        // Verifica se a senha está muito curta
                        if (tbNewPass.TextLength < 7)
                        {
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                ProgramData.MensagemErro("Senha precisa ter 7 ou mais caracteres!");
                            else
                                ProgramData.MensagemErro("Password must have 7 or more chars!");
                        }

                        // Verifica se o e-mail é válido
                        if (tbNewMail.TextLength < 6 && !tbNewMail.Text.Contains("@") && !tbNewMail.Text.Contains("."))
                        {
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                ProgramData.MensagemErro("Verifique se seu e-mail é válido!");
                            else
                                ProgramData.MensagemErro("Invalid provided e-mail address.");

                        }
                    }

            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO AO EXECUTAR FUNÇÃO DE CADASTRO!\nCONTATE O SUPORTE UGNITE EM HTTPS://WWW.SUPPORT.IRONIAWN.COM.BR\nDetalhes do erro abaixo:\n{ex}");
                else
                    ProgramData.MensagemErro($"ERROR WHILE CREATING THE USER!\nCONTACT UGNITE SUPPORT AT HTTPS://WWW.SUPPORT.IRONIAWN.COM.BR\nErro details below:\n{ex}");

                Application.Exit();
            }

            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Muda texto do botão
                btnCreateUser.Text = "CADASTRAR";
            else
                // Muda texto do botão
                btnCreateUser.Text = "SIGN UP";
        }

        /// <summary>
        /// Fecha o aplicativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Minimiza o aplicativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// O usuário esqueceu a senha! 
        /// Abrir a form de recuperação.
        /// </summary>
        private void lbForgotPassword_Click(object sender, EventArgs e)
        {
            // Cria a form em forma de diálogo
            ForgotPassword fPass = new ForgotPassword();

            // Cria ela
            fPass.ShowDialog();
        }

        /// <summary>
        /// Ver os termos de uso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbTermsGet_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Exibe os termos para que o usuário ao se logar/cadastrar fique ciente.
                ProgramData.MensagemSucesso("TERMOS DE UTILIZAÇÃO UGNITE\n\nAo clicar em 'CADASTRAR/ENTRAR' você concorda com os termos abaixo.\n1.: Concorda que forneceu dados corretos.\n2.: Concorda que poderemos entrar em contato a qualquer momento para solicitar confirmação de dados.\n" +
                "3.: Concorda que não utilizará o software para outros fins a não ser aquele que ele foi destinado.\n4.: Concorda que seus dados ficarão cadastrados no banco de dados da Ironiawn S.A.\n5.: Concorda que, caso você seja banido da UGNITE, TODOS os produtos adquiridos com ativação" +
                " na UGNITE serão bloqueados e não poderão mais ser utilizados pela conta.\n6.: Concorda que os banimentos se aplicam a: \n- Utilização indevida do software/site\n- Identificação/e-mail inválido(profano/indagador a violência de qualquer tipo)\n- Mensagens a outro usuário" +
                " com cunho de qualquer tipo de violência ou profanidade\n7.: Concorda que você não está violando nenhuma lei nacional ou internacional ou será denunciado e a conta BANIDA.\n8.:A HUBR é um serviço novo e, portanto, poderá ser interrompido para manutenções sem aviso" +
                " prévio.\n9.: Caso você tenha adquirido algum produto e o serviço venha a ser interrompido sem previsão de volta, não há reembolsos, pois as compras são permanentes e irreversíveis."
                + "\n10.: Os dados armazenados ficam na servidor da Ironiawn S.A, e armazenamos somente: Nome de Utilizador, E-Mail, senha para cadastro na UGNITE, código de segurança UGNITE, URL da imagem UGNITE, se é ou não UGNITE+, IP, mensagens de chat e SO.\n" +
                "11.: Caso você não esteja com a versão atualizada da UGNITE, durante um período de 2 dias úteis após o lançamento da nova versão, sua conta será banida até que você a atualize.", 0);
            else
                // Exibe os termos para que o usuário ao se logar/cadastrar fique ciente.
                ProgramData.MensagemSucesso("UGNITE TERMS OF USE\n\nWhen you click at 'LOGON/SIGIN UP' you agree with terms below.\n1.: Accept that every data is correct.\n2.: Accept that we can contact you to verify the data.\n" +
                "3.: Accept that will not use the software with another intentions.\n4.: Accept that the data will be stored at Ironiawn S.A servers (not in Brazil).\n5.: Accept that if your account gets a 'ban status' you can't recover any information about it, anymore," +
                " and you will not be allowed to open games that are linked to it.\n6.: Accept that 'ban status' can be applied if one of conditions issued to your account: \n- Software misuse/site\n- Invalid ID/e-mail\n- Other user's message" +
                " with any kind of violence or profanity\n7.: You agree that you are not violating any national or international law or will be reported and the account will be banned.\n8.: Ugnite is a new service and therefore may be discontinued for unannounced maintenance." +
                " \n9.: If you have purchased a product and the service is interrupted without a return forecast, there are no refunds as purchases are permanent and irreversible."
                + "\n10.: The stored data is stored on the server of Ironiawn SA, and we only store: Username, E-mail, password to register at UGNITE, security code UGNITE, URL of UGNITE image, whether or not UGNITE +, IP , chat messages and OS.\n" +
                "11.: If you do not have the updated version of UGNITE, within 2 business days after the release of the new version, your account will be banned until you update it.", 0);

        }

        /// <summary>
        /// Exibe uma mensagem sobre desenvolvedor e publisher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbDevORPub_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Cria a mensagem
                ProgramData.MensagemSucesso("VOCÊ É UM@ DESENVOLVEDOR@ OU PUBLISHER DE JOGOS INDIES? A UGNITE É O LUGAR PERFEITO PARA VOCÊ, QUE ESTÁ COMEÇANDO NESSE RAMO!\n\nQUER PUBLICAR SEU JOGO GRATUITAMENTE E AINDA POR CIMA ADMINISTRAR FACILMENTE AS ATUALIZAÇÕES" +
                " DO JOGO SEM TER DOR DE CABEÇA?\nA UGNITE É UM CLIENTE DE JOGOS DIGITAIS PARA AQUELA PESSOA OU EMPRESA QUE QUER VER E TESTAR SE SEU JOGO REALMENTE ESTÁ PRONTO PARA O MERCADO GRANDÃO DE JOGOS QUE ESTÁ A SUA VOLTA (OU FRENTE, VAI QUE..) E " +
                "QUER QUE SEU JOGO ALCANCE NOVOS HORIZONTES RAPIDAMENTE, EM UM PUBLICO ESPECÍFICO E QUE GOSTE DESSES TIPOS DE JOGOS!\n\nQUER TER SEU JOGO PUBLICADO AQUI GRATUITAMENTE, SEM TAXAS DE MANUTENÇÃO*? ENVIE UM E-MAIL PARA shop@ironiawn.com.br" +
                " COM O ASSUNTO: 'PUBLICAÇÃO DE JOGOS' QUE IREMOS ENVIAR MAIS DETALHES SOBRE O FORMULÁRIO DE PUBLICAÇÃO DE JOGOS NA UGNITE.\n\nVOCÊ ESTÁ FAZENDO UM JOGO NA FACULDADE OU COMO UM HOBBIE E QUER TESTAR A PLATAFORMA? TAMBÉM ACEITAMOS ESSE TIPO DE "
                + "PROPOSTA SEM PROBLEMAS, AFINAL, TODOS MERECEM UMA CHANCE E TAMBÉM SOMOS ESSE TIPO DE PESSOA ;)\n A UGNITE ESTÁ DE PORTA ABERTAS A TODOS QUE QUEREM VER SEU JOGO MANEIRASSO SER PUBLICADO!!\nTESTE E DÊ O SEU FEEDBACK SOBRE A UGNITE!", 0);
            else
                // Cria a mensagem
                ProgramData.MensagemSucesso("While Ugnite is still in BETA phase, we encourage you to try to publish your game here, for free!*\nWe are working hard to bring new features at every created update for all users.\n"
                    + "ALL users can send suggestions to new features, games or anything that will bring to Ugnite something that can change significantly the use style of the  app.\nUgnite was made to host new game ideas, without any restrictions (really!)." +
                    "\nSend your application to: shop@ironiawn.com.br with the subject: 'GAME PUBLISHING AT UGNITE-EN'", 0);

        }
        private void btnSwitchLanguage_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["lang"].ToString() != "en")
            {
                Properties.Settings.Default["lang"] = "en";
                Properties.Settings.Default.Save();
                MessageBox.Show("Language saved.\nRestart Ugnite to use it.", "Ugnite - Language Switch");
                btnSwitchLanguage.Text = "MUDAR PARA\nPT-BR";
            }
            else
            {
                Properties.Settings.Default["lang"] = "br";
                Properties.Settings.Default.Save();
                MessageBox.Show("Idioma alterado!\nReinicie a Ugnite para utilizar.", "Ugnite - Alteração de Idioma");
                btnSwitchLanguage.Text = "CHANGE TO\nEN-US";
            }
        }
    }
}
