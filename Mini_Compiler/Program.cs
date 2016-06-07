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
                        a[0][a[0]]=a[2][3];";
            SampleParser parser = new SampleParser(new Lexer.Lexer(new StringContent(code)));
            try
            {
                var tree = parser.Parse();
                tree.ValidateSemantic();
                Console.WriteLine("No errors found.");
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);

            }
            
            Console.ReadKey();
        }
    }
}
