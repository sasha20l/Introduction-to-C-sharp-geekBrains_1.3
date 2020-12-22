using System;
using System.Linq;


namespace ReversString
{
    class Program
    {
        static void Main(string[] args)
        {
            string s;
            while ((s = Console.ReadLine()) != null)
                Console.WriteLine(new string(s.Reverse().ToArray()));
        }
    }
}
