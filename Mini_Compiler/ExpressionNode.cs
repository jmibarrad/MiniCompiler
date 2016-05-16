using Mini_Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Compiler
{
    public class ExpressionNode
    {
    }

    public class SentenceNode
    {
        public SentenceNode NextSentence;
    }

    public class DeclarationNode : SentenceNode
    {
        public string Value;
        public TokenTypes Type;
    }

    public class ReadNode : SentenceNode
    {
        public string Id;
    }

    public class PrintNode : SentenceNode
    {
        public ExpressionNode Expression;
    }

    public class AssignNode : SentenceNode
    {
        public string Id;
        public ExpressionNode Expression;
    }

    public class MultNode : BinaryOperatorNode
    {

    }

    public class DivNode : BinaryOperatorNode
    {
    }



    public class IdNode : ExpressionNode
    {
        public string Value;
    }

    public class NumberNode : ExpressionNode
    {
        public float Value;
    }


}
