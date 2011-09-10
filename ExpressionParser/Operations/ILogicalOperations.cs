using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;

namespace ExpressionParser.Operations
{

    public interface ILogicalOperations
    {
        // to support {"||", "&&" } operators
        IOperand OR(IOperand rhs);
        IOperand AND(IOperand rhs);
    }
	
}
