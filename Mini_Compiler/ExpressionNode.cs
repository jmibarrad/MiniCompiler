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
