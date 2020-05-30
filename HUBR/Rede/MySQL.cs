using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using UGNITE.Rede;

namespace UGNITE
{

    public static class MySQL
    {

        static MySqlConnection connection = null; // Conexão MySQL
        static MySqlDataReader Reader; // Leitura de dados do Banco de Dados

        /// <summary>
        /// Inicializador do script
        /// </summary>
        static MySQL()
        {
            Initialize();
        }

        /// <summary>
        /// Inicialização da conexão com o banco de dados
        /// </summary>
        static void Initialize()
        {
            try
            {
                string server = "ugniteohio.cx9kv59mpdra.us-east-2.rds.amazonaws.com";//SP"ugnite.cfdo1oj0ab4b.sa-east-1.rds.amazonaws.com";
                string database = "Ugnite";
                string uid = "Ironiawn";
                string password = "DG8bbnNDeHpHqGVczvPgHGP";//SP"3QFWH|rHw]NW>ssjqE"; 
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                connection = new MySqlConnection(connectionString);
                OpenGames.conString = connectionString;
            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO NA CONEXÃO COM O BANCO DE DADOS UGNITE");
                else
                    ProgramData.MensagemErro($"ERROR WHILE CONNECTING TO UGNITE'S DATABASE\n{ex}");

                Application.ExitThread();
            }
        }
        /// <summary>
        /// Fecha a conexão com a DB
        /// </summary>
        static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Verifica a existência de um usuário na base
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool GetUsuario(string user)
        {
            bool UE = false;
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reinicializa o Reader
                DbDataReader db;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("SELECT * FROM VAYNE_USERS;", connection);

                // Conecta com a base
                connection.Open();

                // Cria a reader do MySQL para ler valores disponíveis
                db = Command.ExecuteReader();

                while (db.Read())
                {
                    // Lê o campo de nomes
                    if (db["nome"].ToString() == user)
                        UE = true; // Marca que o usuário já existe
                }

                // Fecha a conexão
                connection.Close();
            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO NA CONEXÃO COM O BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO GUS_115");
                else
                    ProgramData.MensagemErro($"ERROR WHILE CONNECTING WITH UGNITE DATABASE.\nERROR_CODE: GUS_117\nUGNITE WILL BE CLOSED.\n{ex}");

                Application.ExitThread();
            }

            return UE;
        }



        /// <summary>
        /// Verificador se a senha para o usuário é válida
        /// </summary>
        public static bool ValidPassword = false;
        /// <summary>
        /// Verifica se a senha fornecida para o usuário é valida
        /// </summary>
        /// <param name="pass">Senha(enviada pela form de login)</param>
        public static void VerifyPassword(string pass)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reinicializa o valor do verificador de senha válida
                ValidPassword = false;

                // Reinicializa o Reader
                Reader = null;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("SELECT password FROM VAYNE_USERS WHERE nome = @user", connection);
                Command.Parameters.AddWithValue("@user", ProgramData.Username); // Substitui a @user pelo usuário ativo

                // Conecta com a base
                connection.Open();

                // Cria a reader do MySQL para ler valores disponíveis
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    // Lê o campo de senha
                    if (Encryptor.Decrypt(Reader.GetString("password"), "HUBR_VAYNE") != pass)
                    {
                        // A senha não é válida. Exibe mensagem de erro.
                        //ProgramData.MensagemErro("SENHA INCORRETA PARA A IDENTIFICAÇÃO INFORMADA.\nPOR FAVOR, VERIFIQUE E TENTE ACESSAR COM A SENHA CORRETA.\n\nCASO TENHA ESQUECIDO A SENHA, ENTRE EM CONTATO COM A UGNITE VIA EMAIL OU CHAT, INFORMANDO SEU CÓDIGO DE SEGURANÇA.\n" +
                        //    "E-MAIL HUBR.: shop@ironiawn.com.br");
                        ValidPassword = false;
                    }
                    else
                        ValidPassword = true; // A senha é válida.
                }

                // Fecha a conexão
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO DE CONEXÃO NO BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO PW155");
                else
                    ProgramData.MensagemErro("ERROR WHILE CONNECTING WITH UGNITE DATABASE.\nERROR_CODE: PW178");
            }
        }


        /// <summary>
        /// Verificação se o e-mail existe na base de dados
        /// </summary>
        public static bool MailExiste = false;
        /// <summary>
        /// Verifica se já não há um e-mail sendo utilizado por outro ID
        /// </summary>
        /// <param name="mail">E-Mail</param>
        public static void GetMail(string mail)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reinicializa o valor de existência de e-mail
                MailExiste = false;

                // Reinicializa o Reader
                DbDataReader db;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("SELECT * FROM VAYNE_USERS;", connection);

                // Conecta com a base
                connection.Open();

                // Cria a reader do MySQL para ler valores disponíveis
                db = Command.ExecuteReader();

                while (db.Read())
                {
                    // Tenta decriptar o e-mail
                    // se der erro, parar
                    try
                    {
                        // Lê o campo de nomes
                        if (Encryptor.Decrypt(db["email"].ToString(), "HUBR_VAYNE") == mail)
                        {
                            MailExiste = true; // Dizer ao validador que existe um e-mail cadastrado
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                ProgramData.MensagemErro("JÁ EXISTE UM USUÁRIO COM ESSE E-MAIL!\nPOR FAVOR, UTILIZE OUTRO EMAIL E TENTE NOVAMENTE."); // Exibe erro
                            else
                                ProgramData.MensagemErro("THERE'S A USER USING THIS E-MAIL ALREADY!\nCHOOSE ANOTHER ONE."); // Exibe erro
                        }
                    }
                    catch
                    {
                        // O e-mail não existe, prosseguir
                        MailExiste = false;
                    }
                }

                // Fecha a conexão
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO NA CONEXÃO COM O BANCO DE DADOS DA UGNITE!\nTENVE NOVAMENTE MAIS TARDE.\nCÓDIGO GETMA210");
                else
                    ProgramData.MensagemErro("ERROR WHILE CONNECTING WITH UGNITE'S DATABASE.\nERROR_CODE: GETMA235");

            }
        }

        /// <summary>
        /// Informação adquirida pela GetInformation(LISTA=TRUE, INFO=LISTAS)
        /// </summary>
        public static List<string> GetDataList;
        /// <summary>
        /// [APENAS PARA ADQUIRIR LISTAS]
        /// [APENAS USUÁRIOS ATIVOS NA SESSÃO VIA _PROGRAMDATA_]\
        /// Adquire qualquer informação sobre o usuário na base
        /// 0 = ID;
        /// 1 = NOME DE USUÁRIO;
        /// 2 = EMAIL DO USUÁRIO;
        /// 3 = SENHA DO USUÁRIO;
        /// 4 = VERIFICA SE É DEV/PUBLISHER DE GAMES
        /// 5 = CÓDIGO DE SEGURANÇA DA CONTA
        /// 6 = IMAGEM DO USUÁRIO
        /// </summary>
        public static void GetGames()
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reinicia as informações anteriores
                GetDataList = new List<string>();
                GetDataList.Clear();

                // Reinicializa o Reader
                Reader = null;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("SELECT GAMECODE FROM VAYNE_GAMES WHERE OWNBY=@user", connection);
                Command.Parameters.AddWithValue("@user", ProgramData.Username); // Substitui a @user pelo usuário ativo na sessão via ProgramData


                // Conecta com a base
                connection.Open();

                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    GetDataList.Add(OpenGames.AvailableGames[int.Parse(Reader["GAMECODE"].ToString())]);
                }

                Reader.Close();
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"HÁ UM ERRO NO SEU USUÁRIO DENTRO DA BASE UGNITE.\nENTRE EM CONTATO PARA CORRIGIR.E-MAIL: shop@ironiawn.com.br");
                else
                    ProgramData.MensagemErro($"ERROR WHILE CONNECTING TO UGNITE'S DATABASE.\nERROR_CODE: GETG293");

                Application.ExitThread();
            }
        }


        /// <summary>
        /// Informação adquirida via GetInformation(int)
        /// </summary>
        public static string GetData = null;
        /// <summary>
        /// [APENAS USUÁRIOS ATIVOS NA SESSÃO VIA _PROGRAMDATA_]
        /// Adquire qualquer informação sobre o usuário na base
        /// 0 = ID;
        /// 1 = NOME DE USUÁRIO;
        /// 2 = EMAIL DO USUÁRIO;
        /// 3 = SENHA DO USUÁRIO;
        /// 4 = VERIFICA SE É DEV/PUBLISHER DE GAMES
        /// 5 = CÓDIGO DE SEGURANÇA DA CONTA
        /// 6 = IMAGEM DO USUÁRIO
        /// 7 = HUBR PLUS
        /// 8 = VERSÃO DO CLIENTE HUBR
        /// 9 = DATA PLUS INICIO [RESTRITO]
        /// 10 = IP DO USUÁRIO
        /// 11 = DATA DE INICIO DE SESSÃO
        /// 12 = DATA DE FIM DE SESSÃO
        /// 13 = IDIOMA
        /// </summary>
        public static void GetInformation(int Info)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // String coluna para ser selecionada
                string[] columnID = new string[15];
                columnID[0] = "ID";
                columnID[1] = "nome";
                columnID[2] = "email";
                columnID[3] = "password";
                columnID[4] = "publisher";
                columnID[5] = "SecurityCode";
                columnID[6] = "image";
                columnID[7] = "PLUS";
                columnID[8] = "HUBR_VERSION";
                columnID[9] = "PLUSDATE";
                columnID[10] = "USER_IP";
                columnID[11] = "LOGINDATE";
                columnID[12] = "LASTLOGIN";
                columnID[13] = "LANG";

                // Reinicia as informações anteriores
                GetData = null;

                // Reinicializa o Reader
                Reader = null;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand(" SELECT " + columnID[Info].ToString() + " FROM VAYNE_USERS WHERE nome=@user", connection);
                Command.Parameters.AddWithValue("@user", ProgramData.Username); // Substitui a @user pelo usuário ativo na sessão via ProgramData


                // Conecta com a base
                connection.Open();

                // Cria a reader do MySQL para ler valores disponíveis
                Reader = Command.ExecuteReader();

                while (Reader.Read())
                {
                    // Verifica se não é o SecurityCode
                    if (Info == 5)
                        GetData = Encryptor.Decrypt(Reader.GetString(columnID[5]), "HUBR_USW");

                    // Verifica se a informação foi a senha ou e-mail
                    if (Info == 3 || Info == 2)
                        // Desencripta o valor e devolve ele "limpo" para a string temporária
                        GetData = Encryptor.Decrypt(Reader.GetString(columnID[Info]), "HUBR_VAYNE");

                    // Não é Código de segurança, email ou senha
                    if (Info != 3 && Info != 2 && Info != 5)
                        // Seta o valor adquirido para a string temporária
                        GetData = Reader.GetString(columnID[Info]);

                }


                // Fecha a conexão
                connection.Close();
            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO NA CONEXÃO COM A BASE DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO GETI385");
                else
                    ProgramData.MensagemErro($"ERROR WHILE CONNECTING TO UGNITE'S DATABASE.\nERROR_CODE: GETI387\n{ex}");

            }
        }


        /// <summary>
        /// Adquire o valor atual da carteira do usuário
        /// </summary>
        public static float GetWalletValue
        {
            get
            {
                try
                {
                    // Inicializa
                    Initialize();

                    // Cria um reader
                    MySqlDataReader dr;

                    // Comando para executar
                    MySqlCommand Command = new MySqlCommand("SELECT Wallet FROM VAYNE_USERS WHERE nome=@user", connection);
                    Command.Parameters.AddWithValue("@user", ProgramData.Username); // Substitui a @user pelo usuário ativo na sessão via ProgramData
                    Command.CommandType = System.Data.CommandType.Text;

                    // Verifica a conexão
                    if (connection.State != System.Data.ConnectionState.Open)
                        connection.Open();


                    // Cria a reader do MySQL para ler valores disponíveis
                    dr = Command.ExecuteReader();

                    // Lê os valores disponíveis na carteira do usuário
                    while (dr.Read())
                        return float.Parse(dr["Wallet"].ToString()); // Converte em número inteiro e retorna
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        ProgramData.MensagemErro($"ERRO NA LEITURA DE DADOS!\nERRO: WL532\nA UGNITE SERÁ FECHADA!");
                    else
                        ProgramData.MensagemErro($"ERROR WHILE READING DATA!\nERROR_CODE: WL430\nUGNITE WILL BE CLOSED.");

                    // Manda um bug report
                    ClassesMail.EnviaReport(ex.ToString());

                    Application.Exit();
                }
                finally
                {
                    // Fecha a conexão com a DB
                    connection.Close();
                }

                return -0.1f;
            }
        }

        /// <summary>
        /// Formata um valor para a moeda do usuário
        /// </summary>
        public static string FormataValor(float val)
        {

            // obtém a cultura local
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            // faz uma cópia das informações de formatação de número da cultura local
            var numberFormatInfo = (System.Globalization.NumberFormatInfo)cultureInfo.NumberFormat.Clone();
            // obtém o valor em moeda estrangeira formatado conforme a cultura local
            var valorFormatado = string.Format(numberFormatInfo, "{0:C}", val);
            

            // Retorna o valor da carteira formatado
            return valorFormatado;
        }

        /// <summary>
        /// Verifica se o usuário está banido dA UGNITE
        /// </summary>
        public static string Banned
        {
            get
            {
                try
                {
                    // Inicializa a conexão
                    Initialize();

                    // Cria uma nova reader
                    DbDataReader X = null;

                    // Comando para executar
                    MySqlCommand Command = new MySqlCommand("SELECT * FROM VAYNE_USERS WHERE nome=@user", connection);
                    Command.Parameters.AddWithValue("@user", ProgramData.Username); // Substitui a @user pelo usuário ativo na sessão via ProgramData


                    // Conecta com a base
                    connection.Open();

                    // Seta o reader
                    X = Command.ExecuteReader();

                    // Lê a linha em que o usuário está
                    while (X.Read())
                    {
                        if (X["Banned"].ToString().Length > 0)
                            return X["Banned"].ToString();
                        else
                            return "-1";
                    }

                    // Fecha a conexão
                    connection.Close();
                }
                catch
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        ProgramData.MensagemErro($"ERRO NA CONEXÃO COM A BASE DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO BGET537");
                    else
                        ProgramData.MensagemErro($"CRITICAL ERROR WHILE CONNECTING TO THE UGNITE'S DATABASE.\nERROR_CODE BGET503\nUGNITE WILL BE CLOSED.");

                    Application.Exit();
                }

                return "-1";
            }
        }

        /// <summary>
        /// [REMOVE] O valor definido da carteira do usuário
        /// </summary>
        /// <param name="i">VALOR QUE VAI SER REMOVIDO</param>
        /// <returns></returns>
        public static void WalletPayDebit(float i)
        {
            try
            {
                // Inicializa
                Initialize();

                // Verifica a conexão
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("UPDATE VAYNE_USERS SET Wallet=@info WHERE nome=@user", connection);
                Command.Parameters.AddWithValue("@user", ProgramData.Username); // Substitui a @user pelo usuário ativo na sessão via ProgramData
                Command.Parameters.AddWithValue("@info", (GetWalletValue - i).ToString()); // Altera o valor para o definido


                // Executa a query
                Command.ExecuteNonQuery();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO NA LEITURA DE DADOS!\nERRO: WL540\nAUGNITE SERÁ FECHADA!");
                else
                    ProgramData.MensagemErro($"ERROR WHILE READING DATA!\nERROR_CODE: WL542\nUGNITE WILL BE CLOSED.");

                Application.Exit();
            }
            finally
            {
                // Fecha a conexão com a DB
                connection.Close();
            }
        }

        /// <summary>
        /// Utilizada para verificar se a informação na base foi atualizada
        /// </summary>
        public static bool InfoUpdated = false;
        /// <summary>
        /// [APENAS USUÁRIOS ATIVOS NA SESSÃO VIA _PROGRAMDATA_]
        /// Atualiza os dados do atual usuário ativo na sessão via ProgramData
        /// 0 = ID;
        /// 1 = NOME DE USUÁRIO;
        /// 2 = EMAIL DO USUÁRIO;
        /// 3 = SENHA DO USUÁRIO;
        /// 4 = VERIFICA SE É DEV/PUBLISHER DE GAMES
        /// 5 = CÓDIGO DE SEGURANÇA DA CONTA
        /// 6 = IMAGEM DA CONTA
        /// 7 = HUBR PLUS
        /// 8 = VERSÃO DO CLIENTE HUBR
        /// 9 = DATA PLUS INICIO [RESTRITO]
        /// 10 = IP DO USUÁRIO
        /// 11 = DATA DE INICIO DE SESSÃO
        /// 12 = DATA DE FIM DE SESSÃO
        /// 13 = IDIOMA
        /// </summary>
        public static void UpdateInformation(int Info, string Information)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reseta a informação de atualizador
                InfoUpdated = false;

                // String coluna para ser selecionada
                string[] columnID = new string[15];
                columnID[0] = "ID";
                columnID[1] = "nome";
                columnID[2] = "email";
                columnID[3] = "password";
                columnID[4] = "publisher";
                columnID[5] = "SecurityCode";
                columnID[6] = "image";
                columnID[7] = "PLUS";
                columnID[8] = "HUBR_VERSION";
                columnID[9] = "PLUSDATE";
                columnID[10] = "USER_IP";
                columnID[11] = "LOGINDATE";
                columnID[12] = "LASTLOGIN";
                columnID[13] = "LANG";


                // Comando para executar
                MySqlCommand Command = new MySqlCommand(" UPDATE VAYNE_USERS SET " + columnID[Info] + "=@info WHERE nome=@user", connection);
                // Primeiro, verifica se não é senha/e-mail que vão ser atualizados na base
                if (Info != 3 && Info != 2)
                {
                    // Adiciona os comandos que serão executados posteriormente
                    Command.Parameters.AddWithValue("@info", Information); // Informação que vai substituir a atual 
                    Command.Parameters.AddWithValue("@user", ProgramData.Username); // Utiliza o usuário ativo na sessão via ProgramData
                }
                else
                {
                    // Adiciona os comandos que serão executados posteriormente via criptografia
                    Command.Parameters.AddWithValue("@user", ProgramData.Username); // Utiliza o usuário ativo na sessão via ProgramData
                    Command.Parameters.AddWithValue("@info", Encryptor.Encrypt(Information, "HUBR_VAYNE")); // Criptografa os dados para serem atualizados na base
                }

                // Conecta com a base
                connection.Open();

                // Cria a reader do MySQL para ler valores disponíveis
                Reader = Command.ExecuteReader();

                // Verifica se a informação fornecida condiz com a atual na base de dados
                GetInformation(Info);

                // Verifica na string se ela é igual a fornecida
                if (GetData != Information)
                    InfoUpdated = false; // ÑÃO foi atualizada na base por um erro.
                else
                    InfoUpdated = true; // FOI atualizada na base com sucesso.

                // Fecha a conexão
                connection.Close();
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO AO ATUALIZAR INFORMAÇÕES NO BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO UPIN406\n{ex}");
                else
                    ProgramData.MensagemErro("ERROR WHILE UPDATING USER'S INFORMATION.\nTRY AGAIN LATER.");

            }
        }

        /// <summary>
        /// Atividade adquirida
        /// </summary>
        public static List<string> ActivitiesGet = new List<string>();
        /// <summary>
        /// Adquire a última atividade do usuário logado
        /// </summary>
        public static void GetYourActivity(string Username)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reinicializa a lista
                ActivitiesGet.Clear();

                // Cria um reader
                MySqlDataReader read;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("SELECT * FROM VAYNE_YOURACTIVITY WHERE USERNAME=@user", connection);
                Command.Parameters.AddWithValue("@user", Username); // Substitui a @user pelo usuário ativo na sessão via ProgramData

                // Conecta com a base
                connection.Open();

                // Cria a reader do MySQL para ler valores disponíveis
                read = Command.ExecuteReader();

                while (read.Read())
                    ActivitiesGet.Add(read["FEED"].ToString());
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO NA CONEXÃO COM A BASE DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO GETACT_678");
                else
                    ProgramData.MensagemErro($"ERROR WHILE DOWNLOADING DATABASE INFORMATION.\nERROR_CODE: GETACT_680");

            }
            finally
            {
                // Fecha a conexão
                connection.Close();
            }
        }

        /// <summary>
        /// Adiciona atividades relacionadas ao usuário
        /// </summary>
        /// <param name="ActionTaken">ATIVIDADE REALIZADA</param>
        public static void UpdateYourActivity(string ActionTaken)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Comando para executar
                MySqlCommand Command = new MySqlCommand(" INSERT INTO VAYNE_YOURACTIVITY (USERNAME, FEED) VALUES(@user, @info)", connection);

                // Adiciona os comandos que serão executados posteriormente
                Command.Parameters.AddWithValue("@info", ActionTaken); // Informação que vai substituir a atual 
                Command.Parameters.AddWithValue("@user", ProgramData.Username); // Utiliza o usuário ativo na sessão via ProgramData

                // Conecta com a base
                connection.Open();

                // Executa o comando de atualização
                Command.ExecuteNonQuery();

                // Fecha a conexão
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO AO ATUALIZAR INFORMAÇÕES NO BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO UPDTACT_445");
                else
                    ProgramData.MensagemErro($"ERROR WHILE UPDATING USER'S INFORMATION.\nERROR_CODE: UPDTINFO_722");

            }
        }

        /// <summary>
        /// Cria um novo usuário para A UGNITE
        /// </summary>
        /// <param name="user">ID</param>
        /// <param name="pass">Senha</param>
        /// <param name="mail">E-Mail</param>
        public static void CriaUsuario(string user, string pass, string mail, int pub)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reseta a informação anterior adquirida
                GetData = null;

                // Cria um código aleatório para segurança da conta do usuário
                string SecurityCode = RandomString(6);

                // Verifica a versão atual dA UGNITE
                string versaoAtual = $"{System.IO.File.ReadAllText(Environment.CurrentDirectory + @"\u.sys\versao.txt")}@{Application.ProductVersion}";

                // Faz o comando QUERY
                MySqlCommand Command = new MySqlCommand($"INSERT INTO VAYNE_USERS (PLUS, PLUSDATE, nome, email, password, publisher, SecurityCode, image, Wallet, Banned , HUBR_VERSION, USER_IP, LOGINDATE, LASTLOGIN, LANG)" +
                    $" VALUES ('0','1ª USER', @user, @mail, @pw, @pub, @SecurityCode, @img, '0', '','{versaoAtual}'," +
                    $" @Ip, @LoginDate, 'FIRSTLOGIN', @lang)", connection);

                // Faz a segurança dos dados 
                Command.Parameters.AddWithValue("@user", user);
                Command.Parameters.AddWithValue("@mail", Encryptor.Encrypt(mail, "HUBR_VAYNE"));
                Command.Parameters.AddWithValue("@pw", Encryptor.Encrypt(pass, "HUBR_VAYNE"));
                Command.Parameters.AddWithValue("@pub", pub.ToString());
                Command.Parameters.AddWithValue("@SecurityCode", Encryptor.Encrypt(SecurityCode, "HUBR_USW"));
                Command.Parameters.AddWithValue("@img", "https://cdn.dicionariopopular.com/imagens/pistola-og.jpg");
                Command.Parameters.AddWithValue("@Ip", getExternalIp());
                Command.Parameters.AddWithValue("@lang", Properties.Settings.Default["lang"].ToString());
                Command.Parameters.AddWithValue("@LoginDate", DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR", true)));

                // Abre a conexão
                connection.Open();

                // Executa a QUERY e adiciona os valores ao banco de dados
                Command.ExecuteNonQuery();

                // Verifica se o usuário existe
                if (GetUsuario(user)) // Verificação de utilização da ID
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Exibe a mensagem pelo diálogo próprio
                        ProgramData.MensagemSucesso("CADASTRO CRIADO COM SUCESSO!\n\nUSUÁRIO.: " + user + " \nEMAIL.: " + mail + "\nGRAVE ESSE CÓDIGO: " + SecurityCode + "\nELE É MUITO IMPORTANTE PARA RECUPERAÇÃO/ATUALIZAÇÃO DE DADOS!");
                    else
                        // Exibe a mensagem pelo diálogo próprio
                        ProgramData.MensagemSucesso("USER CREATED!\n\nUSER ID.: " + user + " \nEMAIL.: " + mail + "\nUGNITE'S SECURE CODE: " + SecurityCode + "\nDON'T FORGET THIS CODE\nHE'LL BE USED LATER FOR FURTHER INFORMATION CHANGING!");

                    // Cadastra, de modo temporário, os dados no ProgramData
                    ProgramData.Username = user; // Usuário ativo
                    ProgramData.Mail = mail; // Email do usuário

                    // Envia um e-mail para o usuário
                    Rede.ClassesMail.EnviaMailNewUser(ProgramData.Username, ProgramData.Mail, SecurityCode);
                }

                // Fecha conexão
                connection.Close();

                // Abre conexão
                connection.Open();

                // Cria um feed de atividade
                // Faz o comando QUERY
                MySqlCommand ActivityCommand = new MySqlCommand("INSERT INTO VAYNE_YOURACTIVITY (USERNAME, FEED) VALUES (@user, @activity)", connection);

                // Faz a segurança dos dados 
                ActivityCommand.Parameters.AddWithValue("@user", user);
                ActivityCommand.Parameters.AddWithValue("@activity", $"0;-;-;-;{DateTime.Today.ToString()}");

                // Executa a query
                ActivityCommand.ExecuteNonQuery();

                // Fecha a conexão
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO NA CONEXÃO COM O BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO NWU1062");
                else
                    ProgramData.MensagemErro($"ERROR WHILE CREATING NEW USER.\nTRY AGAIN LATER.");

            }
        }

        // Cria uma aleatoriedade 
        private static Random random = new Random();
        /// <summary>
        /// Cria uma string aleatória
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Verificador.
        /// Se o jogo verificado foi ativado pelo usuário da sessão atual ou não
        /// GetActivatedGame(Código do Jogo)
        /// </summary>
        public static bool ActivatedByUser = false;

        /// <summary>
        /// Verifica se um jogo foi ativado pelo usuário da sessão
        /// </summary>
        /// <param name="Code">Código do jogo</param>
        public static void GetActivatedGame(string Code)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reinicia as informações anteriores
                GetData = null;

                // Reinicializa o Reader
                Reader = null;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("SELECT OWNBY FROM VAYNE_GAMES WHERE GAMECODE=@code", connection);
                Command.Parameters.AddWithValue("@code", Code); // Verifica o código do jogo


                // Conecta com a base
                connection.Open();

                // Cria a reader do MySQL para ler valores disponíveis
                Reader = Command.ExecuteReader();


                while (Reader.Read())
                {
                    // Verifica se quem ativou o jogo foi o usuário da sessão via ProgramData
                    if (Reader.GetString("OWNBY") == ProgramData.Username)
                        ActivatedByUser = true;
                }

                // Fecha a conexão
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO NA CONEXÃO COM O BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO GETACG524");
                else
                    ProgramData.MensagemErro("ERROR WHILE ACTIVATING GAME KEY.\nERROR_CODE: GETAC_881");

            }
        }

        /// <summary>
        /// Tenta ativar a chave de ativação para Steam
        /// </summary>
        /// <param name="Key"></param>
        public static void RetrievePlatformKey(string Key)
        {
            try
            {
                if (Key.ToUpper().StartsWith("STEAM") || Key.ToUpper().StartsWith("UPLAY")
                    || Key.ToUpper().StartsWith("ORIGIN"))
                {
                    // Se conecta na DB
                    Initialize();

                    // Cria o comando 
                    MySqlCommand COM = new MySqlCommand("SELECT * FROM VAYNE_OTHERPLATFORM WHERE GAMECODE=@Info", connection);
                    COM.Parameters.AddWithValue("@Info", Key.ToUpper());

                    // Zera a reader
                    Reader = null;

                    // Conecta-se a base
                    connection.Open();

                    // Seta a reader
                    Reader = COM.ExecuteReader();

                    // Executa o comando de leitura na DB
                    while (Reader.Read())
                    {
                        // Adquire temporáriamente os valores
                        string GN = Reader["GAMENAME"].ToString();

                        // Verifica se a chave já se encontra ativada na DB
                        if (Reader["ACTIVATED"].ToString() == "1")
                            if (Properties.Settings.Default["lang"].ToString() != "en")
                                MessageBox.Show("CHAVE DE ATIVAÇÃO JÁ RESGATADA!", "Ugnite - Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show("KEY ALREADY TAKEN!", "Ugnite - Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                        else
                        {
                            // Ativa a chave
                            InvalidatePlatformKey(Key);

                            if (Properties.Settings.Default["lang"].ToString() != "en")
                            {
                                // Posta a chave na atividade do usuário
                                // Atualiza registro do usuário
                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"0;ATIVAÇÃO;JOGO {GN.ToString().ToUpper()};CHAVE: {Reader["PLATFORMKEY"].ToString().ToUpper()};{DateTime.Today.ToString()}");

                                // Mostra mensagem de sucesso
                                ProgramData.MensagemSucesso($"CHAVE ATIVADA COM SUCESSO!\n\nDADOS DO JOGO:\nNOME: {GN.ToUpper()}\nCHAVE DE ATIVAÇÃO: {Reader["PLATFORMKEY"].ToString().ToUpper()}\n\nCHAVE DISPONIBILIZADA EM SUAS ATIVIDADES.\nATIVE NA PLATAFORMA EM QUE A CHAVE PERTENCE.");
                            }
                            else
                            {
                                // Posta a chave na atividade do usuário
                                // Atualiza registro do usuário
                                // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                                MySQL.UpdateYourActivity($"0;ACTIVATION;GAME: {GN.ToString().ToUpper()};KEY: {Reader["PLATFORMKEY"].ToString().ToUpper()};{DateTime.Today.ToString()}");

                                // Mostra mensagem de sucesso
                                ProgramData.MensagemSucesso($"ACTIVATION KEY ENABLED!\n\nGAME DATA:\nNAME: {GN.ToUpper()}\nACTIVATION KEY: {Reader["PLATFORMKEY"].ToString().ToUpper()}\n\nKEY ADDED TO YOUR ACTIVITY LOG.\nACTIVATE IT AT KEY'S PLATFORM.");

                            }
                        }
                    }
                }
                else
                {
                    if (Properties.Settings.Default["lang"].ToString() != "en")
                    {
                        MessageBox.Show("CHAVE DE ATIVAÇÃO PARA PLATAFORMA EXTERNA INVÁLIDA.", "Ugnite - ERRO",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        MessageBox.Show("INVALID EXTERNAL PLATFORM KEY.", "Ugnite - ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO GRAVE DE EXECUÇÃO DO APLICATIVO!\nTENTE NOVAMENTE MAIS TARDE.\n A UGNITE SERÁ FECHADA.\nCÓDIGO AEPK974");
                else
                    ProgramData.MensagemErro($"ERROR WHILE RETRIEVIING THE KEY.\nERROR_CODE: AEPK976\nUGNITE WILL BE CLOSED.");

                Application.Exit();
            }
            finally
            {
                // Verifica se a conexão com a DB está aberta
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Verificador.
        /// Se a chave fornecida[GAME] é válida e não foi ativada
        /// VerifyGameKey(CHAVE)
        /// </summary>
        static bool ValidGameKey = false;
        /// <summary>
        /// Verificador.
        /// Código fornecido pela VerifyGameKey(CHAVE DO JOGO)
        /// </summary>
        static string GameCode = "";

        /// <summary>
        /// Verifica se a chave é válida e não foi resgatada anteriormente
        /// </summary>
        /// <param name="Key">CHAVE DE JOGO</param>
        static void VerifyGameKey(string Key)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Reinicia as informações anteriores
                GetData = null;

                // Reinicializa o Reader
                Reader = null;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("SELECT * FROM VAYNE_KEYS WHERE GAMEKEY=@Key", connection);
                Command.Parameters.AddWithValue("@Key", Key); // Verifica a chave fornecida


                // Conecta com a base
                connection.Open();

                // Cria a reader do MySQL para ler valores disponíveis
                Reader = Command.ExecuteReader();


                while (Reader.Read())
                {
                    // Verifica se a chave não foi adquirida anteriormente
                    if (Reader.GetString("REDEEM") != "1")
                    {
                        // A chave é válida
                        ValidGameKey = true;

                        // Seta o código de jogo que será ativado
                        GameCode = Reader.GetString("GAMECODE");
                    }
                }

                // Fecha a conexão
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO NA CONEXÃO COM O BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO VERGK1048");
                else
                    ProgramData.MensagemErro("ERROR WHILE GETTING INFORMATION AT UGNITE'S DATABASE.\nERROR_CODE: VERGK_1050");

            }
        }

        /// <summary>
        /// Invalida uma chave previamente ativada
        /// </summary>
        /// <param name="Key">CHAVE DE JOGO</param>
        static void InvalidateKey(string Key)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Faz o comando QUERY
                MySqlCommand Command = new MySqlCommand("UPDATE VAYNE_KEYS SET REDEEM=@Red, ACTIVATIONIP=@IP, ACTIVATEDBY=@User, ACTIVATIONDATE=@Date WHERE GAMEKEY=@key", connection);

                // Faz a segurança dos dados 
                Command.Parameters.AddWithValue("@key", Key); // A chave que será invalidada
                Command.Parameters.AddWithValue("@Red", "1"); // Diz ao comando que a chave foi ativada
                Command.Parameters.AddWithValue("@User", ProgramData.Username); // Seta quem ativou a chave
                Command.Parameters.AddWithValue("@IP", getExternalIp()); // Pega o IP de quem ativou e manda para a base
                Command.Parameters.AddWithValue("@Date", DateTime.Now); // Data em que foi ativado

                // Abre a conexão
                connection.Open();

                // Executa a QUERY e adiciona os valores ao banco de dados
                Command.ExecuteNonQuery();

                // Fecha a conexão
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO GRAVE DE EXECUÇÃO DO APLICATIVO!\nTENTE NOVAMENTE MAIS TARDE.\n A UGNITE SERÁ FECHADA.\nCÓDIGO ISGK1088");
                else
                    ProgramData.MensagemErro("CRITICAL ERROR WHILE SENDING INFORMATION TO UGNITE'S DATABASE.\nUGNITE WILL BE CLOSED.\nERROR_CODE: ISGK1090");
                Application.Exit();
            }
        }

        /// <summary>
        /// Adquire o IP do usuário
        /// </summary>
        /// <returns></returns>
        public static string getExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new System.Net.WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new System.Text.RegularExpressions.Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }

        /// <summary>
        /// Invalida uma chave previamente ativada para Steam
        /// </summary>
        /// <param name="Key">CHAVE DE JOGO</param>
        static void InvalidatePlatformKey(string Key)
        {
            try
            {
                // Inicializa a conexão
                Initialize();

                // Faz o comando QUERY
                MySqlCommand Command = new MySqlCommand("UPDATE VAYNE_OTHERPLATFORM SET ACTIVATED=@Red, ACTIVATEDBY=@User, ACTIVATIONDATE=@Date, ACTIVATIONIP=@IP WHERE GAMECODE=@key", connection);

                // Faz a segurança dos dados 
                Command.Parameters.AddWithValue("@key", Key); // A chave que será invalidada
                Command.Parameters.AddWithValue("@User", ProgramData.Username); // O usuário que ativou a chave
                Command.Parameters.AddWithValue("@Date", System.DateTime.Now); // A data completa de ativação
                Command.Parameters.AddWithValue("@IP", getExternalIp()); // A data completa de ativação
                Command.Parameters.AddWithValue("@Red", "1"); // Diz ao comando que a chave foi ativada

                // Abre conexão
                connection.Open();

                // Executa a QUERY e adiciona os valores ao banco de dados
                Command.ExecuteNonQuery();

                // Fecha conexão
                connection.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO GRAVE DE EXECUÇÃO DO APLICATIVO!\nTENTE NOVAMENTE MAIS TARDE.\n A UGNITE SERÁ FECHADA.\nCÓDIGO ISPK1145");
                else
                    ProgramData.MensagemErro("CRITICAL ERROR WHILE SENDING INFORMATION TO UGNITE'S DATABASE.\nUGNITE WILL BE CLOSED.\nERROR_CODE: ISPK1147");

                Application.Exit();
            }
        }
        /// <summary>
        /// Ativa um jogo para o usuário
        /// </summary>
        /// <param name="Key">CÓDIGO DO JOGO</param>
        public static void ActivateGameKey(string Key)
        {
            try
            {
                // Primeiro, verifica se a chave não foi resgatada antes ou se existe
                VerifyGameKey(Key);

                // Se a chave é válida, ativar o jogo para a chave do usuário
                if (ValidGameKey)
                {
                    // Inicializa a conexão
                    Initialize();

                    // Reseta a informação anterior adquirida
                    GetData = null;

                    // Faz o comando QUERY
                    MySqlCommand Command = new MySqlCommand("INSERT INTO VAYNE_GAMES (OWNBY, GAMECODE, IP, ACTIVATIONDATE) VALUES (@user, @code, @ip, @ad)", connection);

                    // Faz a segurança dos dados 
                    Command.Parameters.AddWithValue("@user", ProgramData.Username); // Usuário ativo na sessão
                    Command.Parameters.AddWithValue("@code", GameCode); // Jogo para ser adicionado na conta do usuário
                    Command.Parameters.AddWithValue("@ip", getExternalIp()); // IP que ativou o jogo
                    Command.Parameters.AddWithValue("@ad", DateTime.Now); // Data em que foi ativado

                    // Abre a conexão
                    connection.Open();

                    // Executa a QUERY e adiciona os valores ao banco de dados
                    Command.ExecuteNonQuery();

                    // Verifica se o jogo foi ativado realmente
                    GetActivatedGame(GameCode);

                    // Se foi, beleza, avisar o usuário.
                    if (ActivatedByUser)
                    {
                        if (Properties.Settings.Default["lang"].ToString() != "en")
                            ProgramData.MensagemSucesso("JOGO ATIVADO!\nAI SIM JOVEM!!!!\nAPROVEITA BEM ESSA BAGAÇA :P");
                        else
                            ProgramData.MensagemSucesso("GAME ACTIVATED!\nHAVE A GREAT GAME AND A NICE DAY. :D");

                    }


                    // Invalida a chave para que não seja utilizada novamente
                    InvalidateKey(Key);

                    // Reseta tudo que foi utilizado, para evitar erros
                    ActivatedByUser = false;
                    ValidGameKey = false;

                    // Fecha a conexão
                    connection.Close();

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Posta a chave na atividade do usuário
                        // Atualiza registro do usuário
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"0;ATIVAÇÃO;{OpenGames.AvailableGames[int.Parse(GameCode)].ToUpper()};" +
                        $"JOGO ADICIONADO À BIBLIOTECA;{DateTime.Today.ToString()}");
                    else
                        // Posta a chave na atividade do usuário
                        // Atualiza registro do usuário
                        // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                        MySQL.UpdateYourActivity($"0;ACTIVATION;{OpenGames.AvailableGames[int.Parse(GameCode)].ToUpper()};" +
                        $"GAME ADDED TO LIBRARY;{DateTime.Today.ToString()}");


                    // Reseta o gameCode
                    GameCode = null;
                }
                else
                {

                    if (Properties.Settings.Default["lang"].ToString() != "en")
                        // Chave inválida, avisar usuário
                        ProgramData.MensagemErro("CHAVE INVÁLIDA!\n\nELA PODE TER SIDO ATIVADA POR ALGUÉM ANTES OU NÃO EXISTE!");
                    else
                        // Chave inválida, avisar usuário
                        ProgramData.MensagemErro("INVALID KEY!\n\nMAYBE HAS BEEN TAKEN BY SOMEONE OR DON'T EXISTS.");


                }
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro("ERRO NA CONEXÃO COM O BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO AGK1244");
                else
                    ProgramData.MensagemErro("ERROR WHILE RETRIEVING THE KEY.\nTRY AGAIN LATER.\nERROR_CODE: AGK1246");

            }
        }

        /// <summary>
        /// Ativa um jogo para o usuário
        /// </summary>
        /// <param name="Key">CÓDIGO DO JOGO</param>
        public static void ActivateGameKey(bool OwnActivation, string Code, string PaymentType)
        {
            try
            {
                // Ativa o jogo pela forma própria
                if (OwnActivation)
                {
                    // Inicializa a conexão
                    Initialize();

                    // Reseta a informação anterior adquirida
                    GetData = null;

                    // Abre a conexão, se não existir
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    // Faz o comando QUERY
                    MySqlCommand Command = new MySqlCommand("INSERT INTO VAYNE_GAMES (OWNBY, GAMECODE, IP, ACTIVATIONDATE, INVOICEID) VALUES (@user, @code, @ip, @date, @invoiceid)", connection);

                    // Faz a segurança dos dados 
                    Command.Parameters.AddWithValue("@user", ProgramData.Username); // Usuário ativo na sessão
                    Command.Parameters.AddWithValue("@code", Code); // Jogo para ser adicionado na conta do usuário
                    Command.Parameters.AddWithValue("@ip", getExternalIp()); // Manda o IP do usuário ativador para a base
                    Command.Parameters.AddWithValue("@date", DateTime.Now); // Manda a data em que o jogo foi ativo
                    // Cria uma nova invoice
                    InsertInvoiceID(OpenGames.AvailableGames[int.Parse(Code)].ToUpper(), PaymentType);
                    Command.Parameters.AddWithValue("@invoiceid", InvoiceID); // Relaciona com a InvoiceID criada pela Ugnite




                    // Executa a QUERY e adiciona os valores ao banco de dados
                    Command.ExecuteNonQuery();

                    // Reseta tudo que foi utilizado, para evitar erros
                    GameCode = null;
                    ActivatedByUser = false;
                    ValidGameKey = false;

                    // Fecha a conexão
                    connection.Close();
                }
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO NA CONEXÃO COM O BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO AGKOWN1302");
                else
                    ProgramData.MensagemErro("ERROR WHILE RETRIEVING THE KEY.\nTRY AGAIN LATER.\nERROR_CODE: AGKOWN1304");
            }
        }

        // Dias restantes da UGNITE Plus
        public static int RemainingDays = 0;
        /// <summary>
        /// Verifica se o usuário ainda possui A UGNITE Plus válida
        /// </summary>
        public static void VerifyPlus()
        {
            try
            {
                // Verifica o estado da conexão
                Initialize();

                // Reseta os dias restantes
                RemainingDays = 0;

                // Abre a conexão, se não existir
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                // Reinicializa o Reader
                DbDataReader dr;

                // Comando para executar
                MySqlCommand Command = new MySqlCommand("SELECT * FROM VAYNE_USERS WHERE nome=@User", connection);
                Command.Parameters.AddWithValue("@User", ProgramData.Username); // Verifica a chave fornecida

                // Executa o comando de reeader
                dr = Command.ExecuteReader();

                // Lê o banco de dados
                while (dr.Read())
                {
                    // Verifica se o usuário é Plus
                    if (dr["PLUS"].ToString() != "1")
                        // O usuário não é Plus, desativar
                        Program.HUBRPlus = false;
                    else
                    {

                        // Verifica a data em que A UGNITE Plus foi adquirida e compara com a data atual
                        TimeSpan g = DateTime.Parse(dr["PLUSDATE"].ToString(), new CultureInfo("pt-BR", true)) - DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR", true));

                        // Compara com a data atual
                        if (g.Days <= 0)
                        {
                            // Seta os dias restantes
                            RemainingDays = 0;

                            // Desativa A UGNITE Plus do usuário
                            DeactivatePlus();
                        }
                        else
                        {
                            // O usuário ainda é Plus
                            Program.HUBRPlus = true;

                            // Seta os dias restantes
                            RemainingDays = g.Days;
                        }
                    }
                }

                dr.Close();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    MessageBox.Show($"ERRO INTERNO DE EXECUÇÃO.\nERRO 1380", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                    MessageBox.Show($"INTERNAL ERROR.\nERROR_CODE: 1382", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Application.Exit();
            }
        }

        /// <summary>
        /// Desativa A UGNITE Plus do usuário, pois já expirou
        /// </summary>
        public static void DeactivatePlus()
        {
            try
            {
                Initialize();

                // Abre a conexão, se não existir
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                // Faz o comando QUERY
                MySqlCommand Command = new MySqlCommand("UPDATE VAYNE_USERS SET PLUS=@Val, PLUSDATE=@Deactivate WHERE Nome=@User", connection);

                // Faz a segurança dos dados 
                Command.Parameters.AddWithValue("@User", ProgramData.Username); // A chave que será invalidada
                Command.Parameters.AddWithValue("@Val", "0"); // Diz ao comando que a chave foi ativada
                Command.Parameters.AddWithValue("@Deactivate", $"DESATIVADA EM {DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR", true))}");


                // Executa a QUERY e adiciona os valores ao banco de dados
                Command.ExecuteNonQuery();

                // Aplica o valor no programa
                Program.HUBRPlus = false;
                // Reseta os dias restantes
                RemainingDays = 0;

                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Atualiza registro do usuário
                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"0;ALTERAÇÃO;UGNITE PLUS;DESATIVADA (EXPIROU);{DateTime.Today.ToString()}");
                else
                    // Atualiza registro do usuário
                    // Sendo 0 = OK | 1 = ERR0;O QUE FOI FEITO;NOME DA ATIVIDADE;DETALHES;DATA DA ATIVIDADE
                    MySQL.UpdateYourActivity($"0;UPDATE;UGNITE PLUS;DEACTIVATED (EXPIRED);{DateTime.Today.ToString()}");

            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    MessageBox.Show("ERRO INTERNO DE EXECUÇÃO.\nERRO 1425", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                    MessageBox.Show("INTERNAL ERROR.\nERROR_CODE: 1427", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Application.Exit();
            }
            finally
            {
                // Fecha a conexão
                connection.Close();
            }
        }

        /// <summary>
        /// AtivA UGNITE Plus para o usuário
        /// </summary>
        /// <param name="d"></param>
        public static void ActivatePlus(double d)
        {
            try
            {
                DateTime count;

                // Se o usuário é HUBR Plus, somar as datas.
                if (Program.HUBRPlus)
                {
                    // Adquire a informação de data
                    GetInformation(9);

                    // Converte a data
                    count = Convert.ToDateTime(GetData, new CultureInfo("pt-BR", true));
                }
                else
                    // Faz o cálculo de dias adicionados
                    count = Convert.ToDateTime(DateTime.Now.ToString(), new CultureInfo("pt-BR", true));

                var Fim = count.AddDays(d);

                // Faz o comando QUERY
                MySqlCommand Command = new MySqlCommand("UPDATE VAYNE_USERS SET PLUS=@Val, PLUSDATE=@Data WHERE nome=@User", connection);


                // Abre a conexão, se não existir
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                // Faz a segurança dos dados 
                Command.Parameters.AddWithValue("@User", ProgramData.Username); // A chave que será invalidada
                Command.Parameters.AddWithValue("@Val", "1"); // Diz ao comando que a chave foi ativada
                Command.Parameters.AddWithValue("@Data", DateTime.Parse(Fim.ToString(), new CultureInfo("pt-BR", true))); // Seta a data em que A UGNITE Plus foi ativada

                // Executa a QUERY e adiciona os valores ao banco de dados
                Command.ExecuteNonQuery();

                // Aplica o valor no programa
                Program.HUBRPlus = true;

                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Mostra mensagem de sucesso
                    ProgramData.MensagemSucesso($"UGNITE+ ATIVA!\nDIAS ADICIONADOS: {d - 1}\nEXPIRA EM: {DateTime.Parse(Fim.ToString(), new CultureInfo("pt-BR", true))}");
                else
                    // Mostra mensagem de sucesso
                    ProgramData.MensagemSucesso($"UGNITE+ ENABLED!\nADDED DAYS: {d - 1}\nEXPIRES AT: {DateTime.Parse(Fim.ToString(), new CultureInfo("pt-BR", true))} (BRAZILIAN TIME | GMT -3)");


            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    MessageBox.Show($"ERRO INTERNO DE EXECUÇÃO.\nERRO 1500", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show($"INTERNAL ERROR.\nERROR_CODE: 1502", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                // Fecha a conexão
                connection.Close();
            }
        }

        /// <summary>
        /// Atualiza os dados de download sobre um jogo
        /// </summary>
        /// <param name="GameName"></param>
        public static void UpdateGameDetailsDB(string GameName)
        {
            try
            {
                // Inicializa a conexão com a base de dados
                Initialize();


                // Faz o comando QUERY
                MySqlCommand Command = new MySqlCommand("INSERT INTO VAYNE_DOWNLOADACTIVITY (GAMEID, USERID, DOWNLOADATE, INSTALLDATE, USERIP, USEROS) VALUES (@gameid, @userid, @down_date, @inst_date, @ip, @os)", connection);
                Command.Parameters.AddWithValue("@gameid", GameName);
                Command.Parameters.AddWithValue("@userid", ProgramData.Username);
                Command.Parameters.AddWithValue("@down_date", DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR", true)));
                Command.Parameters.AddWithValue("@inst_date", "---");
                Command.Parameters.AddWithValue("@ip", getExternalIp());
                Command.Parameters.AddWithValue("@os", GetOsName());


                // Abre a conexão, se não existir
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                // Executa a Query
                Command.ExecuteNonQuery();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    MessageBox.Show($"ERRO INTERNO DE EXECUÇÃO.\nERRO 1538", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show($"INTERNAL ERROR.\nERROR_CODE: 1540", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Fecha a conexão
                connection.Close();
            }
            finally
            {
                // Fecha a conexão
                connection.Close();
            }
        }
        /// <summary>
        /// Atualiza os dados de download sobre um jogo
        /// </summary>
        /// <param name="GameName"></param>
        public static void UpdateGameDetailsDB(string GameName, bool downloadFinished)
        {
            try
            {
                // Inicializa a conexão com a base de dados
                Initialize();


                // Faz o comando QUERY
                MySqlCommand Command = new MySqlCommand("UPDATE INTO VAYNE_DOWNLOADACTIVITY (GAMEID, USERID, DOWNLOADATE, INSTALLDATE, USERIP, USEROS) VALUES (@gameid, @userid, @down_date, @inst_date, @ip, @os)", connection);
                Command.Parameters.AddWithValue("@gameid", GameName);
                Command.Parameters.AddWithValue("@userid", ProgramData.Username);
                Command.Parameters.AddWithValue("@down_date", DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR", true)));
                Command.Parameters.AddWithValue("@inst_date", "---");
                Command.Parameters.AddWithValue("@ip", getExternalIp());
                Command.Parameters.AddWithValue("@os", GetOsName());


                // Abre a conexão, se não existir
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                // Executa a Query
                Command.ExecuteNonQuery();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    MessageBox.Show($"ERRO INTERNO DE EXECUÇÃO.\nERRO 1538", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show($"INTERNAL ERROR.\nERROR_CODE: 1540", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Fecha a conexão
                connection.Close();
            }
            finally
            {
                // Fecha a conexão
                connection.Close();
            }
        }

        /// <summary>
        /// Adquire a nota de um jogo
        /// </summary>
        /// <param name="GameCode">CÓDIGO DO JOGO</param>
        /// <returns></returns>
        public static int GetGameRating(string GameCode)
        {
            // Cria uma variável de retorno
            int Rating = 0;
            try
            {
                // Cria um comando
                MySqlCommand com = new MySqlCommand("SELECT * FROM VAYNE_GAMENOTES WHERE GAMECODE=@GC", connection);
                com.Parameters.AddWithValue("@GC", GameCode);

                // Cria uma lista
                List<int> Notas = new List<int>();

                // Cria uma reader
                DbDataReader reader;

                // Antes de abrir conexao, verifica se não possui uma aberta anteriormente
                if(connection.State != System.Data.ConnectionState.Open)
                // Abre a conexão com a DB
                connection.Open();

                // Executa as rotinas
                reader = com.ExecuteReader();

                while(reader.Read())
                {
                    // Adiciona todas as notas encontradas
                    Notas.Add(int.Parse(reader["USERRATING"].ToString()));
                }

                // Fecha o reader
                reader.Close();

                // Valor de todas as notas juntas
                for (int i = 0; i < Notas.Count; i++)
                    Rating += Notas[i];

                // Verifica se há notas para o jogo
                if (Notas.Count > 0)
                {
                    // Agora  divide pela quantidade de notas 
                    Rating /= Notas.Count;
                }

                // Verifica se a nota é maior que dez
                if (Rating > 10)
                    Rating = 10;

                // Verifica se a nota é menor que zero
                if (Rating < 0)
                    Rating = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                // Deu algum erro, retornar 0
                Rating = 0;
            }
            finally
            {
                // Fechar a conexão
                connection.Close();
            }

            // Retorna a nota adquirida
            return Rating;
        }

        public static void SetGameRating(string GameCode, string Rating)
        {
            try
            {
                // Cria um comando
                MySqlCommand com = new MySqlCommand("INSERT INTO VAYNE_GAMENOTES (GAMECODE, VOTEDUSER, USERRATING) VALUES (@GC, @VU, @UR)", connection);
                com.Parameters.AddWithValue("@GC", GameCode);
                com.Parameters.AddWithValue("@VU", ProgramData.Username);
                com.Parameters.AddWithValue("@UR", Rating);


                // Antes de abrir conexao, verifica se não possui uma aberta anteriormente
                if (connection.State != System.Data.ConnectionState.Open)
                    // Abre a conexão com a DB
                    connection.Open();

                // Executa o comando para inserir valores
                com.ExecuteNonQuery();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    ProgramData.MensagemErro($"ERRO NA CONEXÃO COM O BANCO DE DADOS DA UGNITE!\nTENTE NOVAMENTE MAIS TARDE.\nCÓDIGO IGAN_1654");
                else
                    ProgramData.MensagemErro("ERROR WHILE RETRIEVING THE KEY.\nTRY AGAIN LATER.\nERROR_CODE: IGAN_1656");

            }
            finally
            {
                // Fechar a conexão
                connection.Close();
            }


        }
        /// <summary>
        /// Verifica se o usuário atual já votou no jogo
        /// </summary>
        /// <param name="GameCode">CÓDIGO DO JOGO</param>
        /// <returns>SIM OU NÃO</returns>
        public static bool UserRated(string GameCode)
        {
            // Cria uma variável de retorno
            bool rated = false;
            try
            {
                // Cria um comando
                MySqlCommand com = new MySqlCommand("SELECT * FROM VAYNE_GAMENOTES WHERE GAMECODE=@GC", connection);
                com.Parameters.AddWithValue("@GC", GameCode);

                // Cria uma reader
                DbDataReader reader;

                // Abre a conexão com a DB
                connection.Open();

                // Executa as rotinas
                reader = com.ExecuteReader();

                while (reader.Read())
                {
                    // Verifica se na lista de notas do jogo, há o usuário atual
                    if (reader["VOTEDUSER"].ToString().ToLower() == ProgramData.Username.ToLower())
                        rated = true;
                    else
                        rated = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Erro " + Ex.ToString());
                // Ocorreu um erro, retornar não
                rated = false;
            }
            finally
            {
                // Fechar a conexão
                connection.Close();
            }

            // Retorna o valor adquirido
            return rated;
        }

        /// <summary>
        /// Invoice criada na void <InsertInvoiceID>
        /// </summary>
        public static string InvoiceID;
        /// <summary>
        /// Faz o pedido de invoice para a base de dados
        /// </summary>
        /// <param name="GameName">Nome do jogo</param>
        /// <param name="PaymentType">UPAY | MERCADOPAGO</param>
        public static void InsertInvoiceID(string GameName, string PaymentType)
        {
            try
            {
                // Inicializa a conexão com a base de dados
                Initialize();

                // Cria uma invoice nova
                InvoiceID = RandomString(15);

                // Faz o comando QUERY
                MySqlCommand Command = new MySqlCommand("INSERT INTO VAYNE_INVOICES (GAMENAME, USERID, USERIP, USEROS, PAYMENTDATE, PAYMENTTYPE, PAYMENTID) " +
                    "VALUES (@gameid, @userid, @ip, @os, @down_date, @type, @id)", connection);
                Command.Parameters.AddWithValue("@gameid", GameName);
                Command.Parameters.AddWithValue("@userid", ProgramData.Username);
                Command.Parameters.AddWithValue("@down_date", DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR", true)));
                Command.Parameters.AddWithValue("@type", PaymentType);
                Command.Parameters.AddWithValue("@id", InvoiceID);
                Command.Parameters.AddWithValue("@ip", getExternalIp());
                Command.Parameters.AddWithValue("@os", GetOsName());

                // Abre a conexão, se não existir
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                // Executa a Query
                Command.ExecuteNonQuery();
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    MessageBox.Show($"ERRO INTERNO DE EXECUÇÃO.\nERRO 1592", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show($"INTERNAL ERROR.\nERROR_CODE: 1594", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Fecha a conexão
                connection.Close();
            }
            finally
            {
                // Fecha a conexão
                connection.Close();
            }
        }

        // Return the OS name.
        static string GetOsName()
        {
            //Get Operating system information.
            OperatingSystem os = Environment.OSVersion;
            //Get version information about the os.
            Version vs = os.Version;
            // Verifica se é x64
            string x64 = Environment.Is64BitOperatingSystem ? " (x64)" : " (x86)";

            //Variable to hold our return value
            string operatingSystem = "";

            if (os.Platform == PlatformID.Win32Windows)
            {
                //This is a pre-NT version of Windows
                switch (vs.Minor)
                {
                    case 0:
                        operatingSystem = "95";
                        break;
                    case 10:
                        if (vs.Revision.ToString() == "2222A")
                            operatingSystem = "98SE";
                        else
                            operatingSystem = "98";
                        break;
                    case 90:
                        operatingSystem = "Me";
                        break;
                    default:
                        break;
                }
            }
            else if (os.Platform == PlatformID.Win32NT)
            {
                switch (vs.Major)
                {
                    case 3:
                        operatingSystem = "NT 3.51";
                        break;
                    case 4:
                        operatingSystem = "NT 4.0";
                        break;
                    case 5:
                        if (vs.Minor == 0)
                            operatingSystem = "2000";
                        else
                            operatingSystem = "XP";
                        break;
                    case 6:
                        if (vs.Minor == 0)
                            operatingSystem = "Vista";
                        else if (vs.Minor == 1)
                            operatingSystem = "7";
                        else if (vs.Minor == 2)
                            operatingSystem = "8";
                        else
                            operatingSystem = "8.1";
                        break;
                    case 10:
                        operatingSystem = "10";
                        break;
                    default:
                        break;
                }
            }
            //Make sure we actually got something in our OS check
            //We don't want to just return " Service Pack 2" or " 32-bit"
            //That information is useless without the OS version.
            if (operatingSystem != "")
            {
                //Got something.  Let's prepend "Windows" and get more info.
                operatingSystem = "Windows " + operatingSystem + x64;
                //See if there's a service pack installed.
                if (os.ServicePack != "")
                {
                    //Append it to the OS name.  i.e. "Windows XP Service Pack 3"
                    operatingSystem += " " + os.ServicePack;
                }
                //Append the OS architecture.  i.e. "Windows XP Service Pack 3 32-bit"
                //operatingSystem += " " + getOSArchitecture().ToString() + "-bit";
            }
            //Return the information we've gathered.
            return operatingSystem;
        }

        #region CONQUISTAS 
        /// <summary>
        /// ADQUIRE A INFORMAÇÃO DE ALGUMA CONQUISTA
        /// ID DE COLUNAS
        /// 0 = DESCRIÇÃO DA CONQUISTA
        /// 1 = LINK DE IMAGEM DA CONQUISTA
        /// 2 = CHAVE DE DESENVOLVEDOR ATRELADA
        /// 3 = NOME DO JOGO ATRELADO
        /// 4 = NOME DA CONQUISTA
        /// </summary>
        /// <param name="ID">CONQUERID DE ACORDO COM A VAYNE_CONQUERS</param>
        /// <returns></returns>
        public static string ConquerInfo(string ID, int coluna) {
            try
            {
                // Seta as colunas de cada disponibilização
                string[] Coluna = new string[6];
                Coluna[0] = "CONQUERDESC";
                Coluna[1] = "CONQUERIMG";
                Coluna[2] = "DEVKEY";
                Coluna[3] = "GAMENAME";
                Coluna[4] = "CONQUERNAME";


                // Inicializa a conexão com a base de dados
                Initialize();

                // Cria uma datareader
                DbDataReader reader;

                // Cria um comando
                MySqlCommand command = new MySqlCommand($"SELECT * FROM VAYNE_CONQUERSTP WHERE CONQUERID=@ID", connection);
                command.Parameters.AddWithValue("@ID", ID);

                // Inicializa a conexão
                connection.Open();

                // Cria e seta um novo reader
                reader = command.ExecuteReader();

                // Faz as verificações e retorna a função
                while(reader.Read())
                {
                    // Tenta retornar o valor da coluna
                    try
                    {
                        // Adquire o valor de acordo com a coluna
                        if (reader[Coluna[coluna]].ToString() != null || string.IsNullOrEmpty(reader[Coluna[coluna]].ToString()))
                            return reader[Coluna[coluna]].ToString();
                        else
                            return "NOK";
                    }
                    catch // Provávelmente a conquista não existe ou não foi cadastrada na bd
                    // Retornar NOK
                    {
                        return "NOK1";
                    }
                }
            }
            catch
            {

            }


            return "NOK2";
        }

        /// <summary>
        /// RETORNA TODAS AS IDs DE CONQUISTAS QUE O USUÁRIO TEM
        /// </summary>
        public static List<string> RetrieveConquers
        {
            get
            {

                // Inicializa a conexão com a base de dados
                Initialize();

                // Cria uma datareader
                DbDataReader reader;

                // Cria uma lista para retornar
                List<string> conquers = new List<string>();

                // Cria um comando
                MySqlCommand command = new MySqlCommand("SELECT * FROM VAYNE_CONQUERS WHERE playerid=@ID", connection);
                command.Parameters.AddWithValue("@ID", ProgramData.Username);

                // Inicializa a conexão
                connection.Open();

                // Cria e seta um novo reader
                reader = command.ExecuteReader();

                // Lê tudo que encontrar lá
                while(reader.Read())
                {
                    // Caso exista conqusitas, adiciona na lista
                    try
                    {
                        // Adiciona os valores encontrados na lista
                        conquers.Add(reader["CONQUERID"].ToString());
                    }
                    catch
                    {
                        // Seta a lista de conquistas como nula
                        conquers = null;
                    }
                }

                // Fecha conexão
                connection.Close();

                // Retorna a lista conquers
                return conquers;
            }
        }

        /// <summary>
        /// ADQUIRE UMA INFORMAÇÃO DA CONQUISTA
        /// 0 = NOME DO JOGO
        /// 1 = DATA DA CONQUISTA
        /// </summary>
        /// <param name="ID">ID da Conquista</param>
        /// <returns></returns>
        public static string UserConquerInfo(string ID, int col)
        {
            // Seta as colunas de cada disponibilização
            string[] Coluna = new string[3];
            Coluna[0] = "gamename";
            Coluna[1] = "conquerdate";

            // Inicializa a conexão com a base de dados
            Initialize();

            // Cria uma datareader
            DbDataReader reader;

            // Cria um comando
            MySqlCommand command = new MySqlCommand("SELECT * FROM VAYNE_CONQUERS WHERE (playerid=@ID AND CONQUERID=@CID)", connection);
            command.Parameters.AddWithValue("@ID", ProgramData.Username);
            command.Parameters.AddWithValue("@CID", ID);

            // Inicializa a conexão
            connection.Open();

            // Cria e seta um novo reader
            reader = command.ExecuteReader();

            // Lê tudo que encontrar lá
            while (reader.Read())
            {
                // Caso exista conqusitas, retorne um valor
                try
                {
                    // Retorna a informação
                    return reader[Coluna[col]].ToString();
                }
                catch
                {
                    // Retorna como NOK
                    return "NOK";
                }
            }

            // Retorna NOK
            return "NOK";
        }


        #endregion

        #region DESENVOLVEDORES
        /// <summary>
        /// Verifica a chave de desenvolvedor.
        /// COLUNAS
        /// 0 = CHAVE DO DESENVOLVEDOR
        /// 1 = DATA DE AUTORIZAÇÃO
        /// 2 = VALIDA OU NÃO
        /// 3 = APTO A UTILIZAR FUNÇÕES FINANCEIRAS OU NÃO
        /// </summary>
        /// <param name="Key">Chave</param>
        /// <param name="c">COLUNA</param>
        /// <returns></returns>
        public static string VerifyDevKey(int c)
        {
            string Retorno = "NOK";
            try
            {
                string[] Coluna = new string[4];
                Coluna[0] = "devkey";
                Coluna[1] = "AUTORIZATIONDATE";
                Coluna[2] = "valid";
                Coluna[3] = "FINANCES";

                // Inicializa a conexão
                Initialize();

                // Cria um reader
                DbDataReader reader;

                // Cria o comando de conexão
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM VAYNE_DEVKEYS WHERE devname=@dn", connection);
                cmd.Parameters.AddWithValue("@dn", ProgramData.Username);

                // Conecta com a base
                connection.Open();

                // Seta a reader para o comando de conexão
                reader = cmd.ExecuteReader();

                // Verifica o valor que o usuário necessita
                while (reader.Read())
                {
                    // Tenta adquirir o valor e retorna
                    try
                    {
                        // Seta o retorno para o valor da coluna adquirida
                        Retorno = reader[Coluna[c]].ToString();
                    }
                    catch
                    {
                        Retorno = "NOK";
                    }
                }
            }
            catch
            {

            }

            return Retorno;
        }

        /// <summary>
        /// Faz a requisição de uma chave de desenvolvedor
        /// </summary>
        public static void RequestDevKey()
        {
            try
            {
                // Inicializa conexão
                Initialize();

                // Cria a devkey
                string DevKey = RandomString(3) + "-" + RandomString(3) + "-" + RandomString(5);

                // Faz o comando da conexão
                MySqlCommand com = new MySqlCommand("INSERT INTO VAYNE_DEVKEYS (devkey, devname, AUTORIZATIONDATE, valid, FINANCES) VALUES (@dk, @dn, @adate, '0', '0')", connection);
                com.Parameters.AddWithValue("@dk", DevKey.ToUpper());
                com.Parameters.AddWithValue("@dn", ProgramData.Username);
                com.Parameters.AddWithValue("@adate", DateTime.Parse(DateTime.Now.ToString(), new CultureInfo("pt-BR", true)));

                // Abre a conexão
                connection.Open();

                // Executa o comando mysql
                com.ExecuteNonQuery();

                // Fecha a conexão
                connection.Close();


                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Exibe mensagem com o código
                    ProgramData.MensagemSucesso("Sua Chave de Desenvolvedor foi requisitada!\nPor favor, aguarde até a validação.\nCHAVE: " + DevKey.ToUpper() + "\n\nFavor não requisitar mais chaves, pois é válida para todos os jogos.");
                else
                    // Exibe mensagem com o código
                    ProgramData.MensagemSucesso("Your Developer Key request was sent!\nPlease, wait until validation.\nKEY: " + DevKey.ToUpper() + "\n\nPlease, do not request any more keys, the key are valid for all of your submited games.");
            }
            catch
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    // Exibe mensagem erro
                    ProgramData.MensagemErro("Erro ao requisitar a Chave de Desenvolvedor.\nTente novamente mais tarde.");
                else
                    // Exibe mensagem erro
                    ProgramData.MensagemErro("Error while requesting your Developer Key.\nTry again later.");

            }
        }

        /// <summary>
        /// Retorna a lista de jogos disponíveis na Ugnite
        /// 0 = Jogos Não-Plus
        /// 1 = Somente jogos plus
        /// 2 = Todos os jogos
        /// </summary>
        public static List<string> AvailableGames(string Plus)
        {
            // Cria uma lista temporária de retorno
            List<string> list = new List<string>();

            // Inicializa a conexão
            Initialize();

            // Cria um reader
            DbDataReader reader;

            // Faz o comando de SELECT
            MySqlCommand com = new MySqlCommand("SELECT * FROM VAYNE_GAMESINFO", connection);

            // Abre a conexão com a DB
            connection.Open();

            // Faz o loop de seleção
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                if (Plus == "0")
                {
                    if (reader["PLUSAVAILABLE"].ToString() == "0")
                        list.Add(reader["GAMENAME"].ToString());
                }
                else if(Plus == "1")
                {
                    if (reader["PLUSAVAILABLE"].ToString() == "1")
                        list.Add(reader["GAMENAME"].ToString());
                }else if(Plus == "2")
                {
                    list.Add(reader["GAMENAME"].ToString());
                }
            }

            // Retorna a lista de jogos encontrados e finaliza a reader
            reader.Close();
            return list;
        }

        // Cria uma array contendo as colunas disponíveis na table
        static List<string> Colunas = new List<string>{ "ID", "GAMENAME", "GAMENUM", "GAMEDEV",
                    "GAMEPRICE", "GAMEPRICE_UPAY", "PLUSAVAILABLE", "GOM", "AVAILABLE", "PUBLISHDATE", "GAMESIZE", "GAMEIMAGE", "DOWNLOADABLE", "GAMEPRICE_INT", "PAYURL", "USEAPI"};

        /// <summary>
        /// Adquire uma informação sobre algum jogo do desenvolvedor
        /// 0 = ID DA LINHA
        /// 1 = NOME DO JOGO
        /// 2 = NÚMERO DO JOGO
        /// 3 = DEV. DO JOGO
        /// 4 = PREÇO DO JOGO (MP)
        /// 5 = PREÇO DO JOGO (UPAY)
        /// 6 = SE ESTÁ DISPONÍVEL NA U+
        /// 7 = SE É GAME OF THE MONTH
        /// 8 = SE ESTÁ DISPONÍVEL OU NÃO NA UGNITE
        /// 9 = DATA DA PUBLICAÇÃO DO JOGO
        /// 10 = TAMANHO DO JOGO
        /// 11 = LINK DA IMAGEM DO JOGO
        /// 12 = SE O JOGO PODE SER BAIXADO/INSTALADO/EXECUTADO
        /// 13 = PREÇO DO JOGO INTERNACIONALMENTE
        /// 14 = URL DE COMPRA DO JOGO (MP/PAYPAL)
        /// 15 = SE O JOGO UTILIZA A UGNITE API
        /// </summary>
        /// <param name="c">COLUNA</param>
        /// <param name="GameCode">CÓDIGO DO JOGO</param>
        /// <returns></returns>
        public static string RequestGameInfo(int c, string GameCode)
        {
            try
            {
                // Inicializa a conexão com a DB
                Initialize();

                // Cria um reader
                DbDataReader reader;

                // Cria os comandos
                MySqlCommand coms = new MySqlCommand("SELECT * FROM VAYNE_GAMESINFO WHERE GAMENUM=@GN", connection);
                coms.Parameters.AddWithValue("@GN", GameCode);

                // Abre a conexão
                connection.Open();

                // Lê a reader
                reader = coms.ExecuteReader();
                while (reader.Read())
                {
                    return reader[Colunas[c]].ToString();
                }

                // Fecha conexão
                connection.Close();
            }
            catch
            {
                return "NOK";
            }

            return "";
        }


        /// <summary>
        /// Adquire uma informação sobre algum jogo do desenvolvedor pelo nome do jogo
        /// 0 = ID DA LINHA
        /// 1 = NOME DO JOGO
        /// 2 = NÚMERO DO JOGO
        /// 3 = DEV. DO JOGO
        /// 4 = PREÇO DO JOGO (MP)
        /// 5 = PREÇO DO JOGO (UPAY)
        /// 6 = SE ESTÁ DISPONÍVEL NA U+
        /// 7 = SE É GAME OF THE MONTH
        /// 8 = SE ESTÁ DISPONÍVEL OU NÃO NA UGNITE
        /// 9 = DATA DA PUBLICAÇÃO DO JOGO
        /// 10 = TAMANHO DO JOGO
        /// 11 = LINK DA IMAGEM DO JOGO
        /// 12 = SE O JOGO PODE SER BAIXADO/INSTALADO/EXECUTADO
        /// 13 = PREÇO DO JOGO INTERNACIONALMENTE
        /// 14 = URL DE COMPRA DO JOGO (MP/PAYPAL)
        /// 15 = SE O JOGO UTILIZA A UGNITE API
        /// </summary>
        /// <param name="c">COLUNA</param>
        /// <param name="GameName">NOME DO JOGO</param>
        /// <returns></returns>
        public static string RequestGameInfoByName(int c, string GameName)
        {
            string r = "NOK";
            try
            {
                // Inicializa a conexão com a DB
                Initialize();

                // Cria um reader
                DbDataReader reader;

                // Cria os comandos
                MySqlCommand coms = new MySqlCommand("SELECT * FROM VAYNE_GAMESINFO WHERE GAMENAME=@GN", connection);
                coms.Parameters.AddWithValue("@GN", GameName);

                // Abre a conexão
                connection.Open();

                // Lê a reader
                reader = coms.ExecuteReader();
                while (reader.Read())
                {
                    r = reader[Colunas[c]].ToString();
                }

                // Fecha conexão e a reader
                reader.Close();
                connection.Close();
            }
            catch
            {
                return r;
            }

            return r;
        }

        /// <summary>
        /// Adquire a quantidade de downloads de um jogo
        /// </summary>
        /// <param name="GameName">Nome do Jogo</param>
        /// <returns></returns>
        public static int RequestDownloadInfo(string GameName)
        {
            try
            {
                // Inicializa a conexão com a DB
                Initialize();

                // Cria uma lista com todos os downloaders
                List<string> downCount = new List<string>();

                // Cria um reader
                DbDataReader reader;

                // Cria os comandos
                MySqlCommand coms = new MySqlCommand("SELECT GAMEID FROM VAYNE_DOWNLOADACTIVITY WHERE GAMEID=@GN", connection);
                coms.Parameters.AddWithValue("@GN", GameName.ToUpper());

                // Verifica se a conexão não está aberta antes de continuar
                if(connection.State != System.Data.ConnectionState.Open)
                // Abre a conexão
                connection.Open();

                // Lê a reader
                reader = coms.ExecuteReader();
                while (reader.Read())
                {
                    downCount.Add(reader["GAMEID"].ToString());
                }

                // Fecha conexão e a reader
                reader.Close();
                connection.Close();

                // Retorna a quantidade de downloads
                return downCount.Count;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Retorna uma lista com o nome dos jogos do desenvolvedor
        /// </summary>
        public static List<string> RequestDevGamesName
        {
            get
            {
                // Cria uma lista temporária para retorno
                List<string> Lista = new List<string>();

                // Inicializa a conexão com a DB
                Initialize();

                // Cria um reader
                DbDataReader reader;

                // Cria os comandos
                MySqlCommand coms = new MySqlCommand("SELECT * FROM VAYNE_GAMESINFO WHERE GAMEDEV=@GD", connection);
                coms.Parameters.AddWithValue("@GD", ProgramData.Username);

                // Abre a conexão
                connection.Open();

                // Lê a reader
                reader = coms.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(reader["GAMENAME"].ToString());
                }

                // Fecha conexão
                connection.Close();

                return Lista;
            }
        }

        /// <summary>
        /// Retorna uma lista com o código dos jogos do desenvolvedor
        /// </summary>
        public static List<string> RequestDevGamesCode
        {
            get
            {
                // Cria uma lista temporária para retorno
                List<string> Lista = new List<string>();

                // Inicializa a conexão com a DB
                Initialize();

                // Cria um reader
                DbDataReader reader;

                // Cria os comandos
                MySqlCommand coms = new MySqlCommand("SELECT * FROM VAYNE_GAMESINFO WHERE GAMEDEV=@GD", connection);
                coms.Parameters.AddWithValue("@GD", ProgramData.Username);

                // Verifica se a conexão não está aberta antes de continuar
                if (connection.State != System.Data.ConnectionState.Open)
                    // Abre a conexão
                    connection.Open();

                // Lê a reader
                reader = coms.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(reader["GAMENUM"].ToString());
                }

                // Fecha conexão e a reader
                reader.Close();
                connection.Close();

                return Lista;
            }
        }

        /// <summary>
        /// Adquire informações sobre as conquistas inseridas pelo Dev
        /// [COLUNAS]
        /// 0 = ID
        /// 1 = NOME DA CONQUISTA
        /// 2 = DESCRIÇÃO
        /// 3 = IMAGEM
        /// 4 = ID DA CONQUISTA
        /// 5 = JOGO ATRELADO
        /// </summary>
        /// <param name="key">Chave do Desenvolvedor</param>
        /// <param name="colID">Identificação da Coluna</param>
        /// <returns></returns>
        public static List<string> RequestDevGamesConquers(string key, int colID)
        {
            // Cria uma lista temporária para retorno
            List<string> Lista = new List<string>();


            // Cria uma array contendo as colunas disponíveis na table
            List<string> Colunas = new List<string>{ "ID", "CONQUERNAME", "CONQUERDESC", "CONQUERIMG",
                    "CONQUERID", "GAMENAME",};

            // Inicializa a conexão com a DB
            Initialize();

            // Cria um reader
            DbDataReader reader;

            // Cria os comandos
            MySqlCommand coms = new MySqlCommand("SELECT * FROM VAYNE_CONQUERSTP WHERE DEVKEY=@KEY", connection);
            coms.Parameters.AddWithValue("@KEY", key);

            // Abre a conexão
            connection.Open();

            // Lê a reader
            reader = coms.ExecuteReader();
            while (reader.Read())
            {
                Lista.Add(reader[Colunas[colID]].ToString());
            }

            // Fecha conexão
            connection.Close();

            return Lista;
        }

        /// <summary>
        /// Inserir uma nova conquista na base
        /// </summary>
        /// <param name="img">REPRESENTAÇÃO</param>
        /// <param name="nome">NOME DA CONQUISTA</param>
        /// <param name="desc">DESCRIÇÃO DA CONQUISTA</param>
        /// <param name="game">JOGO ATRELADO</param>
        /// <param name="id">ID DA CONQUISTA</param>
        public static void InsertConquer(string img, string nome, string desc, string game, string id, string key)
        {
            try
            {
                // Inicializa a conexão com a DB
                Initialize();

                // Cria os comandos
                MySqlCommand coms = new MySqlCommand("INSERT INTO VAYNE_CONQUERSTP (CONQUERNAME, CONQUERDESC, CONQUERIMG, CONQUERID, DEVKEY, GAMENAME)" +
                    " VALUES (@CN, @CD, @CI, @CID, @KEY, @GN)", connection);
                coms.Parameters.AddWithValue("@KEY", key);
                coms.Parameters.AddWithValue("@CN", nome);
                coms.Parameters.AddWithValue("@CD", desc);
                coms.Parameters.AddWithValue("@CI", img);
                coms.Parameters.AddWithValue("@CID", id);
                coms.Parameters.AddWithValue("@GN", game);


                // Abre a conexão, se não existir
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                // Executa a Query
                coms.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                if (Properties.Settings.Default["lang"].ToString() != "en")
                    MessageBox.Show($"CONQUISTA NÃO INSERIDA.\nTALVEZ JÁ EXISTA UMA PARECIDA!", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show($"ACHIEVEMENT WASN'T CREATED!\nMAYBE THERE'S ANOTHER THAT HAS THE SAME INFO.\n{ex}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                // Fecha a conexão
                connection.Close();

            }
        }

        /// <summary>
        /// Atualiza uma informação do jogo
        /// 0 = PLUS
        /// 1 = DISPONÍVEL PARA DOWNLOAD
        /// 2 = PREÇO INTERNACIONAL
        /// 3 = PREÇO UPAY
        /// 4 = SE UTILIZA A UGNITE API
        /// </summary>
        /// <param name="GameCode">Código do jogo</param>
        /// <param name="val">Valor para atualizar</param>
        public static void UpdateGameInfo(string GameCode, int col, string val)
        {
            try
            {
                // Cria uma lista contendo valores de coluna
                List<string> Coluna = new List<string>() { "PLUSAVAILABLE", "DOWNLOADABLE", "GAMEPRICE_INT", "GAMEPRICE_UPAY", "USEAPI" };

                // Inicializa a conexão
                Initialize();

                // Cria um comando
                MySqlCommand com = new MySqlCommand($"UPDATE VAYNE_GAMESINFO SET {Coluna[col]}=@val WHERE GAMENUM=@GNUM", connection);
                com.Parameters.AddWithValue("@val", val);
                com.Parameters.AddWithValue("@GNUM", GameCode);

                // Conecta com a base
                connection.Open();

                // Executa o comando
                com.ExecuteNonQuery();

                // Finaliza a conexão
                connection.Close();
            }
            catch(Exception ex)
            {
                ProgramData.MensagemErro(ex.Message);
            }
        }
        #endregion
    }
}
