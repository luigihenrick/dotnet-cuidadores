using System.Text;
using System.Text.RegularExpressions;

namespace Cuidadores.Util.Extensions
{
    public static class StringExtensions
    {
        public static string OnlyDigits(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Regex regexDigits = new Regex(@"[^\d]");
            return regexDigits.Replace(value, "");
        }

        public static string FormataCpfCnpj(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            string retorno =
                value.Replace(".", "")
                    .Replace(",", "")
                    .Replace(" ", "")
                    .Replace("/", "")
                    .Replace("-", "")
                    .Replace("\\", "");

            switch (retorno.Length)
            {
                case 11:
                    retorno = retorno.Substring(0, 3) + "." + retorno.Substring(3, 3) + "." + retorno.Substring(6, 3) + "-" +
                              retorno.Substring(9, 2);
                    break;
                case 14:
                    retorno = retorno.Substring(0, 2) + "." + retorno.Substring(2, 3) + "." + retorno.Substring(5, 3) + "/" +
                              retorno.Substring(8, 4) + "-" + retorno.Substring(12, 2);
                    break;
            }

            return retorno;
        }

        public static string FormataCep(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            string retorno =
                value.Replace(".", "")
                    .Replace(",", "")
                    .Replace(" ", "")
                    .Replace("/", "")
                    .Replace("-", "")
                    .Replace("\\", "");

            switch (retorno.Length)
            {
                case 5:
                    retorno += "-000";
                    break;
                case 8:
                    retorno = retorno.Substring(0, 5) + "-" + retorno.Substring(5, 3);
                    break;
            }

            return retorno;
        }

        public static string ToMD5(this string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string EncodeForLike(this string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return null;
            }

            return term.Replace("[", "[[]").Replace("%", "[%]"); ;
        }
    }
}
