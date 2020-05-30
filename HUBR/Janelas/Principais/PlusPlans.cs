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
    public partial class PlusPlans : Form
    {
        public PlusPlans()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuyWithWallet_Click(object sender, EventArgs e)
        {
            // Verifica se o usuário tem dinheiro suficiente na wallet
            if (MySQL.GetWalletValue >= 29.90f)
            {
                // Remove valor da carteira
                MySQL.WalletPayDebit(29.90f);

                // Ativa a HUBR Plus do usuário
                MySQL.ActivatePlus(15d);

                // Ativa o verificador
                VerifyPlusStatus();

                // Cria uma invoice
                MySQL.InsertInvoiceID("UGNITE+", "UPAY");


                if (Properties.Settings.Default["lang"].ToString() != "en")
                {
                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"0;COMPRA;UGNITE+;ADICIONADOS 14 DIAS POR R$29.90;{DateTime.Today.ToString()}");
                    MySQL.UpdateYourActivity($"0;FATURA;UGNITE+;ID: {MySQL.InvoiceID};{DateTime.Today.ToString()}");

                    // Exibe mensagem
                    ProgramData.MensagemSucesso($"SUCESSO NA COMPRA!\nFATURA: {MySQL.InvoiceID}\nPRODUTO: UGNITE+ 14 DIAS");
                }
                else
                {
                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"0;BUY;UGNITE+;ADDED 14 DAYS FOR R$29.90;{DateTime.Today.ToString()}");
                    MySQL.UpdateYourActivity($"0;INVOICE;UGNITE+;ID: {MySQL.InvoiceID};{DateTime.Today.ToString()}");

                    // Exibe mensagem
                    ProgramData.MensagemSucesso($"SUCCESSFUL PURCHASE!!\nINVOICE ID: {MySQL.InvoiceID}\nPRODUCT: UGNITE+ 14 DAYS");

                }
            }
            else
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                {
                    ProgramData.MensagemErro("SALDO INSUFICIENTE NA UPAY PARA ADQUIRIR A UGNITE+!");

                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"1;COMPRA;UGNITE+;SALDO INSUFICIENTE PARA 14 DIAS;{DateTime.Today.ToString()}");
                }
                else
                {
                    ProgramData.MensagemErro("INSUFFICIENT FUNDS AT UPAY WALLET.");

                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"1;BUY;UGNITE+;INSUFFICIENT FUNDS TO 14 DAYS;{DateTime.Today.ToString()}");

                }
            }
        }

        void VerifyPlusStatus()
        {
            // Verifica se o usuário é HUBR Plus
            MySQL.VerifyPlus();

            if (Properties.Settings.Default["lang"].ToString() != "en")
            {
                // Se houver, mostrar os dias restantes
                if (MySQL.RemainingDays > 0)
                    lbCurrentUserPlusDetails.Text = $"UGNITE+ ATIVO | {MySQL.RemainingDays} DIA(S) RESTANTE(S) | {MySQL.FormataValor(MySQL.GetWalletValue)} NA UPAY";
                else
                    lbCurrentUserPlusDetails.Text = $"UGNITE+ INATIVO | {MySQL.FormataValor(MySQL.GetWalletValue)} NA UPAY";
            }
            else
            {
                // Se houver, mostrar os dias restantes
                if (MySQL.RemainingDays > 0)
                    lbCurrentUserPlusDetails.Text = $"UGNITE+ ACTIVE | {MySQL.RemainingDays} DAY(S) REMAINING | {MySQL.FormataValor(MySQL.GetWalletValue)} AT UPAY WALLET";
                else
                    lbCurrentUserPlusDetails.Text = $"UGNITE+ INACTIVE | {MySQL.FormataValor(MySQL.GetWalletValue)} AT UPAY WALLET";

            }

        }

        void ApplyTranslation()
        {

            if (Properties.Settings.Default["lang"].ToString() == "en")
            {
                lbPlusPlans.Text = Program.GetStringRM("en", "lbPlusPlans");
                lbPlusInfos.Text = Program.GetStringRM("en", "lbPlusInfos");
                lb14Days.Text = Program.GetStringRM("en", "lb14Days");
                lb30Days.Text = Program.GetStringRM("en", "lb30Days");
                btnPay14Days.Text = Program.GetStringRM("en", "BuyWithUpay");
                btnPay30Days.Text = Program.GetStringRM("en", "BuyWithUpay");
            }
        }

        private void PlusPlans_Load(object sender, EventArgs e)
        {
            // Verifica o estado atual do plano do usuário
            VerifyPlusStatus();

            // Aplica tradução (se houver)
            ApplyTranslation();
        }

        private void btnPay30Days_Click(object sender, EventArgs e)
        {
            // Verifica se o usuário tem dinheiro suficiente na wallet
            if (MySQL.GetWalletValue >= 49.90f)
            {
                // Remove valor da carteira
                MySQL.WalletPayDebit(49.90f);

                // Ativa a HUBR Plus do usuário
                MySQL.ActivatePlus(31d);

                // Ativa o verificador
                VerifyPlusStatus();

                // Cria uma invoice
                MySQL.InsertInvoiceID("UGNITE+", "UPAY");


                if (Properties.Settings.Default["lang"].ToString() != "en")
                {
                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"0;COMPRA;UGNITE+;ADICIONADOS 30 DIAS POR R$49.90;{DateTime.Today.ToString()}");
                    MySQL.UpdateYourActivity($"0;FATURA;UGNITE+;ID: {MySQL.InvoiceID};{DateTime.Today.ToString()}");

                    // Exibe mensagem
                    ProgramData.MensagemSucesso($"SUCESSO NA COMPRA!\nFATURA: {MySQL.InvoiceID}\nPRODUTO: UGNITE+ 30 DIAS");
                }
                else
                {
                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"0;BUY;UGNITE+;ADDED 30 DAYS FOR R$49.90;{DateTime.Today.ToString()}");
                    MySQL.UpdateYourActivity($"0;INVOICE;UGNITE+;ID: {MySQL.InvoiceID};{DateTime.Today.ToString()}");

                    // Exibe mensagem
                    ProgramData.MensagemSucesso($"SUCCESSFUL PURCHASE!!\nINVOICE ID: {MySQL.InvoiceID}\nPRODUCT: UGNITE+ 30 DAYS");

                }
            }
            else
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                {
                    ProgramData.MensagemErro("SALDO INSUFICIENTE NA UPAY PARA ADQUIRIR A UGNITE+!");

                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"1;COMPRA;UGNITE+;SALDO INSUFICIENTE PARA 30 DIAS;{DateTime.Today.ToString()}");
                }
                else
                {
                    ProgramData.MensagemErro("INSUFFICIENT FUNDS AT UPAY WALLET.");

                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"1;BUY;UGNITE+;INSUFFICIENT FUNDS TO 30 DAYS;{DateTime.Today.ToString()}");

                }
            }
        }
    }
}
