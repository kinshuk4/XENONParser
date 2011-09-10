using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;

namespace ExpressionParser.Operations
{
    public interface IArithmeticOperations
    {
        // to support {"+", "-", "*", "/", "%"} operators
        IOperand Plus(IOperand rhs);
        IOperand Minus(IOperand rhs);
        IOperand Multiply(IOperand rhs);
        IOperand Divide(IOperand rhs);
        IOperand Modulo(IOperand rhs);
    }
}
