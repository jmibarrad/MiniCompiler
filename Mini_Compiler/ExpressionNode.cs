﻿using Mini_Compiler.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Mini_Compiler.Semantic;
using Mini_Compiler.Semantic.Types;

namespace Mini_Compiler
{
    public abstract class ExpressionNode
    {
        public abstract BaseType ValidateSemantic();
    }

    public abstract class SentenceNode
    {
        public void ValidateSemantic()
        {
            ValidateNodeSemantic();
            if(NextSentence!=null)
                NextSentence.ValidateSemantic();
        }

        protected abstract void ValidateNodeSemantic();
        public SentenceNode NextSentence;
    }

    public class DeclarationNode : SentenceNode
    {
        public string Value;
        public TokenTypes Type;
        public List<int> Dimensions;
        protected override void ValidateNodeSemantic()
        {
            string typeName = Type == TokenTypes.Int ? "int" : "string";
            SymbolTable.Instance.DeclareVariable(Value,typeName, Dimensions);
        }
    }

    public class ReadNode : SentenceNode
    {
        public string Id;
        protected override void ValidateNodeSemantic()
        {
             SymbolTable.Instance.GetVariable(Id);
        }
    }

    public class PrintNode : SentenceNode
    {
        public ExpressionNode Expression;
        protected override void ValidateNodeSemantic()
        {
            Expression.ValidateSemantic();
        }
    }

    public class AssignNode : SentenceNode
    {
        public IdNode Id;
        public ExpressionNode Expression;
        protected override void ValidateNodeSemantic()
        {
            var idType = Id.ValidateSemantic();
            var exprType = Expression.ValidateSemantic();
            if(! idType.IsAssignable(exprType))
                throw new SemanticException("asig");
        }
    }

    public class MultNode : BinaryOperatorNode
    {
        public override BaseType ValidateSemantic()
        {
            var leftType = LeftOperand.ValidateSemantic();
            var rightType = RightOperand.ValidateSemantic();
            if (leftType is IntType && rightType is IntType)
                return leftType;
            throw new SemanticException($"mul is not supported for {leftType} and {rightType}");
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
    }

    public class NumberNode : ExpressionNode
    {
        public float Value;
        public override BaseType ValidateSemantic()
        {
            return TypesTable.Instance.GetType("int");
        }
    }


}
