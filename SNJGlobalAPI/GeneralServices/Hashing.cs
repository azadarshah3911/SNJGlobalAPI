using System.Security.Cryptography;
using System.Text;

namespace SNJGlobalAPI.GeneralServices
{
    public static class Hashing
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static string GetPassword(byte[] hash, byte[] salt)
        {
            return "NotPoassible To Recover Password";
        }


        public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(hash);
            }
        }
    }
}
