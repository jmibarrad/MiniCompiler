using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Lexer;

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

        public void Parse()
        {
             currentToken = lexer.GetNextToken();
            E();
            if (currentToken.Type != TokenTypes.EOF)
            {
                throw new SyntaxException("Se esperaba EOF");
            }
        }

        private void E()
        {
            T();
            EP();

        }

        private void EP()
        {
            if (currentToken.Type == TokenTypes.Sum)
            {
                currentToken = lexer.GetNextToken();
                T();
                EP();
            }
            else if (currentToken.Type == TokenTypes.Sub)
            {
                currentToken = lexer.GetNextToken();
                T();
                EP();
            }
            else { }
        }

        private void T()
        {
            F();
            TP();
        }

        private void TP()
        {
            if (currentToken.Type == TokenTypes.Mult)
            {
                currentToken = lexer.GetNextToken();
                F();
                TP();
            }
            else if (currentToken.Type == TokenTypes.Div)
            {
                currentToken = lexer.GetNextToken();
                F();
                TP();
            }
            else { }

        }

        private void F()
        {
            if (currentToken.Type == TokenTypes.Number)
            {
                currentToken = lexer.GetNextToken();
            }else if (currentToken.Type == TokenTypes.Id)
            {
                currentToken = lexer.GetNextToken();
            }else if (currentToken.Type == TokenTypes.LeftParent)
            {
                currentToken = lexer.GetNextToken();
                E();
                if (currentToken.Type != TokenTypes.RightParent)
                {
                    throw new SyntaxException("(");

                }
                currentToken = lexer.GetNextToken();
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
