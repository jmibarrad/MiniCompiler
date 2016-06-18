using System;
using System.Collections.Generic;
using Mini_Compiler.Semantic.Types;
using TechTalk.SpecFlow.Infrastructure;

namespace Mini_Compiler.Tree
{
    public abstract class BinaryOperatorNode: ExpressionNode
    {
        public Dictionary<Tuple<BaseType, BaseType>, BaseType> Rules = new Dictionary<Tuple<BaseType, BaseType>, BaseType>();
        public ExpressionNode RightOperand;
        public ExpressionNode LeftOperand;
    }
}
