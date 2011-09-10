using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;

namespace ExpressionParser.Operations
{
    public interface IComparisonOperations
    {
        // to support {"==", "!=","<", "<=", ">", ">="} operators
        IOperand EqualTo(IOperand rhs);
        IOperand NotEqualTo(IOperand rhs);
        IOperand LessThan(IOperand rhs);
        IOperand LessThanOrEqualTo(IOperand rhs);
        IOperand GreaterThan(IOperand rhs);
        IOperand GreaterThanOrEqualTo(IOperand rhs);
    }
}
