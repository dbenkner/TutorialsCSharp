using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace CompareHash
{
    public static class Hasher
    {
        public static void CompHash()
        {
            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;

            sSourceData = "assc";

            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);  

            Console.WriteLine(tmpSource.ToString());

            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            Console.WriteLine(ByteArrayToString(tmpHash));

            sSourceData = "Goog";
            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);

            byte[] tmpNewHash;

            tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            Console.WriteLine(ByteArrayToString(tmpNewHash));

            bool bEqual = false;
            if (tmpNewHash.Length == tmpHash.Length)
            {
                int i = 0;
                while ((i < tmpNewHash.Length) && (tmpNewHash[i] == tmpHash[i]))
                {
                    i += 1;
                }
                if (i == tmpNewHash.Length)
                {
                    bEqual = true;
                }
            }
            if (bEqual) { Console.WriteLine("The two hash values are the same!"); }
            else
            {
                Console.WriteLine("The two hash values are not hte same");
            }

        }
        public static string ComputeSha256Hash(string  input) {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ByteArrayToString(bytes);
            }
        }
        public static string ComputeSha256Hash(string input, int seed)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input + seed));
                return ByteArrayToString(bytes);
            }
        }
        public static string ComputeSha512Hash(string input)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ByteArrayToString(bytes);
            }
        }
        public static string SHA512short(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (byte b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }
                return hashedInputStringBuilder.ToString();
            }
        }

        public static string ByteArrayToString(byte[] arrInput)
        {
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (int i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }

}
