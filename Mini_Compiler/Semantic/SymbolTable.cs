using System.Collections.Generic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler.Semantic
{
    public class SymbolTable
    {
        private Dictionary<string,BaseType> _table;
        private static SymbolTable _instance;


        private SymbolTable()
        {
            _table = new Dictionary<string, BaseType>();
            
        }


        public static SymbolTable Instance => _instance ?? (_instance = new SymbolTable());


        public void DeclareVariable(string name, string typeName)
        {
            if (_table.ContainsKey(name))
            {
                throw new SemanticException($"Variable  :{name} exists.");
            }

            if(TypesTable.Instance.Contains(name))
                throw new SemanticException($"  :{name} iz a taippp.");

            _table.Add(name, TypesTable.Instance.GetType(typeName));
        }

        public BaseType GetVariable(string name)
        {
            if (_table.ContainsKey(name))
            {
                return _table[name];
            }

            throw new SemanticException($"Variable :{name} doesn't exists.");
        }

       
    }
}