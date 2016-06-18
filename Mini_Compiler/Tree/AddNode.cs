using System;
using Mini_Compiler.Semantic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler.Tree
{
    public class AddNode: BinaryOperatorNode
    {
        public AddNode()
        {
            //Rules.Add(new Tuple<BaseType, BaseType>(IntType), );
        }
        public override BaseType ValidateSemantic()
        {
            var leftType = LeftOperand.ValidateSemantic();
            var rightType = RightOperand.ValidateSemantic();
            if (leftType is IntType && rightType is IntType)
                return leftType;
            if (leftType is StringType && rightType is StringType)
                return leftType;
            if (leftType is IntType && rightType is StringType)
                return rightType;
            if (leftType is StringType && rightType is IntType)
                return leftType;
            throw new SemanticException($"add is not supported for {leftType} and {rightType}");
        }

        public override string GenerateCode()
        {
            return $"({LeftOperand.GenerateCode()} + {RightOperand.GenerateCode()})";
        }
    }
}
