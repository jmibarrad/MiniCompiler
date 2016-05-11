using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Lexer;

namespace Mini_Compiler.Syntax
{
    class SampleParser
    {
        private readonly Lexer.Lexer _lexer;
        private Token _currentToken;

        public SampleParser(Lexer.Lexer lexer)
        {
            this._lexer = lexer;
        }

        public void Parse()
        {
            _currentToken = _lexer.GetNextToken();
            E();
            if (_currentToken.Type != TokenTypes.Eof)
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
            if (_currentToken.Type == TokenTypes.SumOperator)
            {
                _currentToken = _lexer.GetNextToken();
                T();
                EP();
            }
            else if (_currentToken.Type == TokenTypes.MinusOperator)
            {
                _currentToken = _lexer.GetNextToken();
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
            if (_currentToken.Type == TokenTypes.MultiplyOperator)
            {
                _currentToken = _lexer.GetNextToken();
                F();
                TP();
            }
            else if (_currentToken.Type == TokenTypes.DivideOperator)
            {
                _currentToken = _lexer.GetNextToken();
                F();
                TP();
            }
            else { }

        }

        private void F()
        {
            if (_currentToken.Type == TokenTypes.NumericLiteral)
            {
                _currentToken = _lexer.GetNextToken();
            }
            else if (_currentToken.Type == TokenTypes.Id)
            {
                _currentToken = _lexer.GetNextToken();
            }
            else if (_currentToken.Type == TokenTypes.OpenParenthesisOperator)
            {
                _currentToken = _lexer.GetNextToken();
                E();
                if (_currentToken.Type != TokenTypes.CloseParenthesisOperator)
                {
                    throw new SyntaxException("(");

                }
                _currentToken = _lexer.GetNextToken();
            }
            else
            {
                throw new SyntaxErrorException("F");
            }
        }
    }
}
