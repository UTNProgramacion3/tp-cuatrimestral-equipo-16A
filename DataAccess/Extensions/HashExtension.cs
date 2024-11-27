using System;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Extensions
{
    public static class HashExtension
    {
        public static string GenerarToken(this string email, DateTime expiration)
        {
            string data = $"{email}|{expiration:o}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }
    }
}
