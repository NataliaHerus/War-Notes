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
        private readonly byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        private readonly string password;

        public Hasher(string password)
        {
            this.password = password;
        }

        public string ComputeHash()
        {
            byte[] bytesToHash = Encoding.UTF8.GetBytes(password);
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(24));
        }
    }
}
