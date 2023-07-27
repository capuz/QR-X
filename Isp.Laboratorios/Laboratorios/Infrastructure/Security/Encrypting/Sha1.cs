using System;

namespace Isp.Laboratorios.Infrastructure.Security.Encrypting
{
    public static class Sha1
    {
        /// <summary>
        /// Encripta con Sha1 un valor string
        /// </summary>
        /// <param name="value">Valor que se va a encriptar</param>
        /// <returns><c>value</c> encriptado con Sha1</returns>
        public static string Encrypt(string value)
        {
            Byte[] bytes = System.Text.Encoding.Unicode.GetBytes(value);
            var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            Byte[] hash = sha1.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}