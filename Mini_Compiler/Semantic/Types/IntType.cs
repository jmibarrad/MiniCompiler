namespace Mini_Compiler.Semantic.Types
{
    class IntType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is IntType;
        }
    }
}