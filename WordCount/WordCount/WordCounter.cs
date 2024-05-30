using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordCount
{
    public static class WordCounter
    {
        public static int LineCount(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            int count = 0;
            try
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    count++;
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return count;
        }
        public static int ByteCount(string path)
        {
            int count = File.ReadAllBytes(path).Count();
            return count;
        }
        public static int WordCount(string path)
        {
            StreamReader reader = new StreamReader(path);
            string sample = reader.ReadToEnd();
            reader.Close();
            char[] delimiters = new char[] {' ', '\n', '\r', '\t'};
            int count = sample.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Count();
            return count;
        }
        public static int LocaleByteCount(string path)
        {
            var full = File.ReadAllBytes(path);
            full.
            var sample = Encoding.Default.GetBytes(full).Length;
            Console.WriteLine($"{sample}");
            return 0;
        }
    }
}
