using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareHash
{
    public static class RandomStringGenerator
    {
        public static readonly List<char> lowerCaseArr = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
        public static readonly List<char> upperCaseArr = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
        public static readonly List<char> numArr = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];


        public static string CreateRandom(string input, int Length) { 
            StringBuilder stringBuilder = new StringBuilder();  
            Random random = new Random();
            List<char> comboList = new List<char>();
            comboList.AddRange(lowerCaseArr);
            comboList.AddRange(upperCaseArr);
            comboList.AddRange(numArr);
            while (stringBuilder.Length < Length)
            {
                var num = random.Next(0, comboList.Count);
                stringBuilder.Append(comboList[num]);
            }
            return stringBuilder.ToString();
        }
    }
}

