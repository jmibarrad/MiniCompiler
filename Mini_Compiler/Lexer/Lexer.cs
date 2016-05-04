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
        private Symbol currentSymbol;

        public Lexer(StringContent content)
        {
            Content = content;
            currentSymbol = content.nextSymbol();
        }

        public Token GetNextToken()
        {
            int state = 0;
            string lexeme = "";
            int tokenRow = 0;
            int tokenColumn = 0;

            while (true)
            {
                switch (state)
                {
                    case 0:
                        if(currentSymbol.CurrentSymbol == '\0')
                        {
                            state = 6;
                            tokenColumn = currentSymbol.Column;
                            tokenRow = currentSymbol.Row;
                            lexeme = "$";
                        }
                        else if (char.IsWhiteSpace(currentSymbol.CurrentSymbol)){
                            state = 0;
                            currentSymbol = Content.nextSymbol();
                        }
                        else if (char.IsLetter(currentSymbol.CurrentSymbol))
                        {
                            state = 1;
                            tokenColumn = currentSymbol.Column;
                            tokenRow = currentSymbol.Row;
                            lexeme += currentSymbol.CurrentSymbol;
                            currentSymbol = Content.nextSymbol();
                        }
                        else if (char.IsDigit(currentSymbol.CurrentSymbol))
                        {
                            state = 2;
                            tokenColumn = currentSymbol.Column;
                            tokenRow = currentSymbol.Row;
                            lexeme += currentSymbol.CurrentSymbol;
                            currentSymbol = Content.nextSymbol();
                        }
                        else if (currentSymbol.CurrentSymbol == '+' || currentSymbol.CurrentSymbol == '*' || currentSymbol.CurrentSymbol == '-' || currentSymbol.CurrentSymbol == '='
                           || currentSymbol.CurrentSymbol == ';' || currentSymbol.CurrentSymbol == '(' || currentSymbol.CurrentSymbol == ')' || currentSymbol.CurrentSymbol == '/' || currentSymbol.CurrentSymbol == '[' || currentSymbol.CurrentSymbol == ']')
                        {
                            lexeme += currentSymbol.CurrentSymbol;
                            currentSymbol = Content.nextSymbol();
                            state = 5;
                        }
                        else if (currentSymbol.CurrentSymbol == '\"')
                        {
                            state = 3;
                            tokenColumn = currentSymbol.Column;
                            tokenRow = currentSymbol.Row;
                            lexeme += currentSymbol.CurrentSymbol;
                            currentSymbol = Content.nextSymbol();
                        }
                        else
                        {
                            throw new LexicalException($"Symbol {currentSymbol.CurrentSymbol} not recognized at Row:{currentSymbol.Row} Col: {currentSymbol.Column}");
                        }
                        break;
                    case 1:
                        if (char.IsLetterOrDigit(currentSymbol.CurrentSymbol))
                        {
                            state = 1;
                            lexeme += currentSymbol.CurrentSymbol;
                            currentSymbol = Content.nextSymbol();
                        }
                        else
                        {
                            return new Token { Type = TokenTypes.Id, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        }
                        break;
                    case 2:
                        if (char.IsDigit(currentSymbol.CurrentSymbol))
                        {
                            state = 2;
                            lexeme += currentSymbol.CurrentSymbol;
                            currentSymbol = Content.nextSymbol();
                        }
                        else
                        {
                            return new Token { Type = TokenTypes.Number, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        }
                        break;
                    case 3:
                        if (currentSymbol.CurrentSymbol != '\"')
                        {
                            lexeme += currentSymbol.CurrentSymbol;
                            currentSymbol = Content.nextSymbol();
                        }
                        else if (currentSymbol.CurrentSymbol == '\"')
                        {
                            lexeme += currentSymbol.CurrentSymbol;
                            currentSymbol = Content.nextSymbol();
                            return new Token { Type = TokenTypes.Number, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        }
                        

                        break;
                    case 5:
                        if (lexeme.Equals("+"))
                            return new Token() { Type = TokenTypes.Sum, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals("*"))
                            return new Token() { Type = TokenTypes.Mult, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals("-"))
                            return new Token() { Type = TokenTypes.Sub, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals("="))
                            return new Token() { Type = TokenTypes.Equal, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals(";"))
                            return new Token() { Type = TokenTypes.Eos, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals("("))
                            return new Token() { Type = TokenTypes.LeftParent, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals(")"))
                            return new Token() { Type = TokenTypes.RightParent, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals("/"))
                            return new Token() { Type = TokenTypes.Div, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals("["))
                            return new Token() { Type = TokenTypes.LeftBracket, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        else if (lexeme.Equals("]"))
                            return new Token() { Type = TokenTypes.RightBracket, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                        break;
                    case 6:
                        return new Token { Type = TokenTypes.EOF, Lexeme = lexeme, Column = tokenColumn, Row = tokenRow };
                    default:
                        break;
                }
            }

            throw new NotImplementedException();
        }
    }
}
