namespace Mini_Compiler.Lexer
{
    public enum TokenTypes{
        EOF,
        Id,
        Number,
        Sum,
        Mult,
        Sub,
        Equal,
        Eos,
        LeftParent,
        Literal,
        RightParent,
        Div,
        LeftBracket,
        RightBracket,
        Read,
        Print,
        Int,
        String,
        Struct,
        LeftKey,
        RightKey,
        Access
    }

    public class Token
    {

        public string Lexeme;
        public TokenTypes Type;
        public int Row;
        public int Column;

        public Token()
        {
        }
    }
}