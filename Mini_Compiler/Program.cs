using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Generator;
using Mini_Compiler.Lexer;
using Mini_Compiler.Semantic;

namespace Mini_Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = @"
                        int a;
                        a = 12;
                        int b;
                        read b;
                        string val;
                        read val;
                        print a + b;
                        ";
            SampleParser parser = new SampleParser(new Lexer.Lexer(new StringContent(code)));
            try
            {
                var tree = parser.Parse();
                tree.ValidateSemantic();
                var javaCode = GenerateMain.InitJavaCode(tree.TreeGenerateCode());
                Console.Write(javaCode);
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
