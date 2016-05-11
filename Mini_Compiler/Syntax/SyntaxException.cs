using System;

namespace Mini_Compiler.Syntax
{
    internal class SyntaxException : Exception
    {
        public SyntaxException(string message)
            : base(message)
        {


        }
    }
}