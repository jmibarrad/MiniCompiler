using Mini_Compiler.Semantic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler.Tree
{
    public class AddNode: BinaryOperatorNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftType = LeftOperand.ValidateSemantic();
            var rightType = RightOperand.ValidateSemantic();
            if (leftType is IntType && rightType is IntType)
                return leftType;
            if (leftType is StringType && rightType is StringType)
                return leftType;
            throw new SemanticException($"add is not supported for {leftType} and {rightType}");
        }

        public override string GenerateCode()
        {
            return $"({LeftOperand.GenerateCode()} + {RightOperand.GenerateCode()})";
        }
    }
}
