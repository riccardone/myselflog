using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using MySelf.Diab.Core.Contracts;

namespace MySelf.Diab.Security
{
    public class CryptoService : ICryptoService
    {
        private const string SecretKey = "q<@bh+v{M~+],|<h&3~7>m0iGbZH^0W+eBk)}S}ST/Z`iUMUKpwTV+C||:+:Fq7r')";

        public string GenerateKey(string name)
        {
            var value = string.Concat(name, SecretKey);
            var key = ComputeHash(value, new SHA256CryptoServiceProvider());
            var hashCodeThatCoudlBeTheSameForDifferentString = String.Format("{0:X}", key.GetHashCode());
            return hashCodeThatCoudlBeTheSameForDifferentString;
        }

        private static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public string GenerateKey()
        {
            var uniqueVal = Guid.NewGuid();
            return String.Format("{0:X}", uniqueVal.GetHashCode()); 
        }
    }
}
