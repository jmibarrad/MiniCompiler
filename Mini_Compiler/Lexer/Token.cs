﻿namespace Mini_Compiler.Lexer
{
    public enum TokenTypes{
        EOF,
        Id,
        Number
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