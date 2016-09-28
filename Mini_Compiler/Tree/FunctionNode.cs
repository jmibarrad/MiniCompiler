using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Compiler.Tree
{
    class FunctionNode : SentenceNode
    {
        public List<ExpressionNode> Parametros;
        public IdNode FunctionName;
        public string ReturnType;
        public List<SentenceNode> SentencesList;

        
       

        protected override void ValidateNodeSemantic()
        {
      
        }

        protected override string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
