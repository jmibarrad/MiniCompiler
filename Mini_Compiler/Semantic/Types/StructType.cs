using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Compiler.Semantic.Types
{
    public class StructType : BaseType
    {
        public StructType()
        {
            Properties = new Dictionary<string, BaseType>();
        }
        public Dictionary<string, BaseType> Properties { get; set; }

        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is StructType;
        }
    }
}
