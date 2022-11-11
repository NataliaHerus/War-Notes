using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Hasher
    {
        private readonly string password;
        public Hasher(string password)
        {
            this.password = password;
        }

        public string ComputeHash()
        {
            string hashed = " ";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                hashed = builder.ToString();
            }
            return hashed;
        }
    }
}
