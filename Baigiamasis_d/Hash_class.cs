using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Baigiamasis_d
{
    class Hash_class
    {
        // Slaptažodžio užkodavimas
        public static string Create(string str)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(str), 0, Encoding.ASCII.GetByteCount(str));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }

        // Tikrina, ar slaptažodis sutampa su hash'u
        public static bool Validate(string pass, string hash)
        {
            string hash1 = Create(pass);
            if (hash1 == hash)
                return true;
            return false;
        }
    }
}
