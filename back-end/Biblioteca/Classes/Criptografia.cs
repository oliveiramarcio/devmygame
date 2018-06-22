using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Classes
{
    public static class Criptografia
    {
        public static string GerarHashMD5(string texto)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(texto);

            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder retorno = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                retorno.Append(hash[i].ToString("X2"));
            }

            return retorno.ToString();
        }
    }
}