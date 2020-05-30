namespace UGNITE
{
    public static class UploadImagem
    {
        public static void UploadImage(string URL)
        {
                // Seta a imagem do usuário
                MySQL.UpdateInformation(6, URL);

                // Atualiza a imagem no ProgramData
                ProgramData.ImagemURL = URL;

            if (Properties.Settings.Default["lang"].ToString() != "en")
                // Mostra mensagem de sucesso!
                ProgramData.MensagemSucesso("IMAGEM DE PERFIL ATUALIZADA!!");
            else
                // Mostra mensagem de sucesso!
                ProgramData.MensagemSucesso("PROFILE IMAGE UPDATED");

        }

    }
}
