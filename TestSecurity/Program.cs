using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogFerit.BL;

namespace TestSecurity
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainText = "128256";
            string password = "Ferit Gezgil";

            string EncText = Security.Encrypt(plainText, password);

            Console.WriteLine(EncText);
        

            string DesText = Security.Decrypt(EncText, password);
            Console.WriteLine(DesText);
            Console.ReadLine();
        }
    }
}
