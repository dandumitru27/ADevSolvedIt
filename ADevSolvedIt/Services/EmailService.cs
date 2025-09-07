using System.Security.Cryptography;
using System.Text;

namespace ADevSolvedIt.Services
{
    public static class EmailService
    {
        public static string GetEmailHash(string email)
        {
            using MD5 md5 = MD5.Create();

            var emailLowercase = email.Trim().ToLower();

            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(emailLowercase));

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
