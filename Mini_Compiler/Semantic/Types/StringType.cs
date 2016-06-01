namespace Mini_Compiler.Semantic.Types
{
    class StringType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is StringType;
        }
    }
}