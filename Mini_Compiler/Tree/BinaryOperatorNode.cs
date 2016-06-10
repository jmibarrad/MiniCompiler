namespace Mini_Compiler.Tree
{
    public abstract class BinaryOperatorNode: ExpressionNode
    {
        public ExpressionNode RightOperand;
        public ExpressionNode LeftOperand;
    }
}
