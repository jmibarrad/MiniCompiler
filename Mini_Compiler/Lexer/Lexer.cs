using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Compiler.Lexer
{
    public class Lexer
    {
        public StringContent Content;
        public Lexer(StringContent content)
        {
            Content = content;
        }

        public Token GetNextToken()
        {
            return null;
        }
    }
}
