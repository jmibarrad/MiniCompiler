using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Semantic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler.Tree
{
    public class StructNode : DeclarationNode
    {
        public string StructType;
        public override void ValidateNodeSemantic()
        {
            var type = TypesTable.Instance.GetType(StructType);
            TypesTable.Instance.RegisterType(Value, type);
        }

        public override string GenerateCode()
        {
            return $"{StructType} {Value} = new {StructType}();";
        }

        public override BaseType GetBaseType()
        {
            return TypesTable.Instance.GetType(StructType);
        }
    }
}
