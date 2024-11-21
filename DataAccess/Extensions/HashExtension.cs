using System;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Extensions
{
    public static class HashExtension
    {
        public static string GenerarToken(this string email, DateTime expiration)
        {
            string data = $"{email}|{Guid.NewGuid()}|{expiration}";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (var sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(dataBytes));
            }
        }
    }
}
