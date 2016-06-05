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
            string code = @"
int a[2][3];
a=a[4];";
            SampleParser parser = new SampleParser(new Lexer.Lexer(new StringContent(code)));
            var tree = parser.Parse();
            tree.ValidateSemantic();
            Console.WriteLine("No errors found.");
            Console.ReadKey();
        }
    }
}
