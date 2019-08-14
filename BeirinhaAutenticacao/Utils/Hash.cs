using System.Security.Cryptography;
using System.Text;

namespace BeirinhaAutenticacao.Utils
{
    public class Hash
    {
        public static string GerarHash(string texto)
        {
            SHA256 sHA256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            byte[] hash = sHA256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X"));
            }

            return result.ToString();
        }
    }
}