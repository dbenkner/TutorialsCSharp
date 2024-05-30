
using System.Diagnostics;
using System.Globalization;
using System.Text;
using WordCount;


var path = @"C:\Users\David\source\repos\TutorialsC#\WordCount\WordCount\test.txt";

int lineCount = WordCounter.LineCount(path);

Console.WriteLine($"Linecount is: {lineCount}");




Console.WriteLine(WordCounter.ByteCount(path));

Console.WriteLine(WordCounter.WordCount(path));

WordCounter.LocaleByteCount(path);