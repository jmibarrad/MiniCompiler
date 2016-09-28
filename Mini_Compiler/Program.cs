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
                        struct Persona {
                            string name;
                        }

                        struct fecha {
                            int a;
                            string b;
                            Persona per;
                            int c;
                        }
                        
                        fecha fch;
                        ";
            SampleParser parser = new SampleParser(new Lexer.Lexer(new StringContent(code)));
            try
            {
                var tree = parser.Parse();
                tree.ValidateSemantic();
                var maincode = tree.TreeGenerateCode();
                var javaCode = GenerateMain.InitJavaCode(maincode);
                Console.Write(javaCode);
                Console.WriteLine("No errors found.");
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);


            }

            Console.ReadKey();
        }
    }
}
