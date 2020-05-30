using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UGNITE.Rede
{
    public static class ClassesMail
    {
        /// <summary>
        /// TEXTO: RECIBO DE TRANSAÇÃO
        /// </summary>
        static string TRANSACTIONRECEIPT = "@TRANSACTIONRECEIPT@";
        /// <summary>
        /// TEXTO: ID DA CONTA DO JOGADOR
        /// </summary>
        static string PLAYERID = "@NICKNAME@";
        /// <summary>
        /// TEXTO: INVOICE
        /// </summary>
        static string ORDERNUMBER = "@ORDERNUMBER@";
        /// <summary>
        /// TEXTO: ID DA CONTA DO JOGADOR
        /// </summary>
        static string UGNITEID = "@UGNITEID@";
        /// <summary>
        /// TEXTO: EMAIL DO USUÁRIO
        /// </summary>
        static string UGNITEMAIL = "@UGNITEMAIL@";
        /// <summary>
        /// TEXTO: CÓDIGO DE SEGURANÇA DO USUÁRIO
        /// </summary>
        static string UGNITESECURECODE = "@UGNITESECURECODE@";
        /// <summary>
        /// TEXTO: DATA DA TRANSAÇÃO
        /// </summary>
        static string PURCHASEDATE = "@PURCHASEDATE@";
        /// <summary>
        /// TEXTO: PREÇO DO PRODUTO
        /// </summary>
        static string PRODPRICE = "@PRODPRICE@";
        /// <summary>
        /// TEXTO: NOME DO PRODUTO
        /// </summary>
        static string PRODUCTNAME = "@PRODUCTNAME@";
        /// <summary>
        /// TEXTO: SALDO DISPONÍVEL NA CARTEIRA UPAY
        /// </summary>
        static string WALLETBALLANCE = "@BALANCE@";
        /// <summary>
        /// TEXTO: TOTAL DA COMPRA
        /// </summary>
        static string TOTAL = "@TOTAL@";
        /// <summary>
        /// TEXTO: VERSÃO DA UGNITE
        /// </summary>
        static string UGNITEVERSION = "@UGVERSION@";
        /// <summary>
        /// TEXTO: DESCRIÇÃO DO BUG
        /// </summary>
        static string BUGDESC = "@BUGDESC@";

        /// <summary>
        /// ARQUIVOS HTML COM CONTEÚDO PARA ENVIO DE TRANSAÇÃO
        /// </summary>
        static string PURCHASEFILE_EN = AppDomain.CurrentDomain.BaseDirectory + "\\htmlapp\\PurchaseEN.html";
        /// <summary>
        /// ARQUIVOS HTML COM CONTEÚDO PARA ENVIO DE TRANSAÇÃO
        /// </summary>
        static string PURCHASEFILE_BR = AppDomain.CurrentDomain.BaseDirectory + "\\htmlapp\\PurchaseBR.html";

        /// <summary>
        /// ARQUIVOS HTML COM CONTEÚDO PARA ENVIO DE INFORMAÇÃO DE NOVO USUÁRIO
        /// </summary>
        static string NEWUSER_EN = AppDomain.CurrentDomain.BaseDirectory + "\\htmlapp\\NewUserEN.html";
        /// <summary>
        /// ARQUIVOS HTML COM CONTEÚDO PARA ENVIO DE INFORMAÇÃO DE NOVO USUÁRIO
        /// </summary>
        static string NEWUSER_BR = AppDomain.CurrentDomain.BaseDirectory + "\\htmlapp\\NewUserBR.html";

        /// <summary>
        /// ARQUIVOS HTML COM CONTEÚDO PARA ENVIO DE INFORMAÇÃO SOBRE RESET DE SENHA
        /// </summary>
        static string PASSRESET_EN = AppDomain.CurrentDomain.BaseDirectory + "\\htmlapp\\ResetPasswordEN.html";
        /// <summary>
        /// ARQUIVOS HTML COM CONTEÚDO PARA ENVIO DE INFORMAÇÃO SOBRE RESET DE SENHA
        /// </summary>
        static string PASSRESET_BR = AppDomain.CurrentDomain.BaseDirectory + "\\htmlapp\\ResetPasswordBR.html";

        /// <summary>
        /// ARQUIVOS HTML COM CONTEÚDO PARA ENVIO DE INFORMAÇÃO SOBRE REPORT DE BUGS
        /// </summary>
        static string BUGREPORT_EN = AppDomain.CurrentDomain.BaseDirectory + "\\htmlapp\\SendBugEN.html";
        /// <summary>
        /// ARQUIVOS HTML COM CONTEÚDO PARA ENVIO DE INFORMAÇÃO SOBRE REPORT DE BUGS
        /// </summary>
        static string BUGREPORT_BR = AppDomain.CurrentDomain.BaseDirectory + "\\htmlapp\\SendBugBR.html";

        /// <summary>
        /// Envia um e-mail com recibo de compra
        /// </summary>
        /// <param name="Invoice">INVOICE</param>
        /// <param name="NomeDaCompra">NOME DO PRODUTO</param>
        /// <param name="DataDaCompra">DATA DA COMPRA</param>
        /// <param name="ValorDaCompra">VALOR DO PRODUTO</param>
        public static void EnviaMailCompra(string Invoice, string NomeDaCompra, string DataDaCompra, string ValorDaCompra)
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                // Lê o arquivo HTML 
                string msgFile = System.IO.File.ReadAllText(PURCHASEFILE_EN);

                // Verifica a versão atual da UGNITE
                string versaoAtual = $"{System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\u.sys\versao.txt")}@{System.Windows.Forms.Application.ProductVersion}";

                // Cria um novo texto
                StringBuilder sb = new StringBuilder(msgFile);

                // Substitui os campos
                sb.Replace(TRANSACTIONRECEIPT, "TRANSACTION RECEIPT"); // TRANSACTION RECEIPT
                sb.Replace(PLAYERID, ProgramData.Username); // USER ID
                sb.Replace(ORDERNUMBER, Invoice); // INVOICE
                sb.Replace(UGNITEID, ProgramData.Username); // USER ID
                sb.Replace(PURCHASEDATE, DataDaCompra); // PURCHASE DATE
                sb.Replace(PRODUCTNAME, NomeDaCompra); // PRODUCT NAME
                sb.Replace(WALLETBALLANCE, "UP$" + MySQL.GetWalletValue.ToString()); // CURRENT WALLET BALLANCE
                sb.Replace(TOTAL, "UP$" + ValorDaCompra); // PURCHASE VALUE (TOTAL)
                sb.Replace(PRODPRICE, "UP$" + ValorDaCompra); // PURCHASE VALUE
                sb.Replace(UGNITEVERSION, versaoAtual); // CURRENT UGNITE'S VERSION


                // Envia um e-mail para o usuário
                ProgramData.EnviaMail(sb.ToString(), ProgramData.Mail, "Thanks for your Purchase! - Ugnite");

                // Notifica o usuário do envio da compra
                ProgramData.NotificaUsuario("PAID", "The product " + NomeDaCompra + " has been added to your library!", "Ugnite - Purchase", 4);
            }
            else
            {
                // Lê o arquivo HTML 
                string msgFile = System.IO.File.ReadAllText(PURCHASEFILE_BR);

                // Verifica a versão atual da UGNITE
                string versaoAtual = $"{System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\u.sys\versao.txt")}@{System.Windows.Forms.Application.ProductVersion}";

                // Cria um novo texto
                StringBuilder sb = new StringBuilder(msgFile);

                // Substitui os campos
                sb.Replace(TRANSACTIONRECEIPT, "RECIBO DE TRANSAÇÃO"); // TRANSACTION RECEIPT
                sb.Replace(PLAYERID, ProgramData.Username); // USER ID
                sb.Replace(ORDERNUMBER, Invoice); // INVOICE
                sb.Replace(UGNITEID, ProgramData.Username); // USER ID
                sb.Replace(PURCHASEDATE, DataDaCompra); // PURCHASE DATE
                sb.Replace(PRODUCTNAME, NomeDaCompra); // PRODUCT NAME
                sb.Replace(WALLETBALLANCE, "UP$" + MySQL.GetWalletValue.ToString()); // CURRENT WALLET BALLANCE
                sb.Replace(TOTAL, "UP$" + ValorDaCompra); // PURCHASE VALUE (TOTAL)
                sb.Replace(PRODPRICE, "UP$" + ValorDaCompra); // PURCHASE VALUE
                sb.Replace(UGNITEVERSION, versaoAtual); // CURRENT UGNITE'S VERSION


                // Envia um e-mail para o usuário
                ProgramData.EnviaMail(sb.ToString(), ProgramData.Mail, "Obrigado pela compra! - Ugnite");

                // Notifica o usuário do envio da compra
                ProgramData.NotificaUsuario("PAID", "O produto " + NomeDaCompra + " foi adicionado a sua biblioteca!", "Ugnite - Compra", 4);

            }
        }

        /// <summary>
        /// Envia um e-mail para o novo usuário
        /// </summary>
        /// <param name="Username">Nome od usuário</param>
        /// <param name="Usermail">E-mail do usuário</param>
        /// <param name="SecurityCode">Código de segurança</param>
        public static void EnviaMailNewUser(string Username, string Usermail, string SecurityCode)
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                // Lê o arquivo HTML 
                string msgFile = System.IO.File.ReadAllText(NEWUSER_EN);


                // Cria um novo texto
                StringBuilder sb = new StringBuilder(msgFile);

                // Substitui definições
                sb.Replace(UGNITEID, Username);
                sb.Replace(UGNITEMAIL, Usermail);
                sb.Replace(UGNITESECURECODE, SecurityCode);

                // Envia um e-mail para o usuário
                ProgramData.EnviaMail(sb.ToString(), ProgramData.Mail, "Account register confirmation - Ugnite");
            }
            else
            {
                // Lê o arquivo HTML 
                string msgFile = System.IO.File.ReadAllText(NEWUSER_BR);


                // Cria um novo texto
                StringBuilder sb = new StringBuilder(msgFile);

                // Substitui definições
                sb.Replace(UGNITEID, Username);
                sb.Replace(UGNITEMAIL, Usermail);
                sb.Replace(UGNITESECURECODE, SecurityCode);

                // Envia um e-mail para o usuário
                ProgramData.EnviaMail(sb.ToString(), ProgramData.Mail, "Confirmação de registro - Ugnite");

            }
        }

        /// <summary>
        /// Envia um e-mail para o usuário notificando sobre alterações de senha
        /// </summary>
        public static void EnviaMailSenha(string Username)
        {
            // Adquire o e-mail do usuário
            MySQL.GetInformation(2);

            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                // Lê o arquivo HTML 
                string msgFile = System.IO.File.ReadAllText(PASSRESET_EN);


                // Cria um novo texto
                StringBuilder sb = new StringBuilder(msgFile);

                // Substitui definições
                sb.Replace(UGNITEID, Username);

                // Envia um e-mail para o usuário
                ProgramData.EnviaMail(sb.ToString(), MySQL.GetData, "Password Notification - Ugnite");
            }
            else
            {
                // Lê o arquivo HTML 
                string msgFile = System.IO.File.ReadAllText(PASSRESET_BR);


                // Cria um novo texto
                StringBuilder sb = new StringBuilder(msgFile);

                // Substitui definições
                sb.Replace(UGNITEID, Username);

                // Envia um e-mail para o usuário
                ProgramData.EnviaMail(sb.ToString(), MySQL.GetData, "Notificação de Senha - Ugnite");

            }

        }

        /// <summary>
        /// Envia uma ocorrência de bug detalhado para a Ugnite
        /// </summary>
        /// <param name="mensagem">Ocorrência completa</param>
        public static void EnviaReport(string mensagem)
        {
            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                // Lê o arquivo HTML 
                string msgFile = System.IO.File.ReadAllText(BUGREPORT_EN);


                // Cria um novo texto
                StringBuilder sb = new StringBuilder(msgFile);

                // Substitui definições
                sb.Replace(UGNITEID, ProgramData.Username);

                // Substitui definições
                sb.Replace(BUGDESC, $"SYSTEM : Windows {Environment.OSVersion} (x64? = {Environment.Is64BitOperatingSystem.ToString().ToUpper()})\nDATE: {DateTime.UtcNow}\nREPORT: {mensagem}\nUSER E-MAIL: {ProgramData.Mail}");

                // Envia um e-mail para a Ugnite
                ProgramData.EnviaMail(sb.ToString(), "bugreport@ironiawn.com.br", $"Bug Report ({DateTime.Now}) | Ugnite");
            }
            else
            {
                // Lê o arquivo HTML 
                string msgFile = System.IO.File.ReadAllText(BUGREPORT_BR);


                // Cria um novo texto
                StringBuilder sb = new StringBuilder(msgFile);

                // Substitui definições
                sb.Replace(UGNITEID, ProgramData.Username);

                // Substitui definições
                sb.Replace(BUGDESC, $"SISTEMA : Windows {Environment.OSVersion} (x64? = {Environment.Is64BitOperatingSystem.ToString().ToUpper()})\nDATA: {DateTime.UtcNow}\nREPORT: {mensagem}\nE-MAIL DO USER: {ProgramData.Mail}");

                // Envia um e-mail para a Ugnite
                ProgramData.EnviaMail(sb.ToString(), "bugreport@ironiawn.com.br", $"Bug Report ({DateTime.Now}) | Ugnite");

            }

        }
    }
}
