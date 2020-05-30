using CefSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UGNITE
{
    public static class VerifyGameURL
    {
        public static void VerifyPenumbra(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0000");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-09f18bc6-c151-42ad-92bb-0acb70561b02";
            OpenGames.GameRunCode = 0;
            OpenGames.LastSeenGame = "Penumbra Collection";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR PENUMBRA COLLECTION";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR PENUMBRA COLLECTION | R$14,90";
            }

        }
        public static void VerifyMinecraft(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0001");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://ironiawn.com.br/HUBRX/Minecraft/pagamentoOK.html";
            OpenGames.GameRunCode = 1;
            OpenGames.LastSeenGame = "Minecraft JAVA Edition";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR MINECRAFT JAVA EDITION";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "ADICIONAR MINECRAFT JAVA EDITION A SUA BIBLIOTECA GRATUITAMENTE";
            }

        }
        public static void VerifyCrossCode(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0002");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-f1a174fb-a16d-4e3e-aa5d-331cccbe1d14";
            OpenGames.GameRunCode = 2;
            OpenGames.LastSeenGame = "Cross Code";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR CROSSCODE";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR CROSS CODE | R$19,89";
            }

        }
        public static void VerifyIrisFall(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0003");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-290d73f8-9d2a-4187-94b4-60173db32d57";
            OpenGames.GameRunCode = 3;
            OpenGames.LastSeenGame = "Iris Fall";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR IRIS FALL";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR IRIS FALL | R$17,90";
            }

        }
        public static void VerifyForestSiege(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0004");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://ironiawn.com.br/HUBRX/Forest Siege/pagamentoOK.html";
            OpenGames.GameRunCode = 4;
            OpenGames.LastSeenGame = "Forest Siege";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR FOREST SIEGE";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "ADICIONAR FOREST SIEGE A SUA BIBLIOTECA GRATUITAMENTE";
            }

        }
        public static void VerifyGTASA(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0005");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-73543e77-55fb-4bc7-b9e8-69ff125236f5";
            OpenGames.GameRunCode = 5;
            OpenGames.LastSeenGame = "GTA San Andreas";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR GTA SAN ANDREAS";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR GTA SAN ANDREAS e GTA SAN ANDREAS MULTIPLAYER | R$9,99";
            }

        }
        public static void VerifySOMA(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0006");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-ed062c61-2af3-440e-a6d5-1aaf5cce44d9";
            OpenGames.GameRunCode = 6;
            OpenGames.LastSeenGame = "SOMA";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR SOMA";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR O JOGO SOMA | R$19,90";
            }

        }
        public static void VerifyOUTLAST2(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0007");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-41d6d447-e2fe-4e37-a995-f604a355c9f5";
            OpenGames.GameRunCode = 7;
            OpenGames.LastSeenGame = "OUTLAST 2";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR OUTLAST 2";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR O JOGO OUTLAST 2 | R$19,90";
            }

        }
        public static void VerifySUPERMEATBOY(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0008");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-0548154e-af69-47cb-a1ec-6b712f61f963";
            OpenGames.GameRunCode = 8;
            OpenGames.LastSeenGame = "SUPER MEAT BOY";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR SUPER MEAT BOY";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR O JOGO SUPER MEAT BOY RACE MODE EDITION | R$9,90";
            }

        }
        public static void VerifyREMOTHEREDTF(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0009");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-22248d66-658b-4874-a0c3-f9bc47febf5c";
            OpenGames.GameRunCode = 9;
            OpenGames.LastSeenGame = "REMOTHERED";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR REMOTHERED: TORMENTED FATHERS HD";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR O JOGO REMOTHERED: TORMENTED FATHERS HD | R$14,90";
            }

        }
        public static void VerifyGTAIV(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0010");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-9cbfcff8-8290-4656-b1c6-047e05c42ff8";
            OpenGames.GameRunCode = 10;
            OpenGames.LastSeenGame = "GTA IV";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR GRAND THEFT AUTO: IV";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR O GRAND THEFT AUTO: IV | R$19,90";
            }

        }
        public static void VerifyCatherineClassic(Label URLShow, Button GameButton)
        {
            // Verifica se o usuário tem o jogo na conta
            MySQL.GetActivatedGame("0011");

            // Alterar botão
            GameButton.Visible = true;
            GameButton.Enabled = true;

            // Altera as propriedades de compra do jogo
            OpenGames.GameBuyURL = "https://www.mercadopago.com/mlb/checkout/start?pref_id=231792972-28eb98ef-1c8e-4dc9-9c35-d9848d8b2b64";
            OpenGames.GameRunCode = 11;
            OpenGames.LastSeenGame = "CATHERINE CLASSIC";

            // Se sim, exibir o botão para jogar
            if (MySQL.ActivatedByUser)
            {
                OpenGames.HasGame = true;
                GameButton.Text = "JOGAR CATHERINE CLASSIC";
            }
            else
            {
                OpenGames.HasGame = false;
                GameButton.Text = "COMPRAR CATHERINE CLASSIC | R$19,90";
            }

        }
    }
}
