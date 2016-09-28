using Mini_Compiler.Semantic.Types;
using Mini_Compiler.Tree;

namespace Mini_Compiler.Semantic
{
    internal class IndexAccesor : Accesor
    {
        public ExpressionNode Expression { get; set; }

        public IndexAccesor(ExpressionNode expression)
        {
            Expression = expression;
        }

        public override BaseType Validate(BaseType type)
        {
            throw new System.NotImplementedException();
        }
    }
}