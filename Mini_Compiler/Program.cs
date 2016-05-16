﻿using System;
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
            SampleParser parser = new SampleParser(new Lexer.Lexer(new StringContent("int a; a= 1+2*2; print a+1;read b;")));
            Console.Write(parser.Parse());
            Console.ReadKey();
            Console.WriteLine("");
        }
    }
}
