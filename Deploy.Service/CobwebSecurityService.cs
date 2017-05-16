using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Deploy.Service
{
    public class Encryption
    {
        private string _iv;
        private string _salt;

        private string Key { get; set; }

        private string Salt
        {
            get
            {
                if (_salt.Length >= 8) return _salt;
                return _salt.PadRight(8, 'x');
            }
            set
            {
                _salt = value;
            }
        }
        private string IV
        {
            get
            {

                if (_iv.Length > 16) return _iv.Substring(0, 16);
                return _iv.PadRight(16, 'x');
            }
            set { _iv = value; }
        }
        private int KeySize { get; set; }
        private int Iterations { get; set; }

        private Rfc2898DeriveBytes Password
        {
            get { return new Rfc2898DeriveBytes(Key, SaltBytes, Iterations); }
        }

        private byte[] KeyBytes
        {
            get { return Password.GetBytes(KeySize / 8); }
        }

        private byte[] IVBytes
        {
            get { return Encoding.UTF8.GetBytes(IV); }
        }

        private byte[] SaltBytes
        {
            get { return Encoding.UTF8.GetBytes(Salt); }
        }

        public Encryption(string key, string salt, int iterations, string iv, int keySize)
        {
            Key = key;
            Salt = salt;
            Iterations = iterations;
            IV = iv;
            KeySize = keySize;
        }

        public string EncryptString(string plainText)
        {
            var textBytes = Encoding.UTF8.GetBytes(plainText);
            var aes = Aes.Create();
            aes.Key = KeyBytes;
            aes.IV = IVBytes;
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(textBytes, 0, textBytes.Length);

            cs.FlushFinalBlock();

            var cryptoBytes = ms.ToArray();

            return Convert.ToBase64String(cryptoBytes);
        }


        public string DecryptString(string base64String)
        {
            var encBytes = Convert.FromBase64String(base64String);
            var aes = Aes.Create();

            var ms = new MemoryStream(encBytes);
            var cs = new CryptoStream(ms, aes.CreateDecryptor(KeyBytes, IVBytes), CryptoStreamMode.Read);

            var textBytes = new byte[encBytes.Length];
            var byteCount = cs.Read(textBytes, 0, textBytes.Length);

            var pt = Encoding.UTF8.GetChars(textBytes, 0, byteCount);
            var plainText = pt.Aggregate("", (current, c) => current + c);

            return plainText;
        }

    }
}
