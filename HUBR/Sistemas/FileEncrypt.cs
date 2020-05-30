using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UGNITE
{
    public static class FileEncrypt
    {
        public static void Encrypt(string File, string outPut)
        {
            StreamWriter fs = new StreamWriter(Directory.GetCurrentDirectory() + "\\" + outPut);
            for (int i = 0; i < System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\" + File).Length; i++)
            {
                fs.WriteLine(Encryptor.Encrypt(System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\" + File)[i], "HUBR"));
            }

            fs.Close();
        }

        /// <summary>
        /// LÊ O ARQUIVO DESCRIPTOGRAFADO EM TEXTO CURTO
        /// </summary>
        public static void Decrypt(string File, string outPut)
        {
                StreamWriter fs = new StreamWriter(System.IO.Path.GetTempPath() + "\\" + outPut);
                for (int i = 0; i < System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\" + File).Length; i++)
                    fs.WriteLine(Encryptor.Decrypt(System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\" + File)[i], "HUBR"));
                fs.Close();

            Text = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\" + outPut);
        }

        /// <summary>
        /// LÊ O ARQUIVO DESCRIPTOGRAFADO EM UM TEXTO LONGO
        /// </summary>
        public static void Decrypt(string File, string outPut, bool Long = true)
        {
            StreamWriter fs = new StreamWriter(System.IO.Path.GetTempPath() + "\\" + outPut);
            for (int i = 0; i < System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\" + File).Length; i++)
                fs.WriteLine(Encryptor.Decrypt(System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "\\" + File)[i], "HUBR"));
            fs.Close();

            TextLong = System.IO.File.ReadAllLines(System.IO.Path.GetTempPath() + "\\" + outPut);
        }

        /// <summary>
        /// UTILIZE O DECRYPT N-LONG
        /// </summary>
        public static string Text;
        /// <summary>
        /// UTILIZE O DECRYPT LONG
        /// </summary>
        public static string[] TextLong;
    }
}
