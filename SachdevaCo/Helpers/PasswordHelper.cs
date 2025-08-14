using System.Security.Cryptography;
using System.Text;

namespace SachdevaCo.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            var buffer = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
                return BitConverter.ToString(buffer).Replace("-", "").ToLower();
            }
        }

        public static string HashPassword(string password, string salt)
        {
            var combined = Encoding.UTF8.GetBytes(password + salt);
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(combined);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string hashOfInput = HashPassword(enteredPassword, storedSalt);
            return hashOfInput.Equals(storedHash);
        }
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

