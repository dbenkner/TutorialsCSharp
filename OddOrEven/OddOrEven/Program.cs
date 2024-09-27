// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");
int[] arr = { 4, 5, 11, 20, 43, 98, 34,55, 23};

List<int> oddList = Odds(arr);

foreach(var num in oddList)
{
    Console.WriteLine(num);
}
static List<int> Odds(int[] arrr)
{
    List<int> ints = new List<int>();
    foreach(int num in arrr)
    {
        if(num % 2 != 0) ints.Add(num);
    }
    return ints;
}   