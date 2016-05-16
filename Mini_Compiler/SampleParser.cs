using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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

        public SentenceNode SentenceList()
        {
            var sentence = Sentence();
            if (currentToken.Type == TokenTypes.EOF)
            {
                return sentence;
            }
            sentence.NextSentence = SentenceList();
            return sentence;
        }

        private SentenceNode Sentence()
        {
            if (currentToken.Type == TokenTypes.Int || currentToken.Type == TokenTypes.String)
            {
                var type = currentToken.Type;
                currentToken = lexer.GetNextToken();
               
                if (currentToken.Type == TokenTypes.Id)
                {
                    string name = currentToken.Lexeme;

                    currentToken = lexer.GetNextToken();

                    if (currentToken.Type == TokenTypes.Eos)
                    {
                        currentToken = lexer.GetNextToken();
                        return new DeclarationNode {Type = type, Value = name};
                    }
                }
            }else if (currentToken.Type == TokenTypes.Read)
            {
           
                currentToken = lexer.GetNextToken();

                if (currentToken.Type == TokenTypes.Id)
                {
                    string name = currentToken.Lexeme;

                    currentToken = lexer.GetNextToken();

                    if (currentToken.Type == TokenTypes.Eos)
                    {

                        currentToken = lexer.GetNextToken();
                        return new ReadNode() { Id = name };
                    }
                }

            }
            else if (currentToken.Type == TokenTypes.Print)
            {
               
                currentToken = lexer.GetNextToken();

                var exp = E();
                    if (currentToken.Type == TokenTypes.Eos)
                    {

                    currentToken = lexer.GetNextToken();
                    return new PrintNode { Expression = exp };
                    }
                
            }
            else if (currentToken.Type == TokenTypes.Id)
            {
                string name = currentToken.Lexeme;
                currentToken = lexer.GetNextToken();
                if (currentToken.Type == TokenTypes.Equal)
                {
                    currentToken = lexer.GetNextToken();
                    var exp = E();

                    if (currentToken.Type == TokenTypes.Eos)
                    {
                        currentToken = lexer.GetNextToken();
                        return new AssignNode {Id = name, Expression = exp};
                    }
                }

            }
            throw new SyntaxErrorException();
        }

        public SentenceNode Parse()
        {
             currentToken = lexer.GetNextToken();
            var eValue = SentenceList();
            if (currentToken.Type != TokenTypes.EOF)
            {
                throw new SyntaxException("Se esperaba EOF");
            }
            return eValue;
        }

        private ExpressionNode E()
        {
            var tValue = T();
            return EP(tValue);

        }

        private ExpressionNode EP(ExpressionNode param)
        {
            if (currentToken.Type == TokenTypes.Sum)
            {
                currentToken = lexer.GetNextToken();
                var tValue = T();
                var node = new AddNode { LeftOperand = param, RightOperand = tValue };

                return EP(node);
            }
            else if (currentToken.Type == TokenTypes.Sub)
            {
                currentToken = lexer.GetNextToken();
                var tValue = T();
                var node = new SubNode { LeftOperand = param, RightOperand = tValue };

                return EP(node);
            }
            else
            {
                return param;
            }
        }

        private ExpressionNode T()
        {
            var fValue = F();
            return TP(fValue);
        }

        private ExpressionNode TP(ExpressionNode param)
        {
            if (currentToken.Type == TokenTypes.Mult)
            {
                currentToken = lexer.GetNextToken();
                var fValue = F();
                var node = new MultNode {LeftOperand = param, RightOperand = fValue};
                return TP(node);
            }
            else if (currentToken.Type == TokenTypes.Div)
            {
                currentToken = lexer.GetNextToken();
                var fValue = F();
                var node = new DivNode() { LeftOperand = param, RightOperand = fValue };
                return TP(node);
            }
            else
            {
                return param;
            }

        }

        private ExpressionNode F()
        {
            if (currentToken.Type == TokenTypes.Number)
            {
                var value = float.Parse(currentToken.Lexeme);
                currentToken = lexer.GetNextToken();
                return new NumberNode {Value = value};
            }else if (currentToken.Type == TokenTypes.Id)
            {
                string id = currentToken.Lexeme;
                currentToken = lexer.GetNextToken();
                return new IdNode
                {
                    Value = id
                        
                };
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
