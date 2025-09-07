using System.Security.Cryptography;
using System.Text;

namespace ADevSolvedIt.Services
{
    public static class IPService
    {
        public static string HashIPWithSalt(HttpContext httpContext)
        {
            var ip = httpContext.Connection.RemoteIpAddress.ToString();

            var salt = "00fb85e0fcbb11d0";

            byte[] buffer = Encoding.UTF8.GetBytes(ip + salt);
            byte[] digest = SHA256.HashData(buffer);

            return Convert.ToHexString(digest).ToLower();
        }
    }
}
