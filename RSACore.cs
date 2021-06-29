using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using PemUtils;

namespace RSACore
{
    public class RSAImpl
    {
        private readonly IConfiguration _config;

        public RSAImpl(IConfigurationBuilder builder)
        {
            _config = builder.Build();
        }

        public string EncryptAndSave(string plainText, string keyFileName)
        {
            using (var rsa = GetRSACryptoProvider())
            {
                var plainTextBytes = Encoding.Unicode.GetBytes(plainText);
                var cipherTextBytes = rsa.Encrypt(plainTextBytes, RSAEncryptionPadding.Pkcs1);
                var cipherText = Convert.ToBase64String(cipherTextBytes);

                File.WriteAllText(@"C:\Temp\crypt.txt", cipherText);

                // Export the RSA private key (put this somewhere safe!)
                using (var fs = File.Create(keyFileName))
                {
                    using (var pem = new PemWriter(fs))
                    {
                        pem.WritePrivateKey(rsa);
                    }
                }

                return cipherText;
            }
        }

        public string LoadAndDecrypt(string strCadena, string keyFileName)
        {
            var plainText = string.Empty;

            using (var rsa = GetRSACryptoProvider(keyFileName))
            {

                var cipherText = strCadena;
                var cipherTextBytes = Convert.FromBase64String(cipherText);
                var plainTextBytes = rsa.Decrypt(cipherTextBytes, RSAEncryptionPadding.Pkcs1);
                plainText = Encoding.Unicode.GetString(plainTextBytes);
            }

            return plainText;
        }

        private RSA GetRSACryptoProvider(string keyFileName = null)
        {
            var rsa = RSA.Create();

            try
            {
                if (string.IsNullOrEmpty(keyFileName))
                {
                    rsa.KeySize = Convert.ToInt32(_config["Settings:KeySize"]);
                }
                else
                {
                    using (var privateKey = File.OpenRead(keyFileName))
                    {
                        using (var pem = new PemReader(privateKey))
                        {
                            var rsaParameters = pem.ReadRsaKey();
                            rsa.ImportParameters(rsaParameters);
                        }
                    }
                }
                return rsa;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetRSACryptoProvider(): {ex}");
                return null;
            }
        }
    }
}