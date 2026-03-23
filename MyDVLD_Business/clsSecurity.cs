using System;
using System.Security.Cryptography;
using System.Text;

namespace MyDVLD_Business
{
    public static class clsSecurity
    {

        public static string GenerateSalt(int size = 16)
        {
            byte[] saltBytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static string HashPassword(string plainPassword, string salt)
        {
            return ComputeHash(plainPassword + salt);
        }

        public static bool VerifyPassword(string plainPassword, string storedHash, string storedSalt)
        {
            string computedHash = HashPassword(plainPassword, storedSalt);
            return computedHash == storedHash;
        }

    }
}
