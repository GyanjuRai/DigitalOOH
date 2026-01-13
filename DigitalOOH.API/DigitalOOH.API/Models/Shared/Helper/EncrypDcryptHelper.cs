
using System.Security.Cryptography;

namespace DigitalOOH.API.Models.Shared.Helper
{
    public static class EncryptDcryptHelper
    {
        private static readonly int saltSize = 16; // 128 bit
        private static readonly int HashSize = 20; // 160 bit
        private static readonly int Iterations = 10000;

        private static string EncryptData(string value)
        {
            byte[] salt;
            RandomNumberGenerator.Fill(salt = new byte[saltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(value, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);
            byte[] hashBytes = new byte[saltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, HashSize);
            return Convert.ToBase64String(hashBytes); 
        }

        public static bool VerifyPassword(string encryptedValue, string plainValue)
        {
            byte[] hashBytes = Convert.FromBase64String(encryptedValue);
            byte[] salt = new byte[saltSize];
            Array.Copy(hashBytes, 0, salt, 0, saltSize);

            var pdbkdf2 = new Rfc2898DeriveBytes(plainValue, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pdbkdf2.GetBytes(HashSize);

            for(int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + saltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
