using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Lexer;
using TechTalk.SpecFlow.Assist.ValueComparers;

namespace Mini_Compiler
{
    class SampleParser
    {
        private Lexer.Lexer lexer;
        private Token currentToken;

        public SampleParser(Lexer.Lexer lexer)
        {
            this.lexer = lexer;
        }

        public float Parse()
        {
             currentToken = lexer.GetNextToken();
            var eValue = E();
            if (currentToken.Type != TokenTypes.EOF)
            {
                throw new SyntaxException("Se esperaba EOF");
            }
            return eValue;
        }

        private float E()
        {
            var tValue = T();
            return EP(tValue);

        }

        private float EP(float param)
        {
            if (currentToken.Type == TokenTypes.Sum)
            {
                currentToken = lexer.GetNextToken();
                var tValue = T();
                return EP(param + tValue);
            }
            else if (currentToken.Type == TokenTypes.Sub)
            {
                currentToken = lexer.GetNextToken();
                var tValue = T();
                return EP(param - tValue);
            }
            else
            {
                return param;
            }
        }

        private float T()
        {
            var fValue = F();
            return TP(fValue);
        }

        private float TP(float param)
        {
            if (currentToken.Type == TokenTypes.Mult)
            {
                currentToken = lexer.GetNextToken();
                var fValue = F();
                return TP(param * fValue);
            }
            else if (currentToken.Type == TokenTypes.Div)
            {
                currentToken = lexer.GetNextToken();
                var fValue = F();
                return TP(param/fValue);
            }
            else
            {
                return param;
            }

        }

        private float F()
        {
            if (currentToken.Type == TokenTypes.Number)
            {
                var value = float.Parse(currentToken.Lexeme);
                currentToken = lexer.GetNextToken();
                return value;
            }else if (currentToken.Type == TokenTypes.Id)
            {
                currentToken = lexer.GetNextToken();
                return 0;
            }else if (currentToken.Type == TokenTypes.LeftParent)
            {
                currentToken = lexer.GetNextToken();
                var value = E();
                if (currentToken.Type != TokenTypes.RightParent)
                {
                    throw new SyntaxException("(");

                }
                currentToken = lexer.GetNextToken();
                return value;
            }
            else
            {
                throw new SyntaxErrorException("F");
            }
        }
    }

    internal class SyntaxException : Exception
    {
        public SyntaxException(string message) : base(message)
        {
            

        }
    }
}
