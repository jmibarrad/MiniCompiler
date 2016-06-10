using Mini_Compiler.Semantic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler.Tree
{
    public class SubNode: BinaryOperatorNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftType = LeftOperand.ValidateSemantic();
            var rightType = RightOperand.ValidateSemantic();
            if (leftType is IntType && rightType is IntType)
                return leftType;
            throw new SemanticException($"sub is not supported for {leftType} and {rightType}");
        }

        public override string GenerateCode()
        {
            return $"({LeftOperand.GenerateCode()} - {RightOperand.GenerateCode()})";
        }
    }
}
