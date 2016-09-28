using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Compiler.Semantic.Types
{
    class FunctionType : BaseType
    {
        public BaseType FunctType;
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is FunctionType;

        }
    }
}
