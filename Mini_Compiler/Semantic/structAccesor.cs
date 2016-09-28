using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Semantic.Types;
using Mini_Compiler.Tree;

namespace Mini_Compiler.Semantic
{
    public class StructAccesor : Accesor
    {
        public string IdNode { get; set; }

        public override BaseType Validate(BaseType type)
        {
            if (!(type is StructType)) throw new SemanticException("It not a record type.");

            var recordType = (StructType)type;

            if (!recordType.Properties.ContainsKey(IdNode)) throw new SemanticException($"Property: {IdNode} doen't exists in record.");

            return recordType.Properties[IdNode];
        }
    }
}
