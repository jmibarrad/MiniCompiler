using Mini_Compiler.Semantic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler.Tree
{
    public class MultNode : BinaryOperatorNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftType = LeftOperand.ValidateSemantic();
            var rightType = RightOperand.ValidateSemantic();
            if (leftType is IntType && rightType is IntType)
                return leftType;
            if (leftType is IntType && rightType is StringType)
                return rightType;
            if (leftType is StringType && rightType is IntType)
                return leftType;
            throw new SemanticException($"mul is not supported for {leftType} and {rightType}");
        }

        public override string GenerateCode()
        {
            var leftType = LeftOperand.ValidateSemantic();
            var rightType = RightOperand.ValidateSemantic();
            if (leftType is IntType && rightType is StringType)
            {
                var count = (NumberNode) LeftOperand;
                var str = (StringNode) RightOperand;
                return $"MultiplyString({count.Value},{str.Value})";
            }
            else if (leftType is StringType && rightType is IntType)
            {
                var str = (StringNode)LeftOperand;
                var count = (NumberNode)RightOperand;
                return $"MultiplyString({count.Value},{str.Value})";
            }
             
            return $"({LeftOperand.GenerateCode()} * {RightOperand.GenerateCode()})";
        }
    }
}