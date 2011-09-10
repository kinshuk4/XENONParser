using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionParser.Design
{
    public interface IOperator
    {
        IOperand Eval(IOperand lhs, IOperand rhs);
    }
}
