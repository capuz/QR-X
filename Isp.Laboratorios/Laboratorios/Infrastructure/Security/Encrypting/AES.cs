using System;

namespace Isp.Laboratorios.Infrastructure.Security.Encrypting
{
    public static class AES
    {
        /// <summary>
        /// Encripta con AES un valor string
        /// </summary>
        /// <param name="value">Valor que se va a encriptar</param>
        /// <param name="key">Llave privada para encriptar</param>
        /// <returns><c>value</c> encriptado con AES</returns>
        public static string Encrypt(string value, string key)
        {
            var AES = new System.Security.Cryptography.RijndaelManaged();
            byte[] _value = System.Text.Encoding.UTF8.GetBytes(value);

            key = Sha1.Encrypt(key).PadRight(32, '=');
            AES.Key = System.Text.Encoding.ASCII.GetBytes(key);
            AES.IV = System.Text.Encoding.ASCII.GetBytes(key.Substring(12, 16));

            string result = Convert.ToBase64String(AES.CreateEncryptor().TransformFinalBlock(_value, 0, _value.Length));
            AES.Clear();
            return result;
        }

        /// <summary>
        /// Desencripta con AES un valor string
        /// </summary>
        /// <param name="value">Valor encriptado que se va a desencriptar</param>
        /// <param name="key">Llave privada para desencriptar</param>
        /// <returns><c>value</c> desencriptado con AES</returns>
        public static string Decrypt(string value, string key)
        {
            var AES = new System.Security.Cryptography.RijndaelManaged();

            byte[] _value = Convert.FromBase64String(value);

            key = Sha1.Encrypt(key).PadRight(32, '=');
            AES.Key = System.Text.Encoding.ASCII.GetBytes(key);
            AES.IV = System.Text.Encoding.ASCII.GetBytes(key.Substring(12, 16));

            string result = System.Text.Encoding.UTF8.GetString(AES.CreateDecryptor().TransformFinalBlock(_value, 0, _value.Length));
            AES.Clear();
            return result;
        }
    }
}