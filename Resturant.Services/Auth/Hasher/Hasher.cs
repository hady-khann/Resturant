using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Resturant.Services.Auth.Hasher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Services.Auth.Hasher
{
    public class Hasher : IHasher
    {
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        private const string Phrase = "@This-is *the_Key.Secret*Phrase#";

        public string Hash_Generator(string Password)
        {
            try
            {
                Password = Password.Trim();
                using (SHA256 hash = SHA256Managed.Create())
                {
                    Encoding enc = Encoding.ASCII;
                    Byte[] result = hash.ComputeHash(enc.GetBytes(Password));

                    return Base64Helper.Base64UrlEncode(result);
                }

            }
            catch
            {
                //log
                return string.Empty;
            }
        }

        public bool Hash_Validator(string Hash_String, string password)
        {
            if (Hash_Generator(password) == Hash_String)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        protected class Base64Helper
        {
            public static string Base64UrlEncode(byte[] arg)
            {
                string s = Convert.ToBase64String(arg); // Regular base64 encoder
                s = s.Replace('+', '-'); // 62nd char of encoding
                s = s.Replace('/', '_'); // 63rd char of encoding
                return s;
            }


            private static byte[] Base64UrlDecode(string arg)
            {
                string s = arg;
                s = s.Replace('-', '+'); // 62nd char of encoding
                s = s.Replace('_', '/'); // 63rd char of encoding
                switch (s.Length % 4) // Pad with trailing '='s
                {
                    case 0:
                        break; // No pad chars in this case
                    case 2:
                        s += "==";
                        break; // Two pad chars
                    case 3:
                        s += "=";
                        break; // One pad char
                    default:
                        throw new Exception("Illegal base64url string!");
                }
                return Convert.FromBase64String(s); // Standard base64 decoder
            }

        }
    }
}
