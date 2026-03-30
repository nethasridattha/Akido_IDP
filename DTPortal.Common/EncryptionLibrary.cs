using System;

using System.Security;

using System.Security.Cryptography;

using System.Text;

namespace DTPortal.Common
{
    public partial class EncryptionLibrary
    {
        private const int NonceSize = 12;

        private const int TagSize = 16;

        private const int KeySize = 32;

        private const int Iterations = 100000;

        public static string EncryptText(string input, string encryptionPassword, string passwordSalt)
        {
            if (string.IsNullOrEmpty(input)) return input;

            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptionPassword);

            byte[] saltBytes = Encoding.UTF8.GetBytes(passwordSalt);

            byte[] plainBytes = Encoding.UTF8.GetBytes(input);

            using var keyDerivation = new Rfc2898DeriveBytes(passwordBytes, saltBytes, Iterations, HashAlgorithmName.SHA256);

            byte[] key = keyDerivation.GetBytes(KeySize);

            byte[] nonce = new byte[NonceSize];

            RandomNumberGenerator.Fill(nonce);

            using var chaCha = new ChaCha20Poly1305(key);

            byte[] cipherText = new byte[plainBytes.Length];

            byte[] tag = new byte[TagSize];

            chaCha.Encrypt(nonce, plainBytes, cipherText, tag);

            byte[] result = new byte[NonceSize + TagSize + cipherText.Length];

            Buffer.BlockCopy(nonce, 0, result, 0, NonceSize);

            Buffer.BlockCopy(tag, 0, result, NonceSize, TagSize);

            Buffer.BlockCopy(cipherText, 0, result, NonceSize + TagSize, cipherText.Length);

            return Convert.ToBase64String(result);

        }

        public static string DecryptText(string input, string encryptionPassword, string passwordSalt)
        {
            if (string.IsNullOrEmpty(input)) return input;

            byte[] fullCipher = Convert.FromBase64String(input);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptionPassword);

            byte[] saltBytes = Encoding.UTF8.GetBytes(passwordSalt);

            byte[] nonce = new byte[NonceSize];

            byte[] tag = new byte[TagSize];

            byte[] cipherText = new byte[fullCipher.Length - NonceSize - TagSize];

            Buffer.BlockCopy(fullCipher, 0, nonce, 0, NonceSize);

            Buffer.BlockCopy(fullCipher, NonceSize, tag, 0, TagSize);

            Buffer.BlockCopy(fullCipher, NonceSize + TagSize, cipherText, 0, cipherText.Length);

            using var keyDerivation = new Rfc2898DeriveBytes(passwordBytes, saltBytes, Iterations, HashAlgorithmName.SHA256);

            byte[] key = keyDerivation.GetBytes(KeySize);

            using var chaCha = new ChaCha20Poly1305(key);

            byte[] plainBytes = new byte[cipherText.Length];

            try
            {
                chaCha.Decrypt(nonce, cipherText, tag, plainBytes);
            }
            catch (CryptographicException)
            {
                throw new SecurityException("Data integrity check failed.");
            }
            return Encoding.UTF8.GetString(plainBytes);
        }

        public static string GenerateToken(ClientKey clientKey, DateTime issuedOn, string password, string passwordSalt)
        {
            try
            {
                string RandomNumber =
                   string.Join(":", new string[]
                   {
                       Convert.ToString(clientKey.ClientId),
                       KeyGenerator.GetUniqueKey(),
                       Convert.ToString(issuedOn.Ticks),
                       clientKey.ClientId
                   });

                return EncryptionLibrary.EncryptText(RandomNumber, password, passwordSalt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static class KeyGenerator
        {
            public static string GetUniqueKey(int maxSize = 36)
            {
                return Convert.ToHexString(RandomNumberGenerator.GetBytes(maxSize / 2));
            }
        }
    }
}

