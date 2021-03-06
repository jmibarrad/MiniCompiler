﻿using System.Collections.Generic;
using Mini_Compiler.Lexer;
using Mini_Compiler.Semantic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler.Tree
{
    public abstract class ExpressionNode
    {
        public abstract BaseType ValidateSemantic();
        public abstract string GenerateCode();

    }

    public abstract class SentenceNode
    {
        public void ValidateSemantic()
        {
            ValidateNodeSemantic();
            if(NextSentence!=null)
                NextSentence.ValidateSemantic();
        }

        public string TreeGenerateCode()
        {
            var fragment = GenerateCode();
            if (NextSentence != null && !(NextSentence is StructDeclarationNode))
            {
                var code = NextSentence.TreeGenerateCode();
                fragment += "\n" + code;
            }
            else if(NextSentence is StructDeclarationNode)
            {
                fragment +=NextSentence.TreeGenerateCode();
            }
            return fragment;
        }

        public abstract void ValidateNodeSemantic();
        public abstract string GenerateCode();

        public SentenceNode NextSentence;

    }

    public class DeclarationNode : SentenceNode
    {
        public string Value;
        public TokenTypes Type;
        public List<int> Dimensions;
        public Dictionary<string, TokenTypes> TypeDictionary = new Dictionary<string, TokenTypes>
        {
            { "int", TokenTypes.Int},
            { "string", TokenTypes.String},
        };
        public override void ValidateNodeSemantic()
        {
            string typeName = Type == TokenTypes.Int ? "int" : "string";
            SymbolTable.Instance.DeclareVariable(Value,typeName, Dimensions);
        }

        public override string GenerateCode()
        {
            string typeName = Type == TokenTypes.Int ? "int" : "String";
            return $"{typeName} {Value};";

        }

        public virtual BaseType GetBaseType()
        {
            return TypesTable.Instance.GetType(Type.ToString().ToLower());
        }
    }

    public class ReadNode : SentenceNode
    {
        public string Id;
        public override void ValidateNodeSemantic()
        {
             SymbolTable.Instance.GetVariable(Id);
        }

        public override string GenerateCode()
        {
            var varType = SymbolTable.Instance.GetVariable(Id);
            var typeValue = varType is IntType ? "Int" : "";
            return $"{Id} = lea.next{typeValue}();";
        }
    }

    public class PrintNode : SentenceNode
    {
        public ExpressionNode Expression;
        public override void ValidateNodeSemantic()
        {
            Expression.ValidateSemantic();
        }

        public override string GenerateCode()
        {
            return $"System.out.println({Expression.GenerateCode()});";
        }
    }

    public class AssignNode : SentenceNode
    {
        public IdNode Id;
        public ExpressionNode Expression;
        public override void ValidateNodeSemantic()
        {
            var idType = Id.ValidateSemantic();
            var exprType = Expression.ValidateSemantic();
            if(! idType.IsAssignable(exprType))
                throw new SemanticException("asig");
        }

        public override string GenerateCode()
        {
            return $"{Id.GenerateCode()} = {Expression.GenerateCode()};";
        }
    }

    public class OnlyDeclareNode : SentenceNode
    {
        public IdNode Id;
        public ExpressionNode Expression;
        public override void ValidateNodeSemantic()
        {
            var idType = Id.ValidateSemantic();
            var exprType = Expression.ValidateSemantic();
            if (!idType.IsAssignable(exprType))
                throw new SemanticException("asig");
        }

        public override string GenerateCode()
        {
            return $"{Id.GenerateCode()} {Expression.GenerateCode()} = new {Id.GenerateCode()}();";
        }
    }

    public class DivNode : BinaryOperatorNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftType = LeftOperand.ValidateSemantic();
            var rightType = RightOperand.ValidateSemantic();
            if (leftType is IntType && rightType is IntType)
                return leftType;
            throw new SemanticException($"div is not supported for {leftType} and {rightType}");
        }

        public override string GenerateCode()
        {
            return $"({LeftOperand.GenerateCode()} / {RightOperand.GenerateCode()})";
        }
    }



    public class IdNode : ExpressionNode
    {
        public string Value;
        public List<Accesor> AccesorsList;
        public override BaseType ValidateSemantic()
        {
            var varType = SymbolTable.Instance.GetVariable(Value);
            foreach (var accesor in AccesorsList)
            {
                if (accesor is IndexAccesor)
                {
                    var currentAccesor = (IndexAccesor) accesor;
                    if (currentAccesor.Expression.ValidateSemantic() is IntType)
                    {
                        if (varType is ArrayType)
                        {
                            var tempArray = (ArrayType) varType;
                            varType = tempArray.Type;


                        }
                        else
                        {
                            throw new SemanticException("variable cant be access, not an array");
                        }
                    }
                    else
                    {
                        throw new SemanticException("Index of array is not Int ");
                    }
                }
            }
            return varType;
        }

        public override string GenerateCode()
        {
            return Value;
        }
    }

    public class NumberNode : ExpressionNode
    {
        public float Value;
        public override BaseType ValidateSemantic()
        {
            return TypesTable.Instance.GetType("int");
        }

        public override string GenerateCode()
        {

            return Value.ToString();
        }
    }


}
