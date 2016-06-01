using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Lexer;

namespace Mini_Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleParser parser = new SampleParser(new Lexer.Lexer(new StringContent("int a;string b; a= 1+2*b; print a+1;read a;")));
            var tree = parser.Parse();
            tree.ValidateSemantic();
            Console.ReadKey();
            Console.WriteLine("");
        }
    }
}
