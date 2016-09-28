using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Generator;
using Mini_Compiler.Semantic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler.Tree
{
    public class StructDeclarationNode : SentenceNode
    {
        public string StructName;
        public List<DeclarationNode> DeclarationList;

        public override void ValidateNodeSemantic()
        {
            var _struct = new StructType();
            foreach (var typeDeclarationNode in DeclarationList)
            {
                var type = typeDeclarationNode.GetBaseType();
                _struct.Properties.Add(typeDeclarationNode.Value, type);
            }
            TypesTable.Instance.RegisterType(StructName, _struct);
        }

        public override string GenerateCode()
        {
            var _class = "\nclass " + StructName + "{\n";
            foreach (var declarationNode in DeclarationList)
            {
                _class += "\t"+declarationNode.GenerateCode() + "\n";
            }
            GenerateMain.ClassDefinitionCode += _class + "}";
            return "";
        }
    }
}
