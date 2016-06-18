using System;
using System.Collections.Generic;
using System.Data;
using Mini_Compiler.Lexer;
using Mini_Compiler.Semantic.Types;
using Mini_Compiler.Tree;

namespace Mini_Compiler.Semantic
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

                    var dimensions = Dimensions();

                    if (currentToken.Type == TokenTypes.Eos)
                    {
                        currentToken = lexer.GetNextToken();
                        return new DeclarationNode {Type = type, Value = name, Dimensions = dimensions};
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
                var idNode = Id();
                if (currentToken.Type == TokenTypes.Equal)
                {
                    currentToken = lexer.GetNextToken();
                    var exp = E();

                    if (currentToken.Type == TokenTypes.Eos)
                    {
                        currentToken = lexer.GetNextToken();
                        return new AssignNode {Id = idNode, Expression = exp};
                    }
                }

            }
            throw new SyntaxErrorException();
        }

        private List<int> Dimensions()
        {
            if (currentToken.Type == TokenTypes.LeftBracket)
            {
                currentToken = lexer.GetNextToken();
                if (currentToken.Type == TokenTypes.Number)
                {
                    int number = int.Parse(currentToken.Lexeme);
                    currentToken = lexer.GetNextToken();
                    if (currentToken.Type == TokenTypes.RightBracket)
                    {
                        currentToken = lexer.GetNextToken();
                        var dimensions = Dimensions();
                        dimensions.Insert(0, number);
                        return dimensions;
                    }
                    throw new SyntaxErrorException("No se cerro bracket");
                }
                throw new SyntaxErrorException("No se cerro bracket");
            }
            return new List<int>();
        }

        public SentenceNode Parse()
        {
             currentToken = lexer.GetNextToken();
            var eValue = SentenceList();
            if (currentToken.Type != TokenTypes.EOF)
            {
                throw new SyntaxException("Se esperaba EOF");
            }
            //int a; a= 1+2*2; print a+1;read b;
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
            }
            else if (currentToken.Type == TokenTypes.String)
            {
                var value = currentToken.Lexeme;
                currentToken = lexer.GetNextToken();
                return new StringNode {Value = value};


            }
            else if (currentToken.Type == TokenTypes.Id)
            {
                return Id();
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

        private IdNode Id()
        {
            string id = currentToken.Lexeme;
            currentToken = lexer.GetNextToken();
            var accesorList = AccesorList();
            return new IdNode
            {
                Value = id,
                AccesorsList = accesorList
            };
        }

        private List<Accesor> AccesorList()
        {
            if (currentToken.Type == TokenTypes.LeftBracket)
            {
                var accessor = Accessor();
                var accessorList = AccesorList();
                accessorList.Insert(0, accessor);
                return accessorList;
            }
            else
            {
                return new List<Accesor>();
            }
        }

        private Accesor Accessor()
        {
            currentToken = lexer.GetNextToken();
            var expression = E();
            if (currentToken.Type != TokenTypes.RightBracket)
            {
                throw new SyntaxErrorException("No se cerro bracket");
            }
            currentToken = lexer.GetNextToken();
            return new IndexAccesor(expression);
        }
    }

    internal class StringNode : ExpressionNode
    {
        public string Value { get; set; }
        public override BaseType ValidateSemantic()
        {
            return TypesTable.Instance.GetType("string");
        }

        public override string GenerateCode()
        {
            return Value;
        }
    }

    internal class IndexAccesor : Accesor
    {
        public ExpressionNode Expression { get; set; }

        public IndexAccesor(ExpressionNode expression)
        {
            Expression = expression;
        }
    }

    public abstract class Accesor
    {
    }

    internal class SyntaxException : Exception
    {
        public SyntaxException(string message) : base(message)
        {
            

        }
    }
}
