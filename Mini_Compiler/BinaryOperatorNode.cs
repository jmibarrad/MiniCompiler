using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler
{
    public abstract class BinaryOperatorNode: ExpressionNode
    {
        public ExpressionNode RightOperand;
        public ExpressionNode LeftOperand;

       
    }
}
